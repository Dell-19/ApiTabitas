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
    public class SerigrafiaRepository : ISerigrafiaRepository<Serigrafia>
    {
        private TabitasContext _context;
        private readonly IMapper _Mapper;
        public SerigrafiaRepository(TabitasContext context, IMapper mapper)
        {
            _context = context;
            _Mapper = mapper;
        }
        public async Task AddSerigrafia(Serigrafia serigrafia)
        {
            var exists = await _context.Serigrafias.AnyAsync(a => a.IdSerigrafia == serigrafia.IdSerigrafia);
            if (exists) {throw new Exception("El modelo (IdGeneral) ya existe."); }
            await _context.Serigrafias.AddAsync(serigrafia);
        }
        public async Task<IEnumerable<SerigrafiaDTO>> GetSerigrafia()
        {
            return await _context.Serigrafias
                .Include(a => a.General)
                .Include(a => a.ProcesoActual)
                .Select(a => _Mapper.Map<SerigrafiaDTO>(a))
                .ToListAsync();
        }
        public async Task<Serigrafia> GetSerigrafiaById(int Idserigrafia)
        {
            return await _context.Serigrafias
                .Include(a => a.General)
                 .Include(u => u.ProcesoActual)
                 .FirstOrDefaultAsync(u => u.IdSerigrafia == Idserigrafia);
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
        public void UpdateSerigrafia(Serigrafia serigrafia)
        {
            var existing = _context.Serigrafias.FirstOrDefault(u => u.IdSerigrafia == serigrafia.IdSerigrafia);
            _context.Serigrafias.Attach(existing);
            _context.Entry(existing).CurrentValues.SetValues(serigrafia);
            _context.SaveChanges();
        }
        public async Task<Serigrafia> GetSerigrafiaByNombre(string Modelo)
        {
            int modeloInt = int.Parse(Modelo);

            return await _context.Serigrafias
                .Include(u => u.ProcesoActual)
                .Include(u => u.General)
                .FirstOrDefaultAsync(u => u.General.Modelo == modeloInt.ToString());
        }
        public async Task<bool> AnyAsync(Func<Serigrafia, bool> predicate)
        {
            return await Task.FromResult(_context.Serigrafias.Any(predicate));
        }
    }
}

