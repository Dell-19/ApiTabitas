using WebTabitas.Models;
using WebTabitas.Repositorio.IRepositorio;

namespace WebTabitas.Repositorio
{
    public class BordadoRepositorio : Repositorio<Bordado>, IBordadoRepositorio
    {
        private readonly IHttpClientFactory _clientFactory;
        public BordadoRepositorio(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }
    }
}
