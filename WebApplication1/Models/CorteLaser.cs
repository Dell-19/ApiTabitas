using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebTabitas.Models
{
    public class CorteLaser
    {
        public int IdCorteLaser { get; set; }
        public int IdGeneral { get; set; }
        public string Modelo { get; set; }
        [Required(ErrorMessage = "La Cantidad es obligatorio")]
        public int? CantidadCortadas { get; set; }
        [Required(ErrorMessage = "El Encargado es obligatorio")]
        public string Encargado { get; set; }
        [Required(ErrorMessage = "La Fecha Recepcion es obligatorio")]
        public DateTime FechaRecepcion { get; set; } = DateTime.Now;
        public DateTime? FechaEntrega { get; set; }
        [Required(ErrorMessage = "El Recibe es obligatorio")]
        public string Recibe { get; set; }
        public string? Incidencias { get; set; }
        public bool? FechaEditada { get; set; }
        public int IdProceso { get; set; } 
        public string Area { get; set; }
    }
}
