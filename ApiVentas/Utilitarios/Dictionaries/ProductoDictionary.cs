using ApiVentas.DTOs;
using ApiVentas.Models;
using System.Linq.Expressions;

namespace ApiVentas.Utilitarios.Dictionaries
{
    public static  class ProductoDictionary
    {
        public static readonly Dictionary<string, Func<DataQuery, Expression<Func<Producto, bool>>>> OptionsQuery = new Dictionary<string, Func<DataQuery, Expression<Func<Producto, bool>>>>
        {
            {"all", dataQuery=>producto=>producto.EstadoId==1 },
            {"id", dataQuery=>producto=> producto.ProdId == dataQuery.TryIntDataFirst() && producto.EstadoId==1 },
            {"descripcion", dataQuery=>producto=> producto.ProdDescripcion.Contains(dataQuery.DataFirstQuery)
                                                                                        && producto.EstadoId==1 },
            {"categoria", dataQuery=>producto=> producto.CategoriaId==dataQuery.TryIntDataFirst() && producto.EstadoId==1 },
            {"empresa", dataQuery=>producto=> producto.EmpresaId==dataQuery.TryIntDataFirst() && producto.EstadoId==1 },
            {"precio", dataQuery=>producto=>  producto.ProdUltPrecio== dataQuery.TryIntDataFirst() && producto.EstadoId==1 },
            {"proveedor", dataQuery=>producto=> producto.ProveedorId==dataQuery.TryIntDataFirst() && producto.EstadoId==1 },
            {"estado", dataQuery=>producto=> producto.EstadoId==dataQuery.TryIntDataFirst() },
            {"rango_precio", dataQuery=>producto=> dataQuery.tryDecimalDataFirst()<=producto.ProdUltPrecio && producto.ProdUltPrecio<=dataQuery.tryDecimalDataSecond()&& producto.EstadoId==1 },            
        };
        public static Expression<Func<Producto, bool>> GetExpression(DataQuery dataQuery)
            {
            Expression<Func<Producto, bool>> query = producto => false;
            if (ProductoDictionary.OptionsQuery.TryGetValue(dataQuery.OpcionData, out var expressionQuery))
            {
                query=expressionQuery(dataQuery);
            }
            return query;
        }
    }
}
