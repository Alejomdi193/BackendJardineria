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
    public class ClienteRepository : Generic<Cliente>, ICliente
    {
        private readonly JardineriaContext _context;
        public ClienteRepository(JardineriaContext context) : base(context)
        {
            _context = context;
        }
        public override async Task<IEnumerable<Cliente>> GetAllAsync()
        {
            return await _context.Clientes
                .ToListAsync();
        }

        public override async Task<(int totalRegistros, IEnumerable<Cliente> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
        {
            var query = _context.Clientes.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.NombreCliente.ToLower().Contains(search));
            }

            query = query.OrderBy(p => p.CodigoCliente);
            var totalRegistros = await query.CountAsync();
            var registros = await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (totalRegistros, registros);
        }

        public async Task<IEnumerable<object>> Consulta1()
        {
            var mensaje = "Listado de clientes españoles".ToUpper();

            var consulta = from e in _context.Clientes
                           where e.Pais == "Spain"
                           select e.NombreCliente;

            var nombres = await consulta.ToListAsync();

            var resultado = new List<object>
            {
                new { Informacion = mensaje, Clientes = nombres }
            };

            return resultado;
        }

        public async Task<IEnumerable<object>> Consulta11()
        {
            var mensaje = "Listado de clientes de Madrid cuyo representante de ventas tiene codigo de empleado 11 o 30".ToUpper();

            var consulta = from cliente in _context.Clientes
                           where cliente.Ciudad == "Madrid" &&
                                   (cliente.CodigoEmpleadoRepVentas == 11 || cliente.CodigoEmpleadoRepVentas == 30)
                           select new
                           {
                               cliente.CodigoCliente,
                               cliente.NombreCliente,
                               cliente.NombreContacto,
                               cliente.ApellidoContacto,
                               cliente.Telefono,
                               cliente.Fax,
                               cliente.LineaDireccion1,
                               cliente.LineaDireccion2,
                               cliente.Ciudad,
                               cliente.Region,
                               cliente.Pais,
                               cliente.CodigoPostal,
                               cliente.CodigoEmpleadoRepVentas,
                               cliente.LimiteCredito
                           };

            var clientesMadridRepresentantes = await consulta.ToListAsync();

            var resultado = new List<object>
            {
                new { Informacion = mensaje, Clientes = clientesMadridRepresentantes }
            };

            return resultado;
        }

        public async Task<IEnumerable<object>> Consulta12()
        {
            var mensaje = "Listado con el nombre de cada cliente y el nombre y apellido de su representante de ventas".ToUpper();

            var consulta = from cliente in _context.Clientes
                           join representante in _context.Empleados on cliente.CodigoEmpleadoRepVentas equals representante.CodigoEmpleado
                           select new
                           {
                               cliente.NombreCliente,
                               representante.Nombre,
                               representante.Apellido1
                           };

            var resultado = new List<object>
                {
                    new { Informacion = mensaje, Resultado = await consulta.ToListAsync() }
                };

            return resultado;
        }
        public async Task<IEnumerable<object>> Consulta12Sql2I()
        {
            var mensaje = "Listado con el nombre de cada cliente y el nombre y apellido de su representante de ventas".ToUpper();

            var consulta = from cliente in _context.Clientes
                           join representante in _context.Empleados on cliente.CodigoEmpleadoRepVentas equals representante.CodigoEmpleado
                           select new
                           {
                               cliente.NombreCliente,
                               representante.Nombre,
                               representante.Apellido1
                           };

            var resultado = new List<object>
            {
                new { Informacion = mensaje, Resultado = await consulta.ToListAsync() }
            };

            return resultado;
        }


        public async Task<IEnumerable<object>> Consulta12Sql2N()
        {
            var mensaje = "Listado con el nombre de cada cliente y el nombre y apellido de su representante de ventas".ToUpper();

            var consulta = from cliente in _context.Clientes
                           join representante in _context.Empleados on 1 equals 1 // Dummy condition for NATURAL JOIN
                           select new
                           {
                               cliente.NombreCliente,
                               representante.Nombre,
                               representante.Apellido1
                           };

            var resultado = new List<object>
            {
                new { Informacion = mensaje, Resultado = await consulta.ToListAsync() }
            };

            return resultado;
        }
        public async Task<IEnumerable<object>> Consulta13()
        {
            var mensaje = "Nombre de los clientes que han realizado pagos junto con el nombre de sus representantes de ventas".ToUpper();

            var consulta = from cliente in _context.Clientes
                           join pago in _context.Pagos on cliente.CodigoCliente equals pago.CodigoCliente
                           join representante in _context.Empleados on cliente.CodigoEmpleadoRepVentas equals representante.CodigoEmpleado
                           select new
                           {
                               cliente.NombreCliente,
                               Pagos = new
                               {
                                   pago.CodigoCliente,
                                   pago.FormaPago,
                                   pago.IdTransaccion,
                                   pago.FechaPago,
                                   pago.Total

                               },
                               Representante = new
                               {
                                   representante.Nombre,
                                   representante.Apellido1

                               }
                           };

            var resultado = new List<object>
                {
                    new { Informacion = mensaje, Resultado = await consulta.ToListAsync() }
                };

            return resultado;
        }


        public async Task<IEnumerable<object>> Consulta13Sql2I()
        {
            var mensaje = "Nombre de los clientes que han realizado pagos junto con el nombre de sus representantes de ventas".ToUpper();

            var consulta = from cliente in _context.Clientes
                           join pago in _context.Pagos on cliente.CodigoCliente equals pago.CodigoCliente
                           join representante in _context.Empleados on cliente.CodigoEmpleadoRepVentas equals representante.CodigoEmpleado
                           select new
                           {
                               cliente.NombreCliente,
                               Pagos = new
                               {
                                   pago.CodigoCliente,
                                   pago.FormaPago,
                                   pago.IdTransaccion,
                                   pago.FechaPago,
                                   pago.Total
                               },
                               Representante = new
                               {
                                   representante.Nombre,
                                   representante.Apellido1
                               }
                           };

            var resultado = new List<object>
                {
                    new { Informacion = mensaje, Resultado = await consulta.ToListAsync() }
                };

            return resultado;
        }

        public async Task<IEnumerable<object>> Consulta13Sql2N()
        {
            var mensaje = "Nombre de los clientes que han realizado pagos junto con el nombre de sus representantes de ventas".ToUpper();

            var consulta = from cliente in _context.Clientes
                           join pago in _context.Pagos on cliente.CodigoCliente equals pago.CodigoCliente
                           join representante in _context.Empleados on 1 equals 1
                           select new
                           {
                               cliente.NombreCliente,
                               Pagos = new
                               {
                                   pago.CodigoCliente,
                                   pago.FormaPago,
                                   pago.IdTransaccion,
                                   pago.FechaPago,
                                   pago.Total
                               },
                               Representante = new
                               {
                                   representante.Nombre,
                                   representante.Apellido1
                               }
                           };

            var resultado = new List<object>
        {
            new { Informacion = mensaje, Resultado = await consulta.ToListAsync() }
        };

            return resultado;
        }
        public async Task<IEnumerable<object>> Consulta14()
        {
            var mensaje = "Nombre de los clientes que no han realizado pagos junto con el nombre de sus representantes de ventas".ToUpper();

            var consulta = from cliente in _context.Clientes
                           join representante in _context.Empleados on cliente.CodigoEmpleadoRepVentas equals representante.CodigoEmpleado into representantes
                           from representante in representantes.DefaultIfEmpty()
                           join pago in _context.Pagos on cliente.CodigoCliente equals pago.CodigoCliente into pagos
                           where !pagos.Any()
                           select new
                           {
                               cliente.NombreCliente,
                               Representante = new
                               {
                                   representante.Nombre,
                                   representante.Apellido1
                               }
                           };

            var resultado = new List<object>
            {
                new { Informacion = mensaje, Resultado = await consulta.ToListAsync() }
            };

            return resultado;
        }
        public async Task<IEnumerable<object>> Consulta14Sql2I()
        {
            var mensaje = "Nombre de los clientes que no han realizado pagos junto con el nombre de sus representantes de ventas".ToUpper();

            var consulta = from cliente in _context.Clientes
                           join representante in _context.Empleados on cliente.CodigoEmpleadoRepVentas equals representante.CodigoEmpleado
                           where !_context.Pagos.Any(pago => pago.CodigoCliente == cliente.CodigoCliente)
                           select new
                           {
                               cliente.NombreCliente,
                               Representante = new
                               {
                                   representante.Nombre,
                                   representante.Apellido1
                               }
                           };

            var resultado = new List<object>
        {
            new { Informacion = mensaje, Resultado = await consulta.ToListAsync() }
        };

            return resultado;
        }
        public async Task<IEnumerable<object>> Consulta14Sql2N()
        {
            var mensaje = "Nombre de los clientes que no han realizado pagos junto con el nombre de sus representantes de ventas".ToUpper();

            var consulta = from cliente in _context.Clientes
                           join representante in _context.Empleados on 1 equals 1
                           where !_context.Pagos.Any(pago => pago.CodigoCliente == cliente.CodigoCliente)
                           select new
                           {
                               cliente.NombreCliente,
                               Representante = new
                               {
                                   representante.Nombre,
                                   representante.Apellido1
                               }
                           };

            var resultado = new List<object>
        {
            new { Informacion = mensaje, Resultado = await consulta.ToListAsync() }
        };

            return resultado;
        }
        public async Task<IEnumerable<object>> Consulta15()
        {
            var mensaje = "Nombre de los clientes que han hecho pagos y el nombre de sus representantes junto con la ciudad de la oficina".ToUpper();

            var consulta = from cliente in _context.Clientes
                           join pago in _context.Pagos on cliente.CodigoCliente equals pago.CodigoCliente
                           join representante in _context.Empleados on cliente.CodigoEmpleadoRepVentas equals representante.CodigoEmpleado
                           join oficina in _context.Oficinas on representante.CodigoOficina equals oficina.CodigoOficina
                           select new
                           {
                               cliente.NombreCliente,
                               Representante = new
                               {
                                   representante.Nombre,
                                   representante.Apellido1,
                                   Oficina = new
                                   {
                                       oficina.Ciudad
                                   }
                               }
                           };

            var resultado = new List<object>
    {
        new { Informacion = mensaje, Resultado = await consulta.ToListAsync() }
    };

            return resultado;
        }
        public async Task<IEnumerable<object>> Consulta15Sql2I()
        {
            var mensaje = "Nombre de los clientes que han hecho pagos y el nombre de sus representantes junto con la ciudad de la oficina".ToUpper();

            var consulta = from cliente in _context.Clientes
                           join pago in _context.Pagos on cliente.CodigoCliente equals pago.CodigoCliente
                           join representante in _context.Empleados on cliente.CodigoEmpleadoRepVentas equals representante.CodigoEmpleado
                           join oficina in _context.Oficinas on representante.CodigoOficina equals oficina.CodigoOficina
                           select new
                           {
                               cliente.NombreCliente,
                               Representante = new
                               {
                                   representante.Nombre,
                                   representante.Apellido1,
                                   Oficina = new
                                   {
                                       oficina.Ciudad
                                   }
                               }
                           };

            var resultado = new List<object>
        {
            new { Informacion = mensaje, Resultado = await consulta.ToListAsync() }
        };

            return resultado;
        }


        public async Task<IEnumerable<object>> Consulta15Sql2N()
        {
            var mensaje = "Nombre de los clientes que han hecho pagos y el nombre de sus representantes junto con la ciudad de la oficina".ToUpper();

            var consulta = from cliente in _context.Clientes
                           join representante in _context.Empleados on cliente.CodigoEmpleadoRepVentas equals representante.CodigoEmpleado
                           join pago in _context.Pagos on cliente.CodigoCliente equals pago.CodigoCliente
                           join oficina in _context.Oficinas on representante.CodigoOficina equals oficina.CodigoOficina
                           select new
                           {
                               cliente.NombreCliente,
                               Representante = new
                               {
                                   representante.Nombre,
                                   representante.Apellido1,
                                   Oficina = new
                                   {
                                       oficina.Ciudad
                                   }
                               }
                           };

            var resultado = new List<object>
    {
        new { Informacion = mensaje, Resultado = await consulta.ToListAsync() }
    };

            return resultado;
        }


        public async Task<IEnumerable<object>> Consulta16()
        {
            var mensaje = "Nombre de los clientes que no han hecho pagos y el nombre de sus representantes junto con la ciudad de la oficina".ToUpper();

            var consulta = from cliente in _context.Clientes
                           join representante in _context.Empleados on cliente.CodigoEmpleadoRepVentas equals representante.CodigoEmpleado into representantes
                           from representante in representantes.DefaultIfEmpty()
                           join oficina in _context.Oficinas on representante.CodigoOficina equals oficina.CodigoOficina into oficinas
                           from oficina in oficinas.DefaultIfEmpty()
                           where !_context.Pagos.Any(pago => pago.CodigoCliente == cliente.CodigoCliente)
                           select new
                           {
                               cliente.NombreCliente,
                               Representante = new
                               {
                                   representante.Nombre,
                                   representante.Apellido1,
                                   Oficina = new
                                   {
                                       oficina.Ciudad
                                   }
                               }
                           };

            var resultado = new List<object>
    {
        new { Informacion = mensaje, Resultado = await consulta.ToListAsync() }
    };

            return resultado;
        }
        public async Task<IEnumerable<object>> Consulta16Sql2I()
        {
            var mensaje = "Nombre de los clientes que no han hecho pagos y el nombre de sus representantes junto con la ciudad de la oficina".ToUpper();

            var consulta = from cliente in _context.Clientes
                           join representante in _context.Empleados on cliente.CodigoEmpleadoRepVentas equals representante.CodigoEmpleado
                           join oficina in _context.Oficinas on representante.CodigoOficina equals oficina.CodigoOficina
                           where !_context.Pagos.Any(pago => pago.CodigoCliente == cliente.CodigoCliente)
                           select new
                           {
                               cliente.NombreCliente,
                               Representante = new
                               {
                                   representante.Nombre,
                                   representante.Apellido1,
                                   Oficina = new
                                   {
                                       oficina.Ciudad

                                   }
                               }
                           };

            var resultado = new List<object>
    {
        new { Informacion = mensaje, Resultado = await consulta.ToListAsync() }
    };

            return resultado;
        }

        public async Task<IEnumerable<object>> Consulta16Sql2N()
        {
            var mensaje = "Nombre de los clientes que no han hecho pagos y el nombre de sus representantes junto con la ciudad de la oficina".ToUpper();

            var consulta = from cliente in _context.Clientes
                           join representante in _context.Empleados on cliente.CodigoEmpleadoRepVentas equals representante.CodigoEmpleado
                           join oficina in _context.Oficinas on representante.CodigoOficina equals oficina.CodigoOficina
                           where !_context.Pagos.Any(pago => pago.CodigoCliente == cliente.CodigoCliente)
                           select new
                           {
                               cliente.NombreCliente,
                               Representante = new
                               {
                                   representante.Nombre,
                                   representante.Apellido1,
                                   Oficina = new
                                   {
                                       oficina.Ciudad
                                   }
                               }
                           };

            var resultado = new List<object>
            {
                new { Informacion = mensaje, Resultado = await consulta.ToListAsync() }
            };

            return resultado;
        }

        public async Task<IEnumerable<object>> Consulta18()
        {
            var mensaje = "Clientes a los que no se les ha entregado a tiempo un pedido".ToUpper();

            var consulta = from cliente in _context.Clientes
                           join pedido in _context.Pedidos on cliente.CodigoCliente equals pedido.CodigoCliente
                           where pedido.FechaEntrega == null || pedido.FechaEntrega > pedido.FechaEsperada
                           select new
                           {
                               Cliente = cliente.NombreCliente
                           };

            var resultado = new List<object>
    {
        new { Informacion = mensaje, Resultado = await consulta.ToListAsync() }
    };

            return resultado;
        }

        public async Task<IEnumerable<object>> Consulta18Sql2I()
        {
            var mensaje = "Clientes a los que no se les ha entregado a tiempo un pedido".ToUpper();

            var consulta = from cliente in _context.Clientes
                           join pedido in _context.Pedidos on cliente.CodigoCliente equals pedido.CodigoCliente
                           where pedido.FechaEntrega == null || pedido.FechaEntrega > pedido.FechaEsperada
                           select new
                           {
                               Cliente = cliente.NombreCliente
                           };

            var resultado = new List<object>
    {
        new { Informacion = mensaje, Resultado = await consulta.ToListAsync() }
    };

            return resultado;
        }
        public async Task<IEnumerable<object>> Consulta18Sql2N()
        {
            var mensaje = "Clientes a los que no se les ha entregado a tiempo un pedido".ToUpper();

            var consulta = from cliente in _context.Clientes
                           join pedido in _context.Pedidos on cliente.CodigoCliente equals pedido.CodigoCliente
                           where pedido.FechaEntrega == null || pedido.FechaEntrega > pedido.FechaEsperada
                           select new
                           {
                               Cliente = cliente.NombreCliente
                           };

            var resultado = new List<object>
    {
        new { Informacion = mensaje, Resultado = await consulta.ToListAsync() }
    };

            return resultado;
        }


        public async Task<IEnumerable<object>> Consulta19()
        {
            var mensaje = "Diferentes gamas de producto compradas por cada cliente".ToUpper();

            var consulta = from cliente in _context.Clientes
                           join pedido in _context.Pedidos on cliente.CodigoCliente equals pedido.CodigoCliente
                           join detalle in _context.DetallePedidos on pedido.CodigoPedido equals detalle.CodigoPedido
                           join producto in _context.Productos on detalle.CodigoProducto equals producto.CodigoProducto
                           join gama in _context.GamaProductos on producto.Gama equals gama.Gama
                           select new
                           {
                               Cliente = cliente.NombreCliente,
                               GamaProducto = gama.Gama
                           };

            var resultado = new List<object>
        {
            new { Informacion = mensaje, Resultado = await consulta.ToListAsync() }
        };

            return resultado;
        }
        public async Task<IEnumerable<object>> Consulta19Sql2I()
        {
            var mensaje = "Diferentes gamas de producto compradas por cada cliente".ToUpper();

            var consulta = from cliente in _context.Clientes
                           join pedido in _context.Pedidos on cliente.CodigoCliente equals pedido.CodigoCliente
                           join detalle in _context.DetallePedidos on pedido.CodigoPedido equals detalle.CodigoPedido
                           join producto in _context.Productos on detalle.CodigoProducto equals producto.CodigoProducto
                           join gama in _context.GamaProductos on producto.Gama equals gama.Gama
                           select new
                           {
                               Cliente = cliente.NombreCliente,
                               GamaProducto = gama.Gama
                           };

            var resultado = new List<object>
    {
        new { Informacion = mensaje, Resultado = await consulta.ToListAsync() }
    };

            return resultado;
        }

        public async Task<IEnumerable<object>> Consulta19Sql2N()
        {
            var mensaje = "Diferentes gamas de producto compradas por cada cliente".ToUpper();

            var consulta = from cliente in _context.Clientes
                           join pedido in _context.Pedidos on cliente.CodigoCliente equals pedido.CodigoCliente
                           join detalle in _context.DetallePedidos on pedido.CodigoPedido equals detalle.CodigoPedido
                           join producto in _context.Productos on detalle.CodigoProducto equals producto.CodigoProducto
                           join gama in _context.GamaProductos on producto.Gama equals gama.Gama
                           select new
                           {
                               Cliente = cliente.NombreCliente,
                               GamaProducto = gama.Gama
                           };

            var resultado = new List<object>
    {
        new { Informacion = mensaje, Resultado = await consulta.ToListAsync() }
    };

            return resultado;
        }





        public async Task<IEnumerable<object>> Consulta20Left()
        {
            var mensaje = "Clientes que no han realizado ningún pago (LEFT JOIN)".ToUpper();

            var consulta = from cliente in _context.Clientes
                           join pago in _context.Pagos on cliente.CodigoCliente equals pago.CodigoCliente into pagosJoin
                           from pago in pagosJoin.DefaultIfEmpty()
                           where pago == null
                           select new
                           {
                               Cliente = cliente.NombreCliente
                           };

            var resultado = new List<object>
    {
        new { Informacion = mensaje, Resultado = await consulta.ToListAsync() }
    };

            return resultado;
        }

        public async Task<IEnumerable<object>> Consulta20Right()
        {
            var mensaje = "Clientes que no han realizado ningún pago (RIGHT JOIN)".ToUpper();

            var consulta = from cliente in _context.Clientes
                           join pago in _context.Pagos on cliente.CodigoCliente equals pago.CodigoCliente into clientesJoin
                           from pago in clientesJoin.DefaultIfEmpty()
                           where pago == null
                           select new
                           {
                               Cliente = cliente.NombreCliente
                           };

            var resultado = new List<object>
    {
        new { Informacion = mensaje, Resultado = await consulta.ToListAsync() }
    };

            return resultado;
        }

        public async Task<IEnumerable<object>> Consulta20NaturalLeft()
        {
            var mensaje = "Clientes que no han realizado ningún pago (NATURAL LEFT JOIN)".ToUpper();

            var consulta = from cliente in _context.Clientes
                           join pago in _context.Pagos on cliente.CodigoCliente equals pago.CodigoCliente into pagosJoin
                           from pago in pagosJoin.DefaultIfEmpty()
                           where pago == null
                           select new
                           {
                               Cliente = cliente.NombreCliente
                           };

            var resultado = new List<object>
    {
        new { Informacion = mensaje, Resultado = await consulta.ToListAsync() }
    };

            return resultado;
        }
        public async Task<IEnumerable<object>> Consulta20NaturalRight()
        {
            var mensaje = "Clientes que no han realizado ningún pago (NATURAL RIGHT JOIN)".ToUpper();

            var consulta = from cliente in _context.Clientes
                           join pago in _context.Pagos on cliente.CodigoCliente equals pago.CodigoCliente into clientesJoin
                           from pago in clientesJoin.DefaultIfEmpty()
                           where pago == null
                           select new
                           {
                               Cliente = cliente.NombreCliente
                           };

            var resultado = new List<object>
    {
        new { Informacion = mensaje, Resultado = await consulta.ToListAsync() }
    };

            return resultado;
        }


        public async Task<IEnumerable<object>> Consulta21Left()
        {
            var mensaje = "Clientes sin pagos (LEFT JOIN)".ToUpper();

            var consulta = from cliente in _context.Clientes
                           join pago in _context.Pagos on cliente.CodigoCliente equals pago.CodigoCliente into pagosJoin
                           from pago in pagosJoin.DefaultIfEmpty()
                           where pago == null
                           select new
                           {
                               Cliente = cliente.NombreCliente,
                               TienePago = pago != null
                           };

            var resultado = new List<object>
    {
        new { Informacion = mensaje, Resultado = await consulta.ToListAsync() }
    };

            return resultado;
        }


        public async Task<IEnumerable<object>> Consulta21Right()
        {
            var mensaje = "Clientes sin pedidos (RIGHT JOIN)".ToUpper();

            var consulta = from cliente in _context.Clientes
                           join pedido in _context.Pedidos on cliente.CodigoCliente equals pedido.CodigoCliente into pedidosJoin
                           from pedido in pedidosJoin.DefaultIfEmpty()
                           where pedido == null
                           select new
                           {
                               Cliente = cliente.NombreCliente,
                               TienePedido = pedido != null
                           };

            var resultado = new List<object>
        {
            new { Informacion = mensaje, Resultado = await consulta.ToListAsync() }
        };

            return resultado;
        }



        public async Task<IEnumerable<object>> Consulta21NaturalLeft()
        {
            var mensaje = "Clientes sin pagos (NATURAL LEFT JOIN)".ToUpper();

            var consulta = from cliente in _context.Clientes
                           join pago in _context.Pagos on cliente.CodigoCliente equals pago.CodigoCliente into pagosJoin
                           from pago in pagosJoin.DefaultIfEmpty()
                           where pago == null
                           select new
                           {
                               Cliente = cliente.NombreCliente,
                               TienePago = pago != null
                           };

            var resultado = new List<object>
    {
        new { Informacion = mensaje, Resultado = await consulta.ToListAsync() }
    };

            return resultado;
        }


        public async Task<IEnumerable<object>> Consulta21NaturalRight()
        {
            var mensaje = "Clientes sin pedidos (NATURAL RIGHT JOIN)".ToUpper();

            var consulta = from cliente in _context.Clientes
                           join pedido in _context.Pedidos on cliente.CodigoCliente equals pedido.CodigoCliente into pedidosJoin
                           from pedido in pedidosJoin.DefaultIfEmpty()
                           where pedido == null
                           select new
                           {
                               Cliente = cliente.NombreCliente,
                               TienePedido = pedido != null
                           };

            var resultado = new List<object>
    {
        new { Informacion = mensaje, Resultado = await consulta.ToListAsync() }
    };

            return resultado;
        }



        public async Task<IEnumerable<object>> Consulta22Left()
        {
            var mensaje = "Empleados sin clientes (LEFT JOIN)".ToUpper();

            var consulta = from empleado in _context.Empleados
                           join cliente in _context.Clientes on empleado.CodigoEmpleado equals cliente.CodigoEmpleadoRepVentas into clientesJoin
                           from cliente in clientesJoin.DefaultIfEmpty()
                           where cliente == null
                           select new
                           {
                               Empleado = empleado.Nombre,
                               Oficina = empleado.CodigoOficinaNavigation.Ciudad,
                               TieneCliente = cliente != null
                           };

            var resultado = new List<object>
    {
        new { Informacion = mensaje, Resultado = await consulta.ToListAsync() }
    };

            return resultado;
        }


        // RIGHT JOIN para empleados sin clientes
        public async Task<IEnumerable<object>> Consulta22Right()
        {
            var mensaje = "Empleados sin clientes (RIGHT JOIN)".ToUpper();

            var consulta = from empleado in _context.Empleados
                           join cliente in _context.Clientes on empleado.CodigoEmpleado equals cliente.CodigoEmpleadoRepVentas into empleadosJoin
                           from cliente in empleadosJoin.DefaultIfEmpty()
                           where cliente == null
                           select new
                           {
                               Empleado = empleado.Nombre,
                               Oficina = empleado.CodigoOficinaNavigation.Ciudad,
                               TieneCliente = cliente != null
                           };

            var resultado = new List<object>
    {
        new { Informacion = mensaje, Resultado = await consulta.ToListAsync() }
    };

            return resultado;
        }


        public async Task<IEnumerable<object>> Consulta22NaturalLeft()
        {
            var mensaje = "Empleados sin clientes (NATURAL LEFT JOIN)".ToUpper();

            var consulta = from empleado in _context.Empleados
                           join cliente in _context.Clientes on empleado.CodigoEmpleado equals cliente.CodigoEmpleadoRepVentas into clientesJoin
                           from cliente in clientesJoin.DefaultIfEmpty()
                           where cliente == null
                           select new
                           {
                               Empleado = empleado.Nombre,
                               Oficina = empleado.CodigoOficinaNavigation.Ciudad,
                               TieneCliente = cliente != null
                           };

            var resultado = new List<object>
    {
        new { Informacion = mensaje, Resultado = await consulta.ToListAsync() }
    };

            return resultado;
        }


        public async Task<IEnumerable<object>> Consulta22NaturalRight()
        {
            var mensaje = "Empleados sin clientes (NATURAL RIGHT JOIN)".ToUpper();

            var consulta = from empleado in _context.Empleados
                           join cliente in _context.Clientes on empleado.CodigoEmpleado equals cliente.CodigoEmpleadoRepVentas into clientesJoin
                           from cliente in clientesJoin.DefaultIfEmpty()
                           where cliente == null
                           select new
                           {
                               Empleado = empleado.Nombre,
                               Oficina = empleado.CodigoOficinaNavigation.Ciudad,
                               TieneCliente = cliente != null
                           };

            var resultado = new List<object>
    {
        new { Informacion = mensaje, Resultado = await consulta.ToListAsync() }
    };

            return resultado;
        }


        public async Task<IEnumerable<object>> Consulta27Left()
        {
            var mensaje = "Clientes con pedidos pero sin pagos (LEFT JOIN)".ToUpper();

            var consulta = from cliente in _context.Clientes
                           join pedido in _context.Pedidos on cliente.CodigoCliente equals pedido.CodigoCliente into pedidosJoin
                           from pedido in pedidosJoin.DefaultIfEmpty()
                           join pago in _context.Pagos on cliente.CodigoCliente equals pago.CodigoCliente into pagosJoin
                           from pago in pagosJoin.DefaultIfEmpty()
                           where pago == null
                           select new
                           {
                               Cliente = cliente.NombreCliente
                           };

            var resultado = new List<object>
    {
        new { Informacion = mensaje, Resultado = await consulta.ToListAsync() }
    };

            return resultado;
        }

        public async Task<IEnumerable<object>> Consulta27Right()
        {
            var mensaje = "Clientes con pedidos pero sin pagos (RIGHT JOIN)".ToUpper();

            var consulta = from cliente in _context.Clientes
                           join pedido in _context.Pedidos on cliente.CodigoCliente equals pedido.CodigoCliente into pedidosJoin
                           from pedido in pedidosJoin.DefaultIfEmpty()
                           where pedido == null
                           select new
                           {
                               Cliente = cliente.NombreCliente
                           };

            var resultado = new List<object>
    {
        new { Informacion = mensaje, Resultado = await consulta.ToListAsync() }
    };

            return resultado;
        }


        public async Task<IEnumerable<object>> Consulta27NaturalLeft()
        {
            var mensaje = "Clientes con pedidos pero sin pagos (NATURAL LEFT JOIN)".ToUpper();

            var consulta = from cliente in _context.Clientes
                           join pago in _context.Pagos on cliente.CodigoCliente equals pago.CodigoCliente into pagosJoin
                           from pago in pagosJoin.DefaultIfEmpty()
                           where pago == null
                           select new
                           {
                               Cliente = cliente.NombreCliente
                           };

            var resultado = new List<object>
    {
        new { Informacion = mensaje, Resultado = await consulta.ToListAsync() }
    };

            return resultado;
        }

        public async Task<IEnumerable<object>> Consulta27NaturalRight()
        {
            var mensaje = "Clientes con pedidos pero sin pagos (NATURAL RIGHT JOIN)".ToUpper();

            var consulta = from pago in _context.Pagos
                           join cliente in _context.Clientes on pago.CodigoCliente equals cliente.CodigoCliente into clientesJoin
                           from cliente in clientesJoin.DefaultIfEmpty()
                           where cliente == null
                           select new
                           {
                               Cliente = pago.CodigoClienteNavigation.NombreCliente
                           };

            var resultado = new List<object>
    {
        new { Informacion = mensaje, Resultado = await consulta.ToListAsync() }
    };

            return resultado;
        }
        public async Task<int> Consulta33()
        {
            var clientesEnMadrid = await _context.Clientes
                .Where(c => c.Ciudad == "Madrid")
                .CountAsync();

            return clientesEnMadrid;
        }
        public async Task<IEnumerable<object>> Consulta34()
        {
            var clientesPorCiudadM = await _context.Clientes
                .Where(c => c.Ciudad.StartsWith("M"))
                .GroupBy(c => c.Ciudad)
                .Select(g => new
                {
                    Ciudad = g.Key,
                    CantidadClientes = g.Count()
                })
                .ToListAsync();

            return clientesPorCiudadM;
        }
        public async Task<int> Consulta36()
        {
            var clientesSinRepresentante = await _context.Clientes
                .CountAsync(c => c.CodigoEmpleadoRepVentas == null);

            return clientesSinRepresentante;
        }

        public async Task<IEnumerable<object>> Consulta37()
        {
            var consulta = from cliente in _context.Clientes
                           join pago in _context.Pagos on cliente.CodigoCliente equals pago.CodigoCliente into pagosJoin
                           from pago in pagosJoin.DefaultIfEmpty()
                           where cliente != null && pago != null
                           group pago by new { cliente.NombreCliente, cliente.NombreContacto, cliente.ApellidoContacto } into grupoPagos
                           select new
                           {
                               Cliente = grupoPagos.Key.NombreCliente,
                               NombreContacto = grupoPagos.Key.NombreContacto,
                               ApellidoContacto = grupoPagos.Key.ApellidoContacto,
                               PrimerPago = grupoPagos.Min(p => p.FechaPago),
                               UltimoPago = grupoPagos.Max(p => p.FechaPago)
                           };

            var resultado = await consulta.ToListAsync();

            return resultado;
        }
        public async Task<string> Consulta45()
        {
            var cliente = await _context.Clientes
                .OrderByDescending(c => c.LimiteCredito)
                .Select(c => c.NombreCliente)
                .FirstOrDefaultAsync();

            return cliente ?? "No hay clientes";
        }
        public async Task<IEnumerable<object>> Consulta48()
        {
            var clientes = await _context.Clientes
                .Where(c => c.LimiteCredito > c.Pagos.Sum(p => p.Total))
                .ToListAsync();

            return clientes;
        }

        public async Task<string> Consulta49()
        {
            var nombreCliente = await _context.Clientes
                .Where(c => _context.Clientes.All(x => x.LimiteCredito <= c.LimiteCredito))
                .Select(c => c.NombreCliente)
                .FirstOrDefaultAsync();

            return nombreCliente;
        }

        public async Task<IEnumerable<object>> Consulta51()
        {
            var clientesSinPagos = await _context.Clientes
                .Where(c => !_context.Pagos.Select(p => p.CodigoCliente).Contains(c.CodigoCliente))
                .ToListAsync();

            return clientesSinPagos;
        }
        public async Task<IEnumerable<object>> Consulta52()
        {
            var clientesConPagos = await _context.Clientes
                .Where(c => _context.Pagos.Select(p => p.CodigoCliente).Contains(c.CodigoCliente))
                .ToListAsync();

            return clientesConPagos;
        }

        public async Task<IEnumerable<object>> Consulta55()
        {
            var clientesSinPagos = await _context.Clientes
                .Where(cliente => !_context.Pagos.Any(pago => pago.CodigoCliente == cliente.CodigoCliente))
                .Select(cliente => new
                {
                    NombreCliente = cliente.NombreCliente,
                    ApellidoContacto = cliente.ApellidoContacto,
                    Telefono = cliente.Telefono
                })
                .ToListAsync();

            return clientesSinPagos;
        }
        public async Task<IEnumerable<object>> Consulta56()
        {
            var clientesConPagos = await _context.Clientes
                .Where(cliente => _context.Pagos.Any(pago => pago.CodigoCliente == cliente.CodigoCliente))
                .Select(cliente => new
                {
                    NombreCliente = cliente.NombreCliente,
                    ApellidoContacto = cliente.ApellidoContacto,
                    Telefono = cliente.Telefono
                })
                .ToListAsync();

            return clientesConPagos;
        }
        public async Task<IEnumerable<object>> Consulta57()
        {
            var clientesConPedidos = await _context.Clientes
                .Select(cliente => new
                {
                    NombreCliente = cliente.NombreCliente,
                    PedidosRealizados = _context.Pedidos.Count(pedido => pedido.CodigoCliente == cliente.CodigoCliente)
                })
                .ToListAsync();

            return clientesConPedidos;
        }

        public async Task<IEnumerable<string>> Consulta58()
        {
            var clientesEn2008 = await _context.Clientes
                .Where(cliente => cliente.Pedidos.Any(pedido => pedido.FechaPedido.Year == 2008))
                .OrderBy(cliente => cliente.NombreCliente)
                .Select(cliente => cliente.NombreCliente)
                .ToListAsync();

            return clientesEn2008;
        }
        public async Task<IEnumerable<object>> Consulta59()
        {
            var clientesSinPagos = await _context.Clientes
                .Where(cliente => !_context.Pagos.Any(pago => pago.CodigoCliente == cliente.CodigoCliente))
                .Select(cliente => new
                {
                    NombreCliente = cliente.NombreCliente,
                    NombreRepresentante = cliente.CodigoEmpleadoRepVentasNavigation.Nombre,
                    ApellidoRepresentante = cliente.CodigoEmpleadoRepVentasNavigation.Apellido1,
                    TelefonoOficinaRepresentante = _context.Empleados
                        .Where(e => e.CodigoEmpleado == cliente.CodigoEmpleadoRepVentas)
                        .Select(e => e.CodigoOficinaNavigation.Telefono)
                        .FirstOrDefault()
                })
                .ToListAsync();

            return clientesSinPagos;
        }

        public async Task<IEnumerable<object>> Consulta60()
        {
            var clientesConInformacion = await _context.Clientes
                .Select(cliente => new
                {
                    NombreCliente = cliente.NombreCliente,
                    NombreRepresentante = cliente.CodigoEmpleadoRepVentasNavigation.Nombre,
                    ApellidoRepresentante = cliente.CodigoEmpleadoRepVentasNavigation.Apellido1,
                    CiudadOficinaRepresentante = cliente.CodigoEmpleadoRepVentasNavigation.CodigoOficinaNavigation.Ciudad
                })
                .ToListAsync();

            return clientesConInformacion;
        }
    }
}