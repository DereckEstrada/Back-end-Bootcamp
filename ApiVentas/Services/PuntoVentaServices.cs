using ApiVentas.DTOs;
using ApiVentas.Interfaces;
using ApiVentas.Models;
using ApiVentas.Utilitarios;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;

namespace ApiVentas.Services
{
    public class PuntoVentaServices: IPuntoVentaServices
    {
        private BaseErpContext _context;
        private PuntoVentaDTO dto = new PuntoVentaDTO();
        private ControlError log = new ControlError();
        private DynamicEmpty empty = new DynamicEmpty();
        public PuntoVentaServices(BaseErpContext context)
        {
            this._context = context;
        }
        public async Task<Respuesta> DeletePuntoVenta(int id)
        {
            var result = new Respuesta();
            try
            {
                var puntoVentaDelete = await _context.PuntoVenta.FirstOrDefaultAsync(x => x.PuntovtaId== id);
                if (puntoVentaDelete != null)
                {
                    puntoVentaDelete.EstadoId = 2;
                    _context.PuntoVenta.Update(puntoVentaDelete);
                    await _context.SaveChangesAsync();
                }
                result.Cod = puntoVentaDelete != null ? "000" : "111";
                result.Mensaje = puntoVentaDelete != null ? "OK" : $"No se encontro registro con id: '{id}'";

            }
            catch (Exception ex)
            {
                result.Cod = "999";
                result.Mensaje = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "DeletePuntoVenta", ex.Message);

            }
            return result;
        }

        public async Task<Respuesta> GetPuntoVenta(string? opcion, string? Data)
        {
            var result = new Respuesta();
            Expression<Func<PuntoVentaDTO, bool>> query = dto.DictionaryPuntoVenta(opcion, Data);
            try
            {
                if (query != null)
                {
                    result.Data = await (from puntoVenta in _context.PuntoVenta
                                         join emision in _context.PuntoEmisionSris on puntoVenta.PuntoEmisionId equals emision.PuntoEmisionId
                                         join s in _context.Sucursals on puntoVenta.SucursalId equals s.SucursalId
                                         join userReg in _context.Usuarios on puntoVenta.UsuIdReg equals userReg.UsuId
                                         join est in _context.Estados on puntoVenta.EstadoId equals est.EstadoId
                                         //join userAct in _context.Usuarios on puntoVenta.UsuIdAct equals userAct.UsuId
                                         select new PuntoVentaDTO
                                         {
                                             PuntovtaId=puntoVenta.PuntovtaId,
                                             PuntovtaNombre=puntoVenta.PuntovtaNombre,
                                             PuntoEmisionId=puntoVenta.PuntoEmisionId,
                                             PuntoEmisionDescrip=emision.PuntoEmision,
                                             EstadoId=puntoVenta.EstadoId,
                                             EstadoDescrip=est.EstadoDescrip,
                                             FechaHoraReg=puntoVenta.FechaHoraReg,
                                             FechaHoraAct=puntoVenta.FechaHoraAct,
                                             UsuIdReg=puntoVenta.UsuIdReg,
                                             UsuRegDescrip=userReg.UsuNombre,
                                             UsuIdAct=puntoVenta.UsuIdAct,
                                             //UsuActDescrip=userAct.UsuNombre,
                                             SucursalId=puntoVenta.SucursalId,
                                             SucursalDescrip=s.SucursalNombre
                                         }).Where(query).ToListAsync();
                }
                result.Cod = empty.IsEmpty(result.Data) ? "111" : "000";
                result.Mensaje = empty.IsEmpty(result.Data) ? $"No se encontro registro con opcion: '{opcion}' con Data: '{Data}'" : "OK";
            }
            catch (Exception ex)
            {
                result.Cod = "999";
                result.Mensaje = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "GetPuntoVenta", ex.Message);

            }
            return result;
        }

        public async Task<Respuesta> PostPuntoVenta(PuntoVentum puntoVenta)
        {
            var result = new Respuesta();
            try
            {
                var id = await _context.PuntoVenta.OrderByDescending(x => x.PuntovtaId).Select(x => x.PuntovtaId).FirstOrDefaultAsync() + 1;
                puntoVenta.PuntovtaId= id;
                puntoVenta.FechaHoraReg = DateTime.Now;
                var validar = puntoVenta.UsuIdReg != null;
                if (validar)
                {
                    _context.PuntoVenta.Add(puntoVenta);
                    await _context.SaveChangesAsync();
                }
                result.Cod = validar ? "000" : "111";
                result.Mensaje = validar ? "Ok" : "No se puede ingresar registro sin datos del usuario";
            }
            catch (Exception ex)
            {
                result.Cod = "999";
                result.Mensaje = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "PostPuntoVenta", ex.Message);

            }
            return result;
        }

        public async Task<Respuesta> PutPuntoVenta(PuntoVentum puntoVenta)
        {
            var result = new Respuesta();
            try
            {
                var validar = await _context.PuntoVenta.AnyAsync(x => x.PuntovtaId== puntoVenta.PuntovtaId);
                var usuarioEdit = puntoVenta.UsuIdAct;
                if (validar && usuarioEdit != null)
                {
                    result.Cod = "000";
                    result.Mensaje = "OK";
                    puntoVenta.UsuIdReg = await _context.PuntoVenta.Where(x => x.PuntovtaId== puntoVenta.PuntovtaId).Select(x => x.UsuIdReg).FirstOrDefaultAsync();
                    puntoVenta.FechaHoraReg = await _context.PuntoVenta.Where(x => x.PuntovtaId == puntoVenta.PuntovtaId).Select(x => x.FechaHoraReg).FirstOrDefaultAsync();
                    puntoVenta.FechaHoraAct = DateTime.Now;
                    _context.PuntoVenta.Update(puntoVenta);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    result.Cod = "111";
                    result.Mensaje = usuarioEdit != null ? $"No se encontro registro con id: '{puntoVenta.PuntovtaId}'" : "No se puede actualizar registro sin los datos del usuario";
                }

            }
            catch (Exception ex)
            {
                result.Cod = "999";
                result.Mensaje = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "PutPuntoVenta", ex.Message);

            }
            return result;
        }
    }
}
