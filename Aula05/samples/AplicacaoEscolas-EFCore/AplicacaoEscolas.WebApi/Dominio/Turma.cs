using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using CSharpFunctionalExtensions;

namespace AplicacaoEscolas.WebApi.Dominio
{
    public sealed class Turma
    {
        private IList<Agenda> _agenda;
        private string _hashConcorrencia;
        
        private Turma(){}
        
        public Turma(Guid id, string descricao, EModalidade modalidade, int totalInscritos,  int quantidadeVagas, 
            List<Agenda> agenda, string hashConcorrencia)
        {
            Id = id;
            Descricao = descricao;
            QuantidadeVagas = quantidadeVagas;
            Modalidade = modalidade;
            TotalInscritos = totalInscritos;
            _agenda = agenda;
            _hashConcorrencia = hashConcorrencia;
        }

        public Guid Id { get;  }
        public string Descricao { get; }
        public EModalidade Modalidade { get; }
        public int QuantidadeVagas { get; }
        public int TotalInscritos { get; private set; }
        public IEnumerable<Agenda> Agenda => _agenda;

        public enum EModalidade
        {
            Futsal,
            Volei,
            Karete
        }

        public void AdicionarAgenda(Agenda agenda)
        {
            _agenda.Add(agenda);
        }
        
        public void AdicionarAgenda(EDiaSemana diaSemana, Horario inicial, Horario final)
        {
            _agenda.Add(new Agenda(Guid.NewGuid(), diaSemana, inicial , final));
        }

        public void RemoverAgendas(IEnumerable<Guid> agendasParaExcluir)
        {
            var agendas = _agenda.Where(c => agendasParaExcluir.Any(id => id == c.Id));
            foreach (var agendaExclusao in agendas)
                _agenda.Remove(agendaExclusao);
        }
        
        public void AtualizarAgenda(Guid id, EDiaSemana diaSemana, Horario horaInicial, Horario horaFinal)
        {
            var agenda = _agenda.FirstOrDefault(c => c.Id == id);
            if (agenda != null)
                agenda = new Agenda(id, diaSemana, horaInicial, horaFinal);
        }
        
        public bool AceitaNovasMatriculas()
        {
            return QuantidadeVagas >= (TotalInscritos + 1);
        }
        
        public void AdicionarAluno()
        {
            TotalInscritos++;
            AtualizarHashConcorrencia();
        }
        
        public static Result<Turma> Criar(string descricao, int modalidade, int quantidadeVagas)
        {
            if(descricao.Length < 5)
                return Result.Failure<Turma>("Descrição deve ter no mínimo 5 caracteres");
            if( quantidadeVagas <=0)
                return Result.Failure<Turma>("Quantidade de vagas deve ser maior que 0");
            
            var turma = new Turma(Guid.NewGuid(), descricao, (EModalidade)modalidade,0, quantidadeVagas, 
                new List<Agenda>(), "");
            turma.AtualizarHashConcorrencia();
            return turma;
        }
        
        private void AtualizarHashConcorrencia()
        {
            using var hash = MD5.Create();
            var data = hash.ComputeHash(
                Encoding.UTF8.GetBytes(
                    $"{Id}{Descricao}{Modalidade}{QuantidadeVagas}{TotalInscritos}"));
            var sBuilder = new StringBuilder();
            foreach (var tbyte in data)
                sBuilder.Append(tbyte.ToString("x2"));
            _hashConcorrencia = sBuilder.ToString();
        }
    }
}
