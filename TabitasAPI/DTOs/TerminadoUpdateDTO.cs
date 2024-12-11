using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TabitasAPI.Models;

namespace TabitasAPI.DTOs
{
    public class TerminadoUpdateDTO
    {
        public DateTime? FechaEntrega { get; set; }
    }
}
