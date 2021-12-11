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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AlunoTypeConfiguration());
        }
    }
}