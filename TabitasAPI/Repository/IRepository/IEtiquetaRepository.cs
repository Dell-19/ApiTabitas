using TabitasAPI.DTOs;
using TabitasAPI.Models;

namespace TabitasAPI.Repository.IRepository
{
    public interface IEtiquetaRepository<TEntity>
    {
        Task<IEnumerable<EtiquetaDTO>> GetEtiqueta();
        Task<Etiqueta> GetEtiquetaById(int Id);
        Task<Etiqueta> GetEtiquetaByNombre(string Modelo);
        Task AddEtiqueta(TEntity entity);
        void UpdateEtiqueta(TEntity entity);
        Task Save();

        Task<bool> AnyAsync(Func<TEntity, bool> predicate);

    }
}
