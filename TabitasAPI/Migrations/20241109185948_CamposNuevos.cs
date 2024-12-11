using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TabitasAPI.Migrations
{
    /// <inheritdoc />
    public partial class CamposNuevos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FKIdGeneral",
                table: "Terminados");

            migrationBuilder.DropColumn(
                name: "FKIdGeneral",
                table: "Serigrafias");

            migrationBuilder.DropColumn(
                name: "FKIdGeneral",
                table: "Maquilas");

            migrationBuilder.DropColumn(
                name: "FKIdGeneral",
                table: "Lavados");

            migrationBuilder.DropColumn(
                name: "FKIdProceso",
                table: "Generales");

            migrationBuilder.DropColumn(
                name: "FKIdGeneral",
                table: "Etiquetas");

            migrationBuilder.DropColumn(
                name: "FKIdGeneral",
                table: "Cortes");

            migrationBuilder.DropColumn(
                name: "FKIdGeneral",
                table: "CorteLaseres");

            migrationBuilder.DropColumn(
                name: "FKIdGeneral",
                table: "Calidades");

            migrationBuilder.DropColumn(
                name: "FKIdGeneral",
                table: "Bordados");

            migrationBuilder.DropColumn(
                name: "FKIdGeneral",
                table: "Almacenes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FKIdGeneral",
                table: "Terminados",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FKIdGeneral",
                table: "Serigrafias",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FKIdGeneral",
                table: "Maquilas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FKIdGeneral",
                table: "Lavados",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FKIdProceso",
                table: "Generales",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FKIdGeneral",
                table: "Etiquetas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FKIdGeneral",
                table: "Cortes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FKIdGeneral",
                table: "CorteLaseres",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FKIdGeneral",
                table: "Calidades",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FKIdGeneral",
                table: "Bordados",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FKIdGeneral",
                table: "Almacenes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
