using ApiVentas.Interfaces;
using ApiVentas.Models;
using ApiVentas.Utilitarios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiVentas.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BodegaController : ControllerBase
    {
        private readonly IBodegaServices _bodega;
        private ControlError log = new ControlError();
        public BodegaController(IBodegaServices bodega)
        {
            this._bodega = bodega;
        }
        [HttpGet]
        [Route("GetBodega")]
        public async Task<Respuesta> GetBodega(string? opcion, string? data)
        {
            var result = new Respuesta();
            try
            {
                result = await _bodega.GetBodega(opcion, data);
            }
            catch (Exception ex)
            {
                log.LogError(this.GetType().Name, "GetBodega", ex.Message);
            }
            return result;
        }
        [HttpPost]
        [Route("PostBodega")]
        public async Task<Respuesta> PostBodega([FromBody] Bodega bodega)
        {
            var result = new Respuesta();
            try
            {
                result = await _bodega.PostBodega(bodega);
            }
            catch (Exception ex)
            {
                log.LogError(this.GetType().Name, "PostBodega", ex.Message);
            }
            return result;
        }
        [HttpPut]
        [Route("PutBodega")]
        public async Task<Respuesta> PutBodega([FromBody] Bodega bodega)
        {
            var result = new Respuesta();
            try
            {
                result = await _bodega.PutBodega(bodega);
            }
            catch (Exception ex)
            {
                log.LogError(this.GetType().Name, "PutBodega", ex.Message);
            }
            return result;
        }
        [HttpDelete]
        [Route("DeleteBodega")]
        public async Task<Respuesta> DeleteBodega(int id)
        {
            var result = new Respuesta();
            try
            {
                result = await _bodega.DeleteBodega(id);
            }
            catch (Exception ex)
            {
                log.LogError(this.GetType().Name, "DeleteBodega", ex.Message);
            }
            return result;
        }
    }
}
