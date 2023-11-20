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
    public class OficinaRepository : Generic<Oficina>, IOficina
    {
        private readonly JardineriaContext _context;
        public OficinaRepository(JardineriaContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<Oficina>> GetAllAsync()
        {
            return await _context.Oficinas
                .ToListAsync();
        }

        public override async Task<Oficina> GetByIdAsync(string codigoOficina)
        {
            return await _context.Oficinas
                .FirstOrDefaultAsync(p => p.CodigoOficina == codigoOficina);
        }

        public override async Task<(int totalRegistros, IEnumerable<Oficina> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
        {
            var query = _context.Oficinas as IQueryable<Oficina>;

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.Ciudad.ToLower().Contains(search));
            }

            query = query.OrderBy(p => p.CodigoOficina);
            var totalRegistros = await query.CountAsync();
            var registros = await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (totalRegistros, registros);
        }


        public async Task<IEnumerable<object>> Consulta26NaturalLeft()
        {
            var mensaje = "Oficinas sin empleados representantes de clientes que han comprado productos de la gama Frutales (NATURAL LEFT JOIN)".ToUpper();

            var consulta = from oficina in _context.Oficinas
                           join empleado in _context.Empleados on oficina.CodigoOficina equals empleado.CodigoOficina into empleadosJoin
                           from empleado in empleadosJoin.DefaultIfEmpty()
                           where !_context.Clientes
                                       .Where(cliente => cliente.CodigoEmpleadoRepVentas != null)
                                       .Select(cliente => cliente.CodigoEmpleadoRepVentas)
                                       .Contains(empleado.CodigoEmpleado)
                           select new
                           {
                               Oficina = oficina.CodigoOficina,
                               Empleado = empleado != null ? empleado.Nombre : null
                           };

                            var resultado = new List<object>
                            {
                                new { Informacion = mensaje, Resultado = await consulta.ToListAsync() }
                            };

            return resultado;
        }

        public async Task<IEnumerable<object>> Consulta26Left()
        {
            var mensaje = "Oficinas sin empleados representantes de clientes que han comprado productos de la gama Frutales (LEFT JOIN)".ToUpper();

            var consulta = from oficina in _context.Oficinas
                           join empleado in _context.Empleados on oficina.CodigoOficina equals empleado.CodigoOficina into empleadosJoin
                           from empleado in empleadosJoin.DefaultIfEmpty()
                           where !_context.Clientes
                                       .Where(cliente => cliente.CodigoEmpleadoRepVentas != null)
                                       .Select(cliente => cliente.CodigoEmpleadoRepVentas)
                                       .Contains(empleado.CodigoEmpleado)
                           select new
                           {
                               Oficina = oficina.CodigoOficina,
                               Empleado = empleado != null ? empleado.Nombre : null
                           };

                        var resultado = new List<object>
                        {
                            new { Informacion = mensaje, Resultado = await consulta.ToListAsync() }
                        };

            return resultado;
        }
    }
}