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
    public class LavadoRepository : ILavadoRepository<Lavado>
    {
        private TabitasContext _context;
        private readonly IMapper _Mapper;
        public LavadoRepository(TabitasContext context, IMapper mapper)
        {
            _context = context;
            _Mapper = mapper;
        }
        public async Task AddLavado(Lavado lavado)
        {
            var exists = await _context.Lavados.AnyAsync(a => a.IdLavado == lavado.IdLavado);
            if (exists) {throw new Exception("El modelo (IdGeneral) ya existe."); }
            await _context.Lavados.AddAsync(lavado);
        }
        public async Task<IEnumerable<LavadoDTO>> GetLavado()
        {
            return await _context.Lavados
                .Include(a => a.General)
                .Include(a => a.ProcesoActual)
                .Select(a => _Mapper.Map<LavadoDTO>(a))
                .ToListAsync();
        }
        public async Task<Lavado> GetLavadoById(int Idlavado)
        {
            return await _context.Lavados
                .Include(a => a.General)
                 .Include(u => u.ProcesoActual)
                 .FirstOrDefaultAsync(u => u.IdLavado == Idlavado);
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
        public void UpdateLavado(Lavado lavado)
        {
            var existing = _context.Lavados.FirstOrDefault(u => u.IdLavado == lavado.IdLavado);
            _context.Lavados.Attach(existing);
            _context.Entry(existing).CurrentValues.SetValues(lavado);
            _context.SaveChanges();
        }
        public async Task<Lavado> GetLavadoByNombre(string Modelo)
        {
            int modeloInt = int.Parse(Modelo);

            return await _context.Lavados
                .Include(u => u.ProcesoActual)
                .Include(u => u.General)
                .FirstOrDefaultAsync(u => u.General.Modelo == modeloInt.ToString());
        }
        public async Task<bool> AnyAsync(Func<Lavado, bool> predicate)
        {
            return await Task.FromResult(_context.Lavados.Any(predicate));
        }
    }
}

