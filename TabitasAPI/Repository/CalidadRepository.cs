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
    public class CalidadRepository : ICalidadRepository<Calidad>
    {
        private TabitasContext _context;
        private readonly IMapper _Mapper;
        public CalidadRepository(TabitasContext context, IMapper mapper)
        {
            _context = context;
            _Mapper = mapper;
        }
        public async Task AddCalidad(Calidad calidad)
        {
            var exists = await _context.Calidades.AnyAsync(a => a.IdCalidad == calidad.IdCalidad);
            if (exists) {throw new Exception("El modelo (IdGeneral) ya existe."); }
            await _context.Calidades.AddAsync(calidad);
        }
        public async Task<IEnumerable<CalidadDTO>> GetCalidad()
        {
            return await _context.Calidades
                .Include(a => a.General)
                .Include(a => a.ProcesoActual)
                .Select(a => _Mapper.Map<CalidadDTO>(a))
                .ToListAsync();
        }
        public async Task<Calidad> GetCalidadById(int Idcalidad)
        {
            return await _context.Calidades
                .Include(a => a.General)
                 .Include(u => u.ProcesoActual)
                 .FirstOrDefaultAsync(u => u.IdCalidad == Idcalidad);
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
        public void UpdateCalidad(Calidad calidad)
        {
            var existing = _context.Calidades.FirstOrDefault(u => u.IdCalidad == calidad.IdCalidad);
            _context.Calidades.Attach(existing);
            _context.Entry(existing).CurrentValues.SetValues(calidad);
            _context.SaveChanges();
        }
        public async Task<Calidad> GetCalidadByNombre(string Modelo)
        {
            int modeloInt = int.Parse(Modelo);

            return await _context.Calidades
                .Include(u => u.ProcesoActual)
                .Include(u => u.General)
                .FirstOrDefaultAsync(u => u.General.Modelo == modeloInt.ToString());
        }
        public async Task<bool> AnyAsync(Func<Calidad, bool> predicate)
        {
            return await Task.FromResult(_context.Calidades.Any(predicate));
        }
    }
}

