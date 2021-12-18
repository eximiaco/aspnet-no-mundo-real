using System;
using CSharpFunctionalExtensions;

namespace AplicacaoEscolas.WebApi.Dominio
{
    public sealed class Aluno
    {
        private Aluno() { }
        
        private Aluno(Guid id, string nome, DateTime dataNascimento, EGenero genero, 
            EnderecoCompleto enderecoResidencial, EnderecoCompleto enderecoCobranca)
        {
            Id = id;
            Nome = nome;
            DataNascimento = dataNascimento;
            Genero = genero;
            EnderecoResidencial = enderecoResidencial;
            EnderecoCobranca = enderecoCobranca;
        }

        public Guid Id { get;  }
        public string Nome { get; }
        public DateTime DataNascimento { get;  }
        public EGenero Genero { get; }
        public EnderecoCompleto EnderecoResidencial { get; }
        public EnderecoCompleto EnderecoCobranca { get; }
        public int Idade => Convert.ToInt16((DateTime.Now - DataNascimento).TotalDays / 365.25);

        public static Result<Aluno> Criar(string nome, DateTime dataNascimento, EGenero genero, 
            EnderecoCompleto enderecoResidencial)
        {
            if (string.IsNullOrEmpty(nome))
                return Result.Failure<Aluno>("Nome deve ser preenchido");
            if(dataNascimento.Date >= DateTime.UtcNow.Date)
                return Result.Failure<Aluno>("Data de nascimento deve ser menor que hoje");
            return new Aluno(Guid.NewGuid(), nome, dataNascimento, genero, enderecoResidencial,
                EnderecoCompleto.CriarVazio());
        }
    }
}