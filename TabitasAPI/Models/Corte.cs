using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TabitasAPI.Models
{
    public class Corte
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCorte { get; set; }
        public string Encargado { get; set; }
        public DateTime EntregaCorte { get; set; }
        public int CantidadCortadas { get; set; }
        public string? RutaImagen { get; set; }
        public string? RutaImagenLocal { get; set; }
        public int PiezasSolicitadas { get; set; }
        public DateTime FechaAVentas { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public string? Incidencias { get; set; }
        public bool? FechaEdita { get; set; }
        public int IdGeneral { get; set; }
        [ForeignKey("IdGeneral")]
        public virtual General General { get; set; }
        public int IdProceso { get; set; }
        [ForeignKey("IdProceso")]
        public virtual ProcesoActual ProcesoActual { get; set; }
    }
}
