using TabitasAPI.DTOs;

namespace TabitasAPI.Services.IServices
{
    public interface IBordadoServices<To, Ti, Tu>
    {

        Task<IEnumerable<To>> GetBordado();
        Task<To> GetBordadoByNombre(string Modelo);
        Task<To> GetBordadoById(int IdBordado);
        Task<To> AddBordado(Ti bordadoInsertDto, string baseUrl);
        Task<To> UpdateBordado(int idBordado, Tu bordadoUpdateDto);
    }
}
