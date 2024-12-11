using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebTabitas.Models
{
    public class Calidad
    {
        public int IdCalidad { get; set; }
        public int IdGeneral { get; set; }
        public string Modelo { get; set; }
        [Required(ErrorMessage = "La Fecha de Recepcion es obligatorio")]
        public DateTime FechaDeRecepcion { get; set; } = DateTime.Now;
        [Required(ErrorMessage = "La Fecha de Recepcion es obligatorio")]
        public DateTime FechaRevision { get; set; } = DateTime.Now;
        [Required(ErrorMessage = "El Estado es obligatorio")]
        public string Estado { get; set; }
        public DateTime? FechaEnvioMaquila { get; set; }
        public DateTime? FechaRecepcionRechazo { get; set; }
        public string? Incidencia { get; set; }
        public int IdProceso { get; set; }
        public string Area { get; set; }
    }
}
