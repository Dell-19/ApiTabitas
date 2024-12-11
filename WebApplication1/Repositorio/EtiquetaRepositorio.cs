using WebTabitas.Models;
using WebTabitas.Repositorio.IRepositorio;

namespace WebTabitas.Repositorio
{
    public class EtiquetaRepositorio : Repositorio<Etiqueta>, IEtiquetaRepositorio
    {
        private readonly IHttpClientFactory _clientFactory;
        public EtiquetaRepositorio(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }
    }
}
