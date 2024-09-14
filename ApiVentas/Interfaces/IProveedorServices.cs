using ApiVentas.Models;
using ApiVentas.Utilitarios;

namespace ApiVentas.Interfaces
{
    public interface IProveedorServices
    {
        Task<Respuesta> GetProveedor(DataQuery dataQuery);
        Task<Respuesta> PostProveedor(Proveedor proveedor);
        Task<Respuesta> PutProveedor(Proveedor proveedor);
        Task<Respuesta> DeleteProveedor(Proveedor proveedor);
    }
}
