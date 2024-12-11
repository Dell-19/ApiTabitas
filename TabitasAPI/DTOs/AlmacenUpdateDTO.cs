using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TabitasAPI.Models;

namespace TabitasAPI.DTOs
{
    public class AlmacenUpdateDTO
    {
        public DateTime? FechaLiberacion { get; set; }
        public DateTime? FechaDevolicionTelas { get; set; }
        public string? Incidencias { get; set; }
    }
}
