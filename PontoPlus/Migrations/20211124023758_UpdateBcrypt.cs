using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PontoPlus.Migrations
{
    public partial class UpdateBcrypt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Email = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Senha = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Departamentos = table.Column<int>(type: "int", nullable: false),
                    EntradaAm = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    SaidaAm = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    EntradaPm = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    SaidaPm = table.Column<TimeSpan>(type: "time(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RegistroPontos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Entrada = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Saida = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    TotalTempo = table.Column<TimeSpan>(type: "time(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistroPontos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegistroPontos_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioMensagens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SenderId = table.Column<int>(type: "int", nullable: false),
                    ReceiverId = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    SendAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    IsRead = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioMensagens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsuarioMensagens_Usuarios_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RegistroPontos_UsuarioId",
                table: "RegistroPontos",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioMensagens_ReceiverId",
                table: "UsuarioMensagens",
                column: "ReceiverId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegistroPontos");

            migrationBuilder.DropTable(
                name: "UsuarioMensagens");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
