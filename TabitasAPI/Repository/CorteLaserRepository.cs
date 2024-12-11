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
    public class CorteLaserRepository : ICorteLaserRepository<CorteLaser>
    {
        private TabitasContext _context;
        private readonly IMapper _Mapper;
        public CorteLaserRepository(TabitasContext context, IMapper mapper)
        {
            _context = context;
            _Mapper = mapper;
        }
        public async Task AddCorteLaser(CorteLaser corteLaser)
        {
            var exists = await _context.CorteLaseres.AnyAsync(a => a.IdCorteLaser == corteLaser.IdCorteLaser);
            if (exists) {throw new Exception("El modelo (IdGeneral) ya existe."); }
            await _context.CorteLaseres.AddAsync(corteLaser);
        }
        public async Task<IEnumerable<CorteLaserDTO>> GetCorteLaser()
        {
            return await _context.CorteLaseres
                .Include(a => a.General)
                .Include(a => a.ProcesoActual)
                .Select(a => _Mapper.Map<CorteLaserDTO>(a))
                .ToListAsync();
        }
        public async Task<CorteLaser> GetCorteLaserById(int IdcorteLaser)
        {
            return await _context.CorteLaseres
                .Include(a => a.General)
                 .Include(u => u.ProcesoActual)
                 .FirstOrDefaultAsync(u => u.IdCorteLaser == IdcorteLaser);
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
        public void UpdateCorteLaser(CorteLaser corteLaser)
        {
            var existing = _context.CorteLaseres.FirstOrDefault(u => u.IdCorteLaser == corteLaser.IdCorteLaser);
            _context.CorteLaseres.Attach(existing);
            _context.Entry(existing).CurrentValues.SetValues(corteLaser);
            _context.SaveChanges();
        }
        public async Task<CorteLaser> GetCorteLaserByNombre(string Modelo)
        {
            int modeloInt = int.Parse(Modelo);

            return await _context.CorteLaseres
                .Include(u => u.ProcesoActual)
                .Include(u => u.General)
                .FirstOrDefaultAsync(u => u.General.Modelo == modeloInt.ToString());
        }
        public async Task<bool> AnyAsync(Func<CorteLaser, bool> predicate)
        {
            return await Task.FromResult(_context.CorteLaseres.Any(predicate));
        }
    }
}

