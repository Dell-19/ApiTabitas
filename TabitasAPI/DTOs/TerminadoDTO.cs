using System.ComponentModel.DataAnnotations.Schema;
using TabitasAPI.Models;

namespace TabitasAPI.DTOs
{
    public class TerminadoDTO
    {
        public int IdTErminado { get; set; }
        public string Encargado { get; set; }
        public int CantidadEntregada { get; set; }
        public int Saldo { get; set; }
        public string MotivoFaltante { get; set; }
        public DateTime? FechaEntrega { get; set; }
        // Datos Tabla General
        public string Modelo { get; set; }
        public string Area { get; set; }
    }
}
