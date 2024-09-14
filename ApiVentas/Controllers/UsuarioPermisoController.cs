//using ApiVentas.Interfaces;
//using ApiVentas.Models;
//using ApiVentas.Utilitarios;
//using Microsoft.AspNetCore.Mvc;

//namespace ApiVentas.Controllers
//{
//    [ApiController]
//    [Route("[controller]")]
//    public class UsuarioPermisoController : Controller
//    {
//        private readonly IUsuarioPermiso _usuarioPermiso;
//        private ControlError log = new ControlError();
//        public UsuarioPermisoController(IUsuarioPermiso usuarioPermiso)
//        {
//            this._usuarioPermiso = usuarioPermiso;
//        }

//        [HttpGet]
//        [Route("GetUsuarioPermiso")]

//        public async Task<Respuesta> GetUsuarioPermiso(int? permisoID, string? usuNombre, string? moduloDescrip)
//        {
//            Respuesta respuesta = new Respuesta();
//            try
//            {
//                respuesta = await _usuarioPermiso.GetUsuarioPermiso(permisoID, usuNombre, moduloDescrip);
//            }
//            catch (Exception ex)
//            {

//                log.LogErrorMetodos("UsuarioPermisoController", ex.Message, "GetUsuarioPermiso");
//            }
//            return respuesta;
//        }

//        [HttpPost]
//        [Route("PostUsuarioPermiso")]

//        public async Task<Respuesta> PostUsuarioPermiso([FromBody] UsuarioPermiso usuarioPermiso)
//        {
//            Respuesta respuesta = new Respuesta();

//            try
//            {
//                respuesta = await _usuarioPermiso.PostUsuarioPermiso(usuarioPermiso);
//            }
//            catch (Exception ex)
//            {

//                log.LogErrorMetodos("UsuarioPermisoController", ex.Message, "PosttUsuarioPermiso");
//            }
//            return respuesta;
//        }

//        [HttpPut]
//        [Route("PutUsuarioPermiso")]

//        public async Task<Respuesta> PutUsuarioPermiso([FromBody] UsuarioPermiso usuarioPermiso)
//        {
//            Respuesta respuesta = new Respuesta();

//            try
//            {
//                respuesta = await _usuarioPermiso.PutUsuarioPermiso(usuarioPermiso);
//            }
//            catch (Exception ex)
//            {

//                log.LogErrorMetodos("UsuarioPermisoController", ex.Message, "PutUsuarioPermiso");
//            }
//            return respuesta;
//        }

//        [HttpDelete]
//        [Route("DeleteUsuarioPermiso")]

//        public async Task<Respuesta> DeleteUsuarioPermiso(int permisoID)
//        {
//            Respuesta respuesta = new Respuesta();

//            try
//            {
//                respuesta = await _usuarioPermiso.DeleteUsuarioPermiso(permisoID);
//            }
//            catch (Exception ex)
//            {

//                log.LogErrorMetodos("UsuarioPermisoController", ex.Message, "DeleteUsuarioPermiso");
//            }
//            return respuesta;
//        }

//    }
//}