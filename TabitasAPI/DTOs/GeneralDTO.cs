﻿namespace TabitasAPI.DTOs
{
    public class GeneralDTO
    {
        public int IdGeneral { get; set; }
        public string? RutaImagen { get; set; }
        public string? RutaImagenLocal { get; set; }
        public string Modelo { get; set; }
        public string Descripcion { get; set; }
        public string RangoTallas { get; set; }
        public string Temporadas { get; set; }
        public string Pt { get; set; }
        public string NumeroOrden { get; set; }
        public int CantidadRequerida { get; set; }
        public DateTime FechaRecepcion { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public string ProcesoActual { get; set; }
    }
}
