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
    public class BordadoRepository : IBordadoRepository<Bordado>
    {
        private TabitasContext _context;
        private readonly IMapper _Mapper;
        public BordadoRepository(TabitasContext context, IMapper mapper)
        {
            _context = context;
            _Mapper = mapper;
        }
        public async Task AddBordado(Bordado bordado)
        {
            var exists = await _context.Bordados.AnyAsync(a => a.IdBordado == bordado.IdBordado);
            if (exists) {throw new Exception("El modelo (IdGeneral) ya existe."); }
            await _context.Bordados.AddAsync(bordado);
        }
        public async Task<IEnumerable<BordadoDTO>> GetBordado()
        {
            return await _context.Bordados
                .Include(a => a.General)
                .Include(a => a.ProcesoActual)
                .Select(a => _Mapper.Map<BordadoDTO>(a))
                .ToListAsync();
        }
        public async Task<Bordado> GetBordadoById(int Idbordado)
        {
            return await _context.Bordados
                .Include(a => a.General)
                 .Include(u => u.ProcesoActual)
                 .FirstOrDefaultAsync(u => u.IdBordado == Idbordado);
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
        public void UpdateBordado(Bordado bordado)
        {
            var existing = _context.Bordados.FirstOrDefault(u => u.IdBordado == bordado.IdBordado);
            _context.Bordados.Attach(existing);
            _context.Entry(existing).CurrentValues.SetValues(bordado);
            _context.SaveChanges();
        }
        public async Task<Bordado> GetBordadoByNombre(string Modelo)
        {
            int modeloInt = int.Parse(Modelo);

            return await _context.Bordados
                .Include(u => u.ProcesoActual)
                .Include(u => u.General)
                .FirstOrDefaultAsync(u => u.General.Modelo == modeloInt.ToString());
        }
        public async Task<bool> AnyAsync(Func<Bordado, bool> predicate)
        {
            return await Task.FromResult(_context.Bordados.Any(predicate));
        }
    }
}

