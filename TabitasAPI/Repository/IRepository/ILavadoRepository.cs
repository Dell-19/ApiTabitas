using TabitasAPI.DTOs;
using TabitasAPI.Models;

namespace TabitasAPI.Repository.IRepository
{
    public interface ILavadoRepository<TEntity>
    {
        Task<IEnumerable<LavadoDTO>> GetLavado();
        Task<Lavado> GetLavadoById(int Id);
        Task<Lavado> GetLavadoByNombre(string Modelo);
        Task AddLavado(TEntity entity);
        void UpdateLavado(TEntity entity);
        Task Save();

        Task<bool> AnyAsync(Func<TEntity, bool> predicate);

    }
}
