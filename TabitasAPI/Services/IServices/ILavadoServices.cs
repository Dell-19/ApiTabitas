using TabitasAPI.DTOs;

namespace TabitasAPI.Services.IServices
{
    public interface ILavadoServices<To, Ti, Tu>
    {

        Task<IEnumerable<To>> GetLavado();
        Task<To> GetLavadoByNombre(string Modelo);
        Task<To> GetLavadoById(int IdLavado);
        Task<To> AddLavado(Ti lavadoInsertDto, string baseUrl);
        Task<To> UpdateLavado(int idLavado, Tu lavadoUpdateDto);
    }
}
