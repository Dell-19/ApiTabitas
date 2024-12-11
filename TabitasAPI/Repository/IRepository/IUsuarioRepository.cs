using TabitasAPI.DTOs;
using TabitasAPI.Models;

namespace TabitasAPI.Repository.IRepository
{
    public interface IUsuarioRepository
    {
        ICollection<Usuarios> GetUsuarios();
        Usuarios GetUsuario(int idUsuario);
        bool IsUniqueUser(string usuario);
        Task<UsuarioLoginRespuestaDTO> Login(UsuarioLoginDTO usuarioLoginDTO);
        Task<Usuarios> Registro(UsuarioInsertDTO usuarioInsertDTO);
    }
}
