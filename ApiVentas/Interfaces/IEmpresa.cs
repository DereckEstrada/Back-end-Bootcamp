using ApiVentas.Models;

namespace ApiVentas.Interfaces
{
    public interface IEmpresa
    {
        Task<Respuesta> GetEmpresa();
        Task<Respuesta> PostEmpresa(Empresa empresa);
        Task<Respuesta> PutEmpresa(Empresa empresa);
        Task<Respuesta> DeleteEmpresa(Empresa empresa);
    }
}