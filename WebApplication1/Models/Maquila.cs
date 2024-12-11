using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebTabitas.Models
{
    public class Maquila
    {
        public int IdMaquila { get; set; }
        public int IdGeneral { get; set; }
        public string Modelo { get; set; }
        [Required(ErrorMessage = "El Maquilero es obligatorio")]
        public string Maquilero1 { get; set; }
        public string? Maquilero2 { get; set; }
        public string? Maquilero3 { get; set; }
        public string? Maquilero4 { get; set; }
        [Required(ErrorMessage = "La fecha de Maquilero es obligatorio")]
        public DateTime FechaEntregaMaq1 { get; set; } = DateTime.Now;
        public DateTime? FechaEntregaMaq2 { get; set; } 
        public DateTime? FechaEntregaMaq3 { get; set; }
        public DateTime? FechaEntregaMaq4 { get; set; }
        [Required(ErrorMessage = "La Fecha de Maquila es obligatorio")]
        public DateTime FechaMaquila { get; set; } = DateTime.Now;
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
