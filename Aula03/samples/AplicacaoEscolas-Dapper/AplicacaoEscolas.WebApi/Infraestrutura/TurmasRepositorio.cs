using System;
using System.Collections.Generic;
using System.Linq;
using AplicacaoEscolas.WebApi.Dominio;
using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace AplicacaoEscolas.WebApi.Infraestrutura
{
    public sealed class TurmasRepositorio
    {
        private readonly IConfiguration _configuracao;


        public TurmasRepositorio(IConfiguration configuracao)
        {
            _configuracao = configuracao;
        }

        public void Inserir(Turma turma)
        {
            using (SqlConnection connection = new SqlConnection(_configuracao.GetConnectionString("Escolas")))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    const string sqlTurma = @"INSERT INTO Turmas (Id, Descricao, Modalidade, QuantidadeVagas) VALUES (@id, @descricao, @modalidade, @quantidadeVagas)";
                    const string sqlAgenda = @"INSERT INTO TurmasAgenda (Id, IdTurma, DiaSemana, HoraInicial, HoraFinal ) VALUES (@id, @idTurma, @diaSemana, @horaInicial, @horaFinal)";

                    connection.Execute(
                        sqlTurma,
                        param: new { id = turma.Id, descricao = turma.Descricao, modalidade = turma.Modalidade, quantidadeVagas=  turma.QuantidadeVagas},
                        transaction:transaction);
                    
                    var agendas =
                    connection.Execute(
                        sqlAgenda,
                        param: turma.Agenda.Select(a=> new
                        {
                            id= Guid.NewGuid(),
                            idTurma = turma.Id,
                            diaSemana = a.DiaSemana,
                            horaInicial = a.HoraInicial,
                            horaFinal =a.HoraFinal
                        }),
                        transaction:transaction);
                    transaction.Commit();
                }
            }
        }

        public void Alterar(Turma turma)
        {
            using (SqlConnection connection = new SqlConnection(_configuracao.GetConnectionString("Escolas")))
            {
                connection.Open();
                
                using (var transaction = connection.BeginTransaction())
                {
                    const string sqlAgendasParaAtualizar = @"SELECT Id FROM TurmasAgenda WHERE Id IN @ids";
                    var idsAgenda = turma.Agenda.Select(a=> a.Id).ToArray();
                    var idsExistentes = connection.Query<Guid>(sqlAgendasParaAtualizar, new {ids = idsAgenda});
                    
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
                    (turmaDto, agendaDto) =>
                    {
                        if (turmaDicionario.TryGetValue(turmaDto.Id, out var turmaExistente))
                        {
                            turmaExistente.AdicionarAgenda(agendaDto.DiaSemana, agendaDto.HoraInicial, agendaDto.HoraFinal);
                            return turmaExistente;
                        }
                        else
                        {
                            var novaTurma = new Turma(turmaDto.Id, turmaDto.Descricao, turmaDto.Modalidade, turmaDto.QuantidadeVagas, new List<Agenda>());
                            novaTurma.AdicionarAgenda(agendaDto.DiaSemana, agendaDto.HoraInicial, agendaDto.HoraFinal);
                            turmaDicionario.Add(turmaDto.Id, novaTurma);
                            return novaTurma;
                        }
                    },
                    splitOn: "IdAgendaSplit",
                    param: new {id = id.ToString()});
                return lista.FirstOrDefault();
            }
        }
    }
}
