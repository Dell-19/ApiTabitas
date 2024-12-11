using AutoMapper;
using TabitasAPI.DTOs;
using TabitasAPI.Models;

namespace TabitasAPI.Mappers
{
    public class MappingDeCorte : Profile
    {
        public MappingDeCorte()
        {
            CreateMap<CorteInsertDTO, Corte>()
                .ForMember(dest => dest.IdProceso, opt => opt.MapFrom(src => src.IdProceso))
                .ForMember(dest => dest.ProcesoActual, opt => opt.Ignore()); 

            CreateMap<Corte, CorteDTO>()
            .ForMember(dest => dest.Modelo, opt => opt.MapFrom(src => src.General != null ? src.General.Modelo.ToString() : null))
            .ForMember(dest => dest.Area, opt => opt.MapFrom(src => src.ProcesoActual != null ? src.ProcesoActual.Area : null));

            CreateMap<CorteUpdateDTO, Corte>()
             .ForMember(dest => dest.FechaAVentas, opt => opt.MapFrom(src => src.FechaAVentas))
             .ForMember(dest => dest.FechaEntrega, opt => opt.MapFrom(src => src.FechaEntrega))
             .ForMember(dest => dest.Incidencias, opt => opt.MapFrom(src => src.Incidencias));
        }
    }

}


