using TabitasAPI.DTOs;
using TabitasAPI.Models;

namespace TabitasAPI.Repository.IRepository
{
    public interface ICorteRepository<TEntity>
    {
        Task<IEnumerable<CorteDTO>> GetCorte();
        Task<Corte> GetCorteById(int Id);
        Task<Corte> GetCorteByNombre(string Modelo);
        Task AddCorte(TEntity entity);
        void UpdateCorte(TEntity entity);
        Task Save();

        Task<bool> AnyAsync(Func<TEntity, bool> predicate);

    }
}
