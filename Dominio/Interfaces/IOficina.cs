using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;

namespace Dominio.Interfaces
{
    public interface IOficina : IGeneric<Oficina>
    {
        Task<IEnumerable<object>> Consulta26Left();

        Task<IEnumerable<object>> Consulta26NaturalLeft();
    }
}