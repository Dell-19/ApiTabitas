using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TabitasAPI.Models;

namespace TabitasAPI.DTOs
{
    public class AlmacenDTO
    {
        public int IdAlmacen { get; set; }
        public string Modelo { get; set; }
        public DateTime FechaRecepcion { get; set; }
        public DateTime? FechaLiberacion { get; set; }
        public string Auxiliar { get; set; }
        public DateTime FechaEntregaAvios { get; set; }
        public DateTime? FechaDevolicionTelas { get; set; }
        public DateTime FechaLibeCorte { get; set; }
        public DateTime FechaCarpeta { get; set; }
        public string? Incidencias { get; set; }
        // Datos Tabla General
        public string Area { get; set; }
    }
}

