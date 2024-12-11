using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TabitasAPI.Models
{
    public class CorteLaser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCorteLaser { get; set; }
        public int CantidadCortadas { get; set; }
        public string Encargado { get; set; }
        public DateTime FechaRecepcion { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public string Recibe { get; set; }
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
