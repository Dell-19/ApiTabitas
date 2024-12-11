using Newtonsoft.Json;
using System.Collections;
using System.Text;
using WebTabitas.Repositorio.IRepositorio;

namespace WebTabitas.Repositorio
{
    public class Repositorio<T> : IRepositorio<T> where T : class
    {
        private readonly IHttpClientFactory _clientFactory;
        public Repositorio(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public async Task<bool> ActualizarAsync(string url, T itemActualizar, string token = "")
        {
            var peticion = new HttpRequestMessage(HttpMethod.Put, url);
            if (itemActualizar != null)
            {
                peticion.Content = new StringContent(JsonConvert.SerializeObject(itemActualizar),
                    Encoding.UTF8, "application/json");
            }
            else { return false; }
            var cliente = _clientFactory.CreateClient();

            if (string.IsNullOrEmpty(token))
            {
                return false;
            }
            cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage respuesta = await cliente.SendAsync(peticion);
            if (respuesta.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return true;
            }
            else { return false; }
        }

        public async Task<IEnumerable> Buscar(string url, string nombre)
        {
            var peticion = new HttpRequestMessage(HttpMethod.Get, url + nombre);
            var cliente = _clientFactory.CreateClient();
            HttpResponseMessage respuesta = await cliente.SendAsync(peticion);
            if (respuesta.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonString = await respuesta.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<T>>(jsonString);
            }
            else { return null; }
        }

        public async Task<bool> CrearAsync(string url, T itemCrear, string token="")
        {
            var peticion = new HttpRequestMessage(HttpMethod.Post, url);
            var multipartContent = new MultipartFormDataContent();

            if (itemCrear != null)
            {
                foreach (var property in typeof(T).GetProperties())
                {
                    var value = property.GetValue(itemCrear);

                    if (value != null)
                    {
                        if (property.PropertyType == typeof(IFormFile))
                        {
                            var file = value as IFormFile;
                            if (file != null)
                            {
                                var streamContent = new StreamContent(file.OpenReadStream());
                                streamContent.Headers.ContentType =
                                    new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);
                                multipartContent.Add(streamContent, property.Name, file.FileName);
                            }
                        }
                        else
                        {
                            var stringContent = new StringContent(value.ToString());
                            multipartContent.Add(stringContent, property.Name);
                        }
                    }
                }
            }
            else
            {
                return false;
            }

            peticion.Content = multipartContent;
            var cliente = _clientFactory.CreateClient();
            if (string.IsNullOrEmpty(token))
            {
                return false;
            }
            cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            try
            {
                HttpResponseMessage respuesta = await cliente.SendAsync(peticion);

                return respuesta.StatusCode == System.Net.HttpStatusCode.Created;
            }
            catch (Exception)
            {
                return false; 
            }
        }



        public async Task<bool> CrearGeneralAsync(string url, T generalCrear,string token="")
        {
            var peticion = new HttpRequestMessage(HttpMethod.Post, url);
            var multipartContent = new MultipartFormDataContent();

            if (generalCrear != null)
            {
                foreach (var property in typeof(T).GetProperties())
                {
                    var value = property.GetValue(generalCrear);

                    if (value != null)
                    {
                        if (property.PropertyType == typeof(IFormFile))
                        {
                            var file = value as IFormFile;
                            if (file != null)
                            {
                                var streamContent = new StreamContent(file.OpenReadStream());
                                streamContent.Headers.ContentType =
                                    new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);
                                multipartContent.Add(streamContent, property.Name, file.FileName);
                            }
                        }
                        else
                        {
                            var stringContent = new StringContent(value.ToString());
                            multipartContent.Add(stringContent, property.Name);
                        }
                    }
                    else
                    {
                        var emptyContent = new StringContent(string.Empty);
                        multipartContent.Add(emptyContent, property.Name);
                    }
                }
            }
            else
            {
                return false;
            }
            peticion.Content = multipartContent;
            var cliente = _clientFactory.CreateClient();
            if (string.IsNullOrEmpty(token))
            {
                return false;
            }
            cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage respuesta = await cliente.SendAsync(peticion);

            if (respuesta.StatusCode == System.Net.HttpStatusCode.Created)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<T> GetAsync(string url, int Id)
        {
            var peticion = new HttpRequestMessage(HttpMethod.Get, url + Id);
            var cliente = _clientFactory.CreateClient();
            HttpResponseMessage respuesta = await cliente.SendAsync(peticion);
            if (respuesta.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonString = await respuesta.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(jsonString);
            }
            else { return null; }
        }

        public async Task<IEnumerable<T>> GetTodoAsync(string url)
        {
            var peticion = new HttpRequestMessage(HttpMethod.Get, url);
            var cliente = _clientFactory.CreateClient();
            HttpResponseMessage respuesta = await cliente.SendAsync(peticion);
            if (respuesta.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonString = await respuesta.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<T>>(jsonString);
            }
            else { return null; }
        }
    }
}
