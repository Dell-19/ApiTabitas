using TabitasAPI.DTOs;
using TabitasAPI.Models;

namespace TabitasAPI.Repository.IRepository
{
    public interface IBordadoRepository<TEntity>
    {
        Task<IEnumerable<BordadoDTO>> GetBordado();
        Task<Bordado> GetBordadoById(int Id);
        Task<Bordado> GetBordadoByNombre(string Modelo);
        Task AddBordado(TEntity entity);
        void UpdateBordado(TEntity entity);
        Task Save();

        Task<bool> AnyAsync(Func<TEntity, bool> predicate);

    }
}
