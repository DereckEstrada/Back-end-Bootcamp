using ApiVentas.Models;
using ApiVentas.Utilitarios;

namespace ApiVentas.Interfaces
{
    public interface IClienteServices
    {
        Task<Respuesta> GetCliente(DataQuery dataQuery);
        Task<Respuesta> PostCliente(Cliente cliente);
        Task<Respuesta> PutCliente(Cliente cliente);
        Task<Respuesta> DeleteCliente(Cliente cliente);
    }
}