using AutoMapper;
using TabitasAPI.DTOs;
using TabitasAPI.Models;

namespace TabitasAPI.Mappers
{
    public class MappingDeAlmacen : Profile
    {
        public MappingDeAlmacen()
        {
            // Mapeo de AlmacenInsertDTO a Almacen con configuraciones específicas
            CreateMap<AlmacenInsertDTO, Almacen>()
                .ForMember(dest => dest.IdProceso, opt => opt.MapFrom(src => src.IdProceso))
                .ForMember(dest => dest.ProcesoActual, opt => opt.Ignore());

            // Mapeo de Almacen a AlmacenDTO
            CreateMap<Almacen, AlmacenDTO>()
            .ForMember(dest => dest.Modelo, opt => opt.MapFrom(src => src.General != null ? src.General.Modelo.ToString() : null))
            .ForMember(dest => dest.Area, opt => opt.MapFrom(src => src.ProcesoActual != null ? src.ProcesoActual.Area : null));
            // Mapeo de AlmacenUpdateDTO a Almacen
            CreateMap<AlmacenUpdateDTO, Almacen>();
             


        }
    }

}


