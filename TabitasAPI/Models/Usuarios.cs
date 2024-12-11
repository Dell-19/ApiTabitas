using System.ComponentModel.DataAnnotations;

namespace TabitasAPI.Models
{
    public class Usuarios
    {
        [Key]
        public int idUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string Nombre { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}