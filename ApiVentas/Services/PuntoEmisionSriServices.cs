using ApiVentas.DTOs;
using ApiVentas.Interfaces;
using ApiVentas.Models;
using ApiVentas.Utilitarios;
using ApiVentas.Utilitarios.Dictionaries;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ApiVentas.Services
{
    public class PuntoEmisionSriServices : IPuntoEmisionSriServices, IServices<PuntoEmisionSri>
    {
        private BaseErpContext _context;
        private ControlError log = new ControlError();
        private DynamicEmpty dynamicEmpty = new DynamicEmpty();
        public PuntoEmisionSriServices(BaseErpContext context)
        {
            this._context = context;
        }

        public async Task<Respuesta> GetPuntoEmisionSri(DataQuery dataQuery)
        {
            var result = new Respuesta();
            try
            {
                result.Data = await _context.PuntoEmisionSris
                                            .Include(puntoEmisionSri => puntoEmisionSri.Empresa)
                                            .Include(puntoEmisionSri => puntoEmisionSri.Sucursal)
                                            .Include(puntoEmisionSri => puntoEmisionSri.Estado)
                                            .Include(puntoEmisionSri => puntoEmisionSri.UsuIdRegNavigation)
                                            .Include(puntoEmisionSri => puntoEmisionSri.Empresa)
                                            .Where(PuntoEmisionSriDictionary.GetExpression(dataQuery))
                                            .Select(puntoEmisionSri => new PuntoEmisionSriDTO
                                            {
                                                PuntoEmisionId=puntoEmisionSri.PuntoEmisionId,
                                                PuntoEmision=puntoEmisionSri.PuntoEmision,
                                                EmpresaId=puntoEmisionSri.EmpresaId,
                                                EmpresaDescripcion=puntoEmisionSri.Empresa.EmpresaNombre,
                                                SucursalId=puntoEmisionSri.SucursalId,
                                                SucursalDescripcion=puntoEmisionSri.Sucursal.SucursalRuc,
                                                CodEstablecimientoSri=puntoEmisionSri.CodEstablecimientoSri,
                                                UltSecuencia=puntoEmisionSri.UltSecuencia,
                                                EstadoId=puntoEmisionSri.EstadoId,
                                                EstadoDescripcion=puntoEmisionSri.Estado.EstadoDescrip,
                                                FechaHoraReg=puntoEmisionSri.FechaHoraReg,
                                                UsuIdReg=puntoEmisionSri.UsuIdReg,
                                                UsuRegName=puntoEmisionSri.UsuIdRegNavigation.UsuNombre,
                                            }).ToListAsync();
                
                result.Code = dynamicEmpty.IsEmpty(result.Data) ? "204" : "200";
                result.Message = dynamicEmpty.IsEmpty(result.Data) ? $"No se encontro registro con opcion:'{dataQuery.OpcionData}' con data: '{dataQuery.DataFirstQuery}'" : "Ok";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "GetPuntoEmisionSri", ex.Message);

            }
            return result;
        }

        public async Task<Respuesta> PostPuntoEmisionSri(PuntoEmisionSri puntoEmisionSri)
        {
            var result = new Respuesta();
            try
            {
                var query = await _context.PuntoEmisionSris.OrderByDescending(puntoEmisionSriDB => puntoEmisionSriDB.PuntoEmisionId)
                                                                .Select(idDB => idDB.PuntoEmisionId).FirstOrDefaultAsync() + 1;
                puntoEmisionSri.PuntoEmisionId = query;
                puntoEmisionSri.FechaHoraReg = DateTime.Now;

                    _context.PuntoEmisionSris.Add(puntoEmisionSri);
                    await _context.SaveChangesAsync();
                
                result.Code =  "200" ;
                result.Message = "Ok";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "PostPuntoEmisionSri", ex.Message);
            }
            return result;
        }

        public async Task<Respuesta> PutPuntoEmisionSri(PuntoEmisionSri puntoEmisionSri)
        {
            var result = new Respuesta();
            try
            {
                var existPuntoEmisionSri = await _context.PuntoEmisionSris.AnyAsync(puntoEmisionSriDB =>
                                                                    puntoEmisionSriDB.PuntoEmisionId 
                                                                    == puntoEmisionSri.PuntoEmisionId);
                if (existPuntoEmisionSri)
                {                    
                    puntoEmisionSri.FechaHoraAct = DateTime.Now;
                    
                    _context.PuntoEmisionSris.Update(puntoEmisionSri);
                    await _context.SaveChangesAsync();
                    result.Data = puntoEmisionSri;
                }
                result.Code = existPuntoEmisionSri ? "200" : "204";
                result.Message = existPuntoEmisionSri ? "Ok" : $"No se encontro registro con id: '{puntoEmisionSri.PuntoEmisionId}'";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "PutPuntoEmisionSri", ex.Message);
            }
            return result;
        }
        public async Task<Respuesta> DeletePuntoEmisionSri(PuntoEmisionSri puntoEmisionSri)
        {
            var result = new Respuesta();
            try
            {
                var existPuntoEmisionSri = await _context.PuntoEmisionSris.AnyAsync(puntoEmisionSriDB =>
                                                                    puntoEmisionSriDB.PuntoEmisionId
                                                                    == puntoEmisionSri.PuntoEmisionId);
                if (existPuntoEmisionSri)
                {
                    puntoEmisionSri.FechaHoraAct=DateTime.Now;
                    puntoEmisionSri.EstadoId = 2;
                    
                    _context.PuntoEmisionSris.Update(puntoEmisionSri);
                    await _context.SaveChangesAsync();
                }
                result.Code = existPuntoEmisionSri ? "200" : "204";
                result.Message = existPuntoEmisionSri ? "Ok" : $"No se encontro registro con id: '{puntoEmisionSri.PuntoEmisionId}'";

            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "DeletePuntoEmisionSri", ex.Message);

            }
            return result;
        }
    }
}
