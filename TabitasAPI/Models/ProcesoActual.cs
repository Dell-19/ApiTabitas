using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TabitasAPI.Models
{
    public class ProcesoActual
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdProceso { get; set; }
        public string Area { get; set; }
    }
}
