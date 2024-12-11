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
    public class MaquilaServices : IMaquilaServices<MaquilaDTO, MaquilaInsertDTO, MaquilaUpdateDTO>
    {
        private IMaquilaRepository<Maquila> _MaquilaRepository;
        private IEtiquetaRepository<Etiqueta> _EtiquetaRepository;
        private IMapper _Mapper;

        public MaquilaServices(IMaquilaRepository<Maquila> maquilaRepository, IEtiquetaRepository<Etiqueta> etiquetaRepository,
                IMapper mapper)
            {
            _MaquilaRepository = maquilaRepository;
            _EtiquetaRepository = etiquetaRepository;
            _Mapper = mapper;
        }
            public async Task<MaquilaDTO> AddMaquila(MaquilaInsertDTO maquilaInsertDto, string baseUrl)
            {
            var existsInEtiqueta = await _EtiquetaRepository.AnyAsync(c => c.IdGeneral == maquilaInsertDto.IdGeneral);
            if (!existsInEtiqueta)
            {
                throw new Exception("El modelo con este IdGeneral no está registrado Etiquetas.");
            }
            var exists = await _MaquilaRepository.AnyAsync(a => a.IdGeneral == maquilaInsertDto.IdGeneral);
            if (exists)
            {
                throw new Exception("El modelo con este IdGeneral ya existe.");
            }
            var maquila = _Mapper.Map<Maquila>(maquilaInsertDto); 
            await _MaquilaRepository.AddMaquila(maquila);
            await _MaquilaRepository.Save();  
            var maquilaDto = _Mapper.Map<MaquilaDTO>(maquila);
            return maquilaDto;
        }
            public async Task<IEnumerable<MaquilaDTO>> GetMaquila()
            {
                var maquilas = await _MaquilaRepository.GetMaquila();
                return _Mapper.Map<IEnumerable<MaquilaDTO>>(maquilas);
        }
        public async Task<MaquilaDTO> GetMaquilaByNombre(string Modelo)
            {
                var maquila = await _MaquilaRepository.GetMaquilaByNombre(Modelo);
                if (maquila != null)
                {
                    var maquilaDto = _Mapper.Map<MaquilaDTO>(maquila);
                    return maquilaDto;
                }
                return null;
            }
            public async Task<MaquilaDTO> GetMaquilaById(int Idmaquila)
            {
                var maquila = await _MaquilaRepository.GetMaquilaById(Idmaquila);
                if (maquila != null)
                {
                    var maquilaDto = _Mapper.Map<MaquilaDTO>(maquila);
                    return maquilaDto;
                }
                return null;
            }
        public async Task<MaquilaDTO> UpdateMaquila(int idMaquila, MaquilaUpdateDTO maquilaUpdateDto)
        {
            var maquila = await _MaquilaRepository.GetMaquilaById(idMaquila);
            if (maquila == null)
            {
                return null;
            }
            if (maquila.FechaEditada == true)
            {
                throw new InvalidOperationException("No se puede modificar el registro más de una vez.");
            }
            maquila = _Mapper.Map<MaquilaUpdateDTO, Maquila>(maquilaUpdateDto, maquila);
                _MaquilaRepository.UpdateMaquila(maquila);
                await _MaquilaRepository.Save();
                var maquilaDto = _Mapper.Map<MaquilaDTO>(maquila);
                return maquilaDto;
        }
    }
}

