using TabitasAPI.DTOs;
using TabitasAPI.Models;

namespace TabitasAPI.Repository.IRepository
{
    public interface ITerminadoRepository<TEntity>
    {
        Task<IEnumerable<TerminadoDTO>> GetTerminado();
        Task<Terminado> GetTerminadoById(int Id);
        Task<Terminado> GetTerminadoByNombre(string Modelo);
        Task AddTerminado(TEntity entity);
        void UpdateTerminado(TEntity entity);
        Task Save();

        Task<bool> AnyAsync(Func<TEntity, bool> predicate);

    }
}
