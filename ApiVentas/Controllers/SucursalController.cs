using ApiVentas.Interfaces;
using ApiVentas.Models;
using ApiVentas.Utilitarios;
using Microsoft.AspNetCore.Mvc;

namespace ApiVentas.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class SucursalController : Controller
    {
        private readonly ISucursal _sucursal;
        private ControlError log = new ControlError();
        public SucursalController(ISucursal sucursal)
        {
            _sucursal = sucursal;
        }
        [HttpGet]
        [Route("GetSucursal")]

        public async Task<Respuesta> GetSucursal(int? sucursalID, string? sucursalRuc, string? estado)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                respuesta = await _sucursal.GetSucursal(sucursalID, sucursalRuc, estado);
            }
            catch (Exception ex)
            {

                log.LogErrorMetodos("SucursalController", ex.Message, "GetSucursal");
            }
            return respuesta;
        }

        [HttpPost]
        [Route("PostSucursal")]

        public async Task<Respuesta> PostSucursal([FromBody] Sucursal sucursal)
        {
            Respuesta respuesta = new Respuesta();

            try
            {
                respuesta = await _sucursal.PostSucursal(sucursal);
            }
            catch (Exception ex)
            {

                log.LogErrorMetodos("SucursalController", ex.Message, "PostSucursal");
            }
            return respuesta;
        }

        [HttpPut]
        [Route("PutSucursal")]

        public async Task<Respuesta> PutSucursal([FromBody] Sucursal sucursal)
        {
            Respuesta respuesta = new Respuesta();

            try
            {
                respuesta = await _sucursal.PutSucursal(sucursal);
            }
            catch (Exception ex)
            {

                log.LogErrorMetodos("SucursalController", ex.Message, "PutSucursal");
            }
            return respuesta;
        }

        [HttpDelete]
        [Route("DeleteSucursal")]

        public async Task<Respuesta> DeleteSucursal(int sucursalID)
        {
            Respuesta respuesta = new Respuesta();

            try
            {
                respuesta = await _sucursal.DeleteSucursal(sucursalID);
            }
            catch (Exception ex)
            {

                log.LogErrorMetodos("SucursalController", ex.Message, "DeleteSucursal");
            }
            return respuesta;
        }

    }
}
