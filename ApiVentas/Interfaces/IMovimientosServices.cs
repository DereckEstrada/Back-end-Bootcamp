using ApiVentas.Models;
using ApiVentas.Utilitarios;

namespace ApiVentas.Interfaces
{
    public interface IMovimientosServices
    {
        Task<Respuesta> GetMovimientoCab(DataQuery dataQuery);
        Task<Respuesta> PostMovimientoCab(MovimientoCab movimientoCab);
        Task<Respuesta> PutMovimientoCab(MovimientoCab movimientoCab);
        Task<Respuesta> DeleteMovimientoCab(MovimientoCab movimientoCab);
        Task<Respuesta> GetMovimientoDetPago(DataQuery dataQuery);
        Task<Respuesta> PostMovimientoDetPago(MovimientoDetPago movimientoDetPago);
        Task<Respuesta> PutMovimientoDetPago(MovimientoDetPago movimientoDetPago);
        Task<Respuesta> DeleteMovimientoDetPago(MovimientoDetPago movimientoDetPago);
        Task<Respuesta> GetMovimientoDetProducto(DataQuery dataQuery);
        Task<Respuesta> PostMovimientoDetProducto(MovimientoDetProducto movimientoDetProducto);
        Task<Respuesta> PutMovimientoDetProducto(MovimientoDetProducto movimientoDetProducto);
        Task<Respuesta> DeleteMovimientoDetProducto(MovimientoDetProducto movimientoDetProducto);
    }
}
