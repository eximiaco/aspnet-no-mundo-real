using AplicacaoEscolas.WebApi.Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AplicacaoEscolas.WebApi.Infraestrutura.EntityConfigurations
{
    public sealed class MatriculaTypeConfiguration : IEntityTypeConfiguration<Matricula>
    {
        public void Configure(EntityTypeBuilder<Matricula> builder)
        {
            builder.ToTable("MatriculasEfetivadas", "Matriculas");
            builder.HasKey(c => c.Id);
            builder
                .HasOne<Aluno>()
                .WithMany()
                .HasForeignKey(c => c.AlunoId);
            builder
                .HasOne<Turma>()
                .WithMany()
                .HasForeignKey(c => c.TurmaId);
            builder.Property(c => c.EfetivadoEm);
            builder.Property(c => c.Situacao)
                .HasColumnType("varchar(20)")
                .HasConversion(new EnumToStringConverter<ESituacaoMatricula>());
        }
    }
}