using System;
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
            builder.Property(c => c.TotalInscritos);
            builder
                .HasMany(c => c.Agenda)
                .WithOne()
                .HasForeignKey("TurmaId")
                .OnDelete(DeleteBehavior.Cascade)
                .Metadata
                .PrincipalToDependent
                .SetField("_agenda");
            
            builder.Property("_hashConcorrencia").HasColumnName("token_concorrencia").HasConversion<string>().IsConcurrencyToken();
            
            builder.Property<DateTime>("DataUltimaAlteracao");
            builder.Property<DateTime>("DataCadastro");
            
        }
    }
}