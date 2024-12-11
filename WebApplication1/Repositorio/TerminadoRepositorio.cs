using WebTabitas.Models;
using WebTabitas.Repositorio.IRepositorio;

namespace WebTabitas.Repositorio
{
    public class TerminadoRepositorio : Repositorio<Terminado>, ITerminadoRepositorio
    {
        private readonly IHttpClientFactory _clientFactory;
        public TerminadoRepositorio(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }
    }
}
