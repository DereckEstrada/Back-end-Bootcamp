using ApiVentas.Models;
using ApiVentas.Utilitarios;

namespace ApiVentas.Interfaces
{
    public interface IProducto
    {
        Task<Respuesta> GetProducto(string? opcion, string? data, string? data2);
        Task<Respuesta> PostProducto(Producto producto);
        Task<Respuesta> PutProducto(Producto producto);
        Task<Respuesta> DeleteProducto(int id);
    }
}
