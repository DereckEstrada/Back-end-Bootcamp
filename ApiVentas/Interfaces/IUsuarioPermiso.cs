using ApiVentas.Models;

namespace ApiVentas.Interfaces
{
    public interface IUsuarioPermiso
    {
        Task<Respuesta> GetUsuarioPermiso(int? permisoID, string? usuNombre, string? moduloDescrip);
        Task<Respuesta> PostUsuarioPermiso(UsuarioPermiso usuarioPermiso);
        Task<Respuesta> PutUsuarioPermiso(UsuarioPermiso usuarioPermiso);
        Task<Respuesta> DeleteUsuarioPermiso(int permisoID);
    }
}
