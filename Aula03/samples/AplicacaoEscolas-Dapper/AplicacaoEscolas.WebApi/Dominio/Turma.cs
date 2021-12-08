using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace AplicacaoEscolas.WebApi.Dominio
{
    public sealed class Turma
    {
        private IList<Agenda> _agenda;
        
        public Turma(Guid id, string descricao, EModalidade modalidade,  int quantidadeVagas, List<Agenda> agenda)
        {
            Id = id;
            Descricao = descricao;
            QuantidadeVagas = quantidadeVagas;
            Modalidade = modalidade;
            _agenda = agenda;
        }

        public Guid Id { get;  }
        public string Descricao { get; }
        public EModalidade Modalidade { get; }
        public int QuantidadeVagas { get; }
        public IEnumerable<Agenda> Agenda  => _agenda;

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
        
        public void LimparAgendas()
        {
            _agenda.Clear();
        }
        
        public static Result<Turma> Criar(string descricao, int modalidade, int quantidadeVagas)
        {
            if(descricao.Length < 5)
                return Result.Failure<Turma>("Descrição deve ter no mínimo 5 caracteres");
            if( quantidadeVagas <=0)
                return Result.Failure<Turma>("Quantidade de vagas deve ser maior que 0");
            
            return new Turma(Guid.NewGuid(), descricao, (EModalidade)modalidade, quantidadeVagas, new List<Agenda>());
        }


    }
}
