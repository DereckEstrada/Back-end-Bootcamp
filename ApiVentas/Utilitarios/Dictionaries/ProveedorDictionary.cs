using ApiVentas.DTOs;
using ApiVentas.Models;
using System.Linq.Expressions;

namespace ApiVentas.Utilitarios.Dictionaries
{
    public static class ProveedorDictionary
    {
        public static readonly Dictionary<string, Func<DataQuery, Expression<Func<Proveedor, bool>>>> OptionsQuery = new Dictionary<string, Func<DataQuery, Expression<Func<Proveedor, bool>>>>
        {
            {"all", dataQuery=>proveedor=> proveedor.EstadoId==1 },
            {"id", dataQuery=>proveedor=>  proveedor.ProvId==dataQuery.TryIntDataFirst() && proveedor.EstadoId==1 },
            {"ruc", dataQuery=>proveedor=> proveedor.ProvRuc.Contains(dataQuery.DataFirstQuery)  && proveedor.EstadoId==1 },
            {"nombre", dataQuery=>proveedor=> proveedor.ProvNomComercial.Contains(dataQuery.DataFirstQuery)
                                                                                        && proveedor.EstadoId==1 },
            {"razon", dataQuery=>proveedor=> proveedor.ProvRazon.Equals(dataQuery.DataFirstQuery) && proveedor.EstadoId==1 },
            {"direccion", dataQuery=>proveedor=> proveedor.ProvDireccion.Contains(dataQuery.DataFirstQuery)
                                                                                        && proveedor.EstadoId==1 },
            {"telefono", dataQuery=>proveedor=> proveedor.ProvTelefono.Equals(dataQuery.DataFirstQuery) 
                                                                                        && proveedor.EstadoId==1 },
            {"ciudad", dataQuery=>proveedor=> proveedor.CiudadId== dataQuery.TryIntDataFirst() && proveedor.EstadoId==1 },
            {"estado", dataQuery=>proveedor=> proveedor.EstadoId==dataQuery.TryIntDataFirst() },
            {"usuario", dataQuery=>proveedor=> proveedor.UsuIdReg==dataQuery.TryIntDataFirst() && proveedor.EstadoId==1 },
        };
        public static Expression<Func<Proveedor, bool>> GetExpression(DataQuery dataQuery)
        {
            Expression<Func<Proveedor, bool>> query = producto => false;
            if (ProveedorDictionary.OptionsQuery.TryGetValue(dataQuery.OpcionData, out var expressionQuery))
            {
                query = expressionQuery(dataQuery);
            }
            return query;
        }
    }
}
