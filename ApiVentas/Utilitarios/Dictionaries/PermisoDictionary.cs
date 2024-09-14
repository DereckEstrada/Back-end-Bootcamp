using ApiVentas.DTOs;
using ApiVentas.Models;
using System.Linq.Expressions;

namespace ApiVentas.Utilitarios.Dictionaries
{
    public static class PermisoDictionary
    {
        public static readonly Dictionary<string, Func<DataQuery, Expression<Func<UsuarioPermiso, bool>>>> OptionsQuery = new Dictionary<string, Func<DataQuery, Expression<Func<UsuarioPermiso, bool>>>>
        {
            {"all", DataQuery=>permiso=>permiso.EstadoId==1 },
            {"id", DataQuery=>permiso=>permiso.PermisoId==DataQuery.TryIntDataFirst() && permiso.EstadoId==1 },
            {"modulo", DataQuery=>permiso=> permiso.ModuloId==DataQuery.TryIntDataFirst() && permiso.EstadoId==1 },
            {"opcion", DataQuery=>permiso=> permiso.OpcionId==DataQuery.TryIntDataFirst() && permiso.EstadoId==1 },
            {"usuario_permiso", DataQuery=>permiso=> permiso.UsuId==DataQuery.TryIntDataFirst() && permiso.EstadoId==1 },
            {"estado", DataQuery=>permiso=> permiso.EstadoId==DataQuery.TryIntDataFirst() },
            {"usuario", DataQuery=>permiso=>permiso.UsuIdReg==DataQuery.TryIntDataFirst() && permiso.EstadoId==1 }
        };
        public static Expression<Func<UsuarioPermiso, bool>> GetExpression(DataQuery dataQuery)
        {
            Expression<Func<UsuarioPermiso, bool>> query = permiso => false;
            if (PermisoDictionary.OptionsQuery.TryGetValue(dataQuery.OpcionData, out var expressionQuery))
            {
                query=expressionQuery(dataQuery);
            }
            return query;
        }
    }
}
