using WebTabitas.Models;
using WebTabitas.Repositorio.IRepositorio;

namespace WebTabitas.Repositorio
{
    public class CorteLaserRepositorio : Repositorio<CorteLaser>, ICorteLaserRepositorio
    {
        private readonly IHttpClientFactory _clientFactory;
        public CorteLaserRepositorio(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }
    }
}
