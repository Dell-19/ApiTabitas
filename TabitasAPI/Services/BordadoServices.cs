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
    public class BordadoServices : IBordadoServices<BordadoDTO, BordadoInsertDTO, BordadoUpdateDTO>
    {
        private IBordadoRepository<Bordado> _BordadoRepository;
        private IEtiquetaRepository<Etiqueta> _EtiquetaRepository;
        private IMapper _Mapper;

        public BordadoServices(IBordadoRepository<Bordado> bordadoRepository, ICorteRepository<Corte> corteRepository,
                IEtiquetaRepository<Etiqueta> etiquetaRepository,
                IMapper mapper)
            {
            _BordadoRepository = bordadoRepository;
            _EtiquetaRepository = etiquetaRepository;
            _Mapper = mapper;
        }
            public async Task<BordadoDTO> AddBordado(BordadoInsertDTO bordadoInsertDto, string baseUrl)
            {
            var existsInEtiqueta = await _EtiquetaRepository.AnyAsync(c => c.IdGeneral == bordadoInsertDto.IdGeneral);
            if (!existsInEtiqueta)
            {
                throw new Exception("El modelo con este IdGeneral no está registrado Etiquetas.");
            }
            var exists = await _BordadoRepository.AnyAsync(a => a.IdGeneral == bordadoInsertDto.IdGeneral);
            if (exists)
            {
                throw new Exception("El modelo con este IdGeneral ya existe.");
            }
            var bordado = _Mapper.Map<Bordado>(bordadoInsertDto); 
            await _BordadoRepository.AddBordado(bordado);
            await _BordadoRepository.Save();  
            var bordadoDto = _Mapper.Map<BordadoDTO>(bordado);
            return bordadoDto;
        }
            public async Task<IEnumerable<BordadoDTO>> GetBordado()
            {
                var bordados = await _BordadoRepository.GetBordado();
                return _Mapper.Map<IEnumerable<BordadoDTO>>(bordados);
        }
        public async Task<BordadoDTO> GetBordadoByNombre(string Modelo)
            {
                var bordado = await _BordadoRepository.GetBordadoByNombre(Modelo);
                if (bordado != null)
                {
                    var bordadoDto = _Mapper.Map<BordadoDTO>(bordado);
                    return bordadoDto;
                }
                return null;
            }
            public async Task<BordadoDTO> GetBordadoById(int Idbordado)
            {
                var bordado = await _BordadoRepository.GetBordadoById(Idbordado);
                if (bordado != null)
                {
                    var bordadoDto = _Mapper.Map<BordadoDTO>(bordado);
                    return bordadoDto;
                }
                return null;
            }
        public async Task<BordadoDTO> UpdateBordado(int idBordado, BordadoUpdateDTO bordadoUpdateDto)
        {
            var bordado = await _BordadoRepository.GetBordadoById(idBordado);
            if (bordado == null)
            {
                return null;
            }
            if (bordado.FechaEditada == true) 
                {
                    throw new InvalidOperationException("No se puede modificar el registro más de una vez.");
                }
                bordado = _Mapper.Map<BordadoUpdateDTO, Bordado>(bordadoUpdateDto, bordado);
                _BordadoRepository.UpdateBordado(bordado);
                await _BordadoRepository.Save();
                var bordadoDto = _Mapper.Map<BordadoDTO>(bordado);
                return bordadoDto;
        }
    }
}

