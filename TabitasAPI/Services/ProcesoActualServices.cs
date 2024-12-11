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
    public class ProcesoActualServices : IProcesoActualServices<ProcesoActualDTO>
    {
            private IProcesoActualRepository<ProcesoActual> _ProcesoActualRepository;
            private IMapper _Mapper;

        public ProcesoActualServices(IProcesoActualRepository<ProcesoActual> procesoActualRepository,
                IMapper mapper)
        {
                _ProcesoActualRepository = procesoActualRepository;
                _Mapper = mapper;
        }
        public async Task<IEnumerable<ProcesoActualDTO>> GetProcesoActual()
        {
            var procesoActuales = await _ProcesoActualRepository.GetProcesoActual(); // Obtener todos los registros de Almacen
                return _Mapper.Map<IEnumerable<ProcesoActualDTO>>(procesoActuales); // Mapear la lista a AlmacenDTO
        }
    }
}

