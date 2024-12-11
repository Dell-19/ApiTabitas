using System.ComponentModel.DataAnnotations;

namespace TabitasAPI.DTOs
{
    public class UsuarioInsertDTO
    {
        [Required(ErrorMessage="El usuario es Obligatorio")]
        public string NombreUsuario { get; set; }
        [Required(ErrorMessage = "El nombre es Obligatorio")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "La contraseña es Obligatoria")]
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
