using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TabitasAPI.Models;

namespace TabitasAPI.DTOs
{
    public class EtiquetaUpdateDTO
    {
        public DateTime? FechaEntregaMaquila { get; set; }
        public DateTime? FechaEntregaTerminado { get; set; }
        public string? Incidencias { get; set; }
    }
}
