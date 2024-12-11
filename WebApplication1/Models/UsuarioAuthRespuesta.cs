using Newtonsoft.Json;

namespace WebTabitas.Models
{
    public class UsuarioAuthRespuesta
    {
        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }

        [JsonProperty("isSuccess")]
        public bool IsSuccess { get; set; }

        [JsonProperty("errorMessage")]
        public List<string> ErrorMessage { get; set; }

        [JsonProperty("result")]
        public ResultData Result { get; set; }
    }
}
