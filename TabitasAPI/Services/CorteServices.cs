using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TabitasAPI.DTOs;
using TabitasAPI.Models;
using TabitasAPI.Repository;
using TabitasAPI.Repository.IRepository;
using TabitasAPI.Services.IServices;
using XAct;

namespace TabitasAPI.Services
{
    public class CorteServices : ICorteServices<CorteDTO, CorteInsertDTO, CorteUpdateDTO>
    {
        private ICorteRepository<Corte> _CorteRepository;
        private IAlmacenRepository<Almacen> _AlmacenRepository;
        private IMapper _Mapper;

        public CorteServices(ICorteRepository<Corte> corteRepository, IAlmacenRepository<Almacen> almacenRepository,
                IMapper mapper)
        {
            _CorteRepository = corteRepository;
            _AlmacenRepository = almacenRepository;
            _Mapper = mapper;
        }
            public async Task<CorteDTO> AddCorte(CorteInsertDTO corteInsertDto, string baseUrl)
            {
            var existsInAlmacen = await _AlmacenRepository.AnyAsync(a => a.IdGeneral == corteInsertDto.IdGeneral);
            if (!existsInAlmacen)
            {
                throw new Exception("El modelo con este IdGeneral no está registrado en Almacen.");
            }
            var exists = await _CorteRepository.AnyAsync(a => a.IdGeneral == corteInsertDto.IdGeneral);
            if (exists)
            {
                throw new Exception("El modelo con este IdGeneral ya existe.");
            }
            var corte = _Mapper.Map<Corte>(corteInsertDto);
            if (corteInsertDto.Imagen != null)
            {
                string nombreDeArchivo = corte.IdCorte +
                    System.Guid.NewGuid().ToString() +
                    Path.GetExtension(corteInsertDto.Imagen.FileName);
                string rutaArchivo = @"wwwroot\ImagenesCorte\" + nombreDeArchivo;
                string ubicacionDirecctorio = Path.Combine(Directory.GetCurrentDirectory(), rutaArchivo);

                FileInfo file = new FileInfo(ubicacionDirecctorio);
                if (file.Exists)
                {
                    file.Delete();
                }
                using (var filestream = new FileStream(ubicacionDirecctorio, FileMode.Create))
                {
                    await corteInsertDto.Imagen.CopyToAsync(filestream);
                }
                corte.RutaImagen = $"{baseUrl}/ImagenesCorte/{nombreDeArchivo}";
                corte.RutaImagenLocal = rutaArchivo;
            }
            else
            {
                corte.RutaImagen = "https://placehold.co/600x400";
            }
            await _CorteRepository.AddCorte(corte);
            await _CorteRepository.Save();  
            var corteDto = _Mapper.Map<CorteDTO>(corte);
            return corteDto;
            }
                public async Task<IEnumerable<CorteDTO>> GetCorte()
            {
                var cortes = await _CorteRepository.GetCorte();
                return _Mapper.Map<IEnumerable<CorteDTO>>(cortes);
        }
        public async Task<CorteDTO> GetCorteByNombre(string Modelo)
            {
                var corte = await _CorteRepository.GetCorteByNombre(Modelo);
                if (corte != null)
                {
                    var corteDto = _Mapper.Map<CorteDTO>(corte);
                    return corteDto;
                }
                return null;
            }
            public async Task<CorteDTO> GetCorteById(int Idcorte)
            {
                var corte = await _CorteRepository.GetCorteById(Idcorte);
                if (corte != null)
                {
                    var corteDto = _Mapper.Map<CorteDTO>(corte);
                    return corteDto;
                }
                return null;
            }
        public async Task<CorteDTO> UpdateCorte(int idCorte, CorteUpdateDTO corteUpdateDto)
        {
            var corte= await _CorteRepository.GetCorteById(idCorte);
            if (corte == null)
            {
                return null;
            }
            if (corte.FechaEdita == true)
            {
                throw new InvalidOperationException("No se puede modificar el registro más de una vez.");
            }
            corte = _Mapper.Map<CorteUpdateDTO, Corte>(corteUpdateDto, corte);
                _CorteRepository.UpdateCorte(corte);
                await _CorteRepository.Save();
                var corteDto = _Mapper.Map<CorteDTO>(corte);
                return corteDto;
        }
    }
}

