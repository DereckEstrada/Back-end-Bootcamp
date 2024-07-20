using ApiVentas.Models;

namespace ApiVentas.Interfaces
{
    public interface ITarjetaCredito
    {
        Task<Respuesta> GetTarjetaCredito(int? tarjetaCredID, string? tarjetaDescrip);
        Task<Respuesta> PostTarjetaCredito(TarjetaCredito tarjetaCredito);
        Task<Respuesta> PutTarjetaCredito(TarjetaCredito tarjetaCredito);
        Task<Respuesta> DeleteTarjetaCredito(int tarjetaCredID);
    }
}
