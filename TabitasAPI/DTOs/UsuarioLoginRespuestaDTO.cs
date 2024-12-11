using TabitasAPI.Models;

namespace TabitasAPI.DTOs
{
    public class UsuarioLoginRespuestaDTO
    {
        public Usuarios Usuarios { get; set; }
        public string role { get; set; }
        public string token { get; set; }
    }
}
