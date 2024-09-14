using ApiVentas.Models;
using System.Linq.Expressions;

namespace ApiVentas.Utilitarios.Dictionaries
{
    public static class PuntoEmisionSriDictionary
    {
        public static readonly Dictionary<string, Func<DataQuery, Expression<Func<PuntoEmisionSri, bool>>>> OptionsQuery = new Dictionary<string, Func<DataQuery, Expression<Func<PuntoEmisionSri, bool>>>> 
        {
            {"all", dataQuery=> puntoEmisionSri=> puntoEmisionSri.EstadoId==1},
            {"id", dataQuery=> puntoEmisionSri=>puntoEmisionSri.PuntoEmisionId==dataQuery.TryIntDataFirst()
                                                                                &&  puntoEmisionSri.EstadoId==1},
            {"emision", dataQuery=> puntoEmisionSri=>puntoEmisionSri.PuntoEmision.Contains(dataQuery.DataFirstQuery)
                                                                                &&  puntoEmisionSri.EstadoId==1},
            {"empresa", dataQuery=> puntoEmisionSri=>puntoEmisionSri.EmpresaId==dataQuery.TryIntDataFirst()
                                                                                &&  puntoEmisionSri.EstadoId==1},
            {"sucursal", dataQuery=> puntoEmisionSri=>puntoEmisionSri.SucursalId==dataQuery.TryIntDataFirst()
                                                                                &&  puntoEmisionSri.EstadoId==1},
            {"sri", dataQuery=> puntoEmisionSri=>puntoEmisionSri.CodEstablecimientoSri.Equals(dataQuery.DataFirstQuery)
                                                                                &&  puntoEmisionSri.EstadoId==1},
            {"secuencia", dataQuery=> puntoEmisionSri=>puntoEmisionSri.UltSecuencia==dataQuery.TryIntDataFirst()
                                                                                &&  puntoEmisionSri.EstadoId==1},
            {"estado", dataQuery=> puntoEmisionSri=>puntoEmisionSri.EstadoId==dataQuery.TryIntDataFirst()},
            {"fecha", dataQuery=> puntoEmisionSri=>puntoEmisionSri.FechaHoraReg==dataQuery.TryDateTimeDataFirst()
                                                                                &&  puntoEmisionSri.EstadoId==1},
            {"usuario", dataQuery=> puntoEmisionSri=>puntoEmisionSri.UsuIdReg==dataQuery.TryIntDataFirst()
                                                                                &&  puntoEmisionSri.EstadoId==1},
        };
        public static Expression<Func<PuntoEmisionSri, bool>> GetExpression(DataQuery dataQuery)
        {
            Expression<Func<PuntoEmisionSri, bool>> query = bodega => false;
            if (PuntoEmisionSriDictionary.OptionsQuery.TryGetValue(dataQuery.OpcionData, out var expressionQuery))
            {
                query = expressionQuery(dataQuery);
            }
            return query;
        }
    }
}
