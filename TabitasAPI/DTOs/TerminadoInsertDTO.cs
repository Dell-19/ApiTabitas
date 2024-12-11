using System.ComponentModel.DataAnnotations.Schema;
using TabitasAPI.Models;

namespace TabitasAPI.DTOs
{
    public class TerminadoInsertDTO
    {
        public string Encargado { get; set; }
        public int CantidadEntregada { get; set; }
        public int Saldo { get; set; }
        public string MotivoFaltante { get; set; }
        public DateTime? FechaEntrega { get; set; }//agregar a migracion
        // Datos Tabla General
        public int IdGeneral { get; set; }
        public int IdProceso { get; set; }
    }
}
