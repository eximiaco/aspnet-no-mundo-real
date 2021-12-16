using System;
using System.Threading;
using System.Threading.Tasks;
using AplicacaoEscolas.WebApi.Dominio;
using Microsoft.EntityFrameworkCore;

namespace AplicacaoEscolas.WebApi.Infraestrutura
{
    public sealed class AlunosRepositorio
    {
        private readonly EscolasDbContext _dbContext;

        public AlunosRepositorio(EscolasDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task InserirAsync(Aluno novoAluno, CancellationToken cancellationToken = default)
        {
            await _dbContext.Alunos.AddAsync(novoAluno, cancellationToken);
        }

        public async Task<Aluno> RecuperarPorIdAsync(Guid id,  CancellationToken cancellationToken = default)
        {
            return await _dbContext
                            .Alunos
                            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}