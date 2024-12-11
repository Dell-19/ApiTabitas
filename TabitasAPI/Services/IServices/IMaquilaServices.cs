using TabitasAPI.DTOs;

namespace TabitasAPI.Services.IServices
{
    public interface IMaquilaServices<To, Ti, Tu>
    {

        Task<IEnumerable<To>> GetMaquila();
        Task<To> GetMaquilaByNombre(string Modelo);
        Task<To> GetMaquilaById(int IdMaquila);
        Task<To> AddMaquila(Ti MaquilaInsertDto, string baseUrl);
        Task<To> UpdateMaquila(int idMaquila, Tu maquilaUpdateDto);
    }
}
