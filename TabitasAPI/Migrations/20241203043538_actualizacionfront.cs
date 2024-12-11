using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TabitasAPI.Migrations
{
    /// <inheritdoc />
    public partial class actualizacionfront : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Agregar la columna FechaEntrega en la tabla Terminados si no existe previamente
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaEntrega",
                table: "Terminados",
                type: "datetime2",
                nullable: true);

            // Alterar la columna FechaEntrega en la tabla Cortes para que sea nullable
            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaEntrega",
                table: "Cortes",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            // Alterar la columna FechaAVentas en la tabla Cortes para que no sea nullable
            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaAVentas",
                table: "Cortes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            // Agregar las columnas RutaImagen y RutaImagenLocal en la tabla Cortes
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

            // Agregar la columna FechaEditada en la tabla Calidades
            migrationBuilder.AddColumn<bool>(
                name: "FechaEditada",
                table: "Calidades",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Eliminar la columna FechaEntrega en la tabla Terminados si existe
            migrationBuilder.DropColumn(
                name: "FechaEntrega",
                table: "Terminados");

            // Eliminar las columnas RutaImagen y RutaImagenLocal en la tabla Cortes
            migrationBuilder.DropColumn(
                name: "RutaImagen",
                table: "Cortes");

            migrationBuilder.DropColumn(
                name: "RutaImagenLocal",
                table: "Cortes");

            // Eliminar la columna FechaEditada en la tabla Calidades
            migrationBuilder.DropColumn(
                name: "FechaEditada",
                table: "Calidades");

            // Restaurar la columna FechaEntrega en la tabla Cortes a no nullable
            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaEntrega",
                table: "Cortes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            // Restaurar la columna FechaAVentas en la tabla Cortes a nullable
            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaAVentas",
                table: "Cortes",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
