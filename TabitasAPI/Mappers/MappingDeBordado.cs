using AutoMapper;
using TabitasAPI.DTOs;
using TabitasAPI.Models;

namespace TabitasAPI.Mappers
{
    public class MappingDeBordado : Profile
    {
        public MappingDeBordado()
        {
            CreateMap<BordadoInsertDTO, Bordado>()
                .ForMember(dest => dest.IdProceso, opt => opt.MapFrom(src => src.IdProceso))
                .ForMember(dest => dest.ProcesoActual, opt => opt.Ignore()); 

            CreateMap<Bordado, BordadoDTO>()
            .ForMember(dest => dest.Modelo, opt => opt.MapFrom(src => src.General != null ? src.General.Modelo.ToString() : null))
            .ForMember(dest => dest.Area, opt => opt.MapFrom(src => src.ProcesoActual != null ? src.ProcesoActual.Area : null));

            CreateMap<BordadoUpdateDTO, Bordado>()
             .ForMember(dest => dest.FechaEntrega, opt => opt.MapFrom(src => src.FechaEntrega))
             .ForMember(dest => dest.Incidencias, opt => opt.MapFrom(src => src.Incidencias)); ;
        }
    }
}


