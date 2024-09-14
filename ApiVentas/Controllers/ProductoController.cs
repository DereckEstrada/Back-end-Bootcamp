using ApiVentas.Interfaces;
using ApiVentas.Models;
using ApiVentas.Utilitarios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ApiVentas.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoServices _productoServices;
        private ControlError log = new ControlError();
        public ProductoController(IProductoServices producto)
        {
            this._productoServices = producto;
        }
        [HttpPost]
        [Route("RestProducto")]
        public async Task<Respuesta> RestProducto([FromBody]Request request)
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
                                result = await this._productoServices.GetProducto(dataQuery);
                            }
                        }
                        break;
                    case "POST":
                        {
                            if (true)
                            {
                                var producto = JsonConvert.DeserializeObject<Producto>(Convert.ToString(request.Data));
                                result = await this._productoServices.PostProducto(producto);
                            }
                        }
                        break;
                    case "PUT":
                        {
                            if (true)
                            {
                                var producto = JsonConvert.DeserializeObject<Producto>(Convert.ToString(request.Data));
                                result = await this._productoServices.PutProducto(producto);
                            }
                        }
                        break;
                    case "DELETE":
                        {
                            if (true)
                            {
                                var producto = JsonConvert.DeserializeObject<Producto>(Convert.ToString(request.Data));
                                result = await this._productoServices.DeleteProducto(producto);
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                log.LogErrorMetodos(this.GetType().Name, "RestProducto", ex.Message);
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
            }
            return result;
        }        
    }
}
