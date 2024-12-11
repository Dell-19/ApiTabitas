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
    public class LavadoServices : ILavadoServices<LavadoDTO, LavadoInsertDTO, LavadoUpdateDTO>
    {
        private ILavadoRepository<Lavado> _LavadoRepository;
        private IEtiquetaRepository<Etiqueta> _EtiquetaRepository;
        private IMapper _Mapper;

        public LavadoServices(ILavadoRepository<Lavado> lavadoRepository, IEtiquetaRepository<Etiqueta> etiquetaRepository,
                IMapper mapper)
            {
            _LavadoRepository = lavadoRepository;
            _EtiquetaRepository = etiquetaRepository;
            _Mapper = mapper;
        }
            public async Task<LavadoDTO> AddLavado(LavadoInsertDTO lavadoInsertDto, string baseUrl)
            {
            var existsInEtiqueta = await _EtiquetaRepository.AnyAsync(c => c.IdGeneral == lavadoInsertDto.IdGeneral);
            if (!existsInEtiqueta)
            {
                throw new Exception("El modelo con este IdGeneral no está registrado Etiquetas.");
            }
            var exists = await _LavadoRepository.AnyAsync(a => a.IdGeneral == lavadoInsertDto.IdGeneral);
            if (exists)
            {
                throw new Exception("El modelo con este IdGeneral ya existe.");
            }
            var lavado = _Mapper.Map<Lavado>(lavadoInsertDto); 
            await _LavadoRepository.AddLavado(lavado);
            await _LavadoRepository.Save();  
            var lavadoDto = _Mapper.Map<LavadoDTO>(lavado);
            return lavadoDto;
        }
            public async Task<IEnumerable<LavadoDTO>> GetLavado()
            {
                var lavados = await _LavadoRepository.GetLavado();
                return _Mapper.Map<IEnumerable<LavadoDTO>>(lavados);
        }
        public async Task<LavadoDTO> GetLavadoByNombre(string Modelo)
            {
                var lavado = await _LavadoRepository.GetLavadoByNombre(Modelo);
                if (lavado != null)
                {
                    var lavadoDto = _Mapper.Map<LavadoDTO>(lavado);
                    return lavadoDto;
                }
                return null;
            }
            public async Task<LavadoDTO> GetLavadoById(int Idlavado)
            {
                var lavado = await _LavadoRepository.GetLavadoById(Idlavado);
                if (lavado != null)
                {
                    var lavadoDto = _Mapper.Map<LavadoDTO>(lavado);
                    return lavadoDto;
                }
                return null;
            }
        public async Task<LavadoDTO> UpdateLavado(int idLavado, LavadoUpdateDTO lavadoUpdateDto)
        {
            var lavado = await _LavadoRepository.GetLavadoById(idLavado);
            if (lavado == null)
            {
                return null;
            }
            if (lavado.FechaEditada == true)
            {
                throw new InvalidOperationException("No se puede modificar el registro más de una vez.");
            }
            lavado = _Mapper.Map<LavadoUpdateDTO, Lavado>(lavadoUpdateDto, lavado);
                _LavadoRepository.UpdateLavado(lavado);
                await _LavadoRepository.Save();
                var lavadoDto = _Mapper.Map<LavadoDTO>(lavado);
                return lavadoDto;
        }
    }
}

