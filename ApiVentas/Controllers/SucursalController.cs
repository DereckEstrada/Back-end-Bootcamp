using ApiVentas.Interfaces;
using ApiVentas.Models;
using ApiVentas.Utilitarios;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ApiVentas.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class SucursalController : Controller
    {
        private readonly ISucursalServices _sucursalServices;
        private ControlError log = new ControlError();
        public SucursalController(ISucursalServices sucursal)
        {
            this._sucursalServices = sucursal;
        }

        [HttpPost]
        [Route("RestSucursal")]

        public async Task<Respuesta> RestSucursal([FromBody] Request request)
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
                                result = await this._sucursalServices.GetSucursal(dataQuery);
                            }
                        }
                        break;
                    case "POST":
                        {
                            if (true)
                            {
                                var sucursal = JsonConvert.DeserializeObject<Sucursal>(Convert.ToString(request.Data));
                                result = await this._sucursalServices.PostSucursal(sucursal);
                            }
                        }
                        break;
                    case "PUT":
                        {
                            if (true)
                            {
                                var sucursal = JsonConvert.DeserializeObject<Sucursal>(Convert.ToString(request.Data));
                                result = await this._sucursalServices.PutSucursal(sucursal);
                            }
                        }
                        break;
                    case "DELETE":
                        {
                            if (true)
                            {
                                var sucursal = JsonConvert.DeserializeObject<Sucursal>(Convert.ToString(request.Data));
                                result = await this._sucursalServices.DeleteSucursal(sucursal);
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                log.LogErrorMetodos(this.GetType().Name, "RestSucursal", ex.Message);
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
            }
            return result;
        }
    }
}
