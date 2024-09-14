using ApiVentas.DTOs;
using ApiVentas.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiVentas.Utilitarios
{
    public class RequestValidator
    {
        private BaseErpContext _context;
        public RequestValidator(BaseErpContext context)
        {
            this._context= context; 
        }
        public bool RequestValidGet(Request request)
        {
            bool result = false;
            int tryIntOpcion = 0;
            int tryIntModulo = 0;
            //int.TryParse(request.Modulo, out tryIntModulo);
            int.TryParse(request.Data.OpcionData, out tryIntOpcion);

            var resultDB =  (from moduloBusqueda in _context.ModuloOpcionesBusqueda
                                  join opcionBusqueda in _context.OpcionesBusqueda on moduloBusqueda.OpcionesBusquedaId equals opcionBusqueda.OpcionesBusquedaId
                                  join modulo in _context.Modulos on moduloBusqueda.ModuloId equals modulo.ModuloId
                                  select new RequestDTO
                                  {
                                      moduloId = modulo.ModuloId,
                                      opcionId = opcionBusqueda.OpcionesBusquedaId,
                                      opcionDescripcion = opcionBusqueda.Descripcion,
                                  }).Where(requestDB => (requestDB.moduloId == tryIntModulo && requestDB.opcionId == tryIntOpcion)).FirstOrDefault();
            request.Data.OpcionData = resultDB.opcionDescripcion;
            if (request.Usuario != null && resultDB!=null)
            {
                result = true;
            }

            return result;
        }
    }
}
