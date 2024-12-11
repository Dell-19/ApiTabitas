using System.ComponentModel.DataAnnotations.Schema;
using TabitasAPI.Models;

namespace TabitasAPI.DTOs
{
    public class MaquilaInsertDTO
    {
        public string Maquilero1 { get; set; }
        public string? Maquilero2 { get; set; }
        public string? Maquilero3 { get; set; }
        public string? Maquilero4 { get; set; }
        public DateTime FechaEntregaMaq1 { get; set; }
        public DateTime? FechaEntregaMaq2 { get; set; }
        public DateTime? FechaEntregaMaq3 { get; set; }
        public DateTime? FechaEntregaMaq4 { get; set; }
        public DateTime FechaMaquila { get; set; }
        public DateTime FechaRecepcion { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public int CantidadRecibida { get; set; }
        public string? Incidencias { get; set; }
        // Datos Tabla General
        public int IdGeneral { get; set; }
        public int IdProceso { get; set; }
    }
}
