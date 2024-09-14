using ApiVentas.Interfaces;
using ApiVentas.Models;
using ApiVentas.Utilitarios;
using ApiVentas.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ejemploEntity.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovimientoController : Controller
    {
        private readonly IMovimientosServices _movimientosServices;
        public ControlError log = new ControlError();
        public string clase = "MovimientoController";

        public MovimientoController(IMovimientosServices movimientos)
        {
            _movimientosServices = movimientos;
        }

        [HttpPost]
        [Route("RestMovimientoCab")]
        public async Task<Respuesta> RestMovimientoCab([FromBody] Request request)
        {
            var result = new Respuesta();
            try
            {
                switch (request.Operacion)
                {
                    case "GET":
                        {
                            if (true)
                            {
                                var dataQuery = JsonConvert.DeserializeObject<DataQuery>(Convert.ToString(request.Data));
                                result = await this._movimientosServices.GetMovimientoCab(dataQuery);
                            }
                        }
                        break;
                    case "POST":
                        {
                            if (true)
                            {
                                var movimientoCab= JsonConvert.DeserializeObject<MovimientoCab>(Convert.ToString(request.Data));
                                result = await this._movimientosServices.PostMovimientoCab(movimientoCab);
                            }
                        }
                        break;
                    case "PUT":
                        {
                            if (true)
                            {
                                var movimientoCab = JsonConvert.DeserializeObject<MovimientoCab>(Convert.ToString(request.Data));
                                result = await this._movimientosServices.PutMovimientoCab(movimientoCab);
                            }
                        }
                        break;
                    case "DELETE":
                        {
                            if (true)
                            {
                                var movimientoCab = JsonConvert.DeserializeObject<MovimientoCab>(Convert.ToString(request.Data));
                                result = await this._movimientosServices.DeleteMovimientoCab(movimientoCab);
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                log.LogErrorMetodos(this.GetType().Name, "RestMovimientoCab", ex.Message);
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
            }
            return result;
        }


        [HttpPost]
        [Route("RestMovimientoDetPagos")]
        public async Task<Respuesta> RestMovimientoDetPagos([FromBody] Request request)
        {
            var result = new Respuesta();
            try
            {
                switch (request.Operacion)
                {
                    case "GET":
                        {
                            if (true)
                            {
                                var dataQuery = JsonConvert.DeserializeObject<DataQuery>(Convert.ToString(request.Data));
                                result = await this._movimientosServices.GetMovimientoDetPago(dataQuery);
                            }
                        }
                        break;
                    case "POST":
                        {
                            if (true)
                            {
                                var movimientoDetPago = JsonConvert.DeserializeObject<MovimientoDetPago>(Convert.ToString(request.Data));
                                result = await this._movimientosServices.PostMovimientoDetPago(movimientoDetPago);
                            }
                        }
                        break;
                    case "PUT":
                        {
                            if (true)
                            {
                                var movimientoDetPago = JsonConvert.DeserializeObject<MovimientoDetPago>(Convert.ToString(request.Data));
                                result = await this._movimientosServices.PutMovimientoDetPago(movimientoDetPago);
                            }
                        }
                        break;
                    case "DELETE":
                        {
                            if (true)
                            {
                                var movimientoDetPago = JsonConvert.DeserializeObject<MovimientoDetPago>(Convert.ToString(request.Data));
                                result = await this._movimientosServices.DeleteMovimientoDetPago(movimientoDetPago);
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                log.LogErrorMetodos(this.GetType().Name, "RestMovimientoDetPagos", ex.Message);
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
            }
            return result;
        }


        [HttpPost]
        [Route("RestMovimientoDetProducto")]
        public async Task<Respuesta> RestMovimientoDetProducto([FromBody] Request request)
        {
            var result = new Respuesta();
            try
            {
                switch (request.Operacion)
                {
                    case "GET":
                        {
                            if (true)
                            {
                                var dataQuery = JsonConvert.DeserializeObject<DataQuery>(Convert.ToString(request.Data));
                                result = await this._movimientosServices.GetMovimientoDetProducto(dataQuery);
                            }
                        }
                        break;
                    case "POST":
                        {
                            if (true)
                            {
                                var movimientoDetProducto = JsonConvert.DeserializeObject<MovimientoDetProducto>(Convert.ToString(request.Data));
                                result = await this._movimientosServices.PostMovimientoDetProducto(movimientoDetProducto);
                            }
                        }
                        break;
                    case "PUT":
                        {
                            if (true)
                            {
                                var movimientoDetProducto = JsonConvert.DeserializeObject<MovimientoDetProducto>(Convert.ToString(request.Data));
                                result = await this._movimientosServices.PostMovimientoDetProducto(movimientoDetProducto);
                            }
                        }
                        break;
                    case "DELETE":
                        {
                            if (true)
                            {
                                var movimientoDetProducto = JsonConvert.DeserializeObject<MovimientoDetProducto>(Convert.ToString(request.Data));
                                result = await this._movimientosServices.DeleteMovimientoDetProducto(movimientoDetProducto);
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                log.LogErrorMetodos(this.GetType().Name, "RestMovimientoDetProducto", ex.Message);
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
            }
            return result;
        }
    }
}
