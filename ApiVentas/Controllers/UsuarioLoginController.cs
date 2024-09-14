//using ApiVentas.DTOs;
//using ApiVentas.Interfaces;
//using ApiVentas.Models;
//using ApiVentas.Utilitarios;
//using Microsoft.AspNetCore.Mvc;
//using Newtonsoft.Json;

//namespace ApiVentas.Controllers
//{
//    [ApiController]
//    [Route("[controller]")]
//    public class UsuarioLoginController : Controller
//    {
//        private readonly IUsuarioLogin _login;
//        private ControlError log = new ControlError();
//        private Validaciones validacion = new Validaciones();
//        public UsuarioLoginController(IUsuarioLogin login)
//        {
//            this._login = login;
//        }
//        [HttpPost]
//        [Route("GetLogin")]
//        public async Task<Respuesta> GetLogin([FromBody] Request request)
//        {
//            Respuesta respuesta = new Respuesta();
//            userLogDto userDto = new userLogDto();

//            try
//            {
//                //   if (validacion.validacionRequest(request))
//                // {
//                userDto = JsonConvert.DeserializeObject<userLogDto>(Convert.ToString(request.data));
//                // }
//                respuesta = await _login.GetLogin(userDto);
//            }
//            catch (Exception ex)
//            {

//                log.LogErrorMetodos("UserController", ex.Message, "GetLogin");
//            }
//            return respuesta;
//        }

//        [HttpPost]
//        [Route("PostLogin")]

//        public async Task<Respuesta> PostLogin([FromBody] UsuarioAutenticacion login)
//        {
//            Respuesta respuesta = new Respuesta();

//            try
//            {
//                respuesta = await _login.PostLogin(login);
//            }
//            catch (Exception ex)
//            {

//                log.LogErrorMetodos("UserController", ex.Message, "PosttUsuario");
//            }
//            return respuesta;
//        }

//        [HttpPut]
//        [Route("PutLogin")]

//        public async Task<Respuesta> PutLogin([FromBody] UsuarioAutenticacion UsuarioAutenticacion)
//        {
//            Respuesta respuesta = new Respuesta();

//            try
//            {
//                respuesta = await _login.PutLogin(UsuarioAutenticacion);
//            }
//            catch (Exception ex)
//            {

//                log.LogErrorMetodos("UserController", ex.Message, "PutLogin");
//            }
//            return respuesta;
//        }


//    }
//}

