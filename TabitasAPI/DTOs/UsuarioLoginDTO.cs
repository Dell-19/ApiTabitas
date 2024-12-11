using System.ComponentModel.DataAnnotations;

namespace TabitasAPI.DTOs
{
    public class UsuarioLoginDTO
    {
        [Required(ErrorMessage = "El usuario es Obligatorio")]
        public string NombreUsuario { get; set; }
        [Required(ErrorMessage = "La contraseña es Obligatoria")]
        public string Password { get; set; }
    }
}
