using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TabitasAPI.Models;

namespace TabitasAPI.DTOs
{
    public class CalidadUpdateDTO
    {
        public DateTime? FechaEnvioMaquila { get; set; }
        public DateTime? FechaRecepcionRechazo { get; set; }
        public string? Incidencia { get; set; }
    }
}
