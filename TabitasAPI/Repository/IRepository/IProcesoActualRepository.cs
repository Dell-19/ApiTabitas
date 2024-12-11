using TabitasAPI.DTOs;
using TabitasAPI.Models;

namespace TabitasAPI.Repository.IRepository
{
    public interface IProcesoActualRepository<TEntity>
    {
        Task<IEnumerable<ProcesoActualDTO>> GetProcesoActual();
        //Task<bool> AnyAsync(Func<TEntity, bool> predicate);

    }
}
