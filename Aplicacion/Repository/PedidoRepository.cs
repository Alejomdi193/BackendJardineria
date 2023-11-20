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
    public class PedidoRepository : Generic<Pedido>, IPedido
    {
        private readonly JardineriaContext _context;
        public PedidoRepository(JardineriaContext context) : base(context)
        {
            _context = context;
        }
        public override async Task<IEnumerable<Pedido>> GetAllAsync()
        {
            return await _context.Pedidos
                .ToListAsync();
        }

        public override async Task<Pedido> GetByIdAsync(int codigoPedido)
        {
            return await _context.Pedidos
                .FirstOrDefaultAsync(p => p.CodigoPedido == codigoPedido);
        }

        public override async Task<(int totalRegistros, IEnumerable<Pedido> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
        {
            var query = _context.Pedidos as IQueryable<Pedido>;

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.Estado.ToLower().Contains(search));
            }

            query = query.OrderBy(p => p.CodigoPedido);
            var totalRegistros = await query.CountAsync();
            var registros = await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (totalRegistros, registros);
        }

        public async Task<IEnumerable<object>> Consulta2()
        {
            var mensaje = "Listado de los estados de un pedido".ToUpper();

            var consulta = from p in _context.Pedidos
                           where p.Estado == "Entregado" || p.Estado == "Pendiente" || p.Estado == "Rechazado"
                           select p.Estado;

            var estados = await consulta.Distinct().ToListAsync();

            var resultado = new List<object>
        {
            new { Informacion = mensaje, Estados = estados }
        };

            return resultado;
        }

        public async Task<object> Consulta4()
        {
            var consulta = from p in _context.Pedidos
                           where p.FechaEsperada < p.FechaEntrega
                           select new
                           {
                               CodigoDePedido = p.CodigoPedido,
                               CodigoDeCliente = p.CodigoCliente,
                               FechaEsperada = p.FechaEsperada,
                               FechaEntrega = p.FechaEntrega,
                               Informacion = "No fue entregado a tiempo"
                           };

            var resultado = await consulta.ToListAsync();
            return resultado;
        }
        public async Task<IEnumerable<object>> Consulta5()
        {
            var mensaje = "Pedidos con entrega anticipada de almenos 2 dias".ToUpper();

            var consulta = from pedido in _context.Pedidos
                           where pedido.FechaEntrega.HasValue &&
                                 pedido.FechaEntrega < pedido.FechaEsperada.AddDays(-2)
                           select new
                           {
                               pedido.CodigoPedido,
                               pedido.CodigoCliente,
                               pedido.FechaEsperada,
                               pedido.FechaEntrega
                           };

            var pedidosEntregaAnticipada = await consulta.ToListAsync();

            var resultado = new List<object>
            {
                new { Informacion = mensaje, Pedidos = pedidosEntregaAnticipada }
            };

            return resultado;
        }

        public async Task<IEnumerable<object>> Consulta6()
        {
            var mensaje = "Pedidos rechazados en 2009".ToUpper();

            var consulta = from pedido in _context.Pedidos
                           where pedido.Estado == "Rechazado" &&
                                 pedido.FechaPedido.Year == 2009
                           select new
                           {
                               pedido.CodigoPedido,
                               pedido.CodigoCliente,
                               pedido.FechaEsperada,
                               pedido.FechaEntrega,
                               pedido.Comentarios
                           };

            var pedidosRechazadosEn2009 = await consulta.ToListAsync();

            var resultado = new List<object>
            {
                new { Informacion = mensaje, Pedidos = pedidosRechazadosEn2009 }
            };

            return resultado;
        }

        public async Task<IEnumerable<object>> Consulta7()
        {
            var mensaje = "Pedidos entregados en enero".ToUpper();

            var consulta = from pedido in _context.Pedidos
                           where pedido.FechaEntrega.HasValue &&
                                 pedido.FechaEntrega.Value.Month == 1
                           select new
                           {
                               pedido.CodigoPedido,
                               pedido.CodigoCliente,
                               pedido.FechaEsperada,
                               pedido.FechaEntrega
                           };

            var pedidosEntregadosEnEnero = await consulta.ToListAsync();

            var resultado = new List<object>
            {
                new { Informacion = mensaje, Pedidos = pedidosEntregadosEnEnero }
            };

            return resultado;
        }

        public async Task<IEnumerable<object>> Consulta32()
        {
            var pedidosPorEstado = await _context.Pedidos
                .GroupBy(p => p.Estado)
                .OrderByDescending(g => g.Count())
                .Select(g => new
                {
                    Estado = g.Key,
                    CantidadPedidos = g.Count()
                })
                .ToListAsync();

            return pedidosPorEstado;
        }

    }
}