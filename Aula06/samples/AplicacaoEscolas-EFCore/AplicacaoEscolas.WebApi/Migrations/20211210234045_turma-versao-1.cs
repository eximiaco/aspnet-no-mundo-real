using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AplicacaoEscolas.WebApi.Migrations
{
    public partial class turmaversao1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Turmas",
                schema: "Matriculas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(100)", nullable: true),
                    Modalidade = table.Column<string>(type: "varchar(20)", nullable: false),
                    QuantidadeVagas = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turmas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TurmasAgenda",
                schema: "Matriculas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DiaSemana = table.Column<string>(type: "varchar(20)", nullable: false),
                    HoraInicial = table.Column<string>(type: "varchar(5)", nullable: false),
                    HoraFinal = table.Column<string>(type: "varchar(5)", nullable: false),
                    AgendaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TurmasAgenda", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TurmasAgenda_Turmas_AgendaId",
                        column: x => x.AgendaId,
                        principalSchema: "Matriculas",
                        principalTable: "Turmas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TurmasAgenda_AgendaId",
                schema: "Matriculas",
                table: "TurmasAgenda",
                column: "AgendaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TurmasAgenda",
                schema: "Matriculas");

            migrationBuilder.DropTable(
                name: "Turmas",
                schema: "Matriculas");
        }
    }
}
