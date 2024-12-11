using System.ComponentModel.DataAnnotations.Schema;
using TabitasAPI.Models;

namespace TabitasAPI.DTOs
{
    public class CorteInsertDTO
    {
        public string Encargado { get; set; }
        public DateTime EntregaCorte { get; set; }
        public int CantidadCortadas { get; set; }
        public string? RutaImagen { get; set; }
        public IFormFile Imagen { get; set; }
        public int PiezasSolicitadas { get; set; }
        public DateTime FechaAVentas { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public string? Incidencias { get; set; }
        // Datos Tabla General
        public int IdGeneral { get; set; }
        public int IdProceso { get; set; }
    }
}
