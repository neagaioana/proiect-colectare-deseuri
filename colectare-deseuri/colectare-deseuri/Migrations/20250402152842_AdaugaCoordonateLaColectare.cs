using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace colectare_deseuri.Migrations
{
    /// <inheritdoc />
    public partial class AdaugaCoordonateLaColectare : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Adresa",
                table: "Colectari",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<float>(
                name: "Latitude",
                table: "Colectari",
                type: "REAL",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Longitude",
                table: "Colectari",
                type: "REAL",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adresa",
                table: "Colectari");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Colectari");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Colectari");
        }
    }
}
