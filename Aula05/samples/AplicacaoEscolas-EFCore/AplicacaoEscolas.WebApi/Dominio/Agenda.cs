using System;

namespace AplicacaoEscolas.WebApi.Dominio
{
    public sealed class Agenda
    {
        public Agenda(Guid id, EDiaSemana diaSemana, Horario horaInicial, Horario horaFinal)
        {
            Id = id;
            DiaSemana = diaSemana;
            HoraInicial = horaInicial;
            HoraFinal = horaFinal;
        }

        public Guid Id { get; }
        public EDiaSemana DiaSemana { get; }
        public Horario HoraInicial { get; }
        public Horario HoraFinal { get; }
    }
}