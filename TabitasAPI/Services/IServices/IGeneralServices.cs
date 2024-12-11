namespace TabitasAPI.Services.IServices
{
    public interface IGeneralServices<To,Ti,Tu>
    {
        Task<IEnumerable<To>> GetGeneral();
        Task<To> GetGeneralByNombre(string Modelo);
        Task<To> GetGeneralById(int Idgeneral);
        Task<To> AddGeneral(Ti generalInsertDto,string baseUrl);
        Task<To> UpdateGeneral(int idGeneral, Tu generalUpdateDto);
    }
}
