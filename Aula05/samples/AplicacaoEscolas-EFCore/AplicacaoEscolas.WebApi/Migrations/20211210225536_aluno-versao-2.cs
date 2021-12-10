using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AplicacaoEscolas.WebApi.Migrations
{
    public partial class alunoversao2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Matriculas");

            migrationBuilder.RenameTable(
                name: "Alunos",
                newName: "Alunos",
                newSchema: "Matriculas");

            migrationBuilder.RenameColumn(
                name: "NomeCompleto",
                schema: "Matriculas",
                table: "Alunos",
                newName: "Nome");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                schema: "Matriculas",
                table: "Alunos",
                type: "varchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCadastro",
                schema: "Matriculas",
                table: "Alunos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataUltimaAlterecao",
                schema: "Matriculas",
                table: "Alunos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "EnderecoResidencialBairro",
                schema: "Matriculas",
                table: "Alunos",
                type: "varchar(60)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnderecoResidencialCep",
                schema: "Matriculas",
                table: "Alunos",
                type: "varchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnderecoResidencialCidade",
                schema: "Matriculas",
                table: "Alunos",
                type: "varchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnderecoResidencialComplemento",
                schema: "Matriculas",
                table: "Alunos",
                type: "varchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnderecoResidencialNumero",
                schema: "Matriculas",
                table: "Alunos",
                type: "varchar(10)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnderecoResidencialPais",
                schema: "Matriculas",
                table: "Alunos",
                type: "varchar(40)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnderecoResidencialRua",
                schema: "Matriculas",
                table: "Alunos",
                type: "varchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnderecoResidencialUF",
                schema: "Matriculas",
                table: "Alunos",
                type: "varchar(2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Genero",
                schema: "Matriculas",
                table: "Alunos",
                type: "varchar(20)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataCadastro",
                schema: "Matriculas",
                table: "Alunos");

            migrationBuilder.DropColumn(
                name: "DataUltimaAlterecao",
                schema: "Matriculas",
                table: "Alunos");

            migrationBuilder.DropColumn(
                name: "EnderecoResidencialBairro",
                schema: "Matriculas",
                table: "Alunos");

            migrationBuilder.DropColumn(
                name: "EnderecoResidencialCep",
                schema: "Matriculas",
                table: "Alunos");

            migrationBuilder.DropColumn(
                name: "EnderecoResidencialCidade",
                schema: "Matriculas",
                table: "Alunos");

            migrationBuilder.DropColumn(
                name: "EnderecoResidencialComplemento",
                schema: "Matriculas",
                table: "Alunos");

            migrationBuilder.DropColumn(
                name: "EnderecoResidencialNumero",
                schema: "Matriculas",
                table: "Alunos");

            migrationBuilder.DropColumn(
                name: "EnderecoResidencialPais",
                schema: "Matriculas",
                table: "Alunos");

            migrationBuilder.DropColumn(
                name: "EnderecoResidencialRua",
                schema: "Matriculas",
                table: "Alunos");

            migrationBuilder.DropColumn(
                name: "EnderecoResidencialUF",
                schema: "Matriculas",
                table: "Alunos");

            migrationBuilder.DropColumn(
                name: "Genero",
                schema: "Matriculas",
                table: "Alunos");

            migrationBuilder.RenameTable(
                name: "Alunos",
                schema: "Matriculas",
                newName: "Alunos");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Alunos",
                newName: "NomeCompleto");

            migrationBuilder.AlterColumn<string>(
                name: "NomeCompleto",
                table: "Alunos",
                type: "varchar(50)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldNullable: true);
        }
    }
}
