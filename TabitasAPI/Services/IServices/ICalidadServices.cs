using TabitasAPI.DTOs;

namespace TabitasAPI.Services.IServices
{
    public interface ICalidadServices<To, Ti, Tu>
    {

        Task<IEnumerable<To>> GetCalidad();
        Task<To> GetCalidadByNombre(string Modelo);
        Task<To> GetCalidadById(int IdCalidad);
        Task<To> AddCalidad(Ti calidadInsertDto, string baseUrl);
        Task<To> UpdateCalidad(int idCalidad, Tu calidadUpdateDto);
    }
}
