using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;

namespace Dominio.Interfaces
{
    public interface ICliente : IGeneric<Cliente>
    {
        Task<IEnumerable<object>> Consulta1();
        Task<IEnumerable<object>> Consulta11();
        Task<IEnumerable<object>> Consulta12();
        Task<IEnumerable<object>> Consulta12Sql2N();
        Task<IEnumerable<object>> Consulta12Sql2I();
        Task<IEnumerable<object>> Consulta13();
        Task<IEnumerable<object>> Consulta13Sql2I();
        Task<IEnumerable<object>> Consulta13Sql2N();
        Task<IEnumerable<object>> Consulta14();
        Task<IEnumerable<object>> Consulta14Sql2I();
        Task<IEnumerable<object>> Consulta14Sql2N();
        Task<IEnumerable<object>> Consulta15();
        Task<IEnumerable<object>> Consulta15Sql2I();
        Task<IEnumerable<object>> Consulta15Sql2N();
        Task<IEnumerable<object>> Consulta16();
        Task<IEnumerable<object>> Consulta16Sql2I();
        Task<IEnumerable<object>> Consulta16Sql2N();
        Task<IEnumerable<object>> Consulta18();
        Task<IEnumerable<object>> Consulta18Sql2I();
        Task<IEnumerable<object>> Consulta18Sql2N();
        Task<IEnumerable<object>> Consulta19();
        Task<IEnumerable<object>> Consulta19Sql2I();
        Task<IEnumerable<object>> Consulta19Sql2N();
        Task<IEnumerable<object>> Consulta20Left();
        Task<IEnumerable<object>> Consulta20Right();
        Task<IEnumerable<object>> Consulta20NaturalLeft();
        Task<IEnumerable<object>> Consulta20NaturalRight();
        Task<IEnumerable<object>> Consulta21Left();
        Task<IEnumerable<object>> Consulta21Right();
        Task<IEnumerable<object>> Consulta21NaturalLeft();
        Task<IEnumerable<object>> Consulta21NaturalRight();
        Task<IEnumerable<object>> Consulta22Left();
        Task<IEnumerable<object>> Consulta22Right();
        Task<IEnumerable<object>> Consulta22NaturalLeft();
        Task<IEnumerable<object>> Consulta22NaturalRight();
        Task<IEnumerable<object>> Consulta27Left();
        Task<IEnumerable<object>> Consulta27Right();
        Task<IEnumerable<object>> Consulta27NaturalLeft();
        Task<IEnumerable<object>> Consulta27NaturalRight();
        Task<int> Consulta33();
        Task<IEnumerable<object>> Consulta34();
        Task<int> Consulta36();
        Task<IEnumerable<object>> Consulta37();
        Task<string> Consulta45();
        Task<IEnumerable<object>> Consulta48();
        Task<string> Consulta49();
        Task<IEnumerable<object>> Consulta51();
        Task<IEnumerable<object>> Consulta52();
        Task<IEnumerable<object>> Consulta55();
        Task<IEnumerable<object>> Consulta56();
        Task<IEnumerable<object>> Consulta57();
        Task<IEnumerable<string>> Consulta58();
        Task<IEnumerable<object>> Consulta59();
        Task<IEnumerable<object>> Consulta60();
    }
}