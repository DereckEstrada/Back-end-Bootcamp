using ApiVentas.Interfaces;
using ApiVentas.Models;
using ApiVentas.Utilitarios;
using Microsoft.AspNetCore.Mvc;

namespace ApiVentas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarjetaCreditoController : Controller
    {
        private readonly ITarjetaCredito _tarjetaCredito;
        private ControlError log = new ControlError();
        public TarjetaCreditoController (ITarjetaCredito tarjetaCredito)
        {
            _tarjetaCredito = tarjetaCredito;
        }
        [HttpGet]
        [Route("GetTarjetaCredito")]

        public async Task<Respuesta> GetTarjetaCredito(int? tarjetaCredID, string? tarjetaDescrip)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                respuesta = await _tarjetaCredito.GetTarjetaCredito(tarjetaCredID, tarjetaDescrip);
            }
            catch (Exception ex)
            {
                log.LogErrorMetodos("TarjetaCreditoController", ex.Message, "GetTarjetaCredito");
            }
            return respuesta;
        }
                

        [HttpPost]
        [Route("PostTarjetaCredito")]

        public async Task<Respuesta> PostTarjetaCredito([FromBody] TarjetaCredito tarjetaCred)
        {
            Respuesta respuesta = new Respuesta();

            try
            {
                respuesta = await _tarjetaCredito.PostTarjetaCredito(tarjetaCred);
            }
            catch (Exception ex)
            {
                log.LogErrorMetodos("TarjetaCreditoController", ex.Message, "GetTarjetaCredito");
            }
            return respuesta;
        }


        [HttpPut]
        [Route("PutTarjetaCredito")]

        public async Task<Respuesta> PutTarjetaCredito([FromBody] TarjetaCredito tarjetaCred)
        {
            Respuesta respuesta = new Respuesta();

            try
            {
                respuesta = await _tarjetaCredito.PutTarjetaCredito(tarjetaCred);
            }
            catch (Exception ex)
            {
                log.LogErrorMetodos("TarjetaCreditoController", ex.Message, "GetTarjetaCredito");
            }
            return respuesta;
        }


        [HttpDelete]
        [Route("DeleteTarjetaCredito")]

        public async Task<Respuesta> DeleteTarjetaCredito(int tarjetaCredID)
        {
            Respuesta respuesta = new Respuesta();

            try
            {
                respuesta = await _tarjetaCredito.DeleteTarjetaCredito(tarjetaCredID);
            }
            catch (Exception ex)
            {

                log.LogErrorMetodos("TarjetaCreditoController", ex.Message, "DeleteTarjetaCredito");
            }
            return respuesta;
        }
    }
}
