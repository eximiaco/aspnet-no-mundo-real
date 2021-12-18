﻿// <auto-generated />
using System;
using AplicacaoEscolas.WebApi.Infraestrutura;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AplicacaoEscolas.WebApi.Migrations
{
    [DbContext(typeof(EscolasDbContext))]
    partial class EscolasDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AplicacaoEscolas.WebApi.Dominio.Agenda", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DiaSemana")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<string>("HoraFinal")
                        .IsRequired()
                        .HasColumnType("varchar(5)");

                    b.Property<string>("HoraInicial")
                        .IsRequired()
                        .HasColumnType("varchar(5)");

                    b.Property<Guid?>("TurmaId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TurmaId");

                    b.ToTable("TurmasAgenda", "Matriculas");
                });

            modelBuilder.Entity("AplicacaoEscolas.WebApi.Dominio.Aluno", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataUltimaAlteracao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Genero")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Nome")
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Alunos", "Matriculas");
                });

            modelBuilder.Entity("AplicacaoEscolas.WebApi.Dominio.Matricula", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AlunoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("EfetivadoEm")
                        .HasColumnType("datetime2");

                    b.Property<string>("Situacao")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<Guid>("TurmaId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AlunoId");

                    b.HasIndex("TurmaId");

                    b.ToTable("MatriculasEfetivadas", "Matriculas");
                });

            modelBuilder.Entity("AplicacaoEscolas.WebApi.Dominio.Turma", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataUltimaAlteracao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Modalidade")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<int>("QuantidadeVagas")
                        .HasColumnType("int");

                    b.Property<int>("TotalInscritos")
                        .HasColumnType("int");

                    b.Property<string>("_hashConcorrencia")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("token_concorrencia");

                    b.HasKey("Id");

                    b.ToTable("Turmas", "Matriculas");
                });

            modelBuilder.Entity("AplicacaoEscolas.WebApi.Dominio.Agenda", b =>
                {
                    b.HasOne("AplicacaoEscolas.WebApi.Dominio.Turma", null)
                        .WithMany("Agenda")
                        .HasForeignKey("TurmaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AplicacaoEscolas.WebApi.Dominio.Aluno", b =>
                {
                    b.OwnsOne("AplicacaoEscolas.WebApi.Dominio.EnderecoCompleto", "EnderecoResidencial", b1 =>
                        {
                            b1.Property<Guid>("AlunoId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Bairro")
                                .HasColumnType("varchar(60)")
                                .HasColumnName("EnderecoResidencialBairro");

                            b1.Property<string>("Cep")
                                .HasColumnType("varchar(20)")
                                .HasColumnName("EnderecoResidencialCep");

                            b1.Property<string>("Cidade")
                                .HasColumnType("varchar(100)")
                                .HasColumnName("EnderecoResidencialCidade");

                            b1.Property<string>("Complemento")
                                .HasColumnType("varchar(20)")
                                .HasColumnName("EnderecoResidencialComplemento");

                            b1.Property<string>("Numero")
                                .HasColumnType("varchar(10)")
                                .HasColumnName("EnderecoResidencialNumero");

                            b1.Property<string>("Pais")
                                .HasColumnType("varchar(40)")
                                .HasColumnName("EnderecoResidencialPais");

                            b1.Property<string>("Rua")
                                .HasColumnType("varchar(100)")
                                .HasColumnName("EnderecoResidencialRua");

                            b1.Property<string>("UF")
                                .HasColumnType("varchar(2)")
                                .HasColumnName("EnderecoResidencialUF");

                            b1.HasKey("AlunoId");

                            b1.ToTable("Alunos");

                            b1.WithOwner()
                                .HasForeignKey("AlunoId");
                        });

                    b.Navigation("EnderecoResidencial");
                });

            modelBuilder.Entity("AplicacaoEscolas.WebApi.Dominio.Matricula", b =>
                {
                    b.HasOne("AplicacaoEscolas.WebApi.Dominio.Aluno", null)
                        .WithMany()
                        .HasForeignKey("AlunoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AplicacaoEscolas.WebApi.Dominio.Turma", null)
                        .WithMany()
                        .HasForeignKey("TurmaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AplicacaoEscolas.WebApi.Dominio.Turma", b =>
                {
                    b.Navigation("Agenda");
                });
#pragma warning restore 612, 618
        }
    }
}
