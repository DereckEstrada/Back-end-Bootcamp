using ApiVentas.Models;
using System.Linq.Expressions;

namespace ApiVentas.Utilitarios.Dictionaries
{
    public static class PuntoVentaDictionary
    {
        public static readonly Dictionary<string, Func<DataQuery, Expression<Func<PuntoVentum, bool>>>> OptionsQuery = new Dictionary<string, Func<DataQuery, Expression<Func<PuntoVentum, bool>>>>
        {
            {"all", dataQuery=>puntoVenta=> puntoVenta.EstadoId==1 },
            {"id", dataQuery=>puntoVenta=> puntoVenta.PuntovtaId==dataQuery.TryIntDataFirst() && puntoVenta.EstadoId==1 },
            {"nombre", dataQuery=>puntoVenta=> puntoVenta.PuntovtaNombre.Contains(dataQuery.DataFirstQuery)
                                                                            && puntoVenta.EstadoId==1 },
            {"emision", dataQuery=>puntoVenta=> puntoVenta.PuntoEmisionId==dataQuery.TryIntDataFirst() && puntoVenta.EstadoId==1 },
            {"sucursal", dataQuery=>puntoVenta=> puntoVenta.SucursalId==dataQuery.TryIntDataFirst() && puntoVenta.EstadoId==1 },
            {"estado", dataQuery=>puntoVenta=> puntoVenta.EstadoId==dataQuery.TryIntDataFirst() },
            {"fecha", dataQuery=>puntoVenta=> puntoVenta.FechaHoraReg==dataQuery.TryDateTimeDataFirst() && puntoVenta.EstadoId==1 },
            {"usuario", dataQuery=>puntoVenta=> puntoVenta.UsuIdReg==dataQuery.TryIntDataFirst() && puntoVenta.EstadoId==1 },
        };
        public static Expression<Func<PuntoVentum, bool>> GetExpression(DataQuery dataQuery)
        {
            Expression<Func<PuntoVentum, bool>> query = bodega => false;
            if (PuntoVentaDictionary.OptionsQuery.TryGetValue(dataQuery.OpcionData, out var expressionQuery))
            {
                query = expressionQuery(dataQuery);
            }
            return query;
        }
    }
}
