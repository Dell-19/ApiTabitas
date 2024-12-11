using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TabitasAPI.Models
{
    public class Almacen
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdAlmacen { get; set; }
        public DateTime FechaRecepcion { get; set; }
        public DateTime? FechaLiberacion { get; set; }
        public string Auxiliar { get; set; }
        public DateTime FechaEntregaAvios { get; set; }
        public DateTime? FechaDevolicionTelas { get; set; }
        public DateTime FechaLibeCorte { get; set; }
        public DateTime FechaCarpeta { get; set; }
        public string? Incidencias { get; set; }
        public bool? FechaEditada { get; set; }
        public int IdGeneral { get; set; }
        [ForeignKey("IdGeneral")]
        public virtual General General { get; set; }
        public int IdProceso { get; set; } 
        [ForeignKey("IdProceso")]
        public virtual ProcesoActual ProcesoActual { get; set; }
    }
}

