using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Persistencia;

namespace Aplicacion.Repository
{
    public class EmpleadoRepository : Generic<Empleado>, IEmpleado
    {
        private readonly JardineriaContext _context;
        public EmpleadoRepository(JardineriaContext context) : base(context)
        {
            _context = context;
        }
        public override async Task<IEnumerable<Empleado>> GetAllAsync()
        {
            return await _context.Empleados
                .ToListAsync();
        }

        public override async Task<Empleado> GetByIdAsync(int codigoEmpleado)
        {
            return await _context.Empleados
                .FirstOrDefaultAsync(p => p.CodigoEmpleado == codigoEmpleado);
        }

        public override async Task<(int totalRegistros, IEnumerable<Empleado> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
        {
            var query = _context.Empleados as IQueryable<Empleado>;

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.Nombre.ToLower().Contains(search));
            }

            query = query.OrderBy(p => p.CodigoEmpleado);
            var totalRegistros = await query.CountAsync();
            var registros = await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (totalRegistros, registros);
        }

        public async Task<IEnumerable<object>> Consulta17()
        {
            var mensaje = "Listado de empleados, nombre de su jefe y nombre del jefe de su jefe".ToUpper();

            var consulta = from empleado in _context.Empleados
                           join jefe in _context.Empleados on empleado.CodigoJefe equals jefe.CodigoEmpleado into jefeJoin
                           from jefe in jefeJoin.DefaultIfEmpty()
                           join jefeDelJefe in _context.Empleados on jefe.CodigoJefe equals jefeDelJefe.CodigoEmpleado into jefeDelJefeJoin
                           from jefeDelJefe in jefeDelJefeJoin.DefaultIfEmpty()
                           where empleado.CodigoJefe != null && jefe.CodigoJefe != null
                           select new
                           {
                               Empleado = empleado.Nombre,
                               Jefe = jefe != null ? jefe.Nombre : null,
                               JefeDelJefe = jefeDelJefe != null ? jefeDelJefe.Nombre : null
                           };

            var resultado = new List<object>
    {
        new { Informacion = mensaje, Resultado = await consulta.ToListAsync() }
    };

            return resultado;
        }

        public async Task<IEnumerable<object>> Consulta17Sql2I()
        {
            var mensaje = "Listado de empleados, nombre de su jefe y nombre del jefe de su jefe".ToUpper();

            var consulta = from empleado in _context.Empleados
                           join jefe in _context.Empleados on empleado.CodigoJefe equals jefe.CodigoEmpleado
                           join jefeDelJefe in _context.Empleados on jefe.CodigoJefe equals jefeDelJefe.CodigoEmpleado
                           select new
                           {
                               Empleado = empleado.Nombre,
                               Jefe = jefe.Nombre,
                               JefeDelJefe = jefeDelJefe.Nombre
                           };

            var resultado = new List<object>
    {
        new { Informacion = mensaje, Resultado = await consulta.ToListAsync() }
    };

            return resultado;
        }
        public async Task<IEnumerable<object>> Consulta17Sql2N()
        {
            var mensaje = "Listado de empleados, nombre de su jefe y nombre del jefe de su jefe".ToUpper();

            var consulta = from empleado in _context.Empleados
                           join jefe in _context.Empleados on 1 equals 1
                           join jefeDelJefe in _context.Empleados on 1 equals 1
                           where empleado.CodigoJefe == jefe.CodigoEmpleado && jefe.CodigoJefe == jefeDelJefe.CodigoEmpleado
                           select new
                           {
                               Empleado = empleado.Nombre,
                               Jefe = jefe.Nombre,
                               JefeDelJefe = jefeDelJefe.Nombre
                           };

            var resultado = new List<object>
    {
        new { Informacion = mensaje, Resultado = await consulta.ToListAsync() }
    };

            return resultado;
        }



        
        public async Task<IEnumerable<object>> Consulta23Left()
        {
            var mensaje = "Empleados sin oficina (LEFT JOIN)".ToUpper();

            var consulta = from empleado in _context.Empleados
                           join oficina in _context.Oficinas on empleado.CodigoOficina equals oficina.CodigoOficina into oficinasJoin
                           from oficina in oficinasJoin.DefaultIfEmpty()
                           where oficina == null
                           select new
                           {
                               Empleado = empleado.Nombre,
                               TieneOficina = oficina != null
                           };

            var resultado = new List<object>
    {
        new { Informacion = mensaje, Resultado = await consulta.ToListAsync() }
    };

            return resultado;
        }

       
        public async Task<IEnumerable<object>> Consulta23Right()
        {
            var mensaje = "Empleados sin cliente (RIGHT JOIN)".ToUpper();

            var consulta = from empleado in _context.Empleados
                           join cliente in _context.Clientes on empleado.CodigoEmpleado equals cliente.CodigoEmpleadoRepVentas into clientesJoin
                           from cliente in clientesJoin.DefaultIfEmpty()
                           where cliente == null
                           select new
                           {
                               Empleado = empleado.Nombre,
                               TieneCliente = cliente != null
                           };

            var resultado = new List<object>
    {
        new { Informacion = mensaje, Resultado = await consulta.ToListAsync() }
    };

            return resultado;
        }

