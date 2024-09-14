using ApiVentas.DTOs;
using ApiVentas.Models;
using System.Linq.Expressions;

namespace ApiVentas.Utilitarios.Dictionaries
{
    public static  class MovimientoCabDictionary
    {
        public static readonly Dictionary<string, Func<DataQuery, Expression<Func<MovimientoCab, bool>>>> OptionsQuery = new Dictionary<string, Func<DataQuery, Expression<Func<MovimientoCab, bool>>>>
        {
            {"all", dataQuery=> movimientoCab=> movimientoCab.EstadoId==1},
            {"id", dataQuery=> movimientoCab=>movimientoCab.EmpresaId==dataQuery.TryIntDataFirst() && movimientoCab.EstadoId==1},
            {"tipo", dataQuery=> movimientoCab=>movimientoCab.TipomovIngEgr==dataQuery.TryIntDataFirst()
                                                                                        && movimientoCab.EstadoId==1},
            {"empresa", dataQuery=> movimientoCab=> movimientoCab.EmpresaId==dataQuery.TryIntDataFirst()
                                                                                        && movimientoCab.EstadoId==1},
            {"sucursal", dataQuery=> movimientoCab=> movimientoCab.SucursalId==dataQuery.TryIntDataFirst()
                                                                                        && movimientoCab.EstadoId==1},
            {"bodega", dataQuery=> movimientoCab=> movimientoCab.BodegaId==dataQuery.TryIntDataFirst()
                                                                                        && movimientoCab.EstadoId==1},
            {"factura", dataQuery=> movimientoCab=>movimientoCab.SecuenciaFactura.Equals(dataQuery.DataFirstQuery)
                                                                                        && movimientoCab.EstadoId==1},
            {"sri", dataQuery=> movimientoCab=>movimientoCab.AutorizacionSri.Equals(dataQuery.DataFirstQuery)
                                                                                        && movimientoCab.EstadoId==1},
            {"cliente", dataQuery=> movimientoCab=> movimientoCab.ClienteId==dataQuery.TryIntDataFirst()
                                                                                        && movimientoCab.EstadoId==1},
            {"punto_venta", dataQuery=> movimientoCab=> movimientoCab.PuntovtaId==dataQuery.TryIntDataFirst()
                                                                                        && movimientoCab.EstadoId==1},
            {"proveedor", dataQuery=> movimientoCab=> movimientoCab.ProveedorId==dataQuery.TryIntDataFirst()
                                                                                        && movimientoCab.EstadoId==1},
            {"estado", dataQuery=> movimientoCab=> movimientoCab.EstadoId==dataQuery.TryIntDataFirst()},
            {"fecha", dataQuery=> movimientoCab=> movimientoCab.FechaHoraReg==dataQuery.TryDateTimeDataFirst()},
            {"usuario", dataQuery=> movimientoCab=>movimientoCab.UsuIdReg==dataQuery.TryIntDataFirst()
                                                                                        && movimientoCab.EstadoId==1},
        };
        public static Expression<Func<MovimientoCab, bool>> GetExpression(DataQuery dataQuery)
        {
            Expression <Func<MovimientoCab, bool>> query=movimientoCab=>false;
            if(MovimientoCabDictionary.OptionsQuery.TryGetValue(dataQuery.OpcionData, out var expressionQuery))
            {
                query=expressionQuery(dataQuery);
            }
            return query;
        }
    }
}
