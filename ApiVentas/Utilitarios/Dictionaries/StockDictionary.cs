using ApiVentas.Models;
using System.Linq.Expressions;

namespace ApiVentas.Utilitarios.Dictionaries
{
    public static class StockDictionary
    {
        public static readonly Dictionary<string, Func<DataQuery, Expression<Func<Stock, bool>>>> OptionsQuery = new Dictionary<string, Func<DataQuery, Expression<Func<Stock, bool>>>>
        {
            {"all", dataQuery=> stock=>stock.EstadoId==1},
            {"id", dataQuery=> stock=> stock.StockId==dataQuery.TryIntDataFirst() && stock.EstadoId==1},
            {"empresa", dataQuery=> stock=> stock.EmpresaId==dataQuery.TryIntDataFirst() && stock.EstadoId==1},
            {"sucursal", dataQuery=> stock=> stock.SucursalId==dataQuery.TryIntDataFirst() && stock.EstadoId==1},
            {"producto", dataQuery=> stock=> stock.ProdId==dataQuery.TryIntDataFirst() && stock.EstadoId==1},
            {"cantidad", dataQuery=> stock=> stock.CantidadStock==dataQuery.TryIntDataFirst() && stock.EstadoId==1},
            {"fecha", dataQuery=> stock=> stock.FechaHoraReg==dataQuery.TryDateTimeDataFirst() && stock.EstadoId==1},
            {"estado", dataQuery=> stock=> stock.EstadoId==dataQuery.TryIntDataFirst()},
            {"rango_cantidad", dataQuery=> stock=> dataQuery.TryIntDataFirst()<=stock.CantidadStock 
                                                                            && stock.CantidadStock<=dataQuery.TryIntDataSecond() 
                                                                            && stock.EstadoId==dataQuery.TryIntDataFirst()},
        };
        public static Expression<Func<Stock, bool>> GetExpression(DataQuery dataQuery)
        {
            Expression<Func<Stock, bool>> query = bodega => false;
            if (StockDictionary.OptionsQuery.TryGetValue(dataQuery.OpcionData, out var expressionQuery))
            {
                query = expressionQuery(dataQuery);
            }
            return query;
        }
    }
}
