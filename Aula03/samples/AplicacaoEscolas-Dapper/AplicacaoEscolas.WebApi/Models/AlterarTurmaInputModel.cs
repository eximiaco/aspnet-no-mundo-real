using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AplicacaoEscolas.WebApi.Models
{
    public sealed class AlterarTurmaInputModel
    {
        [Required]
        [MinLength(5, ErrorMessage = "Tamanho inválido")]
        public string Descricao { get; set; }
        public int QuantidadeVagas { get; set; }
        public int Modalidade { get; set; }
        public List<AgendaInputModel> Agenda { get; set; }

        public sealed class AgendaInputModel
        {
            public string Id { get; set; }
            public int DiaSemana { get; set; }
            public string HoraInicial { get; set; }
            public string HoraFinal { get; set; }
        }
    }
}