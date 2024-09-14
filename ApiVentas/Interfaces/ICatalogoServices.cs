using ApiVentas.Models;
using ApiVentas.Utilitarios;

namespace ApiVentas.Interfaces
{
    public interface ICatalogoServices
    {
        Task<Respuesta> GetCategoria(DataQuery dataQuery);
        Task<Respuesta> PostCategoria(Categorium categoria);
        Task<Respuesta> PutCategoria(Categorium categoria);
        Task<Respuesta> DeleteCategoria(Categorium categoria);      
        Task<Respuesta> GetCiudad(DataQuery dataQuery);
        Task<Respuesta> PostCiudad(Ciudad ciudad);
        Task<Respuesta> PutCiudad(Ciudad ciudad);
        Task<Respuesta> DeleteCiudad(Ciudad ciudad);
        Task<Respuesta> GetPais(DataQuery dataQuery);
        Task<Respuesta> PostPais(Pai pais);
        Task<Respuesta> PutPais(Pai pais);
        Task<Respuesta> DeletePais(Pai pais);
        Task<Respuesta> GetFormaPago();
        Task<Respuesta> PostFormaPago(FormaPago formaPago);
        Task<Respuesta> PutFormaPago(FormaPago formaPago);
        Task<Respuesta> DeleteFormaPago(FormaPago formaPago);
        Task<Respuesta> GetTipoMovimiento();
        Task<Respuesta> PostTipoMovimiento(TipoMovimiento tipoMovimiento);
        Task<Respuesta> PutTipoMovimiento(TipoMovimiento tipoMovimiento);
        Task<Respuesta> DeleteTipoMovimiento(TipoMovimiento tipoMovimiento);
        Task<Respuesta> GetIndustria(DataQuery dataQuery);
        Task<Respuesta> PostIndustria(Industrium industria);
        Task<Respuesta> PutIndustria(Industrium industria);
        Task<Respuesta> DeleteIndustria(Industrium industria);
        Task<Respuesta> GetTarjetaCredito();
        Task<Respuesta> PostTarjetaCredito(TarjetaCredito tarjetaCredito);
        Task<Respuesta> PutTarjetaCredito(TarjetaCredito tarjetaCredito);
        Task<Respuesta> DeleteTarjetaCredito(TarjetaCredito tarjetaCredito);
    }
}
