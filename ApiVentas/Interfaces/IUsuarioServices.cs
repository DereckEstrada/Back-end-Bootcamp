using ApiVentas.Models;
using ApiVentas.Utilitarios;

namespace ApiVentas.Interfaces
{
    public interface IUsuarioServices
    {
        Task<Respuesta> GetUsuario(DataQuery dataQuery);
        Task<Respuesta> PostUsuario(Usuario usuario);
        Task<Respuesta> PutUsuario(Usuario usuario);
        Task<Respuesta> DeleteUsuario(Usuario usuario);
        Task<Respuesta> GetUsuarioRol();
        Task<Respuesta> PostUsuarioRol(UsuarioRol usuarioRol);
        Task<Respuesta> PutUsuarioRol(UsuarioRol usuarioRol);
        Task<Respuesta> DeleteUsuarioRol(UsuarioRol usuarioRol);
        Task<Respuesta> GetUsuarioPermiso(DataQuery dataQuery);
        Task<Respuesta> PostUsuarioPermiso(UsuarioPermiso usuarioPermiso);
        Task<Respuesta> PutUsuarioPermiso(UsuarioPermiso usuarioPermiso);
        Task<Respuesta> DeleteUsuarioPermiso(UsuarioPermiso usuarioPermiso);
        Task<Respuesta> GetOpcion();
        Task<Respuesta> PostOpcion(Opcion opcion);
        Task<Respuesta> PutOpcion(Opcion opcion);
        Task<Respuesta> DeleteOpcion(Opcion opcion);
        Task<Respuesta> GetModulo();
        Task<Respuesta> PostModulo(Modulo modulo);
        Task<Respuesta> PutModulo(Modulo modulo);
        Task<Respuesta> DeleteModulo(Modulo modulo);
        Task<Respuesta> GetRol();
        Task<Respuesta> PostRol(Rol rol);
        Task<Respuesta> PutRol(Rol rol);
        Task<Respuesta> DeleteRol(Rol rol);
        Task<Respuesta> GetUsuarioAutenticacion(DataUsuarioAutenticacion dataUsuarioAutenticacion);
        Task<Respuesta> PostUsuarioAutenticacion(UsuarioAutenticacion usuarioAutenticacion);
        Task<Respuesta> PutUsuarioAutenticacion(UsuarioAutenticacion usuarioAutenticacion);
    }
}
