using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;

namespace Dominio.Interfaces
{
    public interface IProducto : IGeneric<Producto>
    {
        Task<IEnumerable<object>> Consulta10();
        Task<IEnumerable<object>> Consulta24Left();
        Task<IEnumerable<object>> Consulta24Right();
        Task<IEnumerable<object>> Consulta24NaturalLeft();
        Task<IEnumerable<object>> Consulta24NaturalRight();
        Task<IEnumerable<object>> Consulta25Left();
        Task<IEnumerable<object>> Consulta25Right();
        Task<IEnumerable<object>> Consulta25NaturalLeft();
        Task<IEnumerable<object>> Consulta25NaturalRight();
        Task<string> Consulta46();
        Task<string> Consulta47();
        Task<string> Consulta50();
        Task<IEnumerable<Producto>> Consulta53();
    }
}