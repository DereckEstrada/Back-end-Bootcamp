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
    public class PuntoEmisionSriController : ControllerBase
    {
        private readonly IPuntoEmisionSriServices _puntoEmisionSriServices;
        private ControlError log = new ControlError();
        public PuntoEmisionSriController(IPuntoEmisionSriServices puntoEmisionSri)
        {
            this._puntoEmisionSriServices = puntoEmisionSri;
        }
        [HttpPost]
        [Route("RestPuntoEmisionSri")]
        public async Task<Respuesta> RestPuntoEmisionSri([FromBody] Request request)
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
                                result = await this._puntoEmisionSriServices.GetPuntoEmisionSri(dataQuery);
                            }
                        }
                        break;
                    case "POST":
                        {
                            if (true)
                            {
                                var puntoEmisionSri = JsonConvert.DeserializeObject<PuntoEmisionSri>(Convert.ToString(request.Data));
                                result = await this._puntoEmisionSriServices.PostPuntoEmisionSri(puntoEmisionSri);
                            }
                        }
                        break;
                    case "PUT":
                        {
                            if (true)
                            {
                                var puntoEmisionSri = JsonConvert.DeserializeObject<PuntoEmisionSri>(Convert.ToString(request.Data));
                                result = await this._puntoEmisionSriServices.PutPuntoEmisionSri(puntoEmisionSri);
                            }
                        }
                        break;
                    case "DELETE":
                        {
                            if (true)
                            {
                                var puntoEmisionSri = JsonConvert.DeserializeObject<PuntoEmisionSri>(Convert.ToString(request.Data));
                                result = await this._puntoEmisionSriServices.DeletePuntoEmisionSri(puntoEmisionSri);
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                log.LogErrorMetodos(this.GetType().Name, "RestPuntoEmisionSri", ex.Message);
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
            }
            return result;
        }       
    }
}
