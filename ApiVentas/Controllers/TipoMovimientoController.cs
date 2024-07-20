using ApiVentas.Interfaces;
using ApiVentas.Models;
using ApiVentas.Utilitarios;
using Microsoft.AspNetCore.Mvc;

namespace ApiVentas.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class TipoMovimientoController : Controller
    {
        private readonly ITipoMovimiento _tipoMovimiento;
        private ControlError log = new ControlError();
        public TipoMovimientoController (ITipoMovimiento tipoMovimiento)
        {
            _tipoMovimiento = tipoMovimiento;
        }
        [HttpGet]
        [Route("GetTipoMovimiento")]

        public async Task<Respuesta> GetTipoMovimiento(int? tipoMovimientoID, string? movimientoDescrip)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                respuesta = await _tipoMovimiento.GetTipoMovimiento(tipoMovimientoID, movimientoDescrip);
            }
            catch (Exception ex)
            {

                log.LogErrorMetodos("TipoMovimientoController", ex.Message, "GetTipoMovimiento");
            }
            return respuesta;
        }

        [HttpPost]
        [Route("PostTipoMovimiento")]

        public async Task<Respuesta> PostTipoMovimiento([FromBody] TipoMovimiento tipoMovimiento)
        {
            Respuesta respuesta = new Respuesta();

            try
            {
                respuesta = await _tipoMovimiento.PostTipoMovimiento(tipoMovimiento);
            }
            catch (Exception ex)
            {
                log.LogErrorMetodos("TipoMovimientoController", ex.Message, "PostTipoMovimiento");
            }
            return respuesta;
        }

        [HttpPut]
        [Route("PutTipoMovimiento")]

        public async Task<Respuesta> PutTipoMovimiento([FromBody] TipoMovimiento tipoMovimiento)
        {
            Respuesta respuesta = new Respuesta();

            try
            {
                respuesta = await _tipoMovimiento.PutTipoMovimiento(tipoMovimiento);
            }
            catch (Exception ex)
            {

                log.LogErrorMetodos("TipoMovimientoController", ex.Message, "PutTipoMovimiento");
            }
            return respuesta;
        }

        [HttpDelete]
        [Route("DeleteTipoMovimiento")]

        public async Task<Respuesta> DeleteTipoMovimiento(int tipoMovimientoID)
        {
            Respuesta respuesta = new Respuesta();

            try
            {
                respuesta = await _tipoMovimiento.DeleteTipoMovimiento(tipoMovimientoID);
            }
            catch (Exception ex)
            {

                log.LogErrorMetodos("TipoMovimientoController", ex.Message, "DeleteTipoMovimiento");
            }
            return respuesta;
        }

    }
}
