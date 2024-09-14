using ApiVentas.Interfaces;
using ApiVentas.Models;
using ApiVentas.Utilitarios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ApiVentas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BodegaController : Controller
    {
        private readonly IBodegaServices _bodegaServices;
        private ControlError Log = new ControlError();

        public BodegaController(IBodegaServices bodega)
        {
            this._bodegaServices = bodega;
        }

        [HttpPost]
        [Route("RestBodega")]
        public async Task<Respuesta> GetBodega([FromBody] Request request)
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
                                result= await this._bodegaServices.GetBodega(dataQuery);
                            }
                        }
                        break;
                    case "POST":
                        {
                            if (true)
                            {
                                var bodega = JsonConvert.DeserializeObject<Bodega>(Convert.ToString(request.Data));
                                result= await this._bodegaServices.PostBodega(bodega);
                            }
                        }
                        break;
                    case "PUT":
                        {
                            if (true)
                            {
                                var bodega = JsonConvert.DeserializeObject<Bodega>(Convert.ToString(request.Data));
                                result = await this._bodegaServices.PutBodega(bodega);
                            }
                        }
                        break;
                    case "DELETE":
                        {
                            if (true)
                            {
                                var bodega = JsonConvert.DeserializeObject<Bodega>(Convert.ToString(request.Data));
                                result = await this._bodegaServices.DeleteBodega(bodega);
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Log.LogErrorMetodos(this.GetType().Name, "RestBodega", ex.Message);
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
            }
            return result;
        }      
    }
}
