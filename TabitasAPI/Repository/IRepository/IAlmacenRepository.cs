using TabitasAPI.DTOs;
using TabitasAPI.Models;

namespace TabitasAPI.Repository.IRepository
{
    public interface IAlmacenRepository<TEntity>
    {
        Task<IEnumerable<AlmacenDTO>> GetAlmacen();
        Task<Almacen> GetAlmacenById(int Id);
        Task<Almacen> GetAlmacenByNombre(string Modelo);
        Task AddAlmacen(TEntity entity);
        void UpdateAlmacen(TEntity entity);
        Task Save();

        Task<bool> AnyAsync(Func<TEntity, bool> predicate);

    }
}
