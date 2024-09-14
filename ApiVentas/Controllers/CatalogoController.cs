using ApiVentas.Interfaces;
using ApiVentas.Models;
using ApiVentas.Utilitarios;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ApiVentas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CatalogoController : Controller
    {
        private readonly ICatalogoServices _catalogoServices;
        private ControlError log = new ControlError();

        public CatalogoController(ICatalogoServices catalogo)
        {
            this._catalogoServices = catalogo;
        }

        [HttpPost]
        [Route("RestCategoria")]
        public async Task<Respuesta> RestCategoria([FromBody]Request request)
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
                                result = await this._catalogoServices.GetCategoria(dataQuery);
                            }
                        }
                        break;
                    case "POST":
                        {
                            if (true)
                            {
                                var categoria = JsonConvert.DeserializeObject<Categorium>(Convert.ToString(request.Data));
                                result = await this._catalogoServices.PostCategoria(categoria);
                            }
                        }
                        break;
                    case "PUT":
                        {
                            if (true)
                            {
                                var categoria = JsonConvert.DeserializeObject<Categorium>(Convert.ToString(request.Data));
                                result = await this._catalogoServices.DeleteCategoria(categoria);
                            }
                        }
                        break;
                    case "DELETE":
                        {
                            if (true)
                            {
                                var categoria = JsonConvert.DeserializeObject<Categorium>(Convert.ToString(request.Data));
                                result = await this._catalogoServices.DeleteCategoria(categoria);
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                log.LogErrorMetodos(this.GetType().Name, "RestCategoria", ex.Message);
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
            }
            return result;
        }


        [HttpPost]
        [Route("RestCiudad")]
        public async Task<Respuesta> RestCiudad([FromBody] Request request)
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
                                result = await this._catalogoServices.GetCiudad(dataQuery);
                            }
                        }
                        break;
                    case "POST":
                        {
                            if (true)
                            {
                                var ciudad = JsonConvert.DeserializeObject<Ciudad>(Convert.ToString(request.Data));
                                result = await this._catalogoServices.PostCiudad(ciudad);
                            }
                        }
                        break;
                    case "PUT":
                        {
                            if (true)
                            {
                                var ciudad = JsonConvert.DeserializeObject<Ciudad>(Convert.ToString(request.Data));
                                result = await this._catalogoServices.PutCiudad(ciudad);
                            }
                        }
                        break;
                    case "DELETE":
                        {
                            if (true)
                            {
                                var ciudad = JsonConvert.DeserializeObject<Ciudad>(Convert.ToString(request.Data));
                                result = await this._catalogoServices.DeleteCiudad(ciudad);
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                log.LogErrorMetodos(this.GetType().Name, "RestCiudad", ex.Message);
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
            }
            return result;
        }


        [HttpPost]
        [Route("RestPais")]
        public async Task<Respuesta> RestPais([FromBody] Request request)
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
                                result = await this._catalogoServices.GetPais(dataQuery);
                            }
                        }
                        break;
                    case "POST":
                        {
                            if (true)
                            {
                                var pais = JsonConvert.DeserializeObject<Pai>(Convert.ToString(request.Data));
                                result= await this._catalogoServices.PostPais(pais);
                            }
                        }
                        break;
                    case "PUT":
                        {
                            if (true)
                            {
                                var pais = JsonConvert.DeserializeObject<Pai>(Convert.ToString(request.Data));
                                result= await this._catalogoServices.PutPais(pais);
                            }
                        }
                        break;
                    case "DELETE":
                        {
                            if (true)
                            {
                                var pais = JsonConvert.DeserializeObject<Pai>(Convert.ToString(request.Data));
                                result = await this._catalogoServices.DeletePais(pais);
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                log.LogErrorMetodos(this.GetType().Name, "RestPais", ex.Message);
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
            }
            return result;
        }


        [HttpPost]
        [Route("RestFormaPago")]
        public async Task<Respuesta> RestFormaPago([FromBody] Request request)
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
                                result = await this._catalogoServices.GetFormaPago();
                            }
                        }
                        break;
                    case "POST":
                        {
                            if (true)
                            {
                                var formaPago = JsonConvert.DeserializeObject<FormaPago>(Convert.ToString(request.Data));
                                result= await this._catalogoServices.PostFormaPago(formaPago);
                            }
                        }
                        break;
                    case "PUT":
                        {
                            if (true)
                            {
                                var formaPago = JsonConvert.DeserializeObject<FormaPago>(Convert.ToString(request.Data));
                                result= await this._catalogoServices.PutFormaPago(formaPago);
                            }
                        }
                        break;
                    case "DELETE":
                        {
                            if (true)
                            {
                                var formaPago = JsonConvert.DeserializeObject<FormaPago>(Convert.ToString(request.Data));
                                result = await this._catalogoServices.DeleteFormaPago(formaPago);
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                log.LogErrorMetodos(this.GetType().Name, "RestFormaPago", ex.Message);
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
            }
            return result;
        }


        [HttpPost]
        [Route("RestTipoMovimiento")]
        public async Task<Respuesta> RestTipoMovimiento([FromBody] Request request)
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
                                result = await this._catalogoServices.GetTipoMovimiento();
                            }
                        }
                        break;
                    case "POST":
                        {
                            if (true)
                            {
                                var tipoMovimiento = JsonConvert.DeserializeObject<TipoMovimiento>(Convert.ToString(request.Data));
                                result = await this._catalogoServices.PostTipoMovimiento(tipoMovimiento);
                            }
                        }
                        break;
                    case "PUT":
                        {
                            if (true)
                            {
                                var tipoMovimiento = JsonConvert.DeserializeObject<TipoMovimiento>(Convert.ToString(request.Data));
                                result = await this._catalogoServices.PutTipoMovimiento(tipoMovimiento);
                            }
                        }
                        break;
                    case "DELETE":
                        {
                            if (true)
                            {
                                var tipoMovimiento = JsonConvert.DeserializeObject<TipoMovimiento>(Convert.ToString(request.Data));
                                result = await this._catalogoServices.DeleteTipoMovimiento(tipoMovimiento);
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                log.LogErrorMetodos(this.GetType().Name, "RestTipoMovimiento", ex.Message);
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
            }
            return result;
        }


        [HttpPost]
        [Route("RestIndustria")]
        public async Task<Respuesta> RestIndustria([FromBody] Request request)
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
                                result = await this._catalogoServices.GetIndustria(dataQuery);
                            }
                        }
                        break;
                    case "POST":
                        {
                            if (true)
                            {
                                var industria = JsonConvert.DeserializeObject<Industrium>(Convert.ToString(request.Data));
                                result = await this._catalogoServices.PostIndustria(industria);
                            }
                        }
                        break;
                    case "PUT":
                        {
                            if (true)
                            {
                                var industria = JsonConvert.DeserializeObject<Industrium>(Convert.ToString(request.Data));
                                result = await this._catalogoServices.PutIndustria(industria);
                            }
                        }
                        break;
                    case "DELETE":
                        {
                            if (true)
                            {
                                var industria = JsonConvert.DeserializeObject<Industrium>(Convert.ToString(request.Data));
                                result = await this._catalogoServices.DeleteIndustria(industria);
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                log.LogErrorMetodos(this.GetType().Name, "RestIndustria", ex.Message);
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
            }
            return result;
        }


        [HttpPost]
        [Route("RestTarjetaCredito")]
        public async Task<Respuesta> RestTarjetaCredito([FromBody] Request request)
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
                                result = await this._catalogoServices.GetTarjetaCredito();
                            }
                        }
                        break;
                    case "POST":
                        {
                            if (true)
                            {
                                var tarjetaCredito = JsonConvert.DeserializeObject<TarjetaCredito>(Convert.ToString(request.Data));
                                result = await this._catalogoServices.PostTarjetaCredito(tarjetaCredito);
                            }
                        }
                        break;
                    case "PUT":
                        {
                            if (true)
                            {
                                var tarjetaCredito = JsonConvert.DeserializeObject<TarjetaCredito>(Convert.ToString(request.Data));
                                result = await this._catalogoServices.PutTarjetaCredito(tarjetaCredito);
                            }
                        }
                        break;
                    case "DELETE":
                        {
                            if (true)
                            {
                                var tarjetaCredito = JsonConvert.DeserializeObject<TarjetaCredito>(Convert.ToString(request.Data));
                                result = await this._catalogoServices.DeleteTarjetaCredito(tarjetaCredito);
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                log.LogErrorMetodos(this.GetType().Name, "RestTarjetaCredito", ex.Message);
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
            }
            return result;
        }
    }
}
