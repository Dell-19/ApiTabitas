using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebTabitas.Models;

namespace WebTabitas.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Almacen
    {
        public int IdAlmacen { get; set; }

        [Required(ErrorMessage = "El Id del Modelo es obligatorio")]
        public int IdGeneral { get; set; }

        public string Modelo { get; set; } 

        [Required(ErrorMessage = "La Fecha de Recepción es obligatoria")]
        public DateTime FechaRecepcion { get; set; } = DateTime.Now;

        public DateTime? FechaLiberacion { get; set; }

        [Required(ErrorMessage = "El Auxiliar es obligatorio")]
        public string Auxiliar { get; set; }

        [Required(ErrorMessage = "La Fecha de Entrega de Avíos es obligatoria")]
        public DateTime FechaEntregaAvios { get; set; } = DateTime.Now;

        public DateTime? FechaDevolicionTelas { get; set; }

        [Required(ErrorMessage = "La Fecha de Liberación del Corte es obligatoria")]
        public DateTime FechaLibeCorte { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "La Fecha de Carpeta es obligatoria")]
        public DateTime FechaCarpeta { get; set; } = DateTime.Now;

        public string? Incidencias { get; set; }

        [Required(ErrorMessage = "El Id del Área es obligatorio")]
        public int IdProceso { get; set; }

        public string Area { get; set; }  // Información adicional sobre el área
    }

}

