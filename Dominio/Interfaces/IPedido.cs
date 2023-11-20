using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;

namespace Dominio.Interfaces
{
    public interface IPedido : IGeneric<Pedido>
    {
        Task<IEnumerable<object>> Consulta2();
        Task<object> Consulta4();
        Task<IEnumerable<object>> Consulta5();
        Task<IEnumerable<object>> Consulta6();
        Task<IEnumerable<object>> Consulta7();
        Task<IEnumerable<object>> Consulta32();
    }
}