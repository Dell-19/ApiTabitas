using System.ComponentModel.DataAnnotations.Schema;
using TabitasAPI.Models;

namespace TabitasAPI.DTOs
{
    public class SerigrafiaDTO
    {
        public int IdSerigrafia { get; set; }
        public string Encargado { get; set; }
        public DateTime FechaCorte { get; set; }
        public DateTime FechaRecepcion { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public int CantidadRecibida { get; set; }
        public string? Incidencias { get; set; }
        // Datos Tabla General
        public string Modelo { get; set; }
        public string Area { get; set; }
    }
}
