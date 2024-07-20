using ApiVentas.Models;

namespace ApiVentas.Interfaces
{
    public interface IUsuario
    {
        Task<Respuesta> GetUsuario(int? usuID, string? usuNombre);
        Task<Respuesta> PostUsuario(Usuario usuario);
        Task<Respuesta> PutUsuario(Usuario usuario);
        Task<Respuesta> DeleteUsuario(int usuID);
    }
}
