using ApiVentas.Interfaces;
using ApiVentas.Models;
using ApiVentas.Utilitarios;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
namespace ApiVentas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClienteController : Controller
    {
        private readonly IClienteServices _clienteServices;
        private ControlError Log = new ControlError();

        public ClienteController(IClienteServices cliente)
        {
            this._clienteServices = cliente;
        }

        [HttpPost]
        [Route("RestCliente")]
        public async Task<Respuesta> RestCliente([FromBody] Request request)
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
                                result = await this._clienteServices.GetCliente(dataQuery);
                            }
                        }
                        break;
                    case "POST":
                        {
                            if (true)
                            {
                                var cliente = JsonConvert.DeserializeObject<Cliente>(Convert.ToString(request.Data));
                                result = await this._clienteServices.PostCliente(cliente);
                            }
                        }
                        break;
                    case "PUT":
                        {
                            if (true)
                            {
                                var cliente = JsonConvert.DeserializeObject<Cliente>(Convert.ToString(request.Data));
                                result = await this._clienteServices.PutCliente(cliente);
                            }
                        }
                        break;
                    case "DELETE":
                        {
                            if (true)
                            {
                                var cliente = JsonConvert.DeserializeObject<Cliente>(Convert.ToString(request.Data));
                                result = await this._clienteServices.DeleteCliente(cliente);
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Log.LogErrorMetodos(this.GetType().Name, "RestCliente", ex.Message);
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
            }
            return result;
        }
    }
}