        // NATURAL LEFT JOIN: Empleados sin oficina
        public async Task<IEnumerable<object>> Consulta23NaturalLeft()
        {
            var mensaje = "Empleados sin oficina (NATURAL LEFT JOIN)".ToUpper();

            var consulta = from empleado in _context.Empleados
                           join oficina in _context.Oficinas on empleado.CodigoOficina equals oficina.CodigoOficina into oficinasJoin
                           from oficina in oficinasJoin.DefaultIfEmpty()
                           where oficina == null
                           select new
                           {
                               Empleado = empleado.Nombre,
                               TieneOficina = oficina != null
                           };

            var resultado = new List<object>
    {
        new { Informacion = mensaje, Resultado = await consulta.ToListAsync() }
    };

            return resultado;
        }


        public async Task<IEnumerable<object>> Consulta23NaturalRight()
        {
            var mensaje = "Empleados sin cliente (NATURAL RIGHT JOIN)".ToUpper();

            var consulta = from empleado in _context.Empleados
                           join cliente in _context.Clientes on empleado.CodigoEmpleado equals cliente.CodigoEmpleadoRepVentas into clientesJoin
                           from cliente in clientesJoin.DefaultIfEmpty()
                           where cliente == null
                           select new
                           {
                               Empleado = empleado.Nombre,
                               TieneCliente = cliente != null
                           };

            var resultado = new List<object>
        {
            new { Informacion = mensaje, Resultado = await consulta.ToListAsync() }
        };

            return resultado;
        }


