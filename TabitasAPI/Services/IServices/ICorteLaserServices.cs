using TabitasAPI.DTOs;

namespace TabitasAPI.Services.IServices
{
    public interface ICorteLaserServices<To, Ti, Tu>
    {

        Task<IEnumerable<To>> GetCorteLaser();
        Task<To> GetCorteLaserByNombre(string Modelo);
        Task<To> GetCorteLaserById(int IdCorteLaser);
        Task<To> AddCorteLaser(Ti corteLaserInsertDto, string baseUrl);
        Task<To> UpdateCorteLaser(int idCorteLaser, Tu corteLaserUpdateDto);
    }
}
