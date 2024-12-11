using System.ComponentModel.DataAnnotations.Schema;
using TabitasAPI.Models;

namespace TabitasAPI.DTOs
{
    public class LavadoInsertDTO
    {
        public string Proveedor { get; set; }
        public DateTime FechaEnvio { get; set; }
        public DateTime FechaRecepcion { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public int? CantidadRecibida { get; set; }
        public string? Incidencias { get; set; }
        // Datos Tabla General
        public int IdGeneral { get; set; }
        public int IdProceso { get; set; }
    }
}
