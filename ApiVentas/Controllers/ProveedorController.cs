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
    public class ProveedorController : ControllerBase
    {
        private readonly IProveedorServices _proveedorServices;
        private ControlError log = new ControlError();
        public ProveedorController(IProveedorServices proveedor)
        {
            this._proveedorServices = proveedor;
        }
        [HttpPost]
        [Route("RestProveedor")]
        public async Task<Respuesta> RestProveedor([FromBody]Request request)
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
                                result = await this._proveedorServices.GetProveedor(dataQuery);
                            }
                        }
                        break;
                    case "POST":
                        {
                            if (true)
                            {
                                var proveedor = JsonConvert.DeserializeObject<Proveedor>(Convert.ToString(request.Data));
                                return await this._proveedorServices.PostProveedor(proveedor);
                            }
                        }
                        break;
                    case "PUT":
                        {
                            if (true)
                            {
                                var proveedor = JsonConvert.DeserializeObject<Proveedor>(Convert.ToString(request.Data));
                                result = await this._proveedorServices.PutProveedor(proveedor);
                            }
                        }
                        break;
                    case "DELETE":
                        {
                            if (true)
                            {
                                var proveedor = JsonConvert.DeserializeObject<Proveedor>(Convert.ToString(request.Data));
                                result = await this._proveedorServices.DeleteProveedor(proveedor);
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                log.LogErrorMetodos(this.GetType().Name, "RestProveedor", ex.Message);
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
            }
            return result;
        }        
    }
}
