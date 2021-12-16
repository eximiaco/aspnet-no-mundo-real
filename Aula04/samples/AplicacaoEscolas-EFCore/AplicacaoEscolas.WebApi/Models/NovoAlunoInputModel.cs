using System;
using System.ComponentModel.DataAnnotations;

namespace AplicacaoEscolas.WebApi.Models
{
    public class NovoAlunoInputModel
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        public DateTime DataNascimento { get; set; }
        public string Email { get; set; }
    }
}