using WebTabitas.Models;
using WebTabitas.Repositorio.IRepositorio;

namespace WebTabitas.Repositorio
{
    public class SerigrafiaRepositorio : Repositorio<Serigrafia>, ISerigrafiaRepositorio
    {
        private readonly IHttpClientFactory _clientFactory;
        public SerigrafiaRepositorio(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }
    }
}
