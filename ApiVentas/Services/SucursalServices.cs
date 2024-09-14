using ApiVentas.DTOs;
using ApiVentas.Interfaces;
using ApiVentas.Models;
using ApiVentas.Utilitarios;
using ApiVentas.Utilitarios.Dictionaries;
using Microsoft.EntityFrameworkCore;

namespace ApiVentas.Services
{
    public class SucursalServices : ISucursalServices, IServices<Sucursal>
    {
        private readonly BaseErpContext _context;
        private ControlError log = new ControlError();
        private DynamicEmpty dynamicEmpty = new DynamicEmpty();
        public SucursalServices(BaseErpContext context)
        {
            this._context = context;
        }
        public async Task<Respuesta> GetSucursal(DataQuery dataQuery)
        {
            Respuesta result = new Respuesta();
            try
            {

                result.Data = await _context.Sucursals
                                          .Include(sucursal => sucursal.Empresa)
                                          .Include(sucursal => sucursal.Estado)
                                          .Include(sucursal => sucursal.UsuIdRegNavigation)
                                          .Where(SucursalDictionary.GetExpression(dataQuery))
                                          .Select(sucursal => new SucursalDTO
                                          {
                                              SucursalId = sucursal.SucursalId,
                                              SucursalRuc = sucursal.SucursalRuc,
                                              SucursalNombre = sucursal.SucursalNombre,
                                              SucursalDireccion = sucursal.SucursalDireccion,
                                              SucursalTelefono = sucursal.SucursalTelefono,
                                              CodEstablecimientoSri = sucursal.CodEstablecimientoSri,
                                              EmpresaId = sucursal.EmpresaId,
                                              EmpresaDescripcion = sucursal.Empresa.EmpresaNombre,
                                              EstadoId = sucursal.EstadoId,
                                              EstadoDescripcion = sucursal.Estado.EstadoDescrip,
                                              FechaHoraReg = sucursal.FechaHoraReg,
                                              UsuIdReg = sucursal.UsuIdReg,
                                              UsuRegName = sucursal.UsuIdRegNavigation.UsuNombre,
                                          }).ToListAsync();

                result.Code = dynamicEmpty.IsEmpty(result.Data) ? "204" : "200";
                result.Message = dynamicEmpty.IsEmpty(result.Data) ? $"No se encontro registro con opcion:'{dataQuery.OpcionData}' con data: '{dataQuery.DataFirstQuery}'" : "Ok";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "GetSucursal", ex.Message);
            }
            return result;
        }

        public async Task<Respuesta> PostSucursal(Sucursal sucursal)
        {
            Respuesta result = new Respuesta();
            try
            {
                var query = await _context.Sucursals.OrderByDescending(surcursalDB => surcursalDB.SucursalId)
                                                        .Select(idDB => idDB.SucursalId).FirstOrDefaultAsync() + 1;

                sucursal.SucursalId = query;
                sucursal.FechaHoraReg = DateTime.Now;


                _context.Sucursals.Add(sucursal);
                await _context.SaveChangesAsync();

                result.Code = "200";
                result.Data = sucursal;
                result.Message = "Ok";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "PostSucursal", ex.Message);
            }
            return result;
        }

        public async Task<Respuesta> PutSucursal(Sucursal sucursal)
        {
            Respuesta result = new Respuesta();
            try
            {
                bool existSucursal = await _context.Sucursals.AnyAsync(sucursalDB => sucursalDB.SucursalId == sucursal.SucursalId);

                if (existSucursal)
                {
                    sucursal.FechaHoraAct = DateTime.Now;

                    _context.Sucursals.Update(sucursal);
                    await _context.SaveChangesAsync();
                    result.Data = sucursal;
                }

                result.Code = existSucursal ? "200" : "204";
                result.Message = existSucursal ? "Ok" : $"No existe registro con id: '{sucursal.SucursalId}'";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "PutSucursal", ex.Message);
            }
            return result;
        }

        public async Task<Respuesta> DeleteSucursal(Sucursal sucursal)
        {
            Respuesta result = new Respuesta();
            try
            {
                bool existSucursal = await _context.Sucursals.AnyAsync(sucursalDB => sucursalDB.SucursalId == sucursal.SucursalId);

                if (existSucursal)
                {
                    sucursal.FechaHoraAct = DateTime.Now;
                    sucursal.EstadoId = 2;

                    _context.Sucursals.Update(sucursal);
                    await _context.SaveChangesAsync();
                }
                result.Code = existSucursal ? "200" : "204";
                result.Message = existSucursal ? "Ok" : $"No existe registro con id: '{sucursal.SucursalId}'";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "DeleteSucursal", ex.Message);
            }
            return result;
        }


    }
}
