using WebTabitas.Models;
using WebTabitas.Repositorio.IRepositorio;

namespace WebTabitas.Repositorio
{
    public class CorteRepositorio : Repositorio<Corte>, ICorteRepositorio
    {
        private readonly IHttpClientFactory _clientFactory;
        public CorteRepositorio(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }
    }
}
