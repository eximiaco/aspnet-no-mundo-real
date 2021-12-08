using AplicacaoEscolas.WebApi.Dominio;
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
            modelBuilder.Entity<Aluno>().ToTable("Alunos");
            modelBuilder.Entity<Aluno>().HasKey(c => c.Id);
            modelBuilder.Entity<Aluno>()
                .Property(c => c.Nome)
                .HasColumnName("NomeCompleto")
                .HasColumnType("varchar(50)")
                .IsRequired();
            modelBuilder.Entity<Aluno>().Property(c => c.DataNascimento);
        }
    }
}