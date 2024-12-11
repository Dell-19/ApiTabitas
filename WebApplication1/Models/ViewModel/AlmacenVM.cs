using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebTabitas.Models.ViewModel
{
    public class AlmacenVM
    {
        public IEnumerable<SelectListItem> ListaGeneral { get; set; }
        public IEnumerable<SelectListItem> ListaAreas { get; set; }
        public General General { get; set; }
        public Almacen Almacen { get; set; }
        public Etiqueta Etiqueta { get; set; }
        public Corte Corte { get; set; }
        public CorteLaser CorteLaser { get; set; }
        public Bordado Bordado { get; set; }
        public Serigrafia Serigrafia { get; set; }
        public Maquila Maquila { get; set; }
        public Lavado Lavado { get; set; }
        public Calidad Calidad { get; set; }
        public Terminado Terminado { get; set; }
    }
}
