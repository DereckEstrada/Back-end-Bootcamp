using ApiVentas.Models;

namespace ApiVentas.Interfaces
{
    public interface ICiudad
    {
        Task<Respuesta> GetCiudad();
        Task<Respuesta> PostCiudad(Ciudad ciudad);
        Task<Respuesta> PutCiudad(Ciudad ciudad);
        Task<Respuesta> DeleteCiudad(Ciudad ciudad);
    }
}
