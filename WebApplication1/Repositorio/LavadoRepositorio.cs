using WebTabitas.Models;
using WebTabitas.Repositorio.IRepositorio;

namespace WebTabitas.Repositorio
{
    public class LavadoRepositorio : Repositorio<Lavado>, ILavadoRepositorio
    {
        private readonly IHttpClientFactory _clientFactory;
        public LavadoRepositorio(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }
    }
}
