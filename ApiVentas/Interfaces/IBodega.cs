using ApiVentas.Models;

namespace ApiVentas.Interfaces
{
    public interface IBodega
    {
        Task<Respuesta> GetBodega();
        Task<Respuesta> PostBodega(Bodega bodega);
        Task<Respuesta> PutBodega(Bodega bodega);
        Task<Respuesta> DeleteBodega(Bodega bodega);
    }
}
