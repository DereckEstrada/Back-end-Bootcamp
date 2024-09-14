using ApiVentas.DTOs;
using ApiVentas.Models;
using System.Linq.Expressions;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;

namespace ApiVentas.Utilitarios.Dictionaries
{
    public static class BodegaDictionary
    {
        public static readonly Dictionary<string, Func<DataQuery, Expression<Func<Bodega, bool>>>> OptionsQuery = new Dictionary<string, Func<DataQuery, Expression<Func<Bodega, bool>>>>
        {
            {"all", dataQuery=>bodega => bodega.EstadoId == 1},
            {"id", dataQuery=>bodega => bodega.BodegaId == dataQuery.TryIntDataFirst() && bodega.EstadoId == 1},
            {"nombre", dataQuery=>bodega => bodega.BodegaNombre.Contains(dataQuery.DataFirstQuery )},
            {"direccion", dataQuery=>bodega => bodega.BodegaDireccion.Contains(dataQuery.DataFirstQuery) && bodega.EstadoId == 1},
            {"telefono", dataQuery=>bodega => bodega.BodegaTelefono.Equals(dataQuery.DataFirstQuery) && bodega.EstadoId == 1},
            {"sucursal", dataQuery=>bodega => bodega.SucursalId == dataQuery.TryIntDataFirst() && bodega.EstadoId == 1},
            {"estado", dataQuery=>bodega => bodega.EstadoId == dataQuery.TryIntDataFirst()},
            {"fecha", dataQuery=>bodega => bodega.FechaHoraReg == dataQuery.TryDateTimeDataFirst()  && bodega.EstadoId == 1},
            {"usuario", dataQuery=>bodega => bodega.UsuIdReg == dataQuery.TryIntDataFirst() && bodega.EstadoId == 1},
        };
        public static Expression<Func<Bodega, bool>> GetExpression(DataQuery dataQuery)
        {
            Expression<Func<Bodega, bool>> query = bodega => false;
            if (BodegaDictionary.OptionsQuery.TryGetValue(dataQuery.OpcionData, out var expressionQuery))
            {
                query = expressionQuery(dataQuery);
            }
            return query;
        }
    }
}
