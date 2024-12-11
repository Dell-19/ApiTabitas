using WebTabitas.Models;
using WebTabitas.Repositorio.IRepositorio;

namespace WebTabitas.Repositorio
{
    public class MaquilaRepositorio : Repositorio<Maquila>, IMaquilaRepositorio
    {
        private readonly IHttpClientFactory _clientFactory;
        public MaquilaRepositorio(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }
    }
}
