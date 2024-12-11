using TabitasAPI.DTOs;

namespace TabitasAPI.Services.IServices
{
    public interface IAlmacenServices<To, Ti, Tu>
    {

        Task<IEnumerable<To>> GetAlmacen();
        Task<To> GetAlmacenByNombre(string Modelo);
        Task<To> GetAlmacenById(int Idalmacen);
        Task<To> AddAlmacen(Ti almacenInsertDto, string baseUrl);
        Task<To> UpdateAlmacen(int idAlmacen, Tu almacenUpdateDto);
    }
}
