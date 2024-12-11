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
    public class EtiquetaServices : IEtiquetaServices<EtiquetaDTO, EtiquetaInsertDTO, EtiquetaUpdateDTO>
    {
        private IEtiquetaRepository<Etiqueta> _EtiquetaRepository;
        private ICorteRepository<Corte> _CorteRepository;
        private ICorteLaserRepository<CorteLaser> _CorteLaserRepository;
        private IMapper _Mapper;

        public EtiquetaServices(IEtiquetaRepository<Etiqueta> etiquetaRepository,
                                ICorteRepository<Corte> corteRepository,
                                ICorteLaserRepository<CorteLaser> corteLaserRepository,
                                IMapper mapper)
        {
            _EtiquetaRepository = etiquetaRepository;
            _CorteRepository = corteRepository;
            _CorteLaserRepository = corteLaserRepository;
            _Mapper = mapper;
        }
        public async Task<EtiquetaDTO> AddEtiqueta(EtiquetaInsertDTO etiquetaInsertDto, string baseUrl)
        {
            var existsInCorteLaser = await _CorteRepository.AnyAsync(a => a.IdGeneral == etiquetaInsertDto.IdGeneral);
            var existsInCorte = await _CorteRepository.AnyAsync(a => a.IdGeneral == etiquetaInsertDto.IdGeneral);
            if (!existsInCorte && !existsInCorteLaser)
            {
                throw new Exception("El modelo con este IdGeneral no está registrado en Corte o Corte laser.");
            }
            var existsInEtiquetas = await _EtiquetaRepository.AnyAsync(e => e.IdGeneral == etiquetaInsertDto.IdGeneral);
            if (existsInEtiquetas)
            {
                throw new Exception("El modelo con este IdGeneral ya existe.");
            }
            var etiqueta = _Mapper.Map<Etiqueta>(etiquetaInsertDto);
            await _EtiquetaRepository.AddEtiqueta(etiqueta);
            await _EtiquetaRepository.Save();
            var etiquetaDto = _Mapper.Map<EtiquetaDTO>(etiqueta);
            return etiquetaDto;
        }

        public async Task<IEnumerable<EtiquetaDTO>> GetEtiqueta()
            {
                var etiquetas = await _EtiquetaRepository.GetEtiqueta(); 
                return _Mapper.Map<IEnumerable<EtiquetaDTO>>(etiquetas);
        }
        public async Task<EtiquetaDTO> GetEtiquetaByNombre(string Modelo)
            {
                var etiqueta = await _EtiquetaRepository.GetEtiquetaByNombre(Modelo);
                if (etiqueta != null)
                {
                    var etiquetaDto = _Mapper.Map<EtiquetaDTO>(etiqueta);
                    return etiquetaDto;
                }
                return null;
            }
            public async Task<EtiquetaDTO> GetEtiquetaById(int Idetiquetas)
            {
                var etiqueta = await _EtiquetaRepository.GetEtiquetaById(Idetiquetas);
                if (etiqueta != null)
                {
                    var etiquetaDto = _Mapper.Map<EtiquetaDTO>(etiqueta);
                    return etiquetaDto;
                }
                return null;
            }
        public async Task<EtiquetaDTO> UpdateEtiqueta(int idEtiquetas, EtiquetaUpdateDTO etiquetaUpdateDto)
        {
            var etiqueta= await _EtiquetaRepository.GetEtiquetaById(idEtiquetas);
            if (etiqueta == null)
            {
                return null;
            }
            if (etiqueta.FechaEditada == true)
            {
                throw new InvalidOperationException("No se puede modificar el registro más de una vez.");
            }
            etiqueta = _Mapper.Map<EtiquetaUpdateDTO, Etiqueta>(etiquetaUpdateDto, etiqueta);
                _EtiquetaRepository.UpdateEtiqueta(etiqueta);
                await _EtiquetaRepository.Save();
                var etiquetaDto = _Mapper.Map<EtiquetaDTO>(etiqueta);
                return etiquetaDto;
        }
    }
}

