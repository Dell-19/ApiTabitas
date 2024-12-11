using Microsoft.EntityFrameworkCore;
using TabitasAPI.Data;
using TabitasAPI.DTOs;
using TabitasAPI.Models;
using TabitasAPI.Repository.IRepository;

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TabitasAPI.Models;
using AutoMapper;
using Microsoft.Data.SqlClient;

namespace TabitasAPI.Repository
{
    public class ProcesoActualRepository : IProcesoActualRepository<ProcesoActual>
    {
        private TabitasContext _context;
        private readonly IMapper _Mapper;
        public ProcesoActualRepository(TabitasContext context, IMapper mapper)
        {
            _context = context;
            _Mapper = mapper;
        }


        public async Task<IEnumerable<ProcesoActualDTO>> GetProcesoActual()
        {
            
            return await _context.ProcesoActuales
                .Select(a => _Mapper.Map<ProcesoActualDTO>(a))
                .ToListAsync();
           
        }

    }
}

