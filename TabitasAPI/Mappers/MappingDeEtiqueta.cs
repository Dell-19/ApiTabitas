using AutoMapper;
using TabitasAPI.DTOs;
using TabitasAPI.Models;

namespace TabitasAPI.Mappers
{
    public class MappingDeEtiqueta : Profile
    {
        public MappingDeEtiqueta()
        {
            CreateMap<EtiquetaInsertDTO, Etiqueta>()
                .ForMember(dest => dest.IdProceso, opt => opt.MapFrom(src => src.IdProceso))
                .ForMember(dest => dest.ProcesoActual, opt => opt.Ignore()); 

            CreateMap<Etiqueta, EtiquetaDTO>()
            .ForMember(dest => dest.Modelo, opt => opt.MapFrom(src => src.General != null ? src.General.Modelo.ToString() : null))
            .ForMember(dest => dest.Area, opt => opt.MapFrom(src => src.ProcesoActual != null ? src.ProcesoActual.Area : null));

            CreateMap<EtiquetaUpdateDTO, Etiqueta>()
             .ForMember(dest => dest.FechaEntregaMaquila, opt => opt.MapFrom(src => src.FechaEntregaMaquila))
             .ForMember(dest => dest.FechaEntregaTerminado, opt => opt.MapFrom(src => src.FechaEntregaTerminado))
             .ForMember(dest => dest.Incidencias, opt => opt.MapFrom(src => src.Incidencias));
        }
    }

}


