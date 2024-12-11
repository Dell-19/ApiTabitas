using TabitasAPI.DTOs;
using TabitasAPI.Models;

namespace TabitasAPI.Repository.IRepository
{
    public interface ICorteLaserRepository<TEntity>
    {
        Task<IEnumerable<CorteLaserDTO>> GetCorteLaser();
        Task<CorteLaser> GetCorteLaserById(int Id);
        Task<CorteLaser> GetCorteLaserByNombre(string Modelo);
        Task AddCorteLaser(TEntity entity);
        void UpdateCorteLaser(TEntity entity);
        Task Save();

        Task<bool> AnyAsync(Func<TEntity, bool> predicate);

    }
}
