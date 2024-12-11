using TabitasAPI.DTOs;
using TabitasAPI.Models;

namespace TabitasAPI.Repository.IRepository
{
    public interface ICalidadRepository<TEntity>
    {
        Task<IEnumerable<CalidadDTO>> GetCalidad();
        Task<Calidad> GetCalidadById(int Id);
        Task<Calidad> GetCalidadByNombre(string Modelo);
        Task AddCalidad(TEntity entity);
        void UpdateCalidad(TEntity entity);
        Task Save();

        Task<bool> AnyAsync(Func<TEntity, bool> predicate);

    }
}
