using AutoMapper;
using TabitasAPI.DTOs;
using TabitasAPI.Models;

namespace TabitasAPI.Mappers
{
    public class MappingDeGeneral: Profile
    {
        public MappingDeGeneral()
        {
            CreateMap<GeneralInsertDTO, General>();

            CreateMap<General, GeneralDTO>().ForMember(dto => dto.ProcesoActual,
                p=> p.MapFrom(g=> g.ProcesoActual.Area));

            CreateMap<GeneralUpdateDTO, General>();
        }
    }
}
