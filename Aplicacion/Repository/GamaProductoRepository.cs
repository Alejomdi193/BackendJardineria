using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository
{
    public class GamaProductoRepository : Generic<GamaProducto>, IGamaProducto
    {
        private readonly JardineriaContext _context;
        public GamaProductoRepository(JardineriaContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<GamaProducto>> GetAllAsync()
        {
            return await _context.GamaProductos
                .ToListAsync();
        }

        public override async Task<GamaProducto> GetByIdAsync(string gama)
        {
            return await _context.GamaProductos
                .FirstOrDefaultAsync(p => p.Gama == gama);
        }

        public override async Task<(int totalRegistros, IEnumerable<GamaProducto> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
        {
            var query = _context.GamaProductos as IQueryable<GamaProducto>;

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.DescripcionTexto.ToLower().Contains(search));
            }

            query = query.OrderBy(p => p.Gama);
            var totalRegistros = await query.CountAsync();
            var registros = await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (totalRegistros, registros);
        }
    }
}