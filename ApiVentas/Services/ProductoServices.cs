using ApiVentas.DTOs;
using ApiVentas.Interfaces;
using ApiVentas.Models;
using ApiVentas.Utilitarios;
using ApiVentas.Utilitarios.Dictionaries;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;

namespace ApiVentas.Services
{
    public class ProductoServices : IProductoServices, IServices<Producto>
    {
        private BaseErpContext _context;
        private ControlError log = new ControlError();
        private DynamicEmpty dynamicEmpty = new DynamicEmpty();
        public ProductoServices(BaseErpContext context)
        {
            this._context = context;
        }
        public async Task<Respuesta> GetProducto(DataQuery dataQuery)
        {
            var result = new Respuesta();
            try
            {
                result.Data= await _context.Productos
                                    .Include(producto => producto.Categoria)
                                    .Include(producto => producto.Proveedor)
                                    .Include(producto => producto.Empresa)
                                    .Include(producto => producto.Estado)
                                    .Include(producto => producto.UsuIdRegNavigation)
                                    .Where(ProductoDictionary.GetExpression(dataQuery))
                                    .Select(producto =>
                                     new ProductoDTO
                                     {
                                         ProdId = producto.ProdId,
                                         ProdDescripcion = producto.ProdDescripcion,
                                         ProdUltPrecio = producto.ProdUltPrecio,
                                         CategoriaId = producto.CategoriaId,
                                         CategoriaDesripcion = producto.Categoria.CategoriaDescrip,
                                         EmpresaId = producto.EmpresaId,
                                         EmpresaDescripcion = producto.Empresa.EmpresaNombre,
                                         ProveedorId = producto.ProveedorId,
                                         ProveedorDescripcion = producto.Proveedor.ProvRuc,
                                         EstadoId = producto.EstadoId,
                                         EstadoDescripcion = producto.Estado.EstadoDescrip,
                                         FechaHoraReg = producto.FechaHoraReg,
                                         UsuIdReg = producto.UsuIdReg,
                                         UsuRegName = producto.UsuIdRegNavigation.UsuNombre,
                                     }).ToListAsync();

                result.Code = dynamicEmpty.IsEmpty(result.Data) ? "204" : "200";
                result.Message = dynamicEmpty.IsEmpty(result.Data) ? $"No se encontro registro con opcion:'{dataQuery.OpcionData}' con data: '{dataQuery.DataFirstQuery}'" : "Ok";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "GetProducto", ex.Message);

            }
            return result;
        }

        public async Task<Respuesta> PostProducto(Producto producto)
        {
            var result = new Respuesta();
            try
            {
                var query = await _context.Productos.OrderByDescending(productoDB => productoDB.ProdId)
                                                                        .Select(idDB => idDB.ProdId).FirstOrDefaultAsync() + 1;
                producto.ProdId = query;
                producto.FechaHoraReg = DateTime.Now;

                _context.Productos.Add(producto);
                await _context.SaveChangesAsync();

                result.Code = "200";
                result.Data = producto;
                result.Message = "Ok";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "PostProducto", ex.Message);

            }
            return result;
        }

        public async Task<Respuesta> PutProducto(Producto producto)
        {
            var result = new Respuesta();
            try
            {
                bool existProducto = await _context.Productos.AnyAsync(productoDB => productoDB.ProdId == producto.ProdId);

                if (existProducto)
                {
                    producto.FechaHoraAct = DateTime.Now;

                    _context.Productos.Update(producto);
                    await _context.SaveChangesAsync();
                    result.Data = producto;
                }
                result.Code = existProducto ? "200" : "204";
                result.Message = existProducto ? "Ok" : $"No existe registro con id: '{producto.ProdId}'";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "PutProducto", ex.Message);

            }
            return result;
        }
        public async Task<Respuesta> DeleteProducto(Producto producto)
        {
            var result = new Respuesta();
            try
            {
                bool existProducto = await _context.Productos.AnyAsync(productoDB => productoDB.ProdId == producto.ProdId);
                if (existProducto)
                {
                    producto.FechaHoraAct = DateTime.Now;
                    producto.EstadoId = 2;

                    _context.Productos.Update(producto);
                    await _context.SaveChangesAsync();
                }
                result.Code = existProducto ? "200" : "204";
                result.Message = existProducto ? "Ok" : $"No existe registro con id: '{producto.ProdId}'";

            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "DeleteProducto", ex.Message);

            }
            return result;
        }
    }
}
