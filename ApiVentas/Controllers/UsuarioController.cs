using ApiVentas.DTOs;
using ApiVentas.Interfaces;
using ApiVentas.Models;
using ApiVentas.Utilitarios;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
namespace ApiVentas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioServices _usuarioServices;
        private ControlError log = new ControlError();
        private Validaciones validacion = new Validaciones();
        public UsuarioController(IUsuarioServices usuario)
        {
            this._usuarioServices = usuario;
        }

        [HttpPost]
        [Route("RestUsuario")]
        public async Task<Respuesta> RestUsuario([FromBody] Request request)
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
                                result = await this._usuarioServices.GetUsuario(dataQuery);
                            }
                        }
                        break;
                    case "POST":
                        {
                            if (true)
                            {
                                var usuario = JsonConvert.DeserializeObject<Usuario>(Convert.ToString(request.Data));
                                result = await this._usuarioServices.PostUsuario(usuario);
                            }
                        }
                        break;
                    case "PUT":
                        {
                            if (true)
                            {
                                var usuario = JsonConvert.DeserializeObject<Usuario>(Convert.ToString(request.Data));
                                result = await this._usuarioServices.PutUsuario(usuario);
                            }
                        }
                        break;
                    case "DELETE":
                        {
                            if (true)
                            {
                                var usuario = JsonConvert.DeserializeObject<Usuario>(Convert.ToString(request.Data));
                                result = await this._usuarioServices.DeleteUsuario(usuario);
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                log.LogErrorMetodos(this.GetType().Name, "RestUsuario", ex.Message);
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
            }
            return result;
        }


        [HttpPost]
        [Route("RestUsuarioRol")]
        public async Task<Respuesta> RestUsuarioRol([FromBody] Request request)
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
                                result = await this._usuarioServices.GetUsuarioRol();
                            }
                        }
                        break;
                    case "POST":
                        {
                            if (true)
                            {
                                var usuarioRol = JsonConvert.DeserializeObject<UsuarioRol>(Convert.ToString(request.Data));
                                result = await this._usuarioServices.PostUsuarioRol(usuarioRol);
                            }
                        }
                        break;
                    case "PUT":
                        {
                            if (true)
                            {
                                var usuarioRol = JsonConvert.DeserializeObject<UsuarioRol>(Convert.ToString(request.Data));
                                result = await this._usuarioServices.PutUsuarioRol(usuarioRol);
                            }
                        }
                        break;
                    case "DELETE":
                        {
                            if (true)
                            {
                                var usuarioRol = JsonConvert.DeserializeObject<UsuarioRol>(Convert.ToString(request.Data));
                                result = await this._usuarioServices.DeleteUsuarioRol(usuarioRol);
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                log.LogErrorMetodos(this.GetType().Name, "RestUsuarioRol", ex.Message);
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
            }
            return result;
        }


        [HttpPost]
        [Route("RestUsuarioPermiso")]
        public async Task<Respuesta> RestUsuarioPermiso([FromBody] Request request)
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
                                result = await this._usuarioServices.GetUsuarioPermiso(dataQuery);
                            }
                        }
                        break;
                    case "POST":
                        {
                            if (true)
                            {
                                var usuarioPermiso = JsonConvert.DeserializeObject<UsuarioPermiso>(Convert.ToString(request.Data));
                                result = await this._usuarioServices.PostUsuarioPermiso(usuarioPermiso);
                            }
                        }
                        break;
                    case "PUT":
                        {
                            if (true)
                            {
                                var usuarioPermiso = JsonConvert.DeserializeObject<UsuarioPermiso>(Convert.ToString(request.Data));
                                result = await this._usuarioServices.PutUsuarioPermiso(usuarioPermiso);
                            }
                        }
                        break;
                    case "DELETE":
                        {
                            if (true)
                            {
                                var usuarioPermiso = JsonConvert.DeserializeObject<UsuarioPermiso>(Convert.ToString(request.Data));
                                result = await this._usuarioServices.DeleteUsuarioPermiso(usuarioPermiso);
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                log.LogErrorMetodos(this.GetType().Name, "RestUsuarioPermiso", ex.Message);
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
            }
            return result;
        }


        [HttpPost]
        [Route("RestOpcion")]
        public async Task<Respuesta> RestOpcion([FromBody] Request request)
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
                                result = await this._usuarioServices.GetOpcion();
                            }
                        }
                        break;
                    case "POST":
                        {
                            if (true)
                            {
                                var opcion = JsonConvert.DeserializeObject<Opcion>(Convert.ToString(request.Data));
                                result = await this._usuarioServices.PostOpcion(opcion);
                            }
                        }
                        break;
                    case "PUT":
                        {
                            if (true)
                            {
                                var opcion = JsonConvert.DeserializeObject<Opcion>(Convert.ToString(request.Data));
                                result = await this._usuarioServices.PutOpcion(opcion);
                            }
                        }
                        break;
                    case "DELETE":
                        {
                            if (true)
                            {
                                var opcion = JsonConvert.DeserializeObject<Opcion>(Convert.ToString(request.Data));
                                result = await this._usuarioServices.DeleteOpcion(opcion);
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                log.LogErrorMetodos(this.GetType().Name, "RestOpcion", ex.Message);
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
            }
            return result;
        }


        [HttpPost]
        [Route("RestModulo")]
        public async Task<Respuesta> RestModulo([FromBody] Request request)
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
                                result = await this._usuarioServices.GetModulo();
                            }
                        }
                        break;
                    case "POST":
                        {
                            if (true)
                            {
                                var modulo = JsonConvert.DeserializeObject<Modulo>(Convert.ToString(request.Data));
                                result = await this._usuarioServices.PostModulo(modulo);
                            }
                        }
                        break;
                    case "PUT":
                        {
                            if (true)
                            {
                                var modulo = JsonConvert.DeserializeObject<Modulo>(Convert.ToString(request.Data));
                                result = await this._usuarioServices.PutModulo(modulo);
                            }
                        }
                        break;
                    case "DELETE":
                        {
                            if (true)
                            {
                                var modulo = JsonConvert.DeserializeObject<Modulo>(Convert.ToString(request.Data));
                                result = await this._usuarioServices.DeleteModulo(modulo);
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                log.LogErrorMetodos(this.GetType().Name, "RestModulo", ex.Message);
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
            }
            return result;
        }


        [HttpPost]
        [Route("RestRol")]
        public async Task<Respuesta> RestRol([FromBody] Request request)
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
                                result = await this._usuarioServices.GetRol();
                            }
                        }
                        break;
                    case "POST":
                        {
                            if (true)
                            {
                                var rol = JsonConvert.DeserializeObject<Rol>(Convert.ToString(request.Data));
                                result = await this._usuarioServices.PostRol(rol);
                            }
                        }
                        break;
                    case "PUT":
                        {
                            if (true)
                            {
                                var rol = JsonConvert.DeserializeObject<Rol>(Convert.ToString(request.Data));
                                result = await this._usuarioServices.PutRol(rol);
                            }
                        }
                        break;
                    case "DELETE":
                        {
                            if (true)
                            {
                                var rol = JsonConvert.DeserializeObject<Rol>(Convert.ToString(request.Data));
                                result = await this._usuarioServices.DeleteRol(rol);
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                log.LogErrorMetodos(this.GetType().Name, "RestRol", ex.Message);
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
            }
            return result;
        }


        [HttpPost]
        [Route("RestUsuarioAutenticacion")]
        public async Task<Respuesta> RestUsuarioAutenticacion(Request request)
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
                                var dataUsuarioAutenticacion = JsonConvert.DeserializeObject<DataUsuarioAutenticacion>(Convert.ToString(request.Data));
                                result = await this._usuarioServices.GetUsuarioAutenticacion(dataUsuarioAutenticacion);
                            }
                        }
                        break;
                    case "POST":
                        {
                            if (true)
                            {
                                var usuarioAutenticacion= JsonConvert.DeserializeObject<UsuarioAutenticacion>(Convert.ToString(request.Data));
                                result = await this._usuarioServices.PostUsuarioAutenticacion(usuarioAutenticacion);
                            }
                        }
                        break;
                    case "PUT":
                        {
                            if (true)
                            {
                                var usuarioAutenticacion = JsonConvert.DeserializeObject<UsuarioAutenticacion>(Convert.ToString(request.Data));
                                result = await this._usuarioServices.PutUsuarioRol(usuarioAutenticacion);
                            }
                        }
                        break;

                }
            }
            catch (Exception ex)
            {
                log.LogErrorMetodos(this.GetType().Name, "RestUsuarioAutenticacion", ex.Message);
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
            }
            return result;
        }

    }
}