        public async Task<IEnumerable<object>> Consulta26Right()
        {
            var mensaje = "Oficinas sin empleados representantes de clientes que han comprado productos de la gama Frutales (RIGHT JOIN)".ToUpper();

            var consulta = from empleado in _context.Empleados
                           join oficina in _context.Oficinas on empleado.CodigoOficina equals oficina.CodigoOficina into oficinasJoin
                           from oficina in oficinasJoin.DefaultIfEmpty()
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


        public async Task<IEnumerable<object>> Consulta26NaturalRight()
        {
            var mensaje = "Oficinas sin empleados representantes de clientes que han comprado productos de la gama Frutales (NATURAL RIGHT JOIN)".ToUpper();

            var consulta = from empleado in _context.Empleados
                           join oficina in _context.Oficinas on empleado.CodigoOficina equals oficina.CodigoOficina into oficinasJoin
                           from oficina in oficinasJoin.DefaultIfEmpty()
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


        public async Task<IEnumerable<object>> Consulta28Left()
        {
            var mensaje = "Empleados sin clientes y nombre de su jefe (LEFT JOIN)".ToUpper();

            var consulta = from empleado in _context.Empleados
                           join cliente in _context.Clientes on empleado.CodigoEmpleado equals cliente.CodigoEmpleadoRepVentas into clientesJoin
                           from cliente in clientesJoin.DefaultIfEmpty()
                           join jefe in _context.Empleados on empleado.CodigoJefe equals jefe.CodigoEmpleado into jefesJoin
                           from jefe in jefesJoin.DefaultIfEmpty()
                           where cliente == null
                           select new
                           {
                               Empleado = empleado.Nombre,
                               Jefe = jefe != null ? jefe.Nombre : null
                           };

            var resultado = new List<object>
    {
        new { Informacion = mensaje, Resultado = await consulta.ToListAsync() }
    };

            return resultado;
        }


        public async Task<IEnumerable<object>> Consulta28Right()
        {
            var mensaje = "Empleados sin clientes y nombre de su jefe (RIGHT JOIN)".ToUpper();

            var consulta = from empleado in _context.Empleados
                           join cliente in _context.Clientes on empleado.CodigoEmpleado equals cliente.CodigoEmpleadoRepVentas into clientesJoin
                           from cliente in clientesJoin.DefaultIfEmpty()
                           join jefe in _context.Empleados on empleado.CodigoJefe equals jefe.CodigoEmpleado into jefesJoin
                           from jefe in jefesJoin.DefaultIfEmpty()
                           where cliente == null
                           select new
                           {
                               Empleado = empleado.Nombre,
                               Jefe = jefe != null ? jefe.Nombre : null
                           };

            var resultado = new List<object>
    {
        new { Informacion = mensaje, Resultado = await consulta.ToListAsync() }
    };

            return resultado;
        }


        public async Task<IEnumerable<object>> Consulta28NaturalLeft()
        {
            var mensaje = "Empleados sin clientes y nombre de su jefe (NATURAL LEFT JOIN)".ToUpper();

            var consulta = from empleado in _context.Empleados
                           join cliente in _context.Clientes on empleado.CodigoEmpleado equals cliente.CodigoEmpleadoRepVentas into clientesJoin
                           from cliente in clientesJoin.DefaultIfEmpty()
                           join jefe in _context.Empleados on empleado.CodigoJefe equals jefe.CodigoEmpleado into jefesJoin
                           from jefe in jefesJoin.DefaultIfEmpty()
                           where cliente == null
                           select new
                           {
                               Empleado = empleado.Nombre,
                               Jefe = jefe != null ? jefe.Nombre : null
                           };

            var resultado = new List<object>
    {
        new { Informacion = mensaje, Resultado = await consulta.ToListAsync() }
    };

            return resultado;
        }


        public async Task<IEnumerable<object>> Consulta28NaturalRight()
        {
            var mensaje = "Empleados sin clientes y nombre de su jefe (NATURAL RIGHT JOIN)".ToUpper();

            var consulta = from empleado in _context.Empleados
                           join cliente in _context.Clientes on empleado.CodigoEmpleado equals cliente.CodigoEmpleadoRepVentas into clientesJoin
                           from cliente in clientesJoin.DefaultIfEmpty()
                           join jefe in _context.Empleados on empleado.CodigoJefe equals jefe.CodigoEmpleado into jefesJoin
                           from jefe in jefesJoin.DefaultIfEmpty()
                           where cliente == null
                           select new
                           {
                               Empleado = empleado.Nombre,
                               Jefe = jefe != null ? jefe.Nombre : null
                           };

            var resultado = new List<object>
    {
        new { Informacion = mensaje, Resultado = await consulta.ToListAsync() }
    };

            return resultado;
        }
        public async Task<int> Consulta29()
        {
            var cantidadEmpleados = await _context.Empleados.CountAsync();
            return cantidadEmpleados;
        }

        public async Task<IEnumerable<object>> Consulta30()
        {

            var consulta = from cliente in _context.Clientes
                           group cliente by cliente.Pais into grupo
                           select new
                           {
                               Pais = grupo.Key,
                               CantidadClientes = grupo.Count()
                           };


            return await consulta.ToListAsync();
        }
        public async Task<IEnumerable<object>> Consulta35()
        {
            var representantesYClientes = await _context.Empleados
                .Where(e => e.Puesto == "Representante Ventas")
                .Include(e => e.Clientes)
                .Select(e => new
                {
                    Representante = e.Nombre,
                    CantidadClientes = e.Clientes.Count
                })
                .ToListAsync();

            return representantesYClientes;
        }

        public async Task<IEnumerable<object>> Consulta54()
        {
            var consulta = from empleado in _context.Empleados
                           where !_context.Clientes.Any(cliente => cliente.CodigoEmpleadoRepVentas == empleado.CodigoEmpleado)
                           select new
                           {
                               Nombre = empleado.Nombre,
                               Apellidos = empleado.Apellido1 + " " + empleado.Apellido2,
                               Puesto = empleado.Puesto,
                               Telefono = empleado.CodigoOficinaNavigation.Telefono
                           };

            var resultado = await consulta.ToListAsync();

            return resultado;
        }
        public async Task<IEnumerable<object>> Consulta61()
        {
            var empleadosNoRepresentantes = await _context.Empleados
                .Where(e => !_context.Clientes.Any(c => c.CodigoEmpleadoRepVentas == e.CodigoEmpleado))
                .Select(e => new
                {
                    Nombre = e.Nombre,
                    Apellido1 = e.Apellido1,
                    Apellido2 = e.Apellido2,
                    Puesto = e.Puesto,
                    TelefonoOficina = e.CodigoOficinaNavigation.Telefono
                })
                .ToListAsync();

            return empleadosNoRepresentantes;
        }


    }
}