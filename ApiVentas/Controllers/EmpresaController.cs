using ApiVentas.Interfaces;
using ApiVentas.Models;
using ApiVentas.Utilitarios;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ApiVentas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmpresaController : Controller
    {
        private readonly IEmpresaServices _empresaServices;
        private ControlError Log = new ControlError();

        public EmpresaController(IEmpresaServices empresa)
        {
            this._empresaServices= empresa;
        }

        [HttpPost]
        [Route("RestEmpresa")]
        public async Task<Respuesta> RestEmpresa([FromBody] Request request)
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
                                result = await this._empresaServices.GetEmpresa(dataQuery);
                            }
                        }
                        break;
                    case "POST":
                        {
                            if (true)
                            {
                                var empresa = JsonConvert.DeserializeObject<Empresa>(Convert.ToString(request.Data));
                                result = await this._empresaServices.PostEmpresa(empresa);
                            }
                        }
                        break;
                    case "PUT":
                        {
                            if (true)
                            {
                                var empresa = JsonConvert.DeserializeObject<Empresa>(Convert.ToString(request.Data));
                                result = await this._empresaServices.PutEmpresa(empresa);
                            }
                        }
                        break;
                    case "DELETE":
                        {
                            if (true)
                            {
                                var empresa = JsonConvert.DeserializeObject<Empresa>(Convert.ToString(request.Data));
                                result = await this._empresaServices.DeleteEmpresa(empresa);
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Log.LogErrorMetodos(this.GetType().Name, "RestEmpresa", ex.Message);
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
            }
            return result;
        }
    }
}
