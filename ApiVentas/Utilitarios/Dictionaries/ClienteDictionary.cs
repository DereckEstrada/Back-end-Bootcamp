using ApiVentas.DTOs;
using ApiVentas.Models;
using System.Linq.Expressions;

namespace ApiVentas.Utilitarios.Dictionaries
{
    public static class ClienteDictionary
    {
        public static readonly Dictionary<string, Func<DataQuery, Expression<Func<Cliente, bool>>>> OptionsQuery = new Dictionary<string, Func<DataQuery, Expression<Func<Cliente, bool>>>>
        {
            {"all", dataQuery=>cliente=>cliente.EstadoId==1},
            {"id", dataQuery=>cliente=>cliente.ClienteId==dataQuery.TryIntDataFirst() && cliente.EstadoId==1},
            {"ruc", dataQuery=>cliente=>cliente.ClienteRuc.Contains(dataQuery.DataFirstQuery) && cliente.EstadoId==1},
            {"primer_nombre", dataQuery=>cliente=>cliente.ClienteNombre1.Contains(dataQuery.DataFirstQuery) 
                                                                                            && cliente.EstadoId==1},
            {"segundo_nombre", dataQuery=>cliente=>cliente.ClienteNombre2.Contains(dataQuery.DataFirstQuery)
                                                                                            && cliente.EstadoId==1},
            {"primer_apellido", dataQuery=>cliente=>cliente.ClienteApellido1.Contains(dataQuery.DataFirstQuery) 
                                                                                            && cliente.EstadoId==1},
            {"segundo_apellido", dataQuery=>cliente=>cliente.ClienteApellido2.Contains(dataQuery.DataFirstQuery)
                                                                                            && cliente.EstadoId==1},
            {"email", dataQuery=>cliente=> cliente.ClienteEmail.Equals(dataQuery.DataFirstQuery)&& cliente.EstadoId==1},
            {"telefono", dataQuery=>cliente=> cliente.ClienteTelefono.Equals(dataQuery.DataFirstQuery)&& cliente.EstadoId==1},
            {"direccion", dataQuery=>cliente=>  cliente.ClienteDireccion.Equals(dataQuery.DataFirstQuery)&& cliente.EstadoId==1},
            {"estado", dataQuery=>cliente=>  cliente.EstadoId==dataQuery.TryIntDataFirst()},
            {"fecha", dataQuery=>cliente=> cliente.FechaHoraReg==dataQuery.TryDateTimeDataFirst()&& cliente.EstadoId==1},
            {"usuario", dataQuery=>cliente=> cliente.UsuIdReg==dataQuery.TryIntDataFirst()&& cliente.EstadoId==1},
        };
        public static Expression<Func<Cliente, bool>> GetExpression(DataQuery dataQuery)
        {
            Expression<Func<Cliente, bool>> query=cliente=>false;
            if(ClienteDictionary.OptionsQuery.TryGetValue(dataQuery.OpcionData, out var expressionQuery))
            {
                query = expressionQuery(dataQuery);
            }
            return query;   
        }
    }
}
