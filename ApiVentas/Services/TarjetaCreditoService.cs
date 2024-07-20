using ApiVentas.DTOs;
using ApiVentas.Interfaces;
using ApiVentas.Models;
using ApiVentas.Utilitarios;
using Microsoft.EntityFrameworkCore;

namespace ApiVentas.Services
{
    public class TarjetaCreditoService : ITarjetaCredito
    {
        private readonly BaseErpContext _context;
        private ControlError log = new ControlError();
        public TarjetaCreditoService (BaseErpContext context)
        {
            this._context = context;
        }

     

        public async Task<Respuesta> GetTarjetaCredito(int? tarjetaCredID, string? tarjetaDescrip)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                IQueryable<TarjetaCreditoDTO> query =
                    from t in _context.TarjetaCreditos
                    join i in _context.Industria on t.IndustriaId equals i.IndustriaId
                    join es in _context.Estados on t.EstadoId equals es.EstadoId
                    select new TarjetaCreditoDTO()
                    {
                        TarjetaCreditoID = t.TarjetacredId,
                        TarjetaDescripcion = t.TarjetacredDescripcion,
                        IndustriaDescripcion = i.IndustriaDescripcion,
                        EstadoDescripcion = es.EstadoDescrip,
                        FechaReg = t.FechaHoraReg,
                        FechaAct = t.FechaHoraAct,
                        UsuIdReg = t.UsuIdReg,
                        UsuIdAct = t.UsuIdAct
                    };
                if (tarjetaCredID is null && tarjetaDescrip is null)
                {
                    respuesta.Cod = "000";
                    respuesta.Data = await query.ToListAsync();
                    respuesta.Mensaje = "OK";
                }
                else if (tarjetaCredID is not null && tarjetaDescrip is null)
                {
                    respuesta.Cod = "000";
                    respuesta.Data = await (from q in query
                                     where q.TarjetaCreditoID == tarjetaCredID
                                     select q).ToListAsync();
                    respuesta.Mensaje = "OK";
                }
                else if (tarjetaCredID is null && tarjetaDescrip is not null)
                {
                    respuesta.Cod = "000";
                    respuesta.Data = await (from q in query
                                            where q.TarjetaDescripcion.ToLower().Trim() == tarjetaDescrip.ToLower().Trim()
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
                respuesta.Mensaje = $"Se presento un error, comunicarse con el departamento de sistemas";
                log.LogErrorMetodos("TarjetaCreditoService", "GetTarjetaCredito", ex.Message);
            }
            return respuesta;
        }

        public async Task<Respuesta> PostTarjetaCredito(TarjetaCredito tarjetaCredito)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                bool existTarjetacred = await _context.TarjetaCreditos.AnyAsync(x => x.TarjetacredId == tarjetaCredito.TarjetacredId);

                if (!existTarjetacred)
                {
                    int? lastID = await _context.TarjetaCreditos.OrderByDescending(x => x.TarjetacredId).Select(x => x.TarjetacredId).FirstOrDefaultAsync();
                    int newID = (lastID.HasValue ? lastID.Value : 0) + 1;
                    tarjetaCredito.TarjetacredId = newID;
                    tarjetaCredito.FechaHoraReg = DateTime.Now;
                    tarjetaCredito.FechaHoraAct = DateTime.Now;

                    respuesta.Cod = "000";
                    respuesta.Data = tarjetaCredito;
                    respuesta.Mensaje = "Ok";

                    await _context.TarjetaCreditos.AddAsync(tarjetaCredito);
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
                respuesta.Mensaje = $"Se presento un error, comunicarse con el departamento de sistemas";
                log.LogErrorMetodos("TarjetaCreditoService", "PosttTarjetaCredito", ex.Message);
            }
            return respuesta;
        }

        public async Task<Respuesta> PutTarjetaCredito(TarjetaCredito tarjetaCredito)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                bool existTarjetacred = await _context.TarjetaCreditos.AnyAsync(x => x.TarjetacredId == tarjetaCredito.TarjetacredId);

                if (existTarjetacred)
                {
                    tarjetaCredito.FechaHoraAct = DateTime.Now;
                    respuesta.Cod = "000";
                    respuesta.Data = tarjetaCredito;
                    respuesta.Mensaje = "OK";

                    _context.TarjetaCreditos.Update(tarjetaCredito);
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
                respuesta.Mensaje = $"Se presento un error, comunicarse con el departamento de sistemas";
                log.LogErrorMetodos("TarjetaCreditoService", "PostTarjetaCredito", ex.Message);
            }
            return respuesta;
        }

        public async Task<Respuesta> DeleteTarjetaCredito(int tarjetaCredID)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                bool existSucursal = await _context.TarjetaCreditos.AnyAsync(x => x.TarjetacredId == tarjetaCredID);

                if (existSucursal)
                {
                    TarjetaCredito? tarjetaCreditoToDelete = await _context.TarjetaCreditos.Where(x => x.TarjetacredId == tarjetaCredID).FirstOrDefaultAsync();

                    if (tarjetaCreditoToDelete is not null)
                    {
                        tarjetaCreditoToDelete.EstadoId = 2;

                        respuesta.Cod = "000";
                        respuesta.Data = tarjetaCreditoToDelete;
                        respuesta.Mensaje = "OK";

                        _context.TarjetaCreditos.Update(tarjetaCreditoToDelete);
                        await _context.SaveChangesAsync();
                    }
                }
                else
                {
                    respuesta.Cod = "999";
                    respuesta.Mensaje = "TarjetadeCreditoID no existe, no se puede realizar cambios";
                }
            }
            catch (Exception ex)
            {

                respuesta.Cod = "000";
                respuesta.Mensaje = ex.Message; respuesta.Mensaje = $"Se presento un error, comunicarse con el departamento de sistemas";
                log.LogErrorMetodos("TarjetaCreditoService", "DeleteTarjetaCredito", ex.Message);
            }
            return respuesta;
        }
    }
}

