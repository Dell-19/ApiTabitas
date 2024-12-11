using TabitasAPI.DTOs;

namespace TabitasAPI.Services.IServices
{
    public interface ITerminadoServices<To, Ti, Tu>
    {

        Task<IEnumerable<To>> GetTerminado();
        Task<To> GetTerminadoByNombre(string Modelo);
        Task<To> GetTerminadoById(int IdTerminado);
        Task<To> AddTerminado(Ti terminadoInsertDto, string baseUrl);
        Task<To> UpdateTerminado(int idTerminado, Tu terminadoUpdateDto);
    }
}
