using AutoMapper;
using TabitasAPI.DTOs;
using TabitasAPI.Models;

namespace TabitasAPI.Mappers
{
    public class MappingDeMaquila : Profile
    {
        public MappingDeMaquila()
        {
            CreateMap<MaquilaInsertDTO, Maquila>()
                .ForMember(dest => dest.IdProceso, opt => opt.MapFrom(src => src.IdProceso))
                .ForMember(dest => dest.ProcesoActual, opt => opt.Ignore()); 

            CreateMap<Maquila, MaquilaDTO>()
            .ForMember(dest => dest.Modelo, opt => opt.MapFrom(src => src.General != null ? src.General.Modelo.ToString() : null))
            .ForMember(dest => dest.Area, opt => opt.MapFrom(src => src.ProcesoActual != null ? src.ProcesoActual.Area : null));

            CreateMap<MaquilaUpdateDTO, Maquila>()
             .ForMember(dest => dest.FechaEntrega, opt => opt.MapFrom(src => src.FechaEntrega))
             .ForMember(dest => dest.Incidencias, opt => opt.MapFrom(src => src.Incidencias));
        }
    }

}


