using System.Collections;
using System.Text;

namespace WebTabitas.Repositorio.IRepositorio
{
    public interface IRepositorio<T> where T : class
    {
        Task<IEnumerable<T>> GetTodoAsync(string url);
        Task<T> GetAsync(string url, int Id);
        Task<IEnumerable> Buscar(string url, string nombre);
        Task<bool> CrearAsync(string url, T itemCrear, string token );
        Task<bool> CrearGeneralAsync(string url, T generalCrear, string token);
        Task<bool> ActualizarAsync(string url, T itemActualizar, string token);
    }
}
