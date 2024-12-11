using Newtonsoft.Json;

namespace WebTabitas.Models
{
    public class ResultData
    {
        [JsonProperty("usuarios")]
        public UsuarioData Usuarios { get; set; }

        [JsonProperty("role")]
        public string Role { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
