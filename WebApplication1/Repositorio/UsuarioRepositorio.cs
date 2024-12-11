using Newtonsoft.Json;
using System.Text;
using WebTabitas.Models;
using WebTabitas.Repositorio.IRepositorio;

namespace WebTabitas.Repositorio
{
    public class UsuarioRepositorio: Repositorio<UsuarioAuth>, IUsuarioRepositorio
    {
        private readonly IHttpClientFactory _clientFactory;
        public UsuarioRepositorio(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<UsuarioAuth> LoginAsync(string url, UsuarioAuth itemCrear)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            if (itemCrear != null)
            {
                request.Content = new StringContent(
                    JsonConvert.SerializeObject(itemCrear), Encoding.UTF8, "application/json");
            }
            else
            {
                return new UsuarioAuth();
            }

            var cliente = _clientFactory.CreateClient();
            HttpResponseMessage respuesta = await cliente.SendAsync(request);

            if (respuesta.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonString = await respuesta.Content.ReadAsStringAsync();
                var usuarioAuthRespuesta = JsonConvert.DeserializeObject<UsuarioAuthRespuesta>(jsonString);

                if (usuarioAuthRespuesta.IsSuccess && usuarioAuthRespuesta.Result != null)
                {
                    var usuarioAuth = new UsuarioAuth
                    {
                        Id = usuarioAuthRespuesta.Result.Usuarios.idUsuario, // Asegúrate de que esta propiedad sea correcta
                        NombreUsuario = usuarioAuthRespuesta.Result.Usuarios.nombreUsuario, // Cambia UserName por nombreUsuario
                        Nombre = usuarioAuthRespuesta.Result.Usuarios.nombre,
                        Token = usuarioAuthRespuesta.Result.Token
                    };
                    Console.WriteLine($"TokenRecibido:{usuarioAuth.Token}");
                    return usuarioAuth;
                }
                else
                {
                    Console.WriteLine($"Error: {string.Join(", ", usuarioAuthRespuesta.ErrorMessage)}");
                    return new UsuarioAuth();
                }
            }
            else
            {
                var errorContent = await respuesta.Content.ReadAsStringAsync();
                Console.WriteLine($"Error: {respuesta.StatusCode}-{errorContent}");
                return new UsuarioAuth();
            }
        }


        public async Task<bool> RegistroAsync(string url, UsuarioAuth itemCrear)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            if (itemCrear != null)
            {
                request.Content = new StringContent(
                    JsonConvert.SerializeObject(itemCrear), Encoding.UTF8, "aplication/json");
            }
            else
            {
                return false;
            }
            var cliente = _clientFactory.CreateClient();
            HttpResponseMessage respuesta = await cliente.SendAsync(request);
            if (respuesta.StatusCode == System.Net.HttpStatusCode.OK)
            {
              return true;
            }
            else
            {
                return false;
            }
        }
    }
}
