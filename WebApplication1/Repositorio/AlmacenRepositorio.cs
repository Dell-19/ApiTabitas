using WebTabitas.Models;
using WebTabitas.Repositorio.IRepositorio;

namespace WebTabitas.Repositorio
{
    public class AlmacenRepositorio: Repositorio<Almacen>, IAlmacenRepositorio
    {
        private readonly IHttpClientFactory _clientFactory;
        public AlmacenRepositorio(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }
    }
}
