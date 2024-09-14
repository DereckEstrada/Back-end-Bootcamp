using ApiVentas.Models;
using System.Linq.Expressions;

namespace ApiVentas.Utilitarios.Dictionaries
{
    public static class MovimientoDetPagosDictionary
    {
        public static readonly Dictionary<string, Func<DataQuery, Expression<Func<MovimientoDetPago, bool>>>> OptionsQuery = new Dictionary<string, Func<DataQuery, Expression<Func<MovimientoDetPago, bool>>>>
        {
            {"all", dataQuery=> movimientoDetPagos=> movimientoDetPagos.EstadoId==1 },
            {"id", dataQuery=> movimientoDetPagos=> movimientoDetPagos.MovidetPagoId==dataQuery.TryIntDataFirst()
                                                                                            && movimientoDetPagos.EstadoId==1 },
            {"movimientocab", dataQuery=> movimientoDetPagos=> movimientoDetPagos.MovicabId==dataQuery.TryIntDataFirst()
                                                                                            && movimientoDetPagos.EstadoId==1 },
            {"forma_pago", dataQuery=> movimientoDetPagos=> movimientoDetPagos.FpagoId==dataQuery.TryIntDataFirst()
                                                                                            && movimientoDetPagos.EstadoId==1 },
            {"valor", dataQuery=> movimientoDetPagos=> movimientoDetPagos.ValorPagado==dataQuery.tryDecimalDataFirst()
                                                                                            && movimientoDetPagos.EstadoId==1 },
            {"industria", dataQuery=> movimientoDetPagos=> movimientoDetPagos.IndustriaId==dataQuery.TryIntDataFirst()
                                                                                            && movimientoDetPagos.EstadoId==1 },
            {"lote", dataQuery=> movimientoDetPagos=> movimientoDetPagos.Lote.Equals(dataQuery.DataFirstQuery)
                                                                                            && movimientoDetPagos.EstadoId==1 },
            {"voucher", dataQuery=> movimientoDetPagos=> movimientoDetPagos.Voucher.Equals(dataQuery.DataFirstQuery)
                                                                                            && movimientoDetPagos.EstadoId==1 },
            {"tarjeta_credito", dataQuery=> movimientoDetPagos=> movimientoDetPagos.TarjetacredId==dataQuery.TryIntDataFirst()
                                                                                            && movimientoDetPagos.EstadoId==1 },
            {"banco", dataQuery=> movimientoDetPagos=> movimientoDetPagos.BancoId==dataQuery.TryIntDataFirst()
                                                                                            && movimientoDetPagos.EstadoId==1 },
            {"comprobante", dataQuery=> movimientoDetPagos=> movimientoDetPagos.ComprobanteId==dataQuery.TryIntDataFirst()
                                                                                            && movimientoDetPagos.EstadoId==1 },
            {"fecha_pago", dataQuery=> movimientoDetPagos=> movimientoDetPagos.FechaPago==dataQuery.TryDateTimeDataFirst()
                                                                                            && movimientoDetPagos.EstadoId==1 },
            {"cliente", dataQuery=> movimientoDetPagos=> movimientoDetPagos.ClienteId==dataQuery.TryIntDataFirst()
                                                                                            && movimientoDetPagos.EstadoId==1 },
            {"estado", dataQuery=> movimientoDetPagos=> movimientoDetPagos.EstadoId==dataQuery.TryIntDataFirst() },
            {"fecha", dataQuery=> movimientoDetPagos=> movimientoDetPagos.FechaHoraReg==dataQuery.TryDateTimeDataFirst()
                                                                                            && movimientoDetPagos.EstadoId==1 },
            {"usuario", dataQuery=> movimientoDetPagos=> movimientoDetPagos.UsuIdReg==dataQuery.TryIntDataFirst()
                                                                                            && movimientoDetPagos.EstadoId==1 },
            {"rango_valor", dataQuery=> movimientoDetPagos=> dataQuery.tryDecimalDataFirst()<=movimientoDetPagos.ValorPagado
                                                            && movimientoDetPagos.ValorPagado<=dataQuery.tryDecimalDataSecond()
                                                            && movimientoDetPagos.EstadoId==1 },
            {"rango_fecha_pago", dataQuery=> movimientoDetPagos=> dataQuery.TryDateTimeDataFirst()<=movimientoDetPagos.FechaPago
                                                            && movimientoDetPagos.FechaPago<=dataQuery.TryDateTimeDataSecond() 
                                                            && movimientoDetPagos.EstadoId==1 },
            {"rango_fecha", dataQuery=> movimientoDetPagos=> dataQuery.TryDateTimeDataFirst()<=movimientoDetPagos.FechaHoraReg
                                                            && movimientoDetPagos.FechaHoraReg<=dataQuery.TryDateTimeDataSecond() 
                                                            && movimientoDetPagos.EstadoId==1 },
        };
        public static Expression<Func<MovimientoDetPago, bool>> GetExpression(DataQuery dataQuery)
        {
            Expression<Func<MovimientoDetPago, bool>> query = bodega => false;
            if (MovimientoDetPagosDictionary.OptionsQuery.TryGetValue(dataQuery.OpcionData, out var expressionQuery))
            {
                query = expressionQuery(dataQuery);
            }
            return query;
        }
    }
}
