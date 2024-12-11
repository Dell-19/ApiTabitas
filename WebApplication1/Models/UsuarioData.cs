namespace WebTabitas.Models
{
    using Newtonsoft.Json;

    public class UsuarioData
    {
        [JsonProperty("idUsuario")]
        public string idUsuario { get; set; }

        [JsonProperty("nombreUsuario")]
        public string nombreUsuario { get; set; }

        [JsonProperty("nombre")]
        public string nombre { get; set; }
        [JsonProperty("role")]
        public string role { get; set; }
    }
}
