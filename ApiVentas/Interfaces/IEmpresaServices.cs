using ApiVentas.Models;
using ApiVentas.Utilitarios;

namespace ApiVentas.Interfaces
{
    public interface IEmpresaServices
    {
        Task<Respuesta> GetEmpresa(DataQuery dataQuery);
        Task<Respuesta> PostEmpresa(Empresa empresa);
        Task<Respuesta> PutEmpresa(Empresa empresa);
        Task<Respuesta> DeleteEmpresa(Empresa empresa);
    }
}