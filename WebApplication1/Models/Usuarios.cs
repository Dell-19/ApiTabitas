using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace WebTabitas.Models
{
    public class Usuarios
    {
        [JsonProperty("idUsuario")] // Mapea "idUsuario" de la API a "idUsuario" en el front
        public int IdUsuario { get; set; }

        [JsonProperty("nombreUsuario")] // Mapea "nombreUsuario" a "NombreUsuario"
        public string NombreUsuario { get; set; }

        [JsonProperty("nombre")] // Mapea "nombre" a "Nombre"
        public string Nombre { get; set; }

        [JsonProperty("password")] // Mapea "password" a "Password"
        public string Password { get; set; }

        [JsonProperty("role")] // Mapea "role" a "Role"
        public string Role { get; set; }
    }
}