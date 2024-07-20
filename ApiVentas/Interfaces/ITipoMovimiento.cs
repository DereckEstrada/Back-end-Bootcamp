using ApiVentas.Models;

namespace ApiVentas.Interfaces
{
    public interface ITipoMovimiento
    {
        Task<Respuesta> GetTipoMovimiento(int? tipoMovimientoID, string? movimientoDescrip);
        Task<Respuesta> PostTipoMovimiento(TipoMovimiento tipoMovimiento);
        Task<Respuesta> PutTipoMovimiento(TipoMovimiento tipoMovimiento);
        Task<Respuesta> DeleteTipoMovimiento(int tipoMovimientoID);
    }
}
