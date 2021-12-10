using AplicacaoEscolas.WebApi.Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AplicacaoEscolas.WebApi.Infraestrutura.EntityConfigurations
{
    public sealed class TurmaTypeConfiguration : IEntityTypeConfiguration<Turma>
    {
        public void Configure(EntityTypeBuilder<Turma> builder)
        {
            builder.ToTable("Turmas", "Matriculas");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Descricao).HasColumnType("varchar(100)");
            builder.Property(c => c.Modalidade).HasColumnType("varchar(20)")
                .HasConversion(new EnumToStringConverter<Turma.EModalidade>());
            builder.Property(c => c.QuantidadeVagas);
            
            
        }
    }
}