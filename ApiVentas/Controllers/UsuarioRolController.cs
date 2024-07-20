using ApiVentas.Interfaces;
using ApiVentas.Models;
using ApiVentas.Utilitarios;
using Microsoft.AspNetCore.Mvc;

namespace ApiVentas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioRolController : Controller
    {

        private readonly IUsuarioRol _usuarioRol;
        private ControlError log = new ControlError();
        public UsuarioRolController (IUsuarioRol usuarioRol)
        {
            this._usuarioRol = usuarioRol;
        }

        [HttpGet]
        [Route("GetUsuarioRol")]
        public async Task<Respuesta> GetUsuarioRol(int? usuRolID, string? usuNombre, string? rolDescrip)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                respuesta = await _usuarioRol.GetUsuarioRol(usuRolID, usuNombre, rolDescrip);
            }
            catch (Exception ex)
            {

                log.LogErrorMetodos("UsuarioRolController", ex.Message, "GetUsuarioRol");
            }
            return respuesta;
        }

        [HttpPost]
        [Route("PostUsuarioRol")]

        public async Task<Respuesta> PostUsuarioRol([FromBody]UsuarioRol usuarioRol)
        {
            Respuesta respuesta = new Respuesta();

            try
            {
                respuesta = await _usuarioRol.PostUsuarioRol(usuarioRol);
            }
            catch (Exception ex)
            {

                log.LogErrorMetodos("UsuarioRolController", ex.Message, "PostUsuarioRol");
            }
            return respuesta;
        }

        [HttpPut]
        [Route("PutUsuarioRol")]

        public async Task<Respuesta> PutUsuarioRol([FromBody] UsuarioRol usuarioRol)
        {
            Respuesta respuesta = new Respuesta();

            try
            {
                respuesta = await _usuarioRol.PutUsuarioRol(usuarioRol);
            }
            catch (Exception ex)
            {

                log.LogErrorMetodos("UsuarioRolController", ex.Message, "PutUsuarioRol");
            }
            return respuesta;
        }

        [HttpDelete]
        [Route("DeleteUsuarioRol")]

        public async Task<Respuesta> DeleteUsuarioRol(int usuRolID)
        {
            Respuesta respuesta = new Respuesta();

            try
            {
                respuesta = await _usuarioRol.DeleteUsuarioRol(usuRolID);
            }
            catch (Exception ex)
            {

                log.LogErrorMetodos("UsuarioRolController", ex.Message, "DeleteUsuarioRol");
            }
            return respuesta;
        }
    }
}
