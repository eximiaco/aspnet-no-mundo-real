using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AplicacaoEscolas.WebApi.Models
{
    public sealed class Turma
    {
        public string Id { get; set; }
        [Required]
        [MinLength(5, ErrorMessage = "Tamanho inválido")]
        public string Descricao { get; set; }
        public int QuantidadeVagas { get; set; }
        public EModalidade Modalidade { get; set; }
        public List<Agenda> Agenda { get; set; }

        public enum EModalidade
        {
            Futsal,
            Volei,
            Karete
        }
    }

    public struct Horario
    {
        public int Hora { get; set; }
        public int Minuto { get; set; }

        public override string ToString()
        {
            return $"{Hora}:{Minuto}";
        }
    }

    public sealed class Agenda
    {
        public EDiaSemana DiaSemana { get; set; }
        public Horario HoraInicial { get; set; }
        public Horario HoraFinal { get; set; }

        public enum EDiaSemana
        {
            Domingo = 1,
            Segunda = 2,
            Terca = 3,
            Quarta = 4,
            Quinta = 5,
            Sexta = 6,
            Sabado = 7
        }
    }

}
