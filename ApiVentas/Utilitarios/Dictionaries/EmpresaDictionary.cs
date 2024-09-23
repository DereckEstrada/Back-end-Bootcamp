using ApiVentas.DTOs;
using ApiVentas.Models;
using System.Linq.Expressions;

namespace ApiVentas.Utilitarios.Dictionaries
{
    public static class EmpresaDictionary
    {
        public static readonly Dictionary<string, Func<DataQuery, Expression<Func<Empresa, bool>>>> OptionsQuery = new Dictionary<string, Func<DataQuery, Expression<Func<Empresa, bool>>>>
        {
            {"all", dataQuery=>empresa=> empresa.EstadoId==1},
            {"id", dataQuery=>empresa=> empresa.EmpresaId==dataQuery.TryIntDataFirst()&& empresa.EstadoId==1},
            {"ruc", dataQuery=>empresa=> empresa.EmpresaRuc.Contains(dataQuery.DataFirstQuery) && empresa.EstadoId==1},
            {"nombre", dataQuery=>empresa=>empresa.EmpresaNombre.Contains(dataQuery.DataFirstQuery) && empresa.EstadoId==1},
            {"razon", dataQuery=>empresa=>empresa.EmpresaRazon.Equals(dataQuery.DataFirstQuery) && empresa.EstadoId==1},
            {"direccion", dataQuery=>empresa=>empresa.EmpresaDireccionMatriz.Contains(dataQuery.DataFirstQuery) 
                                                                                        && empresa.EstadoId==1},
            {"telefono", dataQuery=>empresa=>empresa.EmpresaTelefonoMatriz.Equals(dataQuery.DataFirstQuery) 
                                                                                        && empresa.EstadoId==1},
            {"ciudad", dataQuery=>empresa=>empresa.CiudadId==dataQuery.TryIntDataFirst() && empresa.EstadoId==1},
            {"estado", dataQuery=>empresa=> empresa.EstadoId==dataQuery.TryIntDataFirst()},
            {"fecha", dataQuery=>empresa=>empresa.FechaHoraReg==dataQuery.TryDateTimeDataFirst() && empresa.EstadoId==1},
            {"usuario", dataQuery=>empresa=>empresa.UsuIdReg==dataQuery.TryIntDataFirst() && empresa.EstadoId==1},
        };
    public static Expression<Func<Empresa, bool>> GetExpression(DataQuery dataQuery)
        {
            Expression<Func<Empresa, bool>> query=empresa=>false;
            if(EmpresaDictionary.OptionsQuery.TryGetValue(dataQuery.OpcionData, out var expressionQuery))
            {
                query = expressionQuery(dataQuery);
            }
            return query;
        }
    }
}
