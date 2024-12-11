using System.ComponentModel.DataAnnotations.Schema;
using TabitasAPI.Models;

namespace TabitasAPI.DTOs
{
    public class CalidadInsertDTO
    {
        public DateTime FechaDeRecepcion { get; set; }
        public DateTime FechaRevision { get; set; }
        public string Estado { get; set; }
        public DateTime? FechaEnvioMaquila { get; set; }
        public DateTime? FechaRecepcionRechazo { get; set; }
        public string? Incidencia { get; set; }
        // Datos Tabla General
        public int IdGeneral { get; set; }
        public int IdProceso { get; set; }
    }
}
