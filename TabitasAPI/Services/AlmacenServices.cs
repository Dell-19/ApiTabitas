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
    // Implementación del servicio para Almacen
    public class AlmacenServices : IAlmacenServices<AlmacenDTO, AlmacenInsertDTO, AlmacenUpdateDTO>
    {
            private IAlmacenRepository<Almacen> _AlmacenRepository;
            private IMapper _Mapper;

        public AlmacenServices(IAlmacenRepository<Almacen> almacenRepository,
                IMapper mapper)
            {
                _AlmacenRepository = almacenRepository;
                _Mapper = mapper;
        }
            public async Task<AlmacenDTO> AddAlmacen(AlmacenInsertDTO almacenInsertDto, string baseUrl)
            {
            var exists = await _AlmacenRepository.AnyAsync(a => a.IdGeneral == almacenInsertDto.IdGeneral);

            if (exists)
            {
                throw new Exception("El modelo ya existe.");
            }
            var almacen = _Mapper.Map<Almacen>(almacenInsertDto); 

            await _AlmacenRepository.AddAlmacen(almacen);
            await _AlmacenRepository.Save();  
            var almacenDto = _Mapper.Map<AlmacenDTO>(almacen);
            return almacenDto;
        }

            public async Task<IEnumerable<AlmacenDTO>> GetAlmacen()
            {
                var almacenes = await _AlmacenRepository.GetAlmacen(); // Obtener todos los registros de Almacen
                return _Mapper.Map<IEnumerable<AlmacenDTO>>(almacenes); // Mapear la lista a AlmacenDTO
        }
        public async Task<AlmacenDTO> GetAlmacenByNombre(string Modelo)
            {
                var almacen = await _AlmacenRepository.GetAlmacenByNombre(Modelo);
                if (almacen != null)
                {
                    var almacenDto = _Mapper.Map<AlmacenDTO>(almacen);
                    return almacenDto;
                }
                return null;
            }
            public async Task<AlmacenDTO> GetAlmacenById(int Idalmacen)
            {
                var almacen = await _AlmacenRepository.GetAlmacenById(Idalmacen);
                if (almacen != null)
                {
                    var almacenDto = _Mapper.Map<AlmacenDTO>(almacen);
                    return almacenDto;
                }
                return null;
            }

        public async Task<AlmacenDTO> UpdateAlmacen(int idAlmacen, AlmacenUpdateDTO almacenUpdateDto)
        {
            var almacen = await _AlmacenRepository.GetAlmacenById(idAlmacen);
            if (almacen == null)
            {
                return null;
            }
            almacen = _Mapper.Map<AlmacenUpdateDTO, Almacen>(almacenUpdateDto, almacen);
            _AlmacenRepository.UpdateAlmacen(almacen);
            await _AlmacenRepository.Save();
            var almacenDto = _Mapper.Map<AlmacenDTO>(almacen);
            return almacenDto;
        }
    }
}

