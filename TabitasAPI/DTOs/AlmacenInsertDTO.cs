using System.ComponentModel.DataAnnotations;

namespace TabitasAPI.DTOs
{
    public class AlmacenInsertDTO
    {
        //[Required] // Esto asegura que se requiere el campo
        public int IdGeneral { get; set; }
        public DateTime FechaRecepcion { get; set; }
        public DateTime? FechaLiberacion { get; set; }
        public string Auxiliar { get; set; }
        public DateTime FechaEntregaAvios { get; set; }
        public DateTime? FechaDevolicionTelas { get; set; }
        public DateTime FechaLibeCorte { get; set; }
        public DateTime FechaCarpeta { get; set; }
        public string? Incidencias { get; set; }
        //public bool FechaEditada { get; set; }

        public int IdProceso { get; set; }
    }

}


