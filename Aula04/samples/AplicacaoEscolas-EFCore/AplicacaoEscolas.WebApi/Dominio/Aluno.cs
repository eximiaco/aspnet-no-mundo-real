using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using CSharpFunctionalExtensions;

namespace AplicacaoEscolas.WebApi.Dominio
{
    public sealed class Aluno
    {
        public Aluno(Guid id, string nome, DateTime dataNascimento, string email)
        {
            Id = id;
            Nome = nome;
            DataNascimento = dataNascimento;
            Email = email;
        }

        public Guid Id { get;  }
        public string Nome { get; }
        public DateTime DataNascimento { get;  }
        public string Email { get;  }
        public int Idade => Convert.ToInt16((DateTime.Now - DataNascimento).TotalDays / 365.25);

        public static Result<Aluno> Criar(string nome, DateTime dataNascimento, string email)
        {
            if (string.IsNullOrEmpty(nome))
                return Result.Failure<Aluno>("Nome deve ser preenchido");
            if(dataNascimento.Date >= DateTime.UtcNow.Date)
                return Result.Failure<Aluno>("Data de nascimento deve ser menor que hoje");
            return new Aluno(Guid.NewGuid(), nome, dataNascimento, email);
        }
    }
}