using AutoMapper;
using TabitasAPI.DTOs;
using TabitasAPI.Models;

namespace TabitasAPI.Mappers
{
    public class MappingDeProcesoActual : Profile
    {
        public MappingDeProcesoActual()
        {
            // Mapeo de Almacen a AlmacenDTO
            CreateMap<ProcesoActual, ProcesoActualDTO>();

        }
    }

}


