using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TabitasAPI.Models
{
    public class Bordado
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdBordado { get; set; }
        public string Encargado { get; set; }
        public DateTime FechaCorte { get; set; }
        public DateTime FechaRecepcion { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public int CantidadRecibida { get; set; }
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
