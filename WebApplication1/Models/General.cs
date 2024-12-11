using System.ComponentModel.DataAnnotations;

namespace WebTabitas.Models
{
    public class General
    {
        public int IdGeneral { get; set; }
        [Required(ErrorMessage = "La Modelo es obligatorio")]
        public string Modelo { get; set; }
        public string RutaImagen { get; set; }
        public IFormFile Imagen { get; set; } // Para cargar la imagen

        [Required(ErrorMessage = "La Descripcion es obligatoria")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "La Rango de Tallas es obligatorio")]
        public string RangoTallas { get; set; }

        [Required(ErrorMessage = "La Temporada es obligatoria")]
        public string Temporadas { get; set; }

        [Required(ErrorMessage = "La Orden de Producción es obligatoria")]
        public string Pt { get; set; }

        [Required(ErrorMessage = "La Numero de Orden  es obligatorio")]
        public string NumeroOrden { get; set; }

        [Required(ErrorMessage = "La Cantidad es obligatoria")]
        public int? CantidadRequerida { get; set; }

        [Required(ErrorMessage = "La Fecha de Recepción es obligatoria")]
        public DateTime FechaRecepcion { get; set; } = DateTime.Today;

        public DateTime? FechaEntrega { get; set; }

        [Required(ErrorMessage = "La Area es obligatoria")]
        public int IdProceso { get; set; }

        public string ProcesoActual { get; set; }
    }
}
