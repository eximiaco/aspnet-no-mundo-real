using Microsoft.EntityFrameworkCore.Migrations;

namespace AplicacaoEscolas.WebApi.Migrations
{
    public partial class alunosversao4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE Alunos SET EmailParticular = ''");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
