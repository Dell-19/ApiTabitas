using System.ComponentModel.DataAnnotations.Schema;
using TabitasAPI.Models;

namespace TabitasAPI.DTOs
{
    public class LavadoDTO
    {
        public int IdLavado { get; set; }
        public string Proveedor { get; set; }
        public DateTime FechaEnvio { get; set; }
        public DateTime FechaRecepcion { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public int? CantidadRecibida { get; set; }
        public string? Incidencias { get; set; }
        // Datos Tabla General
        public string Modelo { get; set; }
        public string Area { get; set; }
    }
}
