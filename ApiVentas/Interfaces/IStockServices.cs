using ApiVentas.Models;
using ApiVentas.Utilitarios;

namespace ApiVentas.Interfaces
{
    public interface IStockServices
    {
        Task<Respuesta> GetStock(DataQuery dataQuery);
        Task<Respuesta> PostStock(Stock stock);
        Task<Respuesta> PutStock(Stock stock);
        Task<Respuesta> DeleteStock(Stock stock);
    }
}
