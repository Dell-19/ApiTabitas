using TabitasAPI.DTOs;
using TabitasAPI.Models;

namespace TabitasAPI.Repository.IRepository
{
    public interface ISerigrafiaRepository<TEntity>
    {
        Task<IEnumerable<SerigrafiaDTO>> GetSerigrafia();
        Task<Serigrafia> GetSerigrafiaById(int Id);
        Task<Serigrafia> GetSerigrafiaByNombre(string Modelo);
        Task AddSerigrafia(TEntity entity);
        void UpdateSerigrafia(TEntity entity);
        Task Save();

        Task<bool> AnyAsync(Func<TEntity, bool> predicate);

    }
}
