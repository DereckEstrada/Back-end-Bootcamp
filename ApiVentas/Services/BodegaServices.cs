using ApiVentas.DTOs;
using ApiVentas.Interfaces;
using ApiVentas.Models;
using ApiVentas.Utilitarios;
using ApiVentas.Utilitarios.Dictionaries;
using Microsoft.EntityFrameworkCore;

namespace ApiVentas.Services
{
    public class BodegaServices : IBodegaServices, IServices<Bodega>
    {
        private readonly BaseErpContext _context;
        private ControlError log = new ControlError();
        private DynamicEmpty dynamicEmpty = new DynamicEmpty();
        public BodegaServices(BaseErpContext context)
        {
            this._context = context;
        }

        public async Task<Respuesta> GetBodega(DataQuery dataQuery)
        {
            var result = new Respuesta();
            try
            {
                
                result.Data =await _context.Bodegas
                                    .Include(bodega => bodega.Sucursal)
                                    .Include(bodega => bodega.Estado)
                                    .Include(Bodega => Bodega.UsuIdRegNavigation)
                                    .Where(BodegaDictionary.GetExpression(dataQuery))
                                    .Select(bodega => new BodegaDTO
                                    {
                                        BodegaId = bodega.BodegaId,
                                        BodegaNombre = bodega.BodegaNombre,
                                        BodegaDireccion = bodega.BodegaDireccion,
                                        BodegaTelefono = bodega.BodegaTelefono,
                                        SucursalId = bodega.SucursalId,
                                        SucursalDescripcion = bodega.Sucursal.SucursalNombre,
                                        EstadoId = bodega.EstadoId,
                                        EstadoDescripcion = bodega.Estado.EstadoDescrip,
                                        FechaHoraReg = bodega.FechaHoraReg,
                                        UsuIdReg = bodega.UsuIdReg,
                                        UsuRegName = bodega.UsuIdRegNavigation.UsuNombre,
                                    }).ToListAsync();

                result.Code = dynamicEmpty.IsEmpty(result.Data) ? "204" : "200";
                result.Message = dynamicEmpty.IsEmpty(result.Data) ? $"No se encontro registro con opcion:'{dataQuery.OpcionData}' con data: '{dataQuery.DataFirstQuery}'" : "Ok";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "GetBodega", ex.Message);
            }

            return result;
        }
        public async Task<Respuesta> PostBodega(Bodega bodega)
        {
            var result = new Respuesta();
            try
            {
                var query = await _context.Bodegas.OrderByDescending(bodegaDB => bodegaDB.BodegaId)
                                                    .Select(idDB => idDB.BodegaId).FirstOrDefaultAsync() + 1;
                bodega.BodegaId = query;
                bodega.FechaHoraReg = DateTime.Now;

                _context.Bodegas.Add(bodega);
                await _context.SaveChangesAsync();

                result.Code = "200";
                result.Data = bodega;
                result.Message = "Ok";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "PostBodega", ex.Message);
            }
            return result;
        }

        public async Task<Respuesta> PutBodega(Bodega bodega)
        {
            var result = new Respuesta();
            try
            {
                bool existBodega = await _context.Bodegas.AnyAsync(bodegaDB => bodegaDB.BodegaId == bodega.BodegaId);
                if (existBodega)
                {
                    bodega.FechaHoraAct = DateTime.Now;

                    _context.Bodegas.Update(bodega);
                    await _context.SaveChangesAsync();
                    result.Data = bodega;
                }
                result.Code = existBodega ? "200" : "204";
                result.Message = existBodega ? "Ok" : $"No existe registro con id: '{bodega.BodegaId}'";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "PutBodega", ex.Message);
            }
            return result;
        }

        public async Task<Respuesta> DeleteBodega(Bodega bodega)
        {
            var result = new Respuesta();
            try
            {
                bool existBodega = await _context.Bodegas.AnyAsync(bodegaDB => bodegaDB.BodegaId == bodega.BodegaId);
                if (existBodega)
                {
                    bodega.FechaHoraAct = DateTime.Now;
                    bodega.EstadoId = 2;

                    _context.Bodegas.Update(bodega);
                    await _context.SaveChangesAsync();
                }
                result.Code = existBodega ? "200" : "204";
                result.Message = existBodega ? "Ok" : $"No existe registro con id: '{bodega.BodegaId}'";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "DeleteBodega", ex.Message);
            }
            return result;
        }
    }
}
