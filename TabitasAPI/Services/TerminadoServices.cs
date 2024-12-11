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
    public class TerminadoServices : ITerminadoServices<TerminadoDTO, TerminadoInsertDTO, TerminadoUpdateDTO>
    {
        private ITerminadoRepository<Terminado> _TerminadoRepository;
        private ICalidadRepository<Calidad> _CalidadRepository;
        private IMapper _Mapper;

        public TerminadoServices(ITerminadoRepository<Terminado> terminadoRepository,
                ICalidadRepository<Calidad> calidadRepository,
                IMapper mapper)
            {
                _TerminadoRepository = terminadoRepository;
                _CalidadRepository = calidadRepository;
                _Mapper = mapper;
        }
            public async Task<TerminadoDTO> AddTerminado(TerminadoInsertDTO terminadoInsertDto, string baseUrl)
            {
            var existsInCalidad = await _CalidadRepository.AnyAsync(a => a.IdGeneral == terminadoInsertDto.IdGeneral);
            if (!existsInCalidad)
            {
                throw new Exception("El modelo con este IdGeneral no está registrado en Calidad.");
            }
            var exists = await _TerminadoRepository.AnyAsync(a => a.IdGeneral == terminadoInsertDto.IdGeneral);
            if (exists)
            {
                throw new Exception("El modelo con este IdGeneral ya existe.");
            }
            var terminado = _Mapper.Map<Terminado>(terminadoInsertDto); 
            await _TerminadoRepository.AddTerminado(terminado);
            await _TerminadoRepository.Save();  
            var terminadoDto = _Mapper.Map<TerminadoDTO>(terminado);
            return terminadoDto;
        }
            public async Task<IEnumerable<TerminadoDTO>> GetTerminado()
            {
                var terminados = await _TerminadoRepository.GetTerminado();
                return _Mapper.Map<IEnumerable<TerminadoDTO>>(terminados);
        }
        public async Task<TerminadoDTO> GetTerminadoByNombre(string Modelo)
            {
                var terminado = await _TerminadoRepository.GetTerminadoByNombre(Modelo);
                if (terminado != null)
                {
                    var terminadoDto = _Mapper.Map<TerminadoDTO>(terminado);
                    return terminadoDto;
                }
                return null;
            }
            public async Task<TerminadoDTO> GetTerminadoById(int Idterminado)
            {
                var terminado = await _TerminadoRepository.GetTerminadoById(Idterminado);
                if (terminado != null)
                {
                    var terminadoDto = _Mapper.Map<TerminadoDTO>(terminado);
                    return terminadoDto;
                }
                return null;
            }
        public async Task<TerminadoDTO> UpdateTerminado(int idTerminado, TerminadoUpdateDTO terminadoUpdateDto)
        {
            var terminado= await _TerminadoRepository.GetTerminadoById(idTerminado);
            if (terminado == null)
            {
                return null;
            }
            if (terminado.FechaEditada == true)
            {
                throw new InvalidOperationException("No se puede modificar el registro más de una vez.");
            }
            terminado = _Mapper.Map<TerminadoUpdateDTO, Terminado>(terminadoUpdateDto, terminado);
                _TerminadoRepository.UpdateTerminado(terminado);
                await _TerminadoRepository.Save();
                var terminadoDto = _Mapper.Map<TerminadoDTO>(terminado);
                return terminadoDto;
        }
    }
}

