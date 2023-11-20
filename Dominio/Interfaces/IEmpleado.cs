using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;

namespace Dominio.Interfaces
{
    public interface IEmpleado : IGeneric<Empleado>
    {
        Task<IEnumerable<object>> Consulta17();
        Task<IEnumerable<object>> Consulta17Sql2I();
        Task<IEnumerable<object>> Consulta17Sql2N();
        Task<IEnumerable<object>> Consulta23Right();
        Task<IEnumerable<object>> Consulta23NaturalLeft();
        Task<IEnumerable<object>> Consulta23NaturalRight();
        Task<IEnumerable<object>> Consulta23Left();
        Task<IEnumerable<object>> Consulta26Right();
        Task<IEnumerable<object>> Consulta26NaturalRight();
        Task<IEnumerable<object>> Consulta28Left();
        Task<IEnumerable<object>> Consulta28Right();
        Task<IEnumerable<object>> Consulta28NaturalLeft();
        Task<IEnumerable<object>> Consulta28NaturalRight();
        Task<int> Consulta29();
        Task<IEnumerable<object>> Consulta30();
        Task<IEnumerable<object>> Consulta35();
        Task<IEnumerable<object>> Consulta54();
        Task<IEnumerable<object>> Consulta61();
       

        
    }
}