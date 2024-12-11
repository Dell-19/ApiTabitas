using TabitasAPI.DTOs;

namespace TabitasAPI.Services.IServices
{
    public interface IEtiquetaServices<To, Ti, Tu>
    {

        Task<IEnumerable<To>> GetEtiqueta();
        Task<To> GetEtiquetaByNombre(string Modelo);
        Task<To> GetEtiquetaById(int Idetiquetas);
        Task<To> AddEtiqueta(Ti etiquetaInsertDto, string baseUrl);
        Task<To> UpdateEtiqueta(int idEtiquetas, Tu etiquetaUpdateDto);
    }
}
