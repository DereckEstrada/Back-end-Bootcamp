using ApiVentas.Interfaces;
using ApiVentas.Models;
using ApiVentas.Utilitarios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ApiVentas.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PuntoVentaController : ControllerBase
    {
        private readonly IPuntoVentaServices _puntoVentaServices;
        private ControlError log = new ControlError();
        public PuntoVentaController(IPuntoVentaServices puntoVenta)
        {
            this._puntoVentaServices = puntoVenta;
        }
        [HttpGet]
        [Route("RestPuntoVenta")]
        public async Task<Respuesta> RestPuntoVenta(Request request)
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
                                result = await this._puntoVentaServices.GetPuntoVenta(dataQuery);
                            }
                        }
                        break;
                    case "POST":
                        {
                            if (true)
                            {
                                var puntoVenta = JsonConvert.DeserializeObject<PuntoVentum>(Convert.ToString(request.Data));
                                result = await this._puntoVentaServices.PostPuntoVenta(puntoVenta);
                            }
                        }
                        break;
                    case "PUT":
                        {
                            if (true)
                            {
                                var puntoVenta = JsonConvert.DeserializeObject<PuntoVentum>(Convert.ToString(request.Data));
                                result = await this._puntoVentaServices.PutPuntoVenta(puntoVenta);
                            }
                        }
                        break;
                    case "DELETE":
                        {
                            if (true)
                            {
                                var puntoVenta = JsonConvert.DeserializeObject<PuntoVentum>(Convert.ToString(request.Data));
                                result = await this._puntoVentaServices.DeletePuntoVenta(puntoVenta);
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                log.LogErrorMetodos(this.GetType().Name, "RestPuntoVenta", ex.Message);
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
            }
            return result;
        }  
    }
}
