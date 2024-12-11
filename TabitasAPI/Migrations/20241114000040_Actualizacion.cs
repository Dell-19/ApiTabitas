using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TabitasAPI.Migrations
{
    public partial class Actualizacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Tabla Almacenes
            migrationBuilder.AddColumn<int>(
                name: "IdProceso",
                table: "Almacenes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Almacenes_IdProceso",
                table: "Almacenes",
                column: "IdProceso");

            migrationBuilder.AddForeignKey(
                name: "FK_Almacenes_ProcesoActuales_IdProceso",
                table: "Almacenes",
                column: "IdProceso",
                principalTable: "ProcesoActuales",
                principalColumn: "IdProceso",
                onDelete: ReferentialAction.Restrict);

            // Tabla Bordados
            migrationBuilder.AddColumn<int>(
                name: "IdProceso",
                table: "Bordados",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Bordados_IdProceso",
                table: "Bordados",
                column: "IdProceso");

            migrationBuilder.AddForeignKey(
                name: "FK_Bordados_ProcesoActuales_IdProceso",
                table: "Bordados",
                column: "IdProceso",
                principalTable: "ProcesoActuales",
                principalColumn: "IdProceso",
                onDelete: ReferentialAction.Restrict);

            // Tabla Calidades
            migrationBuilder.AddColumn<int>(
                name: "IdProceso",
                table: "Calidades",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Calidades_IdProceso",
                table: "Calidades",
                column: "IdProceso");

            migrationBuilder.AddForeignKey(
                name: "FK_Calidades_ProcesoActuales_IdProceso",
                table: "Calidades",
                column: "IdProceso",
                principalTable: "ProcesoActuales",
                principalColumn: "IdProceso",
                onDelete: ReferentialAction.Restrict);

            // Tabla CorteLaseres
            migrationBuilder.AddColumn<int>(
                name: "IdProceso",
                table: "CorteLaseres",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CorteLaseres_IdProceso",
                table: "CorteLaseres",
                column: "IdProceso");

            migrationBuilder.AddForeignKey(
                name: "FK_CorteLaseres_ProcesoActuales_IdProceso",
                table: "CorteLaseres",
                column: "IdProceso",
                principalTable: "ProcesoActuales",
                principalColumn: "IdProceso",
                onDelete: ReferentialAction.Restrict);

            // Tabla Cortes
            migrationBuilder.AddColumn<int>(
                name: "IdProceso",
                table: "Cortes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Cortes_IdProceso",
                table: "Cortes",
                column: "IdProceso");

            migrationBuilder.AddForeignKey(
                name: "FK_Cortes_ProcesoActuales_IdProceso",
                table: "Cortes",
                column: "IdProceso",
                principalTable: "ProcesoActuales",
                principalColumn: "IdProceso",
                onDelete: ReferentialAction.Restrict);

            // Tabla Etiquetas
            migrationBuilder.AddColumn<int>(
                name: "IdProceso",
                table: "Etiquetas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Etiquetas_IdProceso",
                table: "Etiquetas",
                column: "IdProceso");

            migrationBuilder.AddForeignKey(
                name: "FK_Etiquetas_ProcesoActuales_IdProceso",
                table: "Etiquetas",
                column: "IdProceso",
                principalTable: "ProcesoActuales",
                principalColumn: "IdProceso",
                onDelete: ReferentialAction.Restrict);

            // Tabla Lavados
            migrationBuilder.AddColumn<int>(
                name: "IdProceso",
                table: "Lavados",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Lavados_IdProceso",
                table: "Lavados",
                column: "IdProceso");

            migrationBuilder.AddForeignKey(
                name: "FK_Lavados_ProcesoActuales_IdProceso",
                table: "Lavados",
                column: "IdProceso",
                principalTable: "ProcesoActuales",
                principalColumn: "IdProceso",
                onDelete: ReferentialAction.Restrict);

            // Tabla Maquilas
            migrationBuilder.AddColumn<int>(
                name: "IdProceso",
                table: "Maquilas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Maquilas_IdProceso",
                table: "Maquilas",
                column: "IdProceso");

            migrationBuilder.AddForeignKey(
                name: "FK_Maquilas_ProcesoActuales_IdProceso",
                table: "Maquilas",
                column: "IdProceso",
                principalTable: "ProcesoActuales",
                principalColumn: "IdProceso",
                onDelete: ReferentialAction.Restrict);

            // Tabla Serigrafias
            migrationBuilder.AddColumn<int>(
                name: "IdProceso",
                table: "Serigrafias",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Serigrafias_IdProceso",
                table: "Serigrafias",
                column: "IdProceso");

            migrationBuilder.AddForeignKey(
                name: "FK_Serigrafias_ProcesoActuales_IdProceso",
                table: "Serigrafias",
                column: "IdProceso",
                principalTable: "ProcesoActuales",
                principalColumn: "IdProceso",
                onDelete: ReferentialAction.Restrict);

            // Tabla Terminados
            migrationBuilder.AddColumn<int>(
                name: "IdProceso",
                table: "Terminados",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Terminados_IdProceso",
                table: "Terminados",
                column: "IdProceso");

            migrationBuilder.AddForeignKey(
                name: "FK_Terminados_ProcesoActuales_IdProceso",
                table: "Terminados",
                column: "IdProceso",
                principalTable: "ProcesoActuales",
                principalColumn: "IdProceso",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Eliminar claves foráneas
            migrationBuilder.DropForeignKey(name: "FK_Almacenes_ProcesoActuales_IdProceso", table: "Almacenes");
            migrationBuilder.DropForeignKey(name: "FK_Bordados_ProcesoActuales_IdProceso", table: "Bordados");
            migrationBuilder.DropForeignKey(name: "FK_Calidades_ProcesoActuales_IdProceso", table: "Calidades");
            migrationBuilder.DropForeignKey(name: "FK_CorteLaseres_ProcesoActuales_IdProceso", table: "CorteLaseres");
            migrationBuilder.DropForeignKey(name: "FK_Cortes_ProcesoActuales_IdProceso", table: "Cortes");
            migrationBuilder.DropForeignKey(name: "FK_Etiquetas_ProcesoActuales_IdProceso", table: "Etiquetas");
            migrationBuilder.DropForeignKey(name: "FK_Lavados_ProcesoActuales_IdProceso", table: "Lavados");
            migrationBuilder.DropForeignKey(name: "FK_Maquilas_ProcesoActuales_IdProceso", table: "Maquilas");
            migrationBuilder.DropForeignKey(name: "FK_Serigrafias_ProcesoActuales_IdProceso", table: "Serigrafias");
            migrationBuilder.DropForeignKey(name: "FK_Terminados_ProcesoActuales_IdProceso", table: "Terminados");

            // Eliminar índices
            migrationBuilder.DropIndex(name: "IX_Almacenes_IdProceso", table: "Almacenes");
            migrationBuilder.DropIndex(name: "IX_Bordados_IdProceso", table: "Bordados");
            migrationBuilder.DropIndex(name: "IX_Calidades_IdProceso", table: "Calidades");
            migrationBuilder.DropIndex(name: "IX_CorteLaseres_IdProceso", table: "CorteLaseres");
            migrationBuilder.DropIndex(name: "IX_Cortes_IdProceso", table: "Cortes");
            migrationBuilder.DropIndex(name: "IX_Etiquetas_IdProceso", table: "Etiquetas");
            migrationBuilder.DropIndex(name: "IX_Lavados_IdProceso", table: "Lavados");
            migrationBuilder.DropIndex(name: "IX_Maquilas_IdProceso", table: "Maquilas");
            migrationBuilder.DropIndex(name: "IX_Serigrafias_IdProceso", table: "Serigrafias");
            migrationBuilder.DropIndex(name: "IX_Terminados_IdProceso", table: "Terminados");

            // Eliminar las columnas IdProceso
            migrationBuilder.DropColumn(name: "IdProceso", table: "Almacenes");
            migrationBuilder.DropColumn(name: "IdProceso", table: "Bordados");
            migrationBuilder.DropColumn(name: "IdProceso", table: "Calidades");
            migrationBuilder.DropColumn(name: "IdProceso", table: "CorteLaseres");
            migrationBuilder.DropColumn(name: "IdProceso", table: "Cortes");
            migrationBuilder.DropColumn(name: "IdProceso", table: "Etiquetas");
            migrationBuilder.DropColumn(name: "IdProceso", table: "Lavados");
            migrationBuilder.DropColumn(name: "IdProceso", table: "Maquilas");
            migrationBuilder.DropColumn(name: "IdProceso", table: "Serigrafias");
            migrationBuilder.DropColumn(name: "IdProceso", table: "Terminados");
        }
    }
}