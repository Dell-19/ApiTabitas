using WebTabitas.Models;
using WebTabitas.Repositorio.IRepositorio;

namespace WebTabitas.Repositorio
{
    public class GeneralRepositorio: Repositorio<General>, IGeneralRepositorio
    {
        private readonly IHttpClientFactory _clientFactory;
        public GeneralRepositorio(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }
    }
}
