using AutoMapper;
using TabitasAPI.DTOs;
using TabitasAPI.Models;

namespace TabitasAPI.Mappers
{
    public class MappingDeCalidad : Profile
    {
        public MappingDeCalidad()
        {
            CreateMap<CalidadInsertDTO, Calidad>()
                .ForMember(dest => dest.IdProceso, opt => opt.MapFrom(src => src.IdProceso))
                .ForMember(dest => dest.ProcesoActual, opt => opt.Ignore()); 

            CreateMap<Calidad, CalidadDTO>()
            .ForMember(dest => dest.Modelo, opt => opt.MapFrom(src => src.General != null ? src.General.Modelo.ToString() : null))
            .ForMember(dest => dest.Area, opt => opt.MapFrom(src => src.ProcesoActual != null ? src.ProcesoActual.Area : null));

            CreateMap<CalidadUpdateDTO, Calidad>()
             .ForMember(dest => dest.FechaEnvioMaquila, opt => opt.MapFrom(src => src.FechaEnvioMaquila))
             .ForMember(dest => dest.FechaRecepcionRechazo, opt => opt.MapFrom(src => src.FechaRecepcionRechazo))
             .ForMember(dest => dest.Incidencia, opt => opt.MapFrom(src => src.Incidencia)); ;
    }
    }

}


