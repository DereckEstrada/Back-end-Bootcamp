using ApiVentas.DTOs;
using ApiVentas.Interfaces;
using ApiVentas.Models;
using ApiVentas.Utilitarios;
using ApiVentas.Utilitarios.Dictionaries;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;

namespace ApiVentas.Services
{
    public class PuntoVentaServices : IPuntoVentaServices, IServices<PuntoVentaServices>
    {
        private BaseErpContext _context;
        private PuntoVentaDTO dto = new PuntoVentaDTO();
        private ControlError log = new ControlError();
        private DynamicEmpty dynamicEmpty = new DynamicEmpty();
        public PuntoVentaServices(BaseErpContext context)
        {
            this._context = context;
        }

        public async Task<Respuesta> GetPuntoVenta(DataQuery dataQuery)
        {
            var result = new Respuesta();
            try
            {

                result.Data = await _context.PuntoVenta
                                            .Include(puntoVenta => puntoVenta.PuntoEmision)
                                            .Include(puntoVenta => puntoVenta.Sucursal)
                                            .Include(puntoVenta => puntoVenta.Estado)
                                            .Include(puntoVenta => puntoVenta.UsuIdRegNavigation)
                                            .Where(PuntoVentaDictionary.GetExpression(dataQuery))
                                            .Select(puntoVenta=>new PuntoVentaDTO
                                            {
                                                PuntovtaId=puntoVenta.PuntovtaId,
                                                PuntovtaNombre=puntoVenta.PuntovtaNombre,
                                                PuntoEmisionId=puntoVenta.PuntoEmisionId,
                                                PuntoEmisionDescripcion=puntoVenta.PuntoEmision.PuntoEmision,
                                                SucursalId=puntoVenta.SucursalId,
                                                SucursalDescripcion=puntoVenta.Sucursal.SucursalNombre,
                                                EstadoId=puntoVenta.EstadoId,
                                                EstadoDescripcion=puntoVenta.Estado.EstadoDescrip,
                                                FechaHoraReg=puntoVenta.FechaHoraReg,
                                                UsuIdReg=puntoVenta.UsuIdReg,
                                                UsuRegName= puntoVenta.UsuIdRegNavigation.UsuNombre,
                                            }).ToListAsync();

                result.Code = dynamicEmpty.IsEmpty(result.Data) ? "204" : "200";
                result.Message = dynamicEmpty.IsEmpty(result.Data) ? $"No se encontro registro con opcion:'{dataQuery.OpcionData}' con data: '{dataQuery.DataFirstQuery}'" : "Ok";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "GetPuntoVenta", ex.Message);

            }
            return result;
        }

        public async Task<Respuesta> PostPuntoVenta(PuntoVentum puntoVenta)
        {
            var result = new Respuesta();
            try
            {
                var query = await _context.PuntoVenta.OrderByDescending(puntoVentaDB => puntoVentaDB.PuntovtaId)
                                                        .Select(idDB => idDB.PuntovtaId).FirstOrDefaultAsync() + 1;
                puntoVenta.PuntovtaId = query;
                puntoVenta.FechaHoraReg = DateTime.Now;

                _context.PuntoVenta.Add(puntoVenta);
                await _context.SaveChangesAsync();
                result.Code = "200";
                result.Message = "Ok";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "PostPuntoVenta", ex.Message);

            }
            return result;
        }

        public async Task<Respuesta> PutPuntoVenta(PuntoVentum puntoVenta)
        {
            var result = new Respuesta();
            try
            {
                var existPuntoVenta = await _context.PuntoVenta.AnyAsync(puntoVentaDB =>
                                                                            puntoVentaDB.PuntovtaId == puntoVenta.PuntovtaId);
                if (existPuntoVenta)
                {
                    puntoVenta.FechaHoraAct = DateTime.Now;

                    _context.PuntoVenta.Update(puntoVenta);
                    await _context.SaveChangesAsync();
                    result.Data = puntoVenta;
                }
                result.Code = existPuntoVenta ? "200" : "204";
                result.Message = existPuntoVenta ? "Ok" : $"No existe registro con id: '{puntoVenta.PuntovtaId}'";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "PutPuntoVenta", ex.Message);

            }
            return result;
        }
        public async Task<Respuesta> DeletePuntoVenta(PuntoVentum puntoVenta)
        {
            var result = new Respuesta();
            try
            {
                var existPuntoVenta = await _context.PuntoVenta.AnyAsync(puntoVentaDB =>
                                                                            puntoVentaDB.PuntovtaId == puntoVenta.PuntovtaId);
                if (existPuntoVenta)
                {
                    puntoVenta.EstadoId = 2;
                    _context.PuntoVenta.Update(puntoVenta);
                    await _context.SaveChangesAsync();
                }

                result.Code = existPuntoVenta ? "200" : "204";
                result.Message = existPuntoVenta ? "Ok" : $"No se encontro registro con id: '{puntoVenta.PuntovtaId}'";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "DeletePuntoVenta", ex.Message);
            }
            return result;
        }
    }
}
