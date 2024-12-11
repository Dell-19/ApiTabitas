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
    public class CalidadServices : ICalidadServices<CalidadDTO, CalidadInsertDTO, CalidadUpdateDTO>
    {
        private ICalidadRepository<Calidad> _CalidadRepository;
        private ILavadoRepository<Lavado> _LavadoRepository;
        private IMaquilaRepository<Maquila> _MaquilaRepository;
        private IBordadoRepository<Bordado> _BordadoRepository;
        private ISerigrafiaRepository<Serigrafia> _SerigrafiaRepository;
        private IMapper _Mapper;

        public CalidadServices(ICalidadRepository<Calidad> calidadRepository, 
            ILavadoRepository<Lavado> lavadoRepository,
            IMaquilaRepository<Maquila> maquilaRepository,
            IBordadoRepository<Bordado> bordadoRepository,
            ISerigrafiaRepository<Serigrafia> serigrafiaRepository,
                IMapper mapper)
            {
            _CalidadRepository = calidadRepository;
            _LavadoRepository = lavadoRepository;
            _MaquilaRepository = maquilaRepository;
            _BordadoRepository = bordadoRepository;
            _SerigrafiaRepository = serigrafiaRepository;
            _Mapper = mapper;
        }
            public async Task<CalidadDTO> AddCalidad(CalidadInsertDTO calidadInsertDto, string baseUrl)
            {
            var existsInLavado = await _LavadoRepository.AnyAsync(a => a.IdGeneral == calidadInsertDto.IdGeneral);
            var existsInMaquila = await _MaquilaRepository.AnyAsync(a => a.IdGeneral == calidadInsertDto.IdGeneral);
            var existsInBordado = await _BordadoRepository.AnyAsync(a => a.IdGeneral == calidadInsertDto.IdGeneral);
            var existsInSerigrafia = await _SerigrafiaRepository.AnyAsync(a => a.IdGeneral == calidadInsertDto.IdGeneral);
            if (!existsInLavado && !existsInMaquila && !existsInBordado && !existsInSerigrafia)
            {
                throw new Exception("El modelo con este IdGeneral no está registrado en un proceso anterior .");
            }
            var exists = await _CalidadRepository.AnyAsync(a => a.IdGeneral == calidadInsertDto.IdGeneral);
            if (exists)
            {
                throw new Exception("El modelo con este IdGeneral ya existe.");
            }
            var calidad = _Mapper.Map<Calidad>(calidadInsertDto); 
            await _CalidadRepository.AddCalidad(calidad);
            await _CalidadRepository.Save();  
            var calidadDto = _Mapper.Map<CalidadDTO>(calidad);
            return calidadDto;
        }
            public async Task<IEnumerable<CalidadDTO>> GetCalidad()
            {
                var cortes = await _CalidadRepository.GetCalidad();
                return _Mapper.Map<IEnumerable<CalidadDTO>>(cortes);
        }
        public async Task<CalidadDTO> GetCalidadByNombre(string Modelo)
            {
                var calidad = await _CalidadRepository.GetCalidadByNombre(Modelo);
                if (calidad != null)
                {
                    var calidadDto = _Mapper.Map<CalidadDTO>(calidad);
                    return calidadDto;
                }
                return null;
            }
            public async Task<CalidadDTO> GetCalidadById(int Idcalidad)
            {
                var calidad = await _CalidadRepository.GetCalidadById(Idcalidad);
                if (calidad != null)
                {
                    var calidadDto = _Mapper.Map<CalidadDTO>(calidad);
                    return calidadDto;
                }
                return null;
            }
        public async Task<CalidadDTO> UpdateCalidad(int idCalidad, CalidadUpdateDTO calidadUpdateDto)
        {
            var calidad = await _CalidadRepository.GetCalidadById(idCalidad);
            if (calidad == null)
            {
                return null;
            }
            if (calidad.FechaEditada == true)
            {
                throw new InvalidOperationException("No se puede modificar el registro más de una vez.");
            }
                calidad = _Mapper.Map<CalidadUpdateDTO, Calidad>(calidadUpdateDto, calidad);
                _CalidadRepository.UpdateCalidad(calidad);
                await _CalidadRepository.Save();
                var calidadDto = _Mapper.Map<CalidadDTO>(calidad);
                return calidadDto;
        }
    }
}

