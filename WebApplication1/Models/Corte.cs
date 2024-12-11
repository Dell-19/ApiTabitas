using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebTabitas.Models
{
    public class Corte
    {
        public int IdCorte { get; set; }

        [Required(ErrorMessage = "El IdGeneral es obligatorio")]
        public int IdGeneral { get; set; }

        public string Modelo { get; set; }

        [Required(ErrorMessage = "El nombre del encargado es obligatorio")]
        public string Encargado { get; set; } 

        [Required(ErrorMessage = "La fecha de entrega del corte es obligatoria")]
        public DateTime EntregaCorte { get; set; } = DateTime.Now; 

        [Required(ErrorMessage = "La cantidad cortada es obligatoria")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe ingresar una cantidad válida")]
        public int? CantidadCortadas { get; set; } 

        public IFormFile Imagen { get; set; } 

        public string? RutaImagen { get; set; } 

        [Required(ErrorMessage = "El número de piezas solicitadas es obligatorio")]
        public int? PiezasSolicitadas { get; set; } 

        [Required(ErrorMessage = "La fecha de envío a ventas es obligatoria")]
        public DateTime FechaAVentas { get; set; } = DateTime.Now; 

        public DateTime? FechaEntrega { get; set; } 
        public string? Incidencias { get; set; } 
        [Required(ErrorMessage = "El Area del Proceso es obligatorio")]
        public int IdProceso { get; set; } 
        public string Area { get; set; } 
    }

}
