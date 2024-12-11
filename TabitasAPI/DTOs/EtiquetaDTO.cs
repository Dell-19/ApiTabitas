using System.ComponentModel.DataAnnotations.Schema;
using TabitasAPI.Models;

namespace TabitasAPI.DTOs
{
    public class EtiquetaDTO
    {
        public int IdEtiquetas { get; set; }
        public DateTime? FechaEntregaMaquila { get; set; }
        public DateTime? FechaEntregaTerminado { get; set; }
        public string? Incidencias { get; set; }
        // Datos Tabla General
        public string Modelo { get; set; }
        public string Area { get; set; }
    }
}
