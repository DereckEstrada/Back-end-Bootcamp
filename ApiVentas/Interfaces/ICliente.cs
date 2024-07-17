using ApiVentas.Models;

namespace ApiVentas.Interfaces
{
    public interface ICliente
    {
        Task<Respuesta> GetCliente();
        Task<Respuesta> PostCliente(Cliente cliente);
        Task<Respuesta> PutCliente(Cliente cliente);
        Task<Respuesta> DeleteCliente(Cliente cliente);
    }
}