using Microsoft.EntityFrameworkCore;
using TabitasAPI.Models;

namespace TabitasAPI.Data
{
    public class TabitasContext : DbContext
    {
        public TabitasContext(DbContextOptions<TabitasContext> conexion)
            : base(conexion)
        {
        }
        public DbSet<Almacen> Almacenes { get; set; }
        public DbSet<Bordado> Bordados { get; set; }
        public DbSet<Calidad> Calidades { get; set; }
        public DbSet<Corte> Cortes { get; set; }
        public DbSet<CorteLaser> CorteLaseres { get; set; }
        public DbSet<Etiqueta> Etiquetas { get; set; }
        public DbSet<General> Generales { get; set; }
        public DbSet<Lavado> Lavados { get; set; }
        public DbSet<Maquila> Maquilas { get; set; }
        public DbSet<ProcesoActual> ProcesoActuales { get; set; }
        public DbSet<Serigrafia> Serigrafias { get; set; }
        public DbSet<Terminado> Terminados { get; set; }

        public DbSet<Usuarios> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Almacen>()
                .ToTable(tb => tb.HasTrigger("trigger_actualizar_proceso_actual" + "trigger_invoke_set_fecha_editada_on_insert" + "trg_UpdateFechaEditada"));
            modelBuilder.Entity<Etiqueta>()
               .ToTable(tb => tb.HasTrigger("trigger_invoke_set_fecha_etiquetas" + "trg_UpdateFecha_Etiquetas" + "trg_UpdateIdProcesoEtiqueta"));
            modelBuilder.Entity<Corte>()
               .ToTable(tb => tb.HasTrigger("trigger_invoke_set_fecha_Cortes" + "trg_UpdateIdProcesoCorte" + "trg_UpdateFecha_Cortes"));
            modelBuilder.Entity<CorteLaser>()
               .ToTable(tb => tb.HasTrigger("trigger_invoke_set_fecha_CorteLaseres" + "trg_UpdateIdProcesoCorteLaser" + "trg_UpdateFecha_CorteLaseres"));
            modelBuilder.Entity<Serigrafia>()
               .ToTable(tb => tb.HasTrigger("trigger_invoke_set_fecha_Serigrafias" + "trg_UpdateIdProcesoSerigrafia" + "trg_UpdateFecha_Serigrafias"));
            modelBuilder.Entity<Bordado>()
               .ToTable(tb => tb.HasTrigger("trigger_invoke_set_fecha_Bordados" + "trg_UpdateIdProcesoBordado" + "trg_UpdateFecha_Bordados"));
            modelBuilder.Entity<Maquila>()
               .ToTable(tb => tb.HasTrigger("trigger_invoke_set_fecha_Maquilas" + "trg_UpdateIdProcesoMaquila" + "trg_UpdateFecha_Maquilas"));
            modelBuilder.Entity<Lavado>()
               .ToTable(tb => tb.HasTrigger("trigger_invoke_set_fecha_Lavados" + "trg_UpdateIdProcesoLavado" + "trg_UpdateFecha_Lavados"));
            modelBuilder.Entity<Calidad>()
               .ToTable(tb => tb.HasTrigger("trigger_invoke_set_fecha_Calidades" + "trg_UpdateIdProcesoCalidad" + "trg_UpdateFecha_Calidades"));
            modelBuilder.Entity<Terminado>()
               .ToTable(tb => tb.HasTrigger("trigger_invoke_set_fecha_Bordados" + "trg_UpdateIdProcesoBordado" + "trg_UpdateFecha_Terminados"));
        }
    }
}