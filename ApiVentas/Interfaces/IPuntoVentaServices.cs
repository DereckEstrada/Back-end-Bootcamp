using ApiVentas.Models;
using ApiVentas.Utilitarios;

namespace ApiVentas.Interfaces
{
    public interface IPuntoVentaServices
    {
        Task<Respuesta> GetPuntoVenta(DataQuery dataQuery);
        Task<Respuesta> PostPuntoVenta(PuntoVentum puntoVenta);
        Task<Respuesta> PutPuntoVenta(PuntoVentum puntoVenta);
        Task<Respuesta> DeletePuntoVenta(PuntoVentum puntoVenta);
    }
}
