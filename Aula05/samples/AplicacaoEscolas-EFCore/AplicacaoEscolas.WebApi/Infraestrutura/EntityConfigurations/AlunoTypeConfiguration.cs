using System;
using AplicacaoEscolas.WebApi.Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AplicacaoEscolas.WebApi.Infraestrutura.EntityConfigurations
{
    public sealed class AlunoTypeConfiguration : IEntityTypeConfiguration<Aluno>
    {
        public void Configure(EntityTypeBuilder<Aluno> builder)
        {
            builder.ToTable("Alunos", "Matriculas");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Nome).HasColumnType("varchar(100)");
            builder.Property(c => c.DataNascimento);
            
            builder.Property(c => c.Genero)
                .HasConversion(new EnumToStringConverter<EGenero>())
                .HasColumnType("varchar(20)");
            
            builder.OwnsOne(c => c.EnderecoResidencial, 
                enderecoBuilder =>
            {
                enderecoBuilder.Property(c => c.Rua)
                    .HasColumnName("EnderecoResidencialRua").HasColumnType("varchar(100)");
                enderecoBuilder.Property(c => c.Numero)
                    .HasColumnName("EnderecoResidencialNumero").HasColumnType("varchar(10)");
                enderecoBuilder.Property(c => c.Complemento)
                    .HasColumnName("EnderecoResidencialComplemento").HasColumnType("varchar(20)");
                enderecoBuilder.Property(c => c.Bairro)
                    .HasColumnName("EnderecoResidencialBairro").HasColumnType("varchar(60)");
                enderecoBuilder.Property(c => c.Cidade)
                    .HasColumnName("EnderecoResidencialCidade").HasColumnType("varchar(100)");
                enderecoBuilder.Property(c => c.Cep)
                    .HasColumnName("EnderecoResidencialCep").HasColumnType("varchar(20)");
                enderecoBuilder.Property(c => c.UF)
                    .HasColumnName("EnderecoResidencialUF").HasColumnType("varchar(2)");
                enderecoBuilder.Property(c => c.Pais)
                    .HasColumnName("EnderecoResidencialPais").HasColumnType("varchar(40)");
            });
            
            builder.Property<DateTime>("DataUltimaAlteracao");
            builder.Property<DateTime>("DataCadastro");
        }
    }
}