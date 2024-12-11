
using WebTabitas.Models;

namespace WebTabitas.Repositorio.IRepositorio
{
    public interface IUsuarioRepositorio:IRepositorio<UsuarioAuth>
    {
        Task <UsuarioAuth> LoginAsync(string url , UsuarioAuth itemCrear);
        Task<bool> RegistroAsync(string url, UsuarioAuth itemCrear);
    }
}
