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
    public class CorteLaserServices : ICorteLaserServices<CorteLaserDTO, CorteLaserInsertDTO, CorteLaserUpdateDTO>
    {
        private ICorteLaserRepository<CorteLaser> _CorteLaserRepository;
        private IAlmacenRepository<Almacen> _AlmacenRepository;
        private IMapper _Mapper;

        public CorteLaserServices(ICorteLaserRepository<CorteLaser> corteLaserRepository, IAlmacenRepository<Almacen> almacenRepository,
                IMapper mapper)
            {
            _CorteLaserRepository = corteLaserRepository;
            _AlmacenRepository = almacenRepository;
            _Mapper = mapper;
        }
            public async Task<CorteLaserDTO> AddCorteLaser(CorteLaserInsertDTO corteLaserInsertDto, string baseUrl)
            {
            var existsInAlmacen = await _AlmacenRepository.AnyAsync(a => a.IdGeneral == corteLaserInsertDto.IdGeneral);
            if (!existsInAlmacen)
            {
                throw new Exception("El modelo con este IdGeneral no está registrado en Almacen.");
            }
            var exists = await _CorteLaserRepository.AnyAsync(a => a.IdGeneral == corteLaserInsertDto.IdGeneral);
            if (exists)
            {
                throw new Exception("El modelo con este IdGeneral ya existe.");
            }
            var corteLaser = _Mapper.Map<CorteLaser>(corteLaserInsertDto); 
            await _CorteLaserRepository.AddCorteLaser(corteLaser);
            await _CorteLaserRepository.Save();  
            var corteLaserDto = _Mapper.Map<CorteLaserDTO>(corteLaser);
            return corteLaserDto;
        }
            public async Task<IEnumerable<CorteLaserDTO>> GetCorteLaser()
            {
                var corteLaseres = await _CorteLaserRepository.GetCorteLaser();
                return _Mapper.Map<IEnumerable<CorteLaserDTO>>(corteLaseres);
        }
        public async Task<CorteLaserDTO> GetCorteLaserByNombre(string Modelo)
            {
                var corteLaser = await _CorteLaserRepository.GetCorteLaserByNombre(Modelo);
                if (corteLaser != null)
                {
                    var corteLaserDto = _Mapper.Map<CorteLaserDTO>(corteLaser);
                    return corteLaserDto;
                }
                return null;
            }
            public async Task<CorteLaserDTO> GetCorteLaserById(int IdcorteLaser)
            {
                var corteLaser = await _CorteLaserRepository.GetCorteLaserById(IdcorteLaser);
                if (corteLaser != null)
                {
                    var corteLaserDto = _Mapper.Map<CorteLaserDTO>(corteLaser);
                    return corteLaserDto;
                }
                return null;
            }
        public async Task<CorteLaserDTO> UpdateCorteLaser(int idCorteLaser, CorteLaserUpdateDTO corteLaserUpdateDto)
        {
            var corteLaser = await _CorteLaserRepository.GetCorteLaserById(idCorteLaser);
            if (corteLaser == null)
            {
                return null;
            }
            if (corteLaser.FechaEditada == true)
            {
                throw new InvalidOperationException("No se puede modificar el registro más de una vez.");
            }
            corteLaser = _Mapper.Map<CorteLaserUpdateDTO, CorteLaser>(corteLaserUpdateDto, corteLaser);
                _CorteLaserRepository.UpdateCorteLaser(corteLaser);
                await _CorteLaserRepository.Save();
                var corteLaserDto = _Mapper.Map<CorteLaserDTO>(corteLaser);
                return corteLaserDto;
        }
    }
}

