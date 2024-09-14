using ApiVentas.Models;
using ApiVentas.Utilitarios;

namespace ApiVentas.Interfaces
{
    public interface ISucursalServices
    {
        Task<Respuesta> GetSucursal(DataQuery dataQuery);
        Task<Respuesta> PostSucursal(Sucursal sucursal);
        Task<Respuesta> PutSucursal(Sucursal sucursal);
        Task<Respuesta> DeleteSucursal(Sucursal sucursal);
    }
}
