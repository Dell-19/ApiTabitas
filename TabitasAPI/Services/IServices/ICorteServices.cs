using TabitasAPI.DTOs;

namespace TabitasAPI.Services.IServices
{
    public interface ICorteServices<To, Ti, Tu>
    {

        Task<IEnumerable<To>> GetCorte();
        Task<To> GetCorteByNombre(string Modelo);
        Task<To> GetCorteById(int IdCorte);
        Task<To> AddCorte(Ti corteInsertDto, string baseUrl);
        Task<To> UpdateCorte(int idCorte, Tu corteUpdateDto);
    }
}
