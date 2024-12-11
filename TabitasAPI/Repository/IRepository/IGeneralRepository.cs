using TabitasAPI.Models;

namespace TabitasAPI.Repository.IRepository
{
    public interface IGeneralRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> GetGeneral();
        Task<General> GetGeneralByNombre(string Modelo);
        Task<General> GetGeneralById(int Id);
        Task AddGeneral(TEntity entity);
        void UpdateGeneral(TEntity entity);
        Task Save();
        Task<bool> AnyAsync(Func<TEntity, bool> predicate);
    }
}
