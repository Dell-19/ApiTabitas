using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebTabitas.Models
{
    public class Etiqueta
    {
        public int IdEtiquetas { get; set; }
        public int IdGeneral { get; set; }
        public string Modelo { get; set; }
        public DateTime? FechaEntregaMaquila { get; set; } 
        public DateTime? FechaEntregaTerminado { get; set; } 
        public string? Incidencias { get; set; }
        public int IdProceso { get; set; } 
        public string Area { get; set; }
    }
}
