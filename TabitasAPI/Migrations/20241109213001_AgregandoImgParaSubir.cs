using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TabitasAPI.Migrations
{
    /// <inheritdoc />
    public partial class AgregandoImgParaSubir : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //Generales
            migrationBuilder.DropColumn(
                name: "Imagen",
                table: "Generales");

            migrationBuilder.AddColumn<string>(
                name: "RutaImagen",
                table: "Generales",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RutaImagenLocal",
                table: "Generales",
                type: "nvarchar(max)",
                nullable: true);
            //Corte
            migrationBuilder.DropColumn(
                name: "Imagen",
                table: "Cortes");

            migrationBuilder.AddColumn<string>(
                name: "RutaImagen",
                table: "Cortes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RutaImagenLocal",
                table: "Cortes",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RutaImagen",
                table: "Generales");

            migrationBuilder.DropColumn(
                name: "RutaImagenLocal",
                table: "Generales");

            migrationBuilder.AddColumn<string>(
                name: "Imagen",
                table: "Generales",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
            //Corte
            migrationBuilder.DropColumn(
                name: "RutaImagen",
                table: "Cortes");

            migrationBuilder.DropColumn(
                name: "RutaImagenLocal",
                table: "Cortes");

            migrationBuilder.AddColumn<string>(
                name: "Imagen",
                table: "Cortes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
