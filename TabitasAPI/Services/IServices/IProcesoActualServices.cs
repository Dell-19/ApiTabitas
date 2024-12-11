using TabitasAPI.DTOs;

namespace TabitasAPI.Services.IServices
{
    public interface IProcesoActualServices<To>
    {

        Task<IEnumerable<To>> GetProcesoActual();
 
    }
}
