using System.ComponentModel.DataAnnotations.Schema;
using TabitasAPI.Models;

namespace TabitasAPI.DTOs
{
    public class EtiquetaInsertDTO
    {
        //public int IdEtiquetas { get; set; }
        public DateTime? FechaEntregaMaquila { get; set; }
        public DateTime? FechaEntregaTerminado { get; set; }
        public string? Incidencias { get; set; }
        // Datos Tabla General
        public int IdGeneral { get; set; }
        public int IdProceso { get; set; }
    }
}
