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
    public class SerigrafiaServices : ISerigrafiaServices<SerigrafiaDTO, SerigrafiaInsertDTO, SerigrafiaUpdateDTO>
    {
        private ISerigrafiaRepository<Serigrafia> _SerigrafiaRepository;
        private IEtiquetaRepository<Etiqueta> _EtiquetaRepository;
        private IMapper _Mapper;

        public SerigrafiaServices(
            ISerigrafiaRepository<Serigrafia> serigrafiaRepository,
            IEtiquetaRepository<Etiqueta> etiquetaRepository,
            IMapper mapper)
        {
            _SerigrafiaRepository = serigrafiaRepository;
            _EtiquetaRepository = etiquetaRepository;
            _Mapper = mapper;
        }

        public async Task<SerigrafiaDTO> AddSerigrafia(SerigrafiaInsertDTO serigrafiaInsertDto, string baseUrl)
        {
            var existsInEtiqueta = await _EtiquetaRepository.AnyAsync(c => c.IdGeneral == serigrafiaInsertDto.IdGeneral);

            if (!existsInEtiqueta)
            {
                throw new Exception("El modelo con este IdGeneral no está registrado Etiquetas.");
            }
            var existsInSerigrafia = await _SerigrafiaRepository.AnyAsync(s => s.IdGeneral == serigrafiaInsertDto.IdGeneral);
            if (existsInSerigrafia)
            {
                throw new Exception("El modelo con este IdGeneral ya existe en Serigrafía.");
            }
            var serigrafia = _Mapper.Map<Serigrafia>(serigrafiaInsertDto);
            await _SerigrafiaRepository.AddSerigrafia(serigrafia);
            await _SerigrafiaRepository.Save();
            var serigrafiaDto = _Mapper.Map<SerigrafiaDTO>(serigrafia);
            return serigrafiaDto;
        }

        public async Task<IEnumerable<SerigrafiaDTO>> GetSerigrafia()
            {
                var serigrafias = await _SerigrafiaRepository.GetSerigrafia();
                return _Mapper.Map<IEnumerable<SerigrafiaDTO>>(serigrafias);
        }
        public async Task<SerigrafiaDTO> GetSerigrafiaByNombre(string Modelo)
            {
                var serigrafia = await _SerigrafiaRepository.GetSerigrafiaByNombre(Modelo);
                if (serigrafia != null)
                {
                    var serigrafiaDto = _Mapper.Map<SerigrafiaDTO>(serigrafia);
                    return serigrafiaDto;
                }
                return null;
            }
            public async Task<SerigrafiaDTO> GetSerigrafiaById(int Idserigrafia)
            {
                var serigrafia = await _SerigrafiaRepository.GetSerigrafiaById(Idserigrafia);
                if (serigrafia != null)
                {
                    var serigrafiaDto = _Mapper.Map<SerigrafiaDTO>(serigrafia);
                    return serigrafiaDto;
                }
                return null;
            }
        public async Task<SerigrafiaDTO> UpdateSerigrafia(int idSerigrafia, SerigrafiaUpdateDTO serigrafiaUpdateDto)
        {
            var serigrafia = await _SerigrafiaRepository.GetSerigrafiaById(idSerigrafia);
            if (serigrafia == null)
            {
                return null;
            }
            if (serigrafia.FechaEditada == true)
            {
                throw new InvalidOperationException("No se puede modificar el registro más de una vez.");
            }
            serigrafia = _Mapper.Map<SerigrafiaUpdateDTO, Serigrafia>(serigrafiaUpdateDto, serigrafia);
                _SerigrafiaRepository.UpdateSerigrafia(serigrafia);
                await _SerigrafiaRepository.Save();
                var serigrafiaDto = _Mapper.Map<SerigrafiaDTO>(serigrafia);
                return serigrafiaDto;
        }
    }
}

