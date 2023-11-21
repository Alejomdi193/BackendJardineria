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
    public class DetallePedidoRepository : Generic<DetallePedido>, IDetallePedido
    {
        private readonly JardineriaContext _context;
        public DetallePedidoRepository(JardineriaContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<DetallePedido>> GetAllAsync()
        {
            return await _context.DetallePedidos
                .ToListAsync();
        }


        public override async Task<DetallePedido> GetByIdx1Async(int codigoPedido, string codigoProducto)
        {
            return await _context.DetallePedidos
                .FirstOrDefaultAsync(p => p.CodigoPedido == codigoPedido && p.CodigoProducto == codigoProducto);
        }

        public override async Task<(int totalRegistros, IEnumerable<DetallePedido> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
        {
            var query = _context.DetallePedidos as IQueryable<DetallePedido>;

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.Cantidad.ToString().ToLower().Contains(search));
            }

            var totalRegistros = await query.CountAsync();
            var registros = await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (totalRegistros, registros);
        }

        public async Task<IEnumerable<object>> Consulta38()
        {
            var consulta = from detalle in _context.DetallePedidos
                           group detalle by detalle.CodigoPedido into grupoDetalles
                           select new
                           {
                               CodigoPedido = grupoDetalles.Key,
                               NumeroProductosDiferentes = grupoDetalles.Select(det => det.CodigoProducto).Distinct().Count()
                           };

            var resultado = await consulta.ToListAsync();

            return resultado;
        }
        public async Task<IEnumerable<object>> Consulta39()
        {
            var consulta = from detalle in _context.DetallePedidos
                           group detalle by detalle.CodigoPedido into grupoDetalles
                           select new
                           {
                               CodigoPedido = grupoDetalles.Key,
                               SumaCantidadTotal = grupoDetalles.Sum(det => det.Cantidad)
                           };

            var resultado = await consulta.ToListAsync();

            return resultado;
        }

        public async Task<IEnumerable<object>> Consulta40()
        {
            var consulta = await (
                from grupoDetalles in _context.DetallePedidos
                    .GroupBy(detalle => detalle.CodigoProducto)
                    .Select(grupoDetalles => new
                    {
                        CodigoProducto = grupoDetalles.Key,
                        UnidadesVendidas = grupoDetalles.Sum(detalle => detalle.Cantidad)
                    })
                    .OrderByDescending(resultado => resultado.UnidadesVendidas)
                    .Take(20)
                join producto in _context.Productos
                    on grupoDetalles.CodigoProducto equals producto.CodigoProducto
                select new
                {
                    grupoDetalles.CodigoProducto,
                    producto.Nombre,
                    grupoDetalles.UnidadesVendidas
                })
                .ToListAsync();

            return consulta;
        }


        public async Task<IEnumerable<object>> Consulta42()
        {
            var consulta = await (
                from grupoDetalles in _context.DetallePedidos
                    .GroupBy(detalle => detalle.CodigoProducto)
                    .Where(grupoDetalles => grupoDetalles.Key.StartsWith("OR"))
                    .Select(grupoDetalles => new
                    {
                        CodigoProducto = grupoDetalles.Key,
                        UnidadesVendidas = grupoDetalles.Sum(detalle => detalle.Cantidad)
                    })
                    .OrderByDescending(resultado => resultado.UnidadesVendidas)
                    .Take(20)
                join producto in _context.Productos
                    on grupoDetalles.CodigoProducto equals producto.CodigoProducto
                select new
                {
                    grupoDetalles.CodigoProducto,
                    producto.Nombre,
                    grupoDetalles.UnidadesVendidas
                })
                .GroupBy(resultado => resultado.CodigoProducto)
                .Select(grupoProducto => new
                {
                    CodigoProducto = grupoProducto.Key,
                    Detalles = grupoProducto.ToList()
                })
                .ToListAsync();

            return consulta;
        }

        public async Task<IEnumerable<object>> Consulta43()
        {
            var consulta = await _context.DetallePedidos
                .GroupBy(detallePedido => detallePedido.CodigoProducto)
                .ToListAsync();
            
            var _consulta =  consulta
                .Join(
                    _context.Productos,
                    grupo => grupo.Key,
                    producto => producto.CodigoProducto,
                    (consulta, producto) => new
                    {
                        NombreProducto = producto.Nombre,
                        UnidadesVendidad = consulta.Sum(p => p.Cantidad),
                        TotalFacturado = consulta.Sum(p => p.Cantidad * p.PrecioUnidad),
                        TotalFacturadoIva = consulta.Sum(p => p.Cantidad * p.PrecioUnidad * (decimal)1.21)
                    })
                    .Where(resultado => resultado.TotalFacturadoIva > 3000)
                    .OrderByDescending(resultado => resultado.TotalFacturadoIva)
                    .ToList();

                return _consulta;
            ;
        }
    }
}