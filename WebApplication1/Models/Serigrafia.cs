using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebTabitas.Models
{
    public class Serigrafia
    {
        public int IdSerigrafia { get; set; }
        public int IdGeneral { get; set; }
        public string Modelo { get; set; }
        [Required(ErrorMessage = "El Encargado es obligatorio")]
        public string Encargado { get; set; }
        [Required(ErrorMessage = "La Fecha de Corte es obligatorio")]
        public DateTime FechaCorte { get; set; } = DateTime.Now;
        [Required(ErrorMessage = "La Fecha de Recepcion es obligatorio")]
        public DateTime FechaRecepcion { get; set; } = DateTime.Now;
        public DateTime? FechaEntrega { get; set; }
        [Required(ErrorMessage = "La Cantidad es obligatorio")]
        public int? CantidadRecibida { get; set; }
        public string? Incidencias { get; set; }
        public int IdProceso { get; set; }
        public string Area { get; set; }
    }
}
