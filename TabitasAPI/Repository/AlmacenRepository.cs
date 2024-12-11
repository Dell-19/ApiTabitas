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
    public class AlmacenRepository : IAlmacenRepository<Almacen>
    {
        private TabitasContext _context;
        private readonly IMapper _Mapper;
        public AlmacenRepository(TabitasContext context, IMapper mapper)
        {
            _context = context;
            _Mapper = mapper;
        }
        public async Task AddAlmacen(Almacen almacen)
        {
            var exists = await _context.Almacenes.AnyAsync(a => a.IdGeneral == almacen.IdGeneral);
            if (exists) {throw new Exception("El modelo (IdGeneral) ya existe."); }
            await _context.Almacenes.AddAsync(almacen);
        }

        public async Task<IEnumerable<AlmacenDTO>> GetAlmacen()
        {
            
            return await _context.Almacenes
                .Include(a => a.General) 
                .Include(a => a.ProcesoActual) 
                .Select(a => _Mapper.Map<AlmacenDTO>(a))
                .ToListAsync();
           
        }


        public async Task<Almacen> GetAlmacenById(int Idalmacen)
        {
            return await _context.Almacenes
                .Include(a => a.General)
                 .Include(u => u.ProcesoActual)
                 .FirstOrDefaultAsync(u => u.IdAlmacen == Idalmacen);
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

        public void UpdateAlmacen(Almacen almacen)
        {
            var existing = _context.Almacenes.FirstOrDefault(u => u.IdAlmacen == almacen.IdAlmacen);
            _context.Almacenes.Attach(existing);
            _context.Entry(existing).CurrentValues.SetValues(almacen);
            _context.SaveChanges();
        }

        public async Task<Almacen> GetAlmacenByNombre(string Modelo)
        {
            int modeloInt = int.Parse(Modelo);

            return await _context.Almacenes
                .Include(u => u.ProcesoActual)
                .Include(u => u.General)
                .FirstOrDefaultAsync(u => u.General.Modelo == modeloInt.ToString());
        }
        public async Task<bool> AnyAsync(Func<Almacen, bool> predicate)
        {
            return await Task.FromResult(_context.Almacenes.Any(predicate));
        }

    }
}

