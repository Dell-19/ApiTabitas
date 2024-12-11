using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebTabitas.Models
{
    public class Terminado
    {
        public int IdTErminado { get; set; }
        public int IdGeneral { get; set; }
        public string Modelo { get; set; }
        [Required(ErrorMessage = "El Encargado es obligatorio")]
        public string Encargado { get; set; }
        [Required(ErrorMessage = "La Cantidad es obligatorio")]
        public int? CantidadEntregada { get; set; }
        [Required(ErrorMessage = "El Saldo es obligatorio")]
        public int? Saldo { get; set; }
        [Required(ErrorMessage = "El Motivo es obligatorio")]
        public string MotivoFaltante { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public int IdProceso { get; set; } 
        public string Area { get; set; }
    }
}
