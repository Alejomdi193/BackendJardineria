using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;

namespace Dominio.Interfaces
{
    public interface IPago : IGeneric<Pago>
    {
        Task<IEnumerable<object>> Consulta3ConYear();
        Task<IEnumerable<object>> Consulta8();
        Task<IEnumerable<object>> Consulta9();
        Task<IEnumerable<object>> Consulta31();
        Task<IEnumerable<object>> Consulta44();
    }
}