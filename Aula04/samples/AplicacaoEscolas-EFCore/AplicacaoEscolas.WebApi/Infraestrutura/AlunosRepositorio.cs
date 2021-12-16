using System;
using System.Linq;
using AplicacaoEscolas.WebApi.Dominio;

namespace AplicacaoEscolas.WebApi.Infraestrutura
{
    public sealed class AlunosRepositorio
    {
        private readonly EscolasDbContext _dbContext;

        public AlunosRepositorio(EscolasDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Inserir(Aluno novoAluno)
        {
            _dbContext.Alunos.Add(novoAluno);
            
        }

        public Aluno RecuperarPorId(Guid id)
        {
            return _dbContext
                .Alunos
                .FirstOrDefault(c => c.Id == id);
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }
    }
}