using ApiVentas.Models;
using System.Linq.Expressions;

namespace ApiVentas.Utilitarios.Dictionaries
{
    public static class SucursalDictionary
    {
        public static readonly Dictionary<string, Func<DataQuery, Expression<Func<Sucursal, bool>>>> OptionsQuery = new Dictionary<string, Func<DataQuery, Expression<Func<Sucursal, bool>>>>
        {
            {"all", dataQuery=>sucursal=>sucursal.EstadoId==1},
            {"id", dataQuery=>sucursal=>sucursal.SucursalId==dataQuery.TryIntDataFirst() && sucursal.EstadoId==1},
            {"ruc", dataQuery=>sucursal=>sucursal.SucursalRuc.Equals(dataQuery.DataFirstQuery) && sucursal.EstadoId==1},
            {"direccion", dataQuery=>sucursal=>sucursal.SucursalDireccion.Equals(dataQuery.DataFirstQuery) && sucursal.EstadoId==1},
            {"telefono", dataQuery=>sucursal=>sucursal.SucursalTelefono.Equals(dataQuery.DataFirstQuery) && sucursal.EstadoId==1},
            {"empresa", dataQuery=>sucursal=>sucursal.EmpresaId==dataQuery.TryIntDataFirst()&& sucursal.EstadoId==1},
            {"estado", dataQuery=>sucursal=> sucursal.EstadoId==dataQuery.TryIntDataFirst()},
            {"fecha", dataQuery=>sucursal=>sucursal.FechaHoraReg==dataQuery.TryDateTimeDataFirst() && sucursal.EstadoId==1},
            {"usuario", dataQuery=>sucursal=>sucursal.UsuIdReg==dataQuery.TryIntDataFirst() && sucursal.EstadoId==1}
        };
        public static Expression<Func<Sucursal, bool>> GetExpression(DataQuery dataQuery)
        {
            Expression<Func<Sucursal, bool>> query = bodega => false;
            if (SucursalDictionary.OptionsQuery.TryGetValue(dataQuery.OpcionData, out var expressionQuery))
            {
                query = expressionQuery(dataQuery);
            }
            return query;
        }
    }
}
