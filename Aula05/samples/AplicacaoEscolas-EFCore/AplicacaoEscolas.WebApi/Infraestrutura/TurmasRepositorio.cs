using System;
using System.Collections.Generic;
using System.Linq;
using AplicacaoEscolas.WebApi.Dominio;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace AplicacaoEscolas.WebApi.Infraestrutura
{
    public sealed class TurmasRepositorio
    {
        private readonly EscolasDbContext _escolasDbContext;
        private readonly IConfiguration _configuracao;


        public TurmasRepositorio(EscolasDbContext escolasDbContext, IConfiguration configuracao)
        {
            _escolasDbContext = escolasDbContext;
            _configuracao = configuracao;
        }

        public async Task InserirAsync(Turma turma, CancellationToken cancellationToken = default)
        {
            await _escolasDbContext.AddAsync(turma, cancellationToken);
        }

        public void Alterar(Turma turma)
        {
            using (SqlConnection connection = new SqlConnection(_configuracao.GetConnectionString("Escolas")))
            {
                connection.Open();
                const string sqlAgendasParaAtualizar = @"SELECT Id FROM TurmasAgenda WHERE Id IN @ids";
                var idsAgenda = turma.Agenda.Select(a=> a.Id).ToArray();
                var idsExistentes = connection.Query<Guid>(sqlAgendasParaAtualizar, new {ids = idsAgenda});
                
                using (var transaction = connection.BeginTransaction())
                {
                    const string sqlTurma = @"UPDATE Turmas SET Descricao = @Descricao, Modalidade = @Modalidade, QuantidadeVagas = @QuantidadeVagas WHERE Id = @Id";
                    const string sqlInsertAgenda = @"INSERT INTO TurmasAgenda (Id, IdTurma, DiaSemana, HoraInicial, HoraFinal ) VALUES (@Id, @IdTurma, @DiaSemana, @HoraInicial, @HoraFinal)";
                    const string sqlUpdateAgenda = @"UPDATE TurmasAgenda SET DiaSemana = @DiaSemana, HoraInicial = @HoraInicial, HoraFinal = @HoraFinal WHERE Id = @Id";
                    const string sqlDeleteAgenda = @"DELETE FROM TurmasAgenda WHERE Id IN (SELECT Id FROM TurmasAgenda WHERE IdTurma = @idTurma AND Id NOT IN @ids)";
                    
                    var agendasIncluir = turma.Agenda.Where(c=> !idsExistentes.Any(i => i == c.Id));
                    if (agendasIncluir.Any())
                    {
                        connection.Execute(
                            sqlInsertAgenda,
                            param:agendasIncluir
                                .Select(a=> new
                                {
                                    Id= a.Id,
                                    IdTurma = turma.Id,
                                    a.DiaSemana,
                                    a.HoraInicial,
                                    a.HoraFinal
                                }),
                            transaction:transaction);
                    }
                    
                    connection.Execute(
                        sqlDeleteAgenda, 
                        new { ids = idsAgenda, idTurma = turma.Id },
                        transaction:transaction);
                    
                    foreach (var agenda in turma.Agenda.Where(c=> idsExistentes.Any(i => i == c.Id)))
                    {
                        connection.Execute(
                            sqlUpdateAgenda,new
                            {
                                Id= agenda.Id,
                                IdTurma = turma.Id,
                                agenda.DiaSemana,
                                agenda.HoraInicial,
                                agenda.HoraFinal
                            },
                            transaction:transaction);
                    }
                    
                    connection.Execute(
                        sqlTurma,new
                        {
                            turma.Id,
                            turma.Descricao,
                            turma.Modalidade,
                            turma.QuantidadeVagas
                        },
                        transaction:transaction);
                    
                    transaction.Commit();
                }
            }
        }
        
        public IEnumerable<Turma> RecuperarTodos()
        {
            using (var connection = new SqlConnection(_configuracao.GetConnectionString("Escolas")))
            {
                const string sql = @"SELECT 
                                Turmas.Id, 
                                Turmas.Descricao,
                                Turmas.Modalidade,
                                Turmas.QuantidadeVagas,
                                TurmasAgenda.Id AS IdAgendaSplit,
                                TurmasAgenda.Id,
                                TurmasAgenda.DiaSemana,
                                TurmasAgenda.HoraInicial,
                                TurmasAgenda.HoraFinal
                        FROM Turmas
                        LEFT JOIN TurmasAgenda ON TurmasAgenda.IdTurma = Turmas.Id";
                
                var turmaDicionario = new Dictionary<Guid, Turma>();
                var lista = connection.Query<TurmaDTO, AgendaDTO, Turma>(
                    sql,
                    (turma, agenda) =>
                    {
                        if (turmaDicionario.TryGetValue(turma.Id, out var turmaExistente))
                        {
                            turmaExistente.AdicionarAgenda(agenda.DiaSemana, agenda.HoraInicial, agenda.HoraFinal);
                            return turmaExistente;
                        }
                        else
                        {
                            var novaTurma = new Turma(turma.Id, turma.Descricao, turma.Modalidade, turma.QuantidadeVagas, new List<Agenda>());
                            novaTurma.AdicionarAgenda(agenda.DiaSemana, agenda.HoraInicial, agenda.HoraFinal);
                            turmaDicionario.Add(turma.Id, novaTurma);
                            return novaTurma;
                        }
                    },
                    splitOn: "IdAgendaSplit");
                return lista;
            }
        }
        
        public Turma RecuperarPorId(Guid id)
        {
            using (var connection = new SqlConnection(_configuracao.GetConnectionString("Escolas")))
            {
                const string sql = @"SELECT 
                                Turmas.Id, 
                                Turmas.Descricao,
                                Turmas.Modalidade,
                                Turmas.QuantidadeVagas,
                                TurmasAgenda.Id AS IdAgendaSplit,
                                TurmasAgenda.Id,
                                TurmasAgenda.DiaSemana,
                                TurmasAgenda.HoraInicial,
                                TurmasAgenda.HoraFinal
                        FROM Turmas
                        LEFT JOIN TurmasAgenda ON TurmasAgenda.IdTurma = Turmas.Id
                        WHERE Turmas.Id = @id";
                
                var turmaDicionario = new Dictionary<Guid, Turma>();
                var lista = connection.Query<TurmaDTO, AgendaDTO, Turma>(
                    sql,
                    (turma, agenda) =>
                    {
                        if (turmaDicionario.TryGetValue(turma.Id, out var turmaExistente))
                        {
                            turmaExistente.AdicionarAgenda(agenda.DiaSemana, agenda.HoraInicial, agenda.HoraFinal);
                            return turmaExistente;
                        }
                        else
                        {
                            var novaTurma = new Turma(turma.Id, turma.Descricao, turma.Modalidade, turma.QuantidadeVagas, new List<Agenda>());
                            novaTurma.AdicionarAgenda(agenda.DiaSemana, agenda.HoraInicial, agenda.HoraFinal);
                            turmaDicionario.Add(turma.Id, novaTurma);
                            return novaTurma;
                        }
                    },
                    splitOn: "IdAgendaSplit",
                    param: new {id = id.ToString()});
                return lista.FirstOrDefault();
            }
        }
        
        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            await _escolasDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
