using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace colectare_deseuri.Migrations
{
    /// <inheritdoc />
    public partial class FixForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Colectari_Pubele_PubelaId",
                table: "Colectari");

            migrationBuilder.DropIndex(
                name: "IX_Colectari_PubelaId",
                table: "Colectari");

            migrationBuilder.DropColumn(
                name: "PubelaId",
                table: "Colectari");

            migrationBuilder.CreateIndex(
                name: "IX_Colectari_IdPubela",
                table: "Colectari",
                column: "IdPubela");

            migrationBuilder.AddForeignKey(
                name: "FK_Colectari_Pubele_IdPubela",
                table: "Colectari",
                column: "IdPubela",
                principalTable: "Pubele",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Colectari_Pubele_IdPubela",
                table: "Colectari");

            migrationBuilder.DropIndex(
                name: "IX_Colectari_IdPubela",
                table: "Colectari");

            migrationBuilder.AddColumn<int>(
                name: "PubelaId",
                table: "Colectari",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Colectari_PubelaId",
                table: "Colectari",
                column: "PubelaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Colectari_Pubele_PubelaId",
                table: "Colectari",
                column: "PubelaId",
                principalTable: "Pubele",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
