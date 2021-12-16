using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AplicacaoEscolas.WebApi.Migrations
{
    public partial class matriculasv1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TurmasAgenda_Turmas_AgendaId",
                schema: "Matriculas",
                table: "TurmasAgenda");

            migrationBuilder.DropIndex(
                name: "IX_TurmasAgenda_AgendaId",
                schema: "Matriculas",
                table: "TurmasAgenda");

            migrationBuilder.DropColumn(
                name: "AgendaId",
                schema: "Matriculas",
                table: "TurmasAgenda");

            migrationBuilder.AddColumn<Guid>(
                name: "TurmaId",
                schema: "Matriculas",
                table: "TurmasAgenda",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TotalInscritos",
                schema: "Matriculas",
                table: "Turmas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "MatriculasEfetivadas",
                schema: "Matriculas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AlunoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TurmaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EfetivadoEm = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Situacao = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatriculasEfetivadas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatriculasEfetivadas_Alunos_AlunoId",
                        column: x => x.AlunoId,
                        principalSchema: "Matriculas",
                        principalTable: "Alunos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MatriculasEfetivadas_Turmas_TurmaId",
                        column: x => x.TurmaId,
                        principalSchema: "Matriculas",
                        principalTable: "Turmas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TurmasAgenda_TurmaId",
                schema: "Matriculas",
                table: "TurmasAgenda",
                column: "TurmaId");

            migrationBuilder.CreateIndex(
                name: "IX_MatriculasEfetivadas_AlunoId",
                schema: "Matriculas",
                table: "MatriculasEfetivadas",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_MatriculasEfetivadas_TurmaId",
                schema: "Matriculas",
                table: "MatriculasEfetivadas",
                column: "TurmaId");

            migrationBuilder.AddForeignKey(
                name: "FK_TurmasAgenda_Turmas_TurmaId",
                schema: "Matriculas",
                table: "TurmasAgenda",
                column: "TurmaId",
                principalSchema: "Matriculas",
                principalTable: "Turmas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TurmasAgenda_Turmas_TurmaId",
                schema: "Matriculas",
                table: "TurmasAgenda");

            migrationBuilder.DropTable(
                name: "MatriculasEfetivadas",
                schema: "Matriculas");

            migrationBuilder.DropIndex(
                name: "IX_TurmasAgenda_TurmaId",
                schema: "Matriculas",
                table: "TurmasAgenda");

            migrationBuilder.DropColumn(
                name: "TurmaId",
                schema: "Matriculas",
                table: "TurmasAgenda");

            migrationBuilder.DropColumn(
                name: "TotalInscritos",
                schema: "Matriculas",
                table: "Turmas");

            migrationBuilder.AddColumn<Guid>(
                name: "AgendaId",
                schema: "Matriculas",
                table: "TurmasAgenda",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_TurmasAgenda_AgendaId",
                schema: "Matriculas",
                table: "TurmasAgenda",
                column: "AgendaId");

            migrationBuilder.AddForeignKey(
                name: "FK_TurmasAgenda_Turmas_AgendaId",
                schema: "Matriculas",
                table: "TurmasAgenda",
                column: "AgendaId",
                principalSchema: "Matriculas",
                principalTable: "Turmas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
