using ApiVentas.Models;
using System.Linq.Expressions;

namespace ApiVentas.Utilitarios.Dictionaries
{
    public static class MovimientoDetProductoDictionary
    {
        public static readonly Dictionary<string, Func<DataQuery, Expression<Func<MovimientoDetProducto, bool>>>> OptionsQuery = new Dictionary<string, Func<DataQuery, Expression<Func<MovimientoDetProducto, bool>>>>
        {
            {"all", dataQuery=> movimientoDetProducto=>movimientoDetProducto.EstadoId==1 },
            {"id", dataQuery=> movimientoDetProducto=>movimientoDetProducto.MovidetProdId==dataQuery.TryIntDataFirst()
                                                                                              && movimientoDetProducto.EstadoId==1 },
            {"movimentocab", dataQuery=> movimientoDetProducto=>movimientoDetProducto.MovicabId==dataQuery.TryIntDataFirst()
                                                                                              && movimientoDetProducto.EstadoId==1 },
            {"producto", dataQuery=> movimientoDetProducto=>movimientoDetProducto.ProductoId==dataQuery.TryIntDataFirst()
                                                                                              && movimientoDetProducto.EstadoId==1 },
            {"cantidad", dataQuery=> movimientoDetProducto=>movimientoDetProducto.Cantidad==dataQuery.TryIntDataFirst()
                                                                                              && movimientoDetProducto.EstadoId==1 },
            {"precio", dataQuery=> movimientoDetProducto=>movimientoDetProducto.Precio==dataQuery.tryDecimalDataFirst()
                                                                                              && movimientoDetProducto.EstadoId==1 },
            {"fecha", dataQuery=> movimientoDetProducto=>movimientoDetProducto.FechaHoraReg==dataQuery.TryDateTimeDataFirst()
                                                                                              && movimientoDetProducto.EstadoId==1 },
            {"estado", dataQuery=> movimientoDetProducto=>movimientoDetProducto.EstadoId==dataQuery.TryIntDataFirst() },
            {"usuario", dataQuery=> movimientoDetProducto=>movimientoDetProducto.UsuIdReg==dataQuery.TryIntDataFirst()
                                                                                              && movimientoDetProducto.EstadoId==1 },
            {"rango_cantidad", dataQuery=> movimientoDetProducto=>dataQuery.TryIntDataFirst()<=movimientoDetProducto.Cantidad
                                                                       &&movimientoDetProducto.Cantidad<=dataQuery.TryIntDataSecond()
                                                                       && movimientoDetProducto.EstadoId==1 },
            {"rango_precio", dataQuery=> movimientoDetProducto=>dataQuery.tryDecimalDataFirst()<=movimientoDetProducto.Precio
                                                                    && movimientoDetProducto.Precio<=dataQuery.tryDecimalDataSecond()
                                                                    && movimientoDetProducto.EstadoId==1 },
        };
        public static Expression<Func<MovimientoDetProducto, bool>> GetExpression(DataQuery dataQuery)
        {
            Expression<Func<MovimientoDetProducto, bool>> query = bodega => false;
            if (MovimientoDetProductoDictionary.OptionsQuery.TryGetValue(dataQuery.OpcionData, out var expressionQuery))
            {
                query = expressionQuery(dataQuery);
            }
            return query;
        }
    }
}
