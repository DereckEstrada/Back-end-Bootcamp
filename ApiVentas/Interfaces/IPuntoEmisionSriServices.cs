using ApiVentas.Models;
using ApiVentas.Utilitarios;

namespace ApiVentas.Interfaces
{
    public interface IPuntoEmisionSriServices
    {
        Task<Respuesta> GetPuntoEmisionSri(DataQuery dataQuery);
        Task<Respuesta> PostPuntoEmisionSri(PuntoEmisionSri emisionSri);
        Task<Respuesta> PutPuntoEmisionSri(PuntoEmisionSri emisionSri);
        Task<Respuesta> DeletePuntoEmisionSri(PuntoEmisionSri emisionSri);
    }
}
