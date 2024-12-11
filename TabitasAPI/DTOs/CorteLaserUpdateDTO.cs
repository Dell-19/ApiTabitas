using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TabitasAPI.Models;

namespace TabitasAPI.DTOs
{
    public class CorteLaserUpdateDTO
    {
        public DateTime FechaEntrega { get; set; }
        public string? Incidencias { get; set; }
    }
}
