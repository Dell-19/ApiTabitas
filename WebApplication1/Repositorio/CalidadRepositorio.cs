using WebTabitas.Models;
using WebTabitas.Repositorio.IRepositorio;

namespace WebTabitas.Repositorio
{
    public class CalidadRepositorio : Repositorio<Calidad>, ICalidadRepositorio
    {
        private readonly IHttpClientFactory _clientFactory;
        public CalidadRepositorio(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }
    }
}
