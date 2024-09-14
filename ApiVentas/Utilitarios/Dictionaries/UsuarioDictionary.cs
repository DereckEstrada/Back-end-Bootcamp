using ApiVentas.DTOs;
using ApiVentas.Models;
using System.Linq.Expressions;

namespace ApiVentas.Utilitarios.Dictionaries
{
    public static class UsuarioDictionary
    {
        public static readonly Dictionary<string, Func<DataQuery, Expression<Func<Usuario, bool>>>> OptionsQuery = new Dictionary<string, Func<DataQuery, Expression<Func<Usuario, bool>>>>
        {
            {"all", dataQuery=>usuario=>usuario.EstadoId==1 },
            {"id", dataQuery=>usuario=>usuario.UsuId==dataQuery.TryIntDataFirst() && usuario.EstadoId==1 },
            {"nombre", dataQuery=>usuario=>usuario.UsuNombre.Contains(dataQuery.DataFirstQuery) && usuario.EstadoId==1 },
            {"empresa", dataQuery=>usuario=>usuario.EmpresaId==dataQuery.TryIntDataFirst() && usuario.EstadoId==1 },
            {"estado", dataQuery=>usuario=>usuario.EstadoId==dataQuery.TryIntDataFirst()},
            {"usuario", dataQuery=>usuario=>usuario.UsuIdReg==dataQuery.TryIntDataFirst() && usuario.EstadoId==1 },
        };
        public static Expression<Func<Usuario, bool>> GetExpression(DataQuery dataQuery)
        {
            Expression<Func<Usuario, bool>> query=usuario=>false;
            if(UsuarioDictionary.OptionsQuery.TryGetValue(dataQuery.OpcionData, out var expressionQuery))
            {
                query=expressionQuery(dataQuery);
            }
            return query;
        }
    }
}
