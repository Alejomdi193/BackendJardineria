using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;

namespace Dominio.Interfaces
{
    public interface IDetallePedido : IGeneric<DetallePedido>
    {
        Task<IEnumerable<object>> Consulta38();
        Task<IEnumerable<object>> Consulta39();
        Task<IEnumerable<object>> Consulta40();
        Task<IEnumerable<object>> Consulta42();
        Task<IEnumerable<object>> Consulta43();
     
       
    }
}