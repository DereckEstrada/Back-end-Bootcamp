using ApiVentas.Models;

namespace ApiVentas.Interfaces
{
    public interface ISucursal
    {
        Task<Respuesta> GetSucursal(int? sucursalID, string? sucursalRuc, string? estado);
        Task<Respuesta> PostSucursal(Sucursal sucursal);
        Task<Respuesta> PutSucursal(Sucursal sucursal);
        Task<Respuesta> DeleteSucursal(int sucursalID);
    }
}
