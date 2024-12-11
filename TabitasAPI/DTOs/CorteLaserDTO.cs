namespace TabitasAPI.DTOs
{
    public class CorteLaserDTO
    {
        public int IdCorteLaser { get; set; }
        public int CantidadCortadas { get; set; }
        public string Encargado { get; set; }
        public DateTime FechaRecepcion { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public string Recibe { get; set; }
        public string? Incidencias { get; set; }
        // Datos Tabla General
        public string Modelo { get; set; }
        public string Area { get; set; }
    }
}
