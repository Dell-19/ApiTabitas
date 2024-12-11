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
    public class TerminadoRepository : ITerminadoRepository<Terminado>
    {
        private TabitasContext _context;
        private readonly IMapper _Mapper;
        public TerminadoRepository(TabitasContext context, IMapper mapper)
        {
            _context = context;
            _Mapper = mapper;
        }
        public async Task AddTerminado(Terminado terminado)
        {
            var exists = await _context.Terminados.AnyAsync(a => a.IdTErminado == terminado.IdTErminado);
            if (exists) {throw new Exception("El modelo (IdGeneral) ya existe."); }
            await _context.Terminados.AddAsync(terminado);
        }
        public async Task<IEnumerable<TerminadoDTO>> GetTerminado()
        {
            return await _context.Terminados
                .Include(a => a.General)
                .Include(a => a.ProcesoActual)
                .Select(a => _Mapper.Map<TerminadoDTO>(a))
                .ToListAsync();
        }
        public async Task<Terminado> GetTerminadoById(int Idterminado)
        {
            return await _context.Terminados
                .Include(a => a.General)
                .Include(u => u.ProcesoActual)
                .FirstOrDefaultAsync(u => u.IdTErminado == Idterminado);
        }

        public async Task Save()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException sqlEx && sqlEx.Number == 334)
                {
                    throw new InvalidOperationException("No se pueden guardar los cambios", ex);
                }
                throw;
            }
        }
        public void UpdateTerminado(Terminado terminado)
        {
            var existing = _context.Terminados.FirstOrDefault(u => u.IdTErminado == terminado.IdTErminado);
            _context.Terminados.Attach(existing);
            _context.Entry(existing).CurrentValues.SetValues(terminado);
            _context.SaveChanges();
        }
        public async Task<Terminado> GetTerminadoByNombre(string Modelo)
        {
            int modeloInt = int.Parse(Modelo);

            return await _context.Terminados
                .Include(u => u.ProcesoActual)
                .Include(u => u.General)
                .FirstOrDefaultAsync(u => u.General.Modelo == modeloInt.ToString());
        }
        public async Task<bool> AnyAsync(Func<Terminado, bool> predicate)
        {
            return await Task.FromResult(_context.Terminados.Any(predicate));
        }
    }
}

