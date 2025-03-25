using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace colectare_deseuri.Migrations
{
    /// <inheritdoc />
    public partial class TabeleCetateniPubele : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cetateni",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nume = table.Column<string>(type: "TEXT", nullable: false),
                    Prenume = table.Column<string>(type: "TEXT", nullable: false),
                    CNP = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cetateni", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pubele",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pubele", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Colectari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdPubela = table.Column<int>(type: "INTEGER", nullable: false),
                    PubelaId = table.Column<int>(type: "INTEGER", nullable: false),
                    TimpRidicare = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colectari", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Colectari_Pubele_PubelaId",
                        column: x => x.PubelaId,
                        principalTable: "Pubele",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PubeleCetateni",
                columns: table => new
                {
                    IdPubela = table.Column<int>(type: "INTEGER", nullable: false),
                    IdCetatean = table.Column<int>(type: "INTEGER", nullable: false),
                    Adresa = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PubeleCetateni", x => new { x.IdPubela, x.IdCetatean });
                    table.ForeignKey(
                        name: "FK_PubeleCetateni_Cetateni_IdCetatean",
                        column: x => x.IdCetatean,
                        principalTable: "Cetateni",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PubeleCetateni_Pubele_IdPubela",
                        column: x => x.IdPubela,
                        principalTable: "Pubele",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Colectari_PubelaId",
                table: "Colectari",
                column: "PubelaId");

            migrationBuilder.CreateIndex(
                name: "IX_PubeleCetateni_IdCetatean",
                table: "PubeleCetateni",
                column: "IdCetatean");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Colectari");

            migrationBuilder.DropTable(
                name: "PubeleCetateni");

            migrationBuilder.DropTable(
                name: "Cetateni");

            migrationBuilder.DropTable(
                name: "Pubele");
        }
    }
}
