using AutoMapper;
using TabitasAPI.Models;
using TabitasAPI.DTOs;

namespace TabitasAPI.Mappers
{
    public class MappingUsuarios:Profile
    {
        public MappingUsuarios()
        {
            // Mapeo de Usuario a UsuarioDTO
            CreateMap<Usuarios, UsuarioDTO>();

            // Mapeo de UsuarioInsertDTO a Usuario
            CreateMap<UsuarioInsertDTO, Usuarios>();

            // Mapeo de UsuarioLoginDTO a Usuario
            CreateMap<UsuarioLoginDTO, Usuarios>();
        }
    }
}
