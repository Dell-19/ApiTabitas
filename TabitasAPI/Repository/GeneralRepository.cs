using Microsoft.EntityFrameworkCore;
using TabitasAPI.Data;
using TabitasAPI.Models;
using TabitasAPI.Repository.IRepository;

namespace TabitasAPI.Repository
{
    public class GeneralRepository : IGeneralRepository<General>
    {
        private TabitasContext _context;
        public GeneralRepository(TabitasContext context)
        {
            _context = context;
        }
        public async Task AddGeneral(General general)
        {
            // Verificar si el IdGeneral ya existe en la base de datos
            var exists = await _context.Generales.AnyAsync(a => a.IdGeneral == general.IdGeneral);
            if (exists) { throw new Exception("El modelo (IdGeneral) ya existe."); }
            await _context.Generales.AddAsync(general);
        }

        public async Task<IEnumerable<General>> GetGeneral()
        {  
            return await _context.Generales
                .Include(u => u.ProcesoActual)
                .ToListAsync();
        }
        public async Task<General> GetGeneralByNombre(string Modelo)
        {
           return await _context.Generales
                .Include(u => u.ProcesoActual)
                .FirstAsync(u => u.Modelo == Modelo);
        }
        public async Task<General> GetGeneralById(int Idgeneral)
        {
            return await _context.Generales
            .Include(u => u.ProcesoActual)
           .FirstOrDefaultAsync(u => u.IdGeneral == Idgeneral);
        }

        public async Task Save()
        => await _context.SaveChangesAsync();

        public void UpdateGeneral(General general)
        {
            var existingGeneral = _context.Generales.FirstOrDefault(u => u.IdGeneral == general.IdGeneral);
            _context.Generales.Attach(existingGeneral);
            _context.Entry(existingGeneral).CurrentValues.SetValues(general);
            _context.SaveChanges();
        }

        public async Task<bool> AnyAsync(Func<General, bool> predicate)
        {
            return await Task.FromResult(_context.Generales.Any(predicate));
        }
    }
}
