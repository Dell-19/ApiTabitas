using TabitasAPI.DTOs;
using TabitasAPI.Models;

namespace TabitasAPI.Repository.IRepository
{
    public interface IMaquilaRepository<TEntity>
    {
        Task<IEnumerable<MaquilaDTO>> GetMaquila();
        Task<Maquila> GetMaquilaById(int Id);
        Task<Maquila> GetMaquilaByNombre(string Modelo);
        Task AddMaquila(TEntity entity);
        void UpdateMaquila(TEntity entity);
        Task Save();

        Task<bool> AnyAsync(Func<TEntity, bool> predicate);

    }
}
