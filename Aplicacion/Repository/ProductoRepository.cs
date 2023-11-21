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
    public class ProductoRepository : Generic<Producto>, IProducto
    {
        private readonly JardineriaContext _context;
        public ProductoRepository(JardineriaContext context) : base(context)
        {
            _context = context;
        }
        public override async Task<IEnumerable<Producto>> GetAllAsync()
        {
            return await _context.Productos
                .ToListAsync();
        }

        public override async Task<Producto> GetByIdAsync(string codigoProducto)
        {
            return await _context.Productos
                .FirstOrDefaultAsync(p => p.CodigoProducto == codigoProducto);
        }

        public override async Task<(int totalRegistros, IEnumerable<Producto> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
        {
            var query = _context.Productos as IQueryable<Producto>;

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.Nombre.ToLower().Contains(search));
            }

            query = query.OrderBy(p => p.CodigoProducto);
            var totalRegistros = await query.CountAsync();
            var registros = await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (totalRegistros, registros);
        }

        public async Task<IEnumerable<object>> Consulta10()
        {
            var mensaje = "Listado de productos Ornamentales con mas de 100 unidades en stock. ordenados por precio de venta de mayor a menor".ToUpper();

            var consulta = from producto in _context.Productos
                           where producto.Gama == "Ornamentales" && producto.CantidadEnStock > 100
                           orderby producto.PrecioVenta descending
                           select new
                           {
                               producto.CodigoProducto,
                               producto.Nombre,
                               producto.Gama,
                               producto.Dimensiones,
                               producto.Proveedor,
                               producto.Descripcion,
                               producto.CantidadEnStock,
                               producto.PrecioVenta,
                               producto.PrecioProveedor
                           };

            var productosOrnamentalesStockMayor100 = await consulta.ToListAsync();

            var resultado = new List<object>
            {
                new { Informacion = mensaje, Productos = productosOrnamentalesStockMayor100 }
            };

            return resultado;
        }


        public async Task<IEnumerable<object>> Consulta24Left()
        {
            var mensaje = "Productos que nunca han aparecido en un pedido (LEFT JOIN)".ToUpper();

            var consulta = from producto in _context.Productos
                           join detallePedido in _context.DetallePedidos on producto.CodigoProducto equals detallePedido.CodigoProducto into detallesPedidosJoin
                           from detallePedido in detallesPedidosJoin.DefaultIfEmpty()
                           where detallePedido == null
                           select new
                           {
                               Producto = producto.Nombre
                           };

            var resultado = new List<object>
    {
        new { Informacion = mensaje, Resultado = await consulta.ToListAsync() }
    };

            return resultado;
        }

        public async Task<IEnumerable<object>> Consulta24Right()
        {
            var mensaje = "Productos que nunca han aparecido en un pedido (RIGHT JOIN)".ToUpper();

            var consulta = from producto in _context.Productos
                           join detallePedido in _context.DetallePedidos on producto.CodigoProducto equals detallePedido.CodigoProducto into detallesPedidosJoin
                           from detallePedido in detallesPedidosJoin.DefaultIfEmpty()
                           where detallePedido == null
                           select new
                           {
                               Producto = producto.Nombre
                           };

            var resultado = new List<object>
            {
                new { Informacion = mensaje, Resultado = await consulta.ToListAsync() }
            };

            return resultado;
        }

        public async Task<IEnumerable<object>> Consulta24NaturalLeft()
        {
            var mensaje = "Productos que nunca han aparecido en un pedido (NATURAL LEFT JOIN)".ToUpper();

            var consulta = from producto in _context.Productos
                           join detallePedido in _context.DetallePedidos on producto.CodigoProducto equals detallePedido.CodigoProducto into detallesPedidosJoin
                           from detallePedido in detallesPedidosJoin.DefaultIfEmpty()
                           where detallePedido == null
                           select new
                           {
                               Producto = producto.Nombre
                           };

            var resultado = new List<object>
    {
        new { Informacion = mensaje, Resultado = await consulta.ToListAsync() }
    };

            return resultado;
        }

        public async Task<IEnumerable<object>> Consulta24NaturalRight()
        {
            var mensaje = "Productos que nunca han aparecido en un pedido (NATURAL RIGHT JOIN)".ToUpper();

            var consulta = from producto in _context.Productos
                           join detallePedido in _context.DetallePedidos on producto.CodigoProducto equals detallePedido.CodigoProducto into detallesPedidosJoin
                           from detallePedido in detallesPedidosJoin.DefaultIfEmpty()
                           where detallePedido == null
                           select new
                           {
                               Producto = producto.Nombre
                           };

            var resultado = new List<object>
    {
        new { Informacion = mensaje, Resultado = await consulta.ToListAsync() }
    };

            return resultado;
        }

       
        public async Task<IEnumerable<object>> Consulta25Left()
        {
            var mensaje = "Productos que nunca han aparecido en un pedido (LEFT JOIN)".ToUpper();

            var consulta = from producto in _context.Productos
                           join detallePedido in _context.DetallePedidos on producto.CodigoProducto equals detallePedido.CodigoProducto into detallesPedidosJoin
                           from detallePedido in detallesPedidosJoin.DefaultIfEmpty()
                           where detallePedido == null
                           select new
                           {
                               Producto = new
                               {
                                   Nombre = producto.Nombre,
                                   Descripcion = producto.Descripcion,
                                   Imagen = producto.GamaNavigation.Image  // Utilizamos la propiedad Image de GamaProducto
                               }
                           };

            var resultado = new List<object>
    {
        new { Informacion = mensaje, Resultado = await consulta.ToListAsync() }
    };

            return resultado;
        }

        // RIGHT JOIN
        public async Task<IEnumerable<object>> Consulta25Right()
        {
            var mensaje = "Productos que nunca han aparecido en un pedido (RIGHT JOIN)".ToUpper();

            var consulta = from producto in _context.Productos
                           join detallePedido in _context.DetallePedidos on producto.CodigoProducto equals detallePedido.CodigoProducto into detallesPedidosJoin
                           from detallePedido in detallesPedidosJoin.DefaultIfEmpty()
                           where detallePedido == null
                           select new
                           {
                               Producto = new
                               {
                                   Nombre = producto.Nombre,
                                   Descripcion = producto.Descripcion,
                                   Imagen = producto.GamaNavigation.Image  // Utilizamos la propiedad Image de GamaProducto
                               }
                           };

            var resultado = new List<object>
    {
        new { Informacion = mensaje, Resultado = await consulta.ToListAsync() }
    };

            return resultado;
        }




        // NATURAL LEFT JOIN
        public async Task<IEnumerable<object>> Consulta25NaturalLeft()
        {
            var mensaje = "Productos que nunca han aparecido en un pedido (NATURAL LEFT JOIN)".ToUpper();

            var consulta = from producto in _context.Productos
                           join detallePedido in _context.DetallePedidos on producto.CodigoProducto equals detallePedido.CodigoProducto into detallesPedidosJoin
                           from detallePedido in detallesPedidosJoin.DefaultIfEmpty()
                           where detallePedido == null
                           select new
                           {
                               Producto = new
                               {
                                   Nombre = producto.Nombre,
                                   Descripcion = producto.Descripcion,
                                   Imagen = producto.GamaNavigation.Image  // Utilizamos la propiedad Image de GamaProducto
                               }
                           };

            var resultado = new List<object>
    {
        new { Informacion = mensaje, Resultado = await consulta.ToListAsync() }
    };

            return resultado;
        }

        // NATURAL RIGHT JOIN
        public async Task<IEnumerable<object>> Consulta25NaturalRight()
        {
            var mensaje = "Productos que nunca han aparecido en un pedido (NATURAL RIGHT JOIN)".ToUpper();

            var consulta = from producto in _context.Productos
                           join detallePedido in _context.DetallePedidos on producto.CodigoProducto equals detallePedido.CodigoProducto into detallesPedidosJoin
                           from detallePedido in detallesPedidosJoin.DefaultIfEmpty()
                           where detallePedido == null
                           select new
                           {
                               Producto = new
                               {
                                   Nombre = producto.Nombre,
                                   Descripcion = producto.Descripcion,
                                   Imagen = producto.GamaNavigation.Image  // Utilizamos la propiedad Image de GamaProducto
                               }
                           };

            var resultado = new List<object>
    {
        new { Informacion = mensaje, Resultado = await consulta.ToListAsync() }
    };

            return resultado;
        }
        public async Task<string> Consulta46()
        {
            var nombreProducto = await _context.Productos
                .OrderByDescending(p => p.PrecioVenta)
                .Select(p => p.Nombre)
                .FirstOrDefaultAsync();

            return nombreProducto ?? "No hay productos";
        }

        public async Task<string> Consulta47()
        {
            var nombreProducto = await _context.Productos
                .OrderByDescending(p => p.DetallePedidos.Sum(dp => dp.Cantidad))
                .Select(p => p.Nombre)
                .FirstOrDefaultAsync();

            return nombreProducto ?? "No hay productos vendidos";
        }
        public async Task<string> Consulta50()
        {
            var nombreProducto = await _context.Productos
                .Where(p => !_context.Productos.Any(x => x.PrecioVenta > p.PrecioVenta))
                .Select(p => p.Nombre)
                .FirstOrDefaultAsync();

            return nombreProducto;
        }

        public async Task<IEnumerable<Producto>> Consulta53()
        {
            var productosSinPedidos = await _context.Productos
                .Where(p => !_context.DetallePedidos.Select(dp => dp.CodigoProducto).Contains(p.CodigoProducto))
                .ToListAsync();

            return productosSinPedidos;
        }


    }
}