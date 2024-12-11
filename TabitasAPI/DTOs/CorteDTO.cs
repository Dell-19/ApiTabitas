using System.ComponentModel.DataAnnotations.Schema;
using TabitasAPI.Models;

namespace TabitasAPI.DTOs
{
    public class CorteDTO
    {
        public int IdCorte { get; set; }
        public string Encargado { get; set; }
        public DateTime EntregaCorte { get; set; }
        public int CantidadCortadas { get; set; }
        public string? RutaImagen { get; set; }
        public string? RutaImagenLocal { get; set; }
        public int PiezasSolicitadas { get; set; }
        public DateTime FechaAVentas { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public string? Incidencias { get; set; }
        // Datos Tabla General
        public string Modelo { get; set; }
        public string Area { get; set; }
    }
}
