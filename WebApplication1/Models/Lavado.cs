using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebTabitas.Models
{
    public class Lavado
    {
        public int IdLavado { get; set; }
        public int IdGeneral { get; set; }
        public string Modelo { get; set; }
        [Required(ErrorMessage = "El Proovedor es obligatorio")]
        public string Proveedor { get; set; }
        [Required(ErrorMessage = "La Fecha de Envio es obligatorio")]
        public DateTime FechaEnvio { get; set; } = DateTime.Now;
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
