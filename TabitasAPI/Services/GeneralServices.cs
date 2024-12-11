using AutoMapper;
using TabitasAPI.DTOs;
using TabitasAPI.Models;
using TabitasAPI.Repository;
using TabitasAPI.Repository.IRepository;
using TabitasAPI.Services.IServices;

namespace TabitasAPI.Services
{  
    public class GeneralServices : IGeneralServices<GeneralDTO, GeneralInsertDTO, GeneralUpdateDTO>
    {
        private IGeneralRepository<General> _GeneralRepository;
        private IMapper _Mapper;
        public GeneralServices(IGeneralRepository<General> generalRepository,
            IMapper mapper)
        {
            _GeneralRepository = generalRepository;
            _Mapper = mapper;
        }
        public async Task<GeneralDTO> AddGeneral(GeneralInsertDTO generalInsertDto,string baseUrl)
        {
            var exists = await _GeneralRepository.AnyAsync(a => a.Modelo == generalInsertDto.Modelo);

            if (exists)
            {
                throw new Exception("El modelo ya existe.");
            }
            var general = _Mapper.Map<General>(generalInsertDto);
            if (generalInsertDto.Imagen != null)
            {
                string nombreDeArchivo = general.IdGeneral +
                    System.Guid.NewGuid().ToString() +
                    Path.GetExtension(generalInsertDto.Imagen.FileName);
                string rutaArchivo = @"wwwroot\ImagenesGeneral\" + nombreDeArchivo;
                string ubicacionDirecctorio = Path.Combine(Directory.GetCurrentDirectory(), rutaArchivo);

                FileInfo file = new FileInfo(ubicacionDirecctorio);
                if (file.Exists)
                {
                    file.Delete();
                }
                using (var filestream = new FileStream(ubicacionDirecctorio, FileMode.Create))
                {
                    await generalInsertDto.Imagen.CopyToAsync(filestream);
                } 
                general.RutaImagen =$"{baseUrl}/ImagenesGeneral/{nombreDeArchivo}";
                general.RutaImagenLocal = rutaArchivo;
            }
            else
            {
                general.RutaImagen = "https://placehold.co/600x400";
            }
            await _GeneralRepository.AddGeneral(general);
            await _GeneralRepository.Save();
            var generalDto = _Mapper.Map<GeneralDTO>(general);
            return generalDto;
        }
        public async Task<IEnumerable<GeneralDTO>> GetGeneral()
        {
            var general = await _GeneralRepository.GetGeneral();
            return _Mapper.Map<IEnumerable<GeneralDTO>>(general);
        }
        public async Task<GeneralDTO> GetGeneralByNombre(string Modelo)
        {
            var general = await _GeneralRepository.GetGeneralByNombre(Modelo);
            if(general != null)
            {
                var generalDto = _Mapper.Map<GeneralDTO>(general);
                return generalDto;
            }
            return null;
        }
        public async Task<GeneralDTO> GetGeneralById(int Idgeneral)
        {
            var general = await _GeneralRepository.GetGeneralById(Idgeneral);
            if (general != null)
            {
                var generalDto = _Mapper.Map<GeneralDTO>(general);
                return generalDto;
            }
            return null;
        }
        public async Task<GeneralDTO> UpdateGeneral(int idGeneral, GeneralUpdateDTO generalUpdateDto)
        {
            var general = await _GeneralRepository.GetGeneralById(idGeneral);
            if(general != null )
            {
                general = _Mapper.Map<GeneralUpdateDTO, General>(generalUpdateDto, general);

                _GeneralRepository.UpdateGeneral(general);
                await _GeneralRepository.Save();

                var generalDto = _Mapper.Map<GeneralDTO>(general);
                return generalDto;
            }
            return null;
        }
    }
}
