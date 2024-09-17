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
    public class StockController : ControllerBase
    {
        private readonly IStockServices _stockServices;
        private ControlError log = new ControlError();
        public StockController(IStockServices stock)
        {
            this._stockServices = stock;
        }
        [HttpPost]
        [Route("RestStock")]
        public async Task<Respuesta> RestStock([FromBody] Request request)
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
                                result = await this._stockServices.GetStock(dataQuery);
                            }
                        }
                        break;
                    case "POST":
                        {
                            if (true)
                            {
                                var stock = JsonConvert.DeserializeObject<Stock>(Convert.ToString(request.Data));
                                result = await this._stockServices.PostStock(stock);
                            }
                        }
                        break;
                    case "PUT":
                        {
                            if (true)
                            {
                                var stock = JsonConvert.DeserializeObject<Stock>(Convert.ToString(request.Data));
                                result = await this._stockServices.PutStock(stock);
                            }
                        }
                        break;
                    case "DELETE":
                        {
                            if (true)
                            {
                                var stock = JsonConvert.DeserializeObject<Stock>(Convert.ToString(request.Data));
                                return await this._stockServices.DeleteStock(stock);
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                log.LogErrorMetodos(this.GetType().Name, "RestStock", ex.Message);
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
            }
            return result;
        }
    }
}
