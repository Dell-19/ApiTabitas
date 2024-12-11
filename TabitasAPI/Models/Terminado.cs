using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TabitasAPI.Models
{
    public class Terminado
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdTErminado { get; set; }
        public string Encargado { get; set; }
        public int CantidadEntregada { get; set; }
        public int Saldo { get; set; }
        public string MotivoFaltante { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public bool? FechaEditada { get; set; }
        public int IdGeneral { get; set; }
        [ForeignKey("IdGeneral")]
        public virtual General General { get; set; }
        public int IdProceso { get; set; } 
        [ForeignKey("IdProceso")]
        public virtual ProcesoActual ProcesoActual { get; set; }
    }
}
