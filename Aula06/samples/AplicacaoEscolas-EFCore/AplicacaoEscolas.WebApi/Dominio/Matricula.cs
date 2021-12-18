using System;
using CSharpFunctionalExtensions;

namespace AplicacaoEscolas.WebApi.Dominio
{
    public sealed class Matricula
    {
        private Matricula() { }
        private Matricula(Guid id, Guid alunoId, Guid turmaId, DateTime efetivadoEm, ESituacaoMatricula situacao)
        {
            Id = id;
            AlunoId = alunoId;
            TurmaId = turmaId;
            EfetivadoEm = efetivadoEm;
            Situacao = situacao;
        }

        public Guid Id { get; }
        public Guid AlunoId { get; }
        public Guid TurmaId { get;  }
        public DateTime EfetivadoEm { get; }
        public ESituacaoMatricula Situacao { get; }

        public static Result<Matricula> Criar(Aluno aluno, Turma turma)
        {
            if (!turma.AceitaNovasMatriculas())
                return Result.Failure<Matricula>("Turma não aceita novas matrículas");
            return new Matricula(Guid.NewGuid(), aluno.Id, turma.Id, DateTime.UtcNow, ESituacaoMatricula.Ativo);
        }
    }

    public enum ESituacaoMatricula
    {
        Ativo,
        Cancelado
    }
}