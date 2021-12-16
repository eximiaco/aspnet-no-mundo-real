using Microsoft.EntityFrameworkCore.Migrations;

namespace AplicacaoEscolas.WebApi.Migrations
{
    public partial class alunoversao21 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataUltimaAlterecao",
                schema: "Matriculas",
                table: "Alunos",
                newName: "DataUltimaAlteracao");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataUltimaAlteracao",
                schema: "Matriculas",
                table: "Alunos",
                newName: "DataUltimaAlterecao");
        }
    }
}
