using System;
using AplicacaoEscolas.WebApi.Dominio;

namespace AplicacaoEscolas.WebApi.Infraestrutura
{
    public class TurmaDTO
    {
        public Guid Id { get;  set;}
        public string Descricao { get; set;}
        public Turma.EModalidade Modalidade { get; set;}
        public int QuantidadeVagas { get; set;}
    }
    
    public class AgendaDTO
    {
        public Guid Id { get; set;}
        public EDiaSemana DiaSemana { get; set;}
        public Horario HoraInicial { get; set;}
        public Horario HoraFinal { get; set;}
    }
}