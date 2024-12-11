using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebTabitas.Models
{
    public class Bordado
    {
        public int IdBordado { get; set; }
        public int IdGeneral { get; set; }
        public string Modelo { get; set; }
        [Required(ErrorMessage = "El Encargado es obligatorio")]
        public string Encargado { get; set; }
        [Required(ErrorMessage = "La Fecha Corte es obligatorio")]
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
