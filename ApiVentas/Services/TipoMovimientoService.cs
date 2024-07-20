using ApiVentas.DTOs;
using ApiVentas.Interfaces;
using ApiVentas.Models;
using ApiVentas.Utilitarios;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ApiVentas.Services
{
    public class TipoMovimientoService : ITipoMovimiento
    {
        private readonly BaseErpContext _context;
        private ControlError log = new ControlError();
        public TipoMovimientoService(BaseErpContext context)
        {
            this._context = context;
        }

        public async Task<Respuesta> GetTipoMovimiento(int? tipoMovimientoID, string? movimientoDescrip)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                IQueryable<TipoMovimientoDTO> query =
                    from t in _context.TipoMovimientos
                    join es in _context.Estados on t.EstadoId equals es.EstadoId
                    select new TipoMovimientoDTO()
                    {
                        TipomovId = t.TipomovId,
                        TipomovDescrip = t.TipomovDescrip,
                        TipomovIngEgr = t.TipomovIngEgr,
                        EstadoDescrip = es.EstadoDescrip,
                        FechaHoraReg = t.FechaHoraReg,
                        FechaHoraAct = t.FechaHoraAct,
                        UsuIdReg = t.UsuIdReg,
                        UsuIdAct = t.UsuIdAct
                    };


                if (tipoMovimientoID is null && movimientoDescrip is null)
                {
                    respuesta.Cod = "000";
                    respuesta.Data = await query.ToListAsync();
                    respuesta.Mensaje = "OK";
                }
                else if (tipoMovimientoID is not null && movimientoDescrip is null)
                {
                    respuesta.Cod = "000";
                    respuesta.Data = await (from q in query
                                            where q.TipomovId == tipoMovimientoID
                                            select q).ToListAsync();   
                    respuesta.Mensaje = "OK";
                }
                else if (tipoMovimientoID is null && movimientoDescrip is not null)
                {
                    respuesta.Cod = "000";
                    respuesta.Data = await (from q in query
                                            where q.TipomovDescrip == movimientoDescrip
                                            select q).ToListAsync();
                    respuesta.Mensaje = "OK";
                }
                else
                {
                    respuesta.Cod = "999";
                    respuesta.Mensaje = "Datos nos existentes";
                }
            }
            catch (Exception ex)
            {

                respuesta.Cod = "000";
                respuesta.Mensaje = ex.Message; respuesta.Mensaje = $"Se presento un error, comunicarse con el departamento de sistemas";
                log.LogErrorMetodos("TipoMovimientoService", "GetTipoMovimiento", ex.Message);
            }
            return respuesta;
        }

        public async Task<Respuesta> PostTipoMovimiento(TipoMovimiento tipoMovimiento)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                bool existTipomov = await _context.TipoMovimientos.AnyAsync(x => x.TipomovId == tipoMovimiento.TipomovId);

                if (!existTipomov)
                {
                    int? lastID = await _context.TipoMovimientos.OrderByDescending(x => x.TipomovId).Select(x => x.TipomovId).FirstOrDefaultAsync();
                    int newID = (lastID.HasValue ? lastID.Value : 0) + 1;
                    tipoMovimiento.TipomovId = newID;
                    tipoMovimiento.FechaHoraReg = DateTime.Now;
                    tipoMovimiento.FechaHoraAct = DateTime.Now;

                    respuesta.Cod = "000";
                    respuesta.Data = tipoMovimiento;
                    respuesta.Mensaje = "Ok";

                    await _context.TipoMovimientos.AddAsync(tipoMovimiento);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    respuesta.Cod = "999";
                    respuesta.Mensaje = "TipoMovID ya se encuentra registrado";
                }
            }
            catch (Exception ex)
            {

                respuesta.Cod = "000";
                respuesta.Mensaje = ex.Message; respuesta.Mensaje = $"Se presento un error, comunicarse con el departamento de sistemas";
                log.LogErrorMetodos("TipoMovimientoService", "PostTipoMovimiento", ex.Message);
            }
            return respuesta;
        }

        public async Task<Respuesta> PutTipoMovimiento(TipoMovimiento tipoMovimiento)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                bool existTipomov = await _context.TipoMovimientos.AnyAsync(x => x.TipomovId == tipoMovimiento.TipomovId);

                if (existTipomov)
                {
                    tipoMovimiento.FechaHoraAct = DateTime.Now;
                    respuesta.Cod = "000";
                    respuesta.Data = tipoMovimiento;
                    respuesta.Mensaje = "OK";

                    _context.TipoMovimientos.Update(tipoMovimiento);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    respuesta.Cod = "000";
                    respuesta.Mensaje = "TipoMovID no existente, no se puedo realizar cambios";
                }
            }
            catch (Exception ex)
            {

                respuesta.Cod = "000";
                respuesta.Mensaje = ex.Message; respuesta.Mensaje = $"Se presento un error, comunicarse con el departamento de sistemas";
                log.LogErrorMetodos("TipoMovimientoService", "PutTipoMovimiento", ex.Message);
            }
            return respuesta;
        }
        public async Task<Respuesta> DeleteTipoMovimiento(int tipoMovimientoID)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                bool existTipomov = await _context.TipoMovimientos.AnyAsync(x => x.TipomovId == tipoMovimientoID);

                if (existTipomov)
                {
                    TipoMovimiento? tipoMovimientoToDelete = await _context.TipoMovimientos.Where(x => x.TipomovId == tipoMovimientoID).FirstOrDefaultAsync();

                    if (tipoMovimientoToDelete is not null)
                    {
                        tipoMovimientoToDelete.EstadoId = 2;

                        respuesta.Cod = "000";
                        respuesta.Data = tipoMovimientoToDelete;
                        respuesta.Mensaje = "OK";

                        _context.TipoMovimientos.Update(tipoMovimientoToDelete);
                        await _context.SaveChangesAsync();
                    }
                }
                else
                {
                    respuesta.Cod = "999";
                    respuesta.Mensaje = "TipoMovID no existe, no se puede realizar cambios";
                }
            }
            catch (Exception ex)
            {

                respuesta.Cod = "000";
                respuesta.Mensaje = ex.Message; respuesta.Mensaje = $"Se presento un error, comunicarse con el departamento de sistemas";
                log.LogErrorMetodos("TipoMovimientoService", "DeleteTipoMovimiento", ex.Message);
            }
            return respuesta;
        }
    }
}
