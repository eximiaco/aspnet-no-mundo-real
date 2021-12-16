using Microsoft.EntityFrameworkCore.Migrations;

namespace AplicacaoEscolas.WebApi.Migrations
{
    public partial class alunosversao3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmailParticular",
                table: "Alunos",
                type: "varchar(100)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailParticular",
                table: "Alunos");
        }
    }
}
