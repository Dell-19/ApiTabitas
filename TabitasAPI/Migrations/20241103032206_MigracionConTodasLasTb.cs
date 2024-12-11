using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TabitasAPI.Migrations
{
    /// <inheritdoc />
    public partial class MigracionConTodasLasTb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProcesoActuales",
                columns: table => new
                {
                    IdProceso = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Area = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcesoActuales", x => x.IdProceso);
                });

            migrationBuilder.CreateTable(
                name: "Generales",
                columns: table => new
                {
                    IdGeneral = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Imagen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Modelo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RangoTallas = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Temporadas = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumeroOrden = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CantidadRequerida = table.Column<int>(type: "int", nullable: false),
                    FechaRecepcion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaEntrega = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FKIdProceso = table.Column<int>(type: "int", nullable: false),
                    IdProceso = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Generales", x => x.IdGeneral);
                    table.ForeignKey(
                        name: "FK_Generales_ProcesoActuales_IdProceso",
                        column: x => x.IdProceso,
                        principalTable: "ProcesoActuales",
                        principalColumn: "IdProceso",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Almacenes",
                columns: table => new
                {
                    IdAlmacen = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaRecepcion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaLiberacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Auxiliar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaEntregaAvios = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaDevolicionTelas = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaLibeCorte = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaCarpeta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Incidencias = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaEditada = table.Column<bool>(type: "bit", nullable: false),
                    FKIdGeneral = table.Column<int>(type: "int", nullable: false),
                    IdGeneral = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Almacenes", x => x.IdAlmacen);
                    table.ForeignKey(
                        name: "FK_Almacenes_Generales_IdGeneral",
                        column: x => x.IdGeneral,
                        principalTable: "Generales",
                        principalColumn: "IdGeneral",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bordados",
                columns: table => new
                {
                    IdBordado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Encargado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCorte = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaRecepcion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaEntrega = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CantidadRecibida = table.Column<int>(type: "int", nullable: false),
                    Incidencias = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaEditada = table.Column<bool>(type: "bit", nullable: false),
                    FKIdGeneral = table.Column<int>(type: "int", nullable: false),
                    IdGeneral = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bordados", x => x.IdBordado);
                    table.ForeignKey(
                        name: "FK_Bordados_Generales_IdGeneral",
                        column: x => x.IdGeneral,
                        principalTable: "Generales",
                        principalColumn: "IdGeneral",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Calidades",
                columns: table => new
                {
                    IdCalidad = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaDeRecepcion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaRevision = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaEnvioMaquila = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaRecepcionRechazo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Incidencia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaEditada = table.Column<bool>(type: "bit", nullable: true),
                    FKIdGeneral = table.Column<int>(type: "int", nullable: false),
                    IdGeneral = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calidades", x => x.IdCalidad);
                    table.ForeignKey(
                        name: "FK_Calidades_Generales_IdGeneral",
                        column: x => x.IdGeneral,
                        principalTable: "Generales",
                        principalColumn: "IdGeneral",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CorteLaseres",
                columns: table => new
                {
                    IdCorteLaser = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CantidadCortadas = table.Column<int>(type: "int", nullable: false),
                    Encargado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaRecepcion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaEntrega = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Recibe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Incidencias = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaEditada = table.Column<bool>(type: "bit", nullable: false),
                    FKIdGeneral = table.Column<int>(type: "int", nullable: false),
                    IdGeneral = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CorteLaseres", x => x.IdCorteLaser);
                    table.ForeignKey(
                        name: "FK_CorteLaseres_Generales_IdGeneral",
                        column: x => x.IdGeneral,
                        principalTable: "Generales",
                        principalColumn: "IdGeneral",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cortes",
                columns: table => new
                {
                    IdCorte = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Encargado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntregaCorte = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CantidadCortadas = table.Column<int>(type: "int", nullable: false),
                    Imagen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PiezasSolicitadas = table.Column<int>(type: "int", nullable: false),
                    FechaAVentas = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaEntrega = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Incidencias = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaEdita = table.Column<bool>(type: "bit", nullable: false),
                    FKIdGeneral = table.Column<int>(type: "int", nullable: false),
                    IdGeneral = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cortes", x => x.IdCorte);
                    table.ForeignKey(
                        name: "FK_Cortes_Generales_IdGeneral",
                        column: x => x.IdGeneral,
                        principalTable: "Generales",
                        principalColumn: "IdGeneral",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Etiquetas",
                columns: table => new
                {
                    IdEtiquetas = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaEntregaMaquila = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaEntregaTerminado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Incidencias = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaEditada = table.Column<bool>(type: "bit", nullable: false),
                    FKIdGeneral = table.Column<int>(type: "int", nullable: false),
                    IdGeneral = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Etiquetas", x => x.IdEtiquetas);
                    table.ForeignKey(
                        name: "FK_Etiquetas_Generales_IdGeneral",
                        column: x => x.IdGeneral,
                        principalTable: "Generales",
                        principalColumn: "IdGeneral",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lavados",
                columns: table => new
                {
                    IdLavado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Proveedor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaEnvio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaRecepcion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaEntrega = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CantidadRecibida = table.Column<int>(type: "int", nullable: true),
                    Incidencias = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaEditada = table.Column<bool>(type: "bit", nullable: false),
                    FKIdGeneral = table.Column<int>(type: "int", nullable: false),
                    IdGeneral = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lavados", x => x.IdLavado);
                    table.ForeignKey(
                        name: "FK_Lavados_Generales_IdGeneral",
                        column: x => x.IdGeneral,
                        principalTable: "Generales",
                        principalColumn: "IdGeneral",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Maquilas",
                columns: table => new
                {
                    IdMaquila = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Maquilero1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Maquilero2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maquilero3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maquilero4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaEntregaMaq1 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaEntregaMaq2 = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaEntregaMaq3 = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaEntregaMaq4 = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaMaquila = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaRecepcion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaEntrega = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CantidadRecibida = table.Column<int>(type: "int", nullable: false),
                    Incidencias = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaEditada = table.Column<bool>(type: "bit", nullable: false),
                    FKIdGeneral = table.Column<int>(type: "int", nullable: false),
                    IdGeneral = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maquilas", x => x.IdMaquila);
                    table.ForeignKey(
                        name: "FK_Maquilas_Generales_IdGeneral",
                        column: x => x.IdGeneral,
                        principalTable: "Generales",
                        principalColumn: "IdGeneral",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Serigrafias",
                columns: table => new
                {
                    IdSerigrafia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Encargado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCorte = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaRecepcion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaEntrega = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CantidadRecibida = table.Column<int>(type: "int", nullable: false),
                    Incidencias = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaEditada = table.Column<bool>(type: "bit", nullable: false),
                    FKIdGeneral = table.Column<int>(type: "int", nullable: false),
                    IdGeneral = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Serigrafias", x => x.IdSerigrafia);
                    table.ForeignKey(
                        name: "FK_Serigrafias_Generales_IdGeneral",
                        column: x => x.IdGeneral,
                        principalTable: "Generales",
                        principalColumn: "IdGeneral",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Terminados",
                columns: table => new
                {
                    IdTErminado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Encargado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CantidadEntregada = table.Column<int>(type: "int", nullable: false),
                    Saldo = table.Column<int>(type: "int", nullable: false),
                    MotivoFaltante = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaEntrega = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaEditada = table.Column<bool>(type: "bit", nullable: false),
                    FKIdGeneral = table.Column<int>(type: "int", nullable: false),
                    IdGeneral = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Terminados", x => x.IdTErminado);
                    table.ForeignKey(
                        name: "FK_Terminados_Generales_IdGeneral",
                        column: x => x.IdGeneral,
                        principalTable: "Generales",
                        principalColumn: "IdGeneral",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Almacenes_IdGeneral",
                table: "Almacenes",
                column: "IdGeneral");

            migrationBuilder.CreateIndex(
                name: "IX_Bordados_IdGeneral",
                table: "Bordados",
                column: "IdGeneral");

            migrationBuilder.CreateIndex(
                name: "IX_Calidades_IdGeneral",
                table: "Calidades",
                column: "IdGeneral");

            migrationBuilder.CreateIndex(
                name: "IX_CorteLaseres_IdGeneral",
                table: "CorteLaseres",
                column: "IdGeneral");

            migrationBuilder.CreateIndex(
                name: "IX_Cortes_IdGeneral",
                table: "Cortes",
                column: "IdGeneral");

            migrationBuilder.CreateIndex(
                name: "IX_Etiquetas_IdGeneral",
                table: "Etiquetas",
                column: "IdGeneral");

            migrationBuilder.CreateIndex(
                name: "IX_Generales_IdProceso",
                table: "Generales",
                column: "IdProceso");

            migrationBuilder.CreateIndex(
                name: "IX_Lavados_IdGeneral",
                table: "Lavados",
                column: "IdGeneral");

            migrationBuilder.CreateIndex(
                name: "IX_Maquilas_IdGeneral",
                table: "Maquilas",
                column: "IdGeneral");

            migrationBuilder.CreateIndex(
                name: "IX_Serigrafias_IdGeneral",
                table: "Serigrafias",
                column: "IdGeneral");

            migrationBuilder.CreateIndex(
                name: "IX_Terminados_IdGeneral",
                table: "Terminados",
                column: "IdGeneral");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Almacenes");

            migrationBuilder.DropTable(
                name: "Bordados");

            migrationBuilder.DropTable(
                name: "Calidades");

            migrationBuilder.DropTable(
                name: "CorteLaseres");

            migrationBuilder.DropTable(
                name: "Cortes");

            migrationBuilder.DropTable(
                name: "Etiquetas");

            migrationBuilder.DropTable(
                name: "Lavados");

            migrationBuilder.DropTable(
                name: "Maquilas");

            migrationBuilder.DropTable(
                name: "Serigrafias");

            migrationBuilder.DropTable(
                name: "Terminados");

            migrationBuilder.DropTable(
                name: "Generales");

            migrationBuilder.DropTable(
                name: "ProcesoActuales");
        }
    }
}
