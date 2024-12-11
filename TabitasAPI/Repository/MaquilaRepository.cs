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
    public class MaquilaRepository : IMaquilaRepository<Maquila>
    {
        private TabitasContext _context;
        private readonly IMapper _Mapper;
        public MaquilaRepository(TabitasContext context, IMapper mapper)
        {
            _context = context;
            _Mapper = mapper;
        }
        public async Task AddMaquila(Maquila maquila)
        {
            var exists = await _context.Maquilas.AnyAsync(a => a.IdMaquila == maquila.IdMaquila);
            if (exists) {throw new Exception("El modelo (IdGeneral) ya existe."); }
            await _context.Maquilas.AddAsync(maquila);
        }
        public async Task<IEnumerable<MaquilaDTO>> GetMaquila()
        {
            return await _context.Maquilas
                .Include(a => a.General)
                .Include(a => a.ProcesoActual)
                .Select(a => _Mapper.Map<MaquilaDTO>(a))
                .ToListAsync();
        }
        public async Task<Maquila> GetMaquilaById(int Idmaquila)
        {
            return await _context.Maquilas
                .Include(a => a.General)
                 .Include(u => u.ProcesoActual)
                 .FirstOrDefaultAsync(u => u.IdMaquila == Idmaquila);
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
        public void UpdateMaquila(Maquila maquila)
        {
            var existing = _context.Maquilas.FirstOrDefault(u => u.IdMaquila == maquila.IdMaquila);
            _context.Maquilas.Attach(existing);
            _context.Entry(existing).CurrentValues.SetValues(maquila);
            _context.SaveChanges();
        }
        public async Task<Maquila> GetMaquilaByNombre(string Modelo)
        {
            int modeloInt = int.Parse(Modelo);

            return await _context.Maquilas
                .Include(u => u.ProcesoActual)
                .Include(u => u.General)
                .FirstOrDefaultAsync(u => u.General.Modelo == modeloInt.ToString());
        }
        public async Task<bool> AnyAsync(Func<Maquila, bool> predicate)
        {
            return await Task.FromResult(_context.Maquilas.Any(predicate));
        }
    }
}

