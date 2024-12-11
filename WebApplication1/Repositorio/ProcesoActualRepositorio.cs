using WebTabitas.Models;
using WebTabitas.Repositorio.IRepositorio;

namespace WebTabitas.Repositorio
{
    public class ProcesoActualRepositorio : Repositorio<ProcesoActual>, IProcesoActualRepositorio
    {
        private readonly IHttpClientFactory _clientFactory;
        public ProcesoActualRepositorio(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }
    }
}
