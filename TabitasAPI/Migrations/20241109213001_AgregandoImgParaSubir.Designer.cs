﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TabitasAPI.Data;

#nullable disable

namespace TabitasAPI.Migrations
{
    [DbContext(typeof(TabitasContext))]
    [Migration("20241109213001_AgregandoImgParaSubir")]
    partial class AgregandoImgParaSubir
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TabitasAPI.Models.Almacen", b =>
                {
                    b.Property<int>("IdAlmacen")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdAlmacen"));

                    b.Property<string>("Auxiliar")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaCarpeta")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaDevolicionTelas")
                        .HasColumnType("datetime2");

                    b.Property<bool>("FechaEditada")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaEntregaAvios")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaLibeCorte")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaLiberacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaRecepcion")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdGeneral")
                        .HasColumnType("int");

                    b.Property<string>("Incidencias")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdAlmacen");

                    b.HasIndex("IdGeneral");

                    b.ToTable("Almacenes");
                });

            modelBuilder.Entity("TabitasAPI.Models.Bordado", b =>
                {
                    b.Property<int>("IdBordado")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdBordado"));

                    b.Property<int>("CantidadRecibida")
                        .HasColumnType("int");

                    b.Property<string>("Encargado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaCorte")
                        .HasColumnType("datetime2");

                    b.Property<bool>("FechaEditada")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("FechaEntrega")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaRecepcion")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdGeneral")
                        .HasColumnType("int");

                    b.Property<string>("Incidencias")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdBordado");

                    b.HasIndex("IdGeneral");

                    b.ToTable("Bordados");
                });

            modelBuilder.Entity("TabitasAPI.Models.Calidad", b =>
                {
                    b.Property<int>("IdCalidad")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCalidad"));

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaDeRecepcion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaEnvioMaquila")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaRecepcionRechazo")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaRevision")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdGeneral")
                        .HasColumnType("int");

                    b.Property<string>("Incidencia")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdCalidad");

                    b.HasIndex("IdGeneral");

                    b.ToTable("Calidades");
                });

            modelBuilder.Entity("TabitasAPI.Models.Corte", b =>
                {
                    b.Property<int>("IdCorte")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCorte"));

                    b.Property<int>("CantidadCortadas")
                        .HasColumnType("int");

                    b.Property<string>("Encargado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EntregaCorte")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaAVentas")
                        .HasColumnType("datetime2");

                    b.Property<bool>("FechaEdita")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaEntrega")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdGeneral")
                        .HasColumnType("int");

                    b.Property<string>("Incidencias")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PiezasSolicitadas")
                        .HasColumnType("int");

                    b.HasKey("IdCorte");

                    b.HasIndex("IdGeneral");

                    b.ToTable("Cortes");
                });

            modelBuilder.Entity("TabitasAPI.Models.CorteLaser", b =>
                {
                    b.Property<int>("IdCorteLaser")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCorteLaser"));

                    b.Property<int>("CantidadCortadas")
                        .HasColumnType("int");

                    b.Property<string>("Encargado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("FechaEditada")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("FechaEntrega")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaRecepcion")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdGeneral")
                        .HasColumnType("int");

                    b.Property<string>("Recibe")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdCorteLaser");

                    b.HasIndex("IdGeneral");

                    b.ToTable("CorteLaseres");
                });

            modelBuilder.Entity("TabitasAPI.Models.Etiqueta", b =>
                {
                    b.Property<int>("IdEtiquetas")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdEtiquetas"));

                    b.Property<bool>("FechaEditada")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("FechaEntregaMaquila")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaEntregaTerminado")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdGeneral")
                        .HasColumnType("int");

                    b.Property<string>("Incidencias")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdEtiquetas");

                    b.HasIndex("IdGeneral");

                    b.ToTable("Etiquetas");
                });

            modelBuilder.Entity("TabitasAPI.Models.General", b =>
                {
                    b.Property<int>("IdGeneral")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdGeneral"));

                    b.Property<int>("CantidadRequerida")
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("FechaEntrega")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaRecepcion")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdProceso")
                        .HasColumnType("int");

                    b.Property<string>("Modelo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumeroOrden")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RangoTallas")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RutaImagen")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RutaImagenLocal")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Temporadas")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdGeneral");

                    b.HasIndex("IdProceso");

                    b.ToTable("Generales");
                });

            modelBuilder.Entity("TabitasAPI.Models.Lavado", b =>
                {
                    b.Property<int>("IdLavado")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdLavado"));

                    b.Property<int?>("CantidadRecibida")
                        .HasColumnType("int");

                    b.Property<bool>("FechaEditada")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("FechaEntrega")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaEnvio")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaRecepcion")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdGeneral")
                        .HasColumnType("int");

                    b.Property<string>("Incidencias")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Proveedor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdLavado");

                    b.HasIndex("IdGeneral");

                    b.ToTable("Lavados");
                });

            modelBuilder.Entity("TabitasAPI.Models.Maquila", b =>
                {
                    b.Property<int>("IdMaquila")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdMaquila"));

                    b.Property<int>("CantidadRecibida")
                        .HasColumnType("int");

                    b.Property<bool>("FechaEditada")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("FechaEntrega")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaEntregaMaq1")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaEntregaMaq2")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaEntregaMaq3")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaMaquila")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaRecepcion")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdGeneral")
                        .HasColumnType("int");

                    b.Property<string>("Incidencias")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Maquilero1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Maquilero2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Maquilero3")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdMaquila");

                    b.HasIndex("IdGeneral");

                    b.ToTable("Maquilas");
                });

            modelBuilder.Entity("TabitasAPI.Models.ProcesoActual", b =>
                {
                    b.Property<int>("IdProceso")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdProceso"));

                    b.Property<string>("Area")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdProceso");

                    b.ToTable("ProcesoActuales");
                });

            modelBuilder.Entity("TabitasAPI.Models.Serigrafia", b =>
                {
                    b.Property<int>("IdSerigrafia")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdSerigrafia"));

                    b.Property<int>("CantidadRecibida")
                        .HasColumnType("int");

                    b.Property<string>("Encargado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaCorte")
                        .HasColumnType("datetime2");

                    b.Property<bool>("FechaEditada")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("FechaEntrega")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaRecepcion")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdGeneral")
                        .HasColumnType("int");

                    b.Property<string>("Incidencias")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdSerigrafia");

                    b.HasIndex("IdGeneral");

                    b.ToTable("Serigrafias");
                });

            modelBuilder.Entity("TabitasAPI.Models.Terminado", b =>
                {
                    b.Property<int>("IdTErminado")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdTErminado"));

                    b.Property<int>("CantidadEntregada")
                        .HasColumnType("int");

                    b.Property<string>("Encargado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("FechaEditada")
                        .HasColumnType("bit");

                    b.Property<int>("IdGeneral")
                        .HasColumnType("int");

                    b.Property<string>("MotivoFaltante")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Saldo")
                        .HasColumnType("int");

                    b.HasKey("IdTErminado");

                    b.HasIndex("IdGeneral");

                    b.ToTable("Terminados");
                });

            modelBuilder.Entity("TabitasAPI.Models.Almacen", b =>
                {
                    b.HasOne("TabitasAPI.Models.General", "General")
                        .WithMany()
                        .HasForeignKey("IdGeneral")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("General");
                });

            modelBuilder.Entity("TabitasAPI.Models.Bordado", b =>
                {
                    b.HasOne("TabitasAPI.Models.General", "General")
                        .WithMany()
                        .HasForeignKey("IdGeneral")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("General");
                });

            modelBuilder.Entity("TabitasAPI.Models.Calidad", b =>
                {
                    b.HasOne("TabitasAPI.Models.General", "General")
                        .WithMany()
                        .HasForeignKey("IdGeneral")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("General");
                });

            modelBuilder.Entity("TabitasAPI.Models.Corte", b =>
                {
                    b.HasOne("TabitasAPI.Models.General", "General")
                        .WithMany()
                        .HasForeignKey("IdGeneral")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("General");
                });

            modelBuilder.Entity("TabitasAPI.Models.CorteLaser", b =>
                {
                    b.HasOne("TabitasAPI.Models.General", "General")
                        .WithMany()
                        .HasForeignKey("IdGeneral")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("General");
                });

            modelBuilder.Entity("TabitasAPI.Models.Etiqueta", b =>
                {
                    b.HasOne("TabitasAPI.Models.General", "General")
                        .WithMany()
                        .HasForeignKey("IdGeneral")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("General");
                });

            modelBuilder.Entity("TabitasAPI.Models.General", b =>
                {
                    b.HasOne("TabitasAPI.Models.ProcesoActual", "ProcesoActual")
                        .WithMany()
                        .HasForeignKey("IdProceso")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProcesoActual");
                });

            modelBuilder.Entity("TabitasAPI.Models.Lavado", b =>
                {
                    b.HasOne("TabitasAPI.Models.General", "General")
                        .WithMany()
                        .HasForeignKey("IdGeneral")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("General");
                });

            modelBuilder.Entity("TabitasAPI.Models.Maquila", b =>
                {
                    b.HasOne("TabitasAPI.Models.General", "General")
                        .WithMany()
                        .HasForeignKey("IdGeneral")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("General");
                });

            modelBuilder.Entity("TabitasAPI.Models.Serigrafia", b =>
                {
                    b.HasOne("TabitasAPI.Models.General", "General")
                        .WithMany()
                        .HasForeignKey("IdGeneral")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("General");
                });

            modelBuilder.Entity("TabitasAPI.Models.Terminado", b =>
                {
                    b.HasOne("TabitasAPI.Models.General", "General")
                        .WithMany()
                        .HasForeignKey("IdGeneral")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("General");
                });
#pragma warning restore 612, 618
        }
    }
}
