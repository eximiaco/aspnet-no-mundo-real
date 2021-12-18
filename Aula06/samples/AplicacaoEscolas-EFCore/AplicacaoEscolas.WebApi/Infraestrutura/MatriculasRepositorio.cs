using System;
using System.Collections.Generic;
using AplicacaoEscolas.WebApi.Dominio;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AplicacaoEscolas.WebApi.Infraestrutura
{
    public sealed class MatriculasRepositorio
    {
        private readonly EscolasDbContext _escolasDbContext;
        private readonly IConfiguration _configuracao;


        public MatriculasRepositorio(EscolasDbContext escolasDbContext, IConfiguration configuracao)
        {
            _escolasDbContext = escolasDbContext;
            _configuracao = configuracao;
        }

        public async Task InserirAsync(Matricula matricula, CancellationToken cancellationToken = default)
        {
            await _escolasDbContext.AddAsync(matricula, cancellationToken);
        }

        public async Task<Matricula> RecuperarPorIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _escolasDbContext
                .Matriculas
                .FirstOrDefaultAsync(c=> c.Id == id, cancellationToken);
        }
        
        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            await _escolasDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
