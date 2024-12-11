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
    public class EtiquetaRepository : IEtiquetaRepository<Etiqueta>
    {
        private TabitasContext _context;
        private readonly IMapper _Mapper;
        public EtiquetaRepository(TabitasContext context, IMapper mapper)
        {
            _context = context;
            _Mapper = mapper;
        }
        public async Task AddEtiqueta(Etiqueta etiqueta)
        {
            var exists = await _context.Etiquetas.AnyAsync(a => a.IdEtiquetas == etiqueta.IdEtiquetas);
            if (exists) {throw new Exception("El modelo (IdGeneral) ya existe."); }
            await _context.Etiquetas.AddAsync(etiqueta);
        }

        public async Task<IEnumerable<EtiquetaDTO>> GetEtiqueta()
        {
            
            return await _context.Etiquetas
                .Include(a => a.General) 
                .Include(a => a.ProcesoActual)
                .Select(a => _Mapper.Map<EtiquetaDTO>(a))
                .ToListAsync();
           
        }


        public async Task<Etiqueta> GetEtiquetaById(int Idetiquetas)
        {
            return await _context.Etiquetas
                .Include(a => a.General)
                 .Include(u => u.ProcesoActual)
                 .FirstOrDefaultAsync(u => u.IdEtiquetas == Idetiquetas);
        }

        public async Task Save()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Verifica si el error es relacionado con los triggers y la cláusula OUTPUT
                if (ex.InnerException is SqlException sqlEx && sqlEx.Number == 334)
                {
                    throw new InvalidOperationException("No se pueden guardar los cambios", ex);
                }
                throw;
            }
        }

        public void UpdateEtiqueta(Etiqueta etiqueta)
        {
            var existing = _context.Etiquetas.FirstOrDefault(u => u.IdEtiquetas == etiqueta.IdEtiquetas);
            _context.Etiquetas.Attach(existing);
            _context.Entry(existing).CurrentValues.SetValues(etiqueta);
            _context.SaveChanges();
        }

        public async Task<Etiqueta> GetEtiquetaByNombre(string Modelo)
        {
            int modeloInt = int.Parse(Modelo);

            return await _context.Etiquetas
                .Include(u => u.ProcesoActual)
                .Include(u => u.General)
                .FirstOrDefaultAsync(u => u.General.Modelo == modeloInt.ToString());
        }
        public async Task<bool> AnyAsync(Func<Etiqueta, bool> predicate)
        {
            return await Task.FromResult(_context.Etiquetas.Any(predicate));
        }

    }
}

