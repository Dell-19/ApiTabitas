using Newtonsoft.Json;

namespace WebTabitas.Models
{
    public class UsuarioAuth
    {
        [JsonProperty("idUsuario")]
        public string Id { get; set; }

        [JsonProperty("nombreUsuario")]
        public string NombreUsuario { get; set; }

        [JsonProperty("nombre")]
        public string Nombre { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
