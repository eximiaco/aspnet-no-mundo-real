using Microsoft.EntityFrameworkCore.Migrations;

namespace AplicacaoEscolas.WebApi.Migrations
{
    public partial class turmasv2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "token_concorrencia",
                schema: "Matriculas",
                table: "Turmas",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "token_concorrencia",
                schema: "Matriculas",
                table: "Turmas");
        }
    }
}
