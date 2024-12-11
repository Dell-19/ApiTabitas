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
    public class CorteRepository : ICorteRepository<Corte>
    {
        private TabitasContext _context;
        private readonly IMapper _Mapper;
        public CorteRepository(TabitasContext context, IMapper mapper)
        {
            _context = context;
            _Mapper = mapper;
        }
        public async Task AddCorte(Corte corte)
        {
            var exists = await _context.Cortes.AnyAsync(a => a.IdCorte == corte.IdCorte);
            if (exists) {throw new Exception("El modelo (IdGeneral) ya existe."); }
            await _context.Cortes.AddAsync(corte);
        }
        public async Task<IEnumerable<CorteDTO>> GetCorte()
        {
            return await _context.Cortes
                .Include(a => a.General)
                .Include(a => a.ProcesoActual)
                .Select(a => _Mapper.Map<CorteDTO>(a))
                .ToListAsync();
        }
        public async Task<Corte> GetCorteById(int Idcorte)
        {
            return await _context.Cortes
                .Include(a => a.General)
                 .Include(u => u.ProcesoActual)
                 .FirstOrDefaultAsync(u => u.IdCorte == Idcorte);
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
        public void UpdateCorte(Corte corte)
        {
            var existing = _context.Cortes.FirstOrDefault(u => u.IdCorte == corte.IdCorte);
            _context.Cortes.Attach(existing);
            _context.Entry(existing).CurrentValues.SetValues(corte);
            _context.SaveChanges();
        }
        public async Task<Corte> GetCorteByNombre(string Modelo)
        {
            int modeloInt = int.Parse(Modelo);

            return await _context.Cortes
                .Include(u => u.ProcesoActual)
                .Include(u => u.General)
                .FirstOrDefaultAsync(u => u.General.Modelo == modeloInt.ToString());
        }
        public async Task<bool> AnyAsync(Func<Corte, bool> predicate)
        {
            return await Task.FromResult(_context.Cortes.Any(predicate));
        }
    }
}

