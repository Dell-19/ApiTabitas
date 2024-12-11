using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TabitasAPI.Models
{
    public class Calidad
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCalidad { get; set; }
        public DateTime FechaDeRecepcion { get; set; }
        public DateTime FechaRevision { get; set; }
        public string Estado { get; set; }
        public DateTime? FechaEnvioMaquila { get; set; }
        public DateTime? FechaRecepcionRechazo { get; set; }
        public string? Incidencia { get; set; }
        public bool? FechaEditada { get; set; }
        public int IdGeneral { get; set; }
        [ForeignKey("IdGeneral")]
        public virtual General General { get; set; }
        public int IdProceso { get; set; }
        [ForeignKey("IdProceso")]
        public virtual ProcesoActual ProcesoActual { get; set; }
    }
}
