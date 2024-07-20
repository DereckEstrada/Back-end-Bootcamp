using ApiVentas.Interfaces;
using ApiVentas.Models;
using ApiVentas.Utilitarios;
using Microsoft.AspNetCore.Mvc;

namespace ApiVentas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUsuario _usuario;
        private ControlError log = new ControlError();
        public UserController( IUsuario usuario)
        {
            _usuario = usuario;
        }
        [HttpGet]
        [Route("GetUsuario")]

        public async Task<Respuesta> GetUsuario(int? usuID, string? usuNombre)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                respuesta = await _usuario.GetUsuario(usuID, usuNombre);
            }
            catch (Exception ex)
            {

                log.LogErrorMetodos("UserController", ex.Message, "GetUsuario");
            }
            return respuesta;
        }

        [HttpPost]
        [Route("PostUsuario")]

        public async Task<Respuesta> PostUsuario([FromBody] Usuario usuario)
        {
            Respuesta respuesta = new Respuesta();

            try
            {
                respuesta = await _usuario.PostUsuario(usuario);
            }
            catch (Exception ex)
            {

                log.LogErrorMetodos("UserController", ex.Message, "PosttUsuario");
            }
            return respuesta;
        }

        [HttpPut]
        [Route("PutUsuario")]

        public async Task<Respuesta> PutUsuario([FromBody] Usuario Usuario)
        {
            Respuesta respuesta = new Respuesta();

            try
            {
                respuesta = await _usuario.PutUsuario(Usuario);
            }
            catch (Exception ex)
            {

                log.LogErrorMetodos("UserController", ex.Message, "PutUsuario");
            }
            return respuesta;
        }

        [HttpDelete]
        [Route("DeleteUsuario")]

        public async Task<Respuesta> DeleteUsuario(int usuID)
        {
            Respuesta respuesta = new Respuesta();

            try
            {
                respuesta = await _usuario.DeleteUsuario(usuID);
            }
            catch (Exception ex)
            {

                log.LogErrorMetodos("UserController", ex.Message, "DeleteUsuario");
            }
            return respuesta;
        }
    }
}
