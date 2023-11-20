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
    public class PagoRepository : Generic<Pago>, IPago
    {
        private readonly JardineriaContext _context;
        public PagoRepository(JardineriaContext context) : base(context)
        {
            _context = context;
        }
        public override async Task<IEnumerable<Pago>> GetAllAsync()
        {
            return await _context.Pagos
                //.Include(p => p.)
                .ToListAsync();
        }

        public override async Task<Pago> GetByIdx2Async(int codigoCliente, string idTransaccion)
        {
            return await _context.Pagos
                .FirstOrDefaultAsync(p => p.CodigoCliente == codigoCliente && p.IdTransaccion == idTransaccion);
        }

        public override async Task<(int totalRegistros, IEnumerable<Pago> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
        {
            var query = _context.Pagos as IQueryable<Pago>;

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.FechaPago.ToString().ToLower().Contains(search));
            }

            var totalRegistros = await query.CountAsync();
            var registros = await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (totalRegistros, registros);
        }

        public async Task<IEnumerable<object>> Consulta3ConYear()
        {
            var mensaje = "Codigos de clientes que pagaron en 2008".ToUpper();
            var consulta = from p in _context.Pagos
                           where p.FechaPago.Year == 2008
                           select p.CodigoCliente;

            var codigosCliente = await consulta.Distinct().ToListAsync();

            var resultado = new List<object>
                {
                    new { Informacion = mensaje, CodigosDeClientes = codigosCliente }
                };

            return resultado;
        }

        public async Task<IEnumerable<object>> Consulta8()
        {
            var mensaje = "Pagos realizados en 2008 mediante Paypal (Ordenado de mayor a menor)".ToUpper();

            var consulta = from pago in _context.Pagos
                           where pago.FechaPago.Year == 2008 && pago.IdTransaccion == "Paypal"
                           orderby pago.Total descending
                           select new
                           {
                               pago.CodigoCliente,
                               pago.FormaPago,
                               pago.IdTransaccion,
                               pago.FechaPago,
                               pago.Total
                           };

            var paypal = await consulta.ToListAsync();

            var resultado = new List<object>
            {
                new { Informacion = mensaje, Pagos = paypal }
            };

            return resultado;
        }
        public async Task<IEnumerable<object>> Consulta9()
        {
            var mensaje = "Listado de formas de pago sin repeticiones".ToUpper();
            var formasDePago = await _context.Pagos
                .Select(p => p.IdTransaccion)
                .Distinct()
                .ToListAsync();

            var resultado = new List<object>
            {
                new { Informacion = mensaje, FormasDePago = formasDePago }
            };

            return resultado;
        }

        public async Task<IEnumerable<object>> Consulta31()
        {
            var mensaje = "medio de pago en 2009".ToUpper();
            var pagoMedio = await _context.Pagos
                .Where(p => p.FechaPago.Year == 2009)
                .AverageAsync(p => p.Total);

            var resultado = new List<object>
            {
                new { Information = mensaje,PagoMedio = pagoMedio }
            };

            return resultado;
        }

        public async Task<IEnumerable<object>> Consulta44()
        {
            var consulta = await (
                from pago in _context.Pagos
                group pago by pago.FechaPago.Year into pagosPorAno
                select new
                {
                    Ano = pagosPorAno.Key,
                    SumaTotal = pagosPorAno.Sum(p => p.Total)
                })
                .ToListAsync();

            return consulta;
        }


    }
}