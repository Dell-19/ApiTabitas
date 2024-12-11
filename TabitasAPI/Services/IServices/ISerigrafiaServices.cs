using TabitasAPI.DTOs;

namespace TabitasAPI.Services.IServices
{
    public interface ISerigrafiaServices<To, Ti, Tu>
    {

        Task<IEnumerable<To>> GetSerigrafia();
        Task<To> GetSerigrafiaByNombre(string Modelo);
        Task<To> GetSerigrafiaById(int IdSerigrafia);
        Task<To> AddSerigrafia(Ti serigrafiaInsertDto, string baseUrl);
        Task<To> UpdateSerigrafia(int idSerigrafia, Tu SerigrafiaUpdateDto);
    }
}
