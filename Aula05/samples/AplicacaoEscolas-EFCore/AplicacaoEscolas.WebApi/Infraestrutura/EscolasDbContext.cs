using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AplicacaoEscolas.WebApi.Dominio;
using AplicacaoEscolas.WebApi.Infraestrutura.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace AplicacaoEscolas.WebApi.Infraestrutura
{
    public class EscolasDbContext : DbContext
    {
        public EscolasDbContext(DbContextOptions options)
            : base(options)
        {
            
        }
        
        public DbSet<Aluno> Alunos { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                foreach (var item in ChangeTracker.Entries())
                {
                    if (item.State is Microsoft.EntityFrameworkCore.EntityState.Modified or Microsoft.EntityFrameworkCore.EntityState.Added
                        && item.Properties.Any(c => c.Metadata.Name == "DataUltimaAlteracao"))
                        item.Property("DataUltimaAlteracao").CurrentValue = DateTime.UtcNow;

                    if (item.State == Microsoft.EntityFrameworkCore.EntityState.Added 
                        && item.Properties.Any(c => c.Metadata.Name == "DataCadastro"))
                            item.Property("DataCadastro").CurrentValue = DateTime.UtcNow;
                }
                return await base.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException e)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AlunoTypeConfiguration());
        }
    }
}