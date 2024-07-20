using ApiVentas.Models;

namespace ApiVentas.Interfaces
{
    public interface IUsuarioRol
    {
        Task<Respuesta> GetUsuarioRol(int? usuRolID, string? usuNombre, string? rolDescrip);
        Task<Respuesta> PostUsuarioRol(UsuarioRol usuarioRol);
        Task<Respuesta> PutUsuarioRol(UsuarioRol usuarioRol);
        Task<Respuesta> DeleteUsuarioRol(int usuRolID);

    }
}
