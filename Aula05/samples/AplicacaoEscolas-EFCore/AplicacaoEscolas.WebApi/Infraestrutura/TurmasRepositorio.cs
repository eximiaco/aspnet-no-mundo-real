using System;
using System.Collections.Generic;
using AplicacaoEscolas.WebApi.Dominio;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AplicacaoEscolas.WebApi.Infraestrutura
{
    public sealed class TurmasRepositorio
    {
        private readonly EscolasDbContext _escolasDbContext;
        private readonly IConfiguration _configuracao;


        public TurmasRepositorio(EscolasDbContext escolasDbContext, IConfiguration configuracao)
        {
            _escolasDbContext = escolasDbContext;
            _configuracao = configuracao;
        }

        public async Task InserirAsync(Turma turma, CancellationToken cancellationToken = default)
        {
            await _escolasDbContext.AddAsync(turma, cancellationToken);
        }

        public void Alterar(Turma turma)
        {
            // Nada a fazer EF CORE fazer o Tracking da Entidade quando recuperamos a mesma
        }
        
        public async Task<IEnumerable<Turma>> RecuperarTodosAsync(CancellationToken cancellationToken = default)
        {
            return await _escolasDbContext
                .Turmas
                .Include(c => c.Agenda)
                .ToListAsync(cancellationToken);
        }
        
        public async Task<Turma> RecuperarPorIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _escolasDbContext
                .Turmas
                .Include(c => c.Agenda)
                .FirstOrDefaultAsync(c=> c.Id == id, cancellationToken);
        }
        
        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            await _escolasDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
