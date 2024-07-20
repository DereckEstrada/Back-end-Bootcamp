using ApiVentas.DTOs;
using ApiVentas.Interfaces;
using ApiVentas.Models;
using ApiVentas.Utilitarios;
using Microsoft.EntityFrameworkCore;

namespace ApiVentas.Services
{
    public class SucursalService : ISucursal
    {
        private readonly BaseErpContext _context;
        private ControlError log = new ControlError();
        public SucursalService(BaseErpContext context)
        {
            this._context = context;
        }
        public async Task<Respuesta> GetSucursal(int? sucursalID, string? sucursalRuc, string? estado)
        {
            Respuesta respuesta = new Respuesta();
            try
            {

                IQueryable<SucursalDTO> query =
                        from s in _context.Sucursals
                        join es in _context.Estados on s.EstadoId equals es.EstadoId
                        join emp in _context.Empresas on s.EmpresaId equals emp.EmpresaId
                        select new SucursalDTO()

                        {
                            SucursalId = s.SucursalId,
                            SucursalNombre = s.SucursalNombre,
                            SucursalRuc = s.SucursalRuc,
                            SucursalDireccion = s.SucursalDireccion,
                            SucursalTelefono = s.SucursalTelefono,
                            SucursalEstado = es.EstadoDescrip,
                            FechaHoraReg = s.FechaHoraReg,
                            UsuIdReg = s.UsuIdReg,
                            FechaHoraAct = s.FechaHoraAct,
                            UsuIdAct = s.UsuIdAct,
                            EmpresaNombre = emp.EmpresaNombre,
                            CodEstablecimientoSri = s.CodEstablecimientoSri
                        };
                if (sucursalID is null && sucursalRuc is null)
                {
                    respuesta.Cod = "000";
                    respuesta.Data = await query.ToListAsync();
                    respuesta.Mensaje = "OK";
                }
                else if (sucursalID is not null && sucursalRuc is null && estado is null)
                {
                    respuesta.Cod = "000";
                    respuesta.Data = await (from q in query
                                            where q.SucursalId == sucursalID
                                            select q).ToListAsync();
                    respuesta.Mensaje = "OK";
                }
                else if (sucursalID is null && sucursalRuc is not null && estado is null)
                {
                    respuesta.Cod = "000";
                    respuesta.Data = await (from q in query
                                            where q.SucursalRuc == sucursalRuc
                                            select q).ToListAsync();
                    respuesta.Mensaje = "OK";
                }
                else if (sucursalID is null && sucursalRuc is null && estado is not null)
                {
                    respuesta.Cod = "000";
                    respuesta.Data = await (from q in query
                                            where q.SucursalEstado == estado
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
                log.LogErrorMetodos("SucursalService", "GetSucursal", ex.Message);
            }
            return respuesta;
        }

        public async Task<Respuesta> PostSucursal(Sucursal sucursal)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                bool existSucursal = await _context.Sucursals.AnyAsync(x => x.SucursalId == sucursal.SucursalId);

                if (!existSucursal)
                {
                    int? lastID = await _context.Sucursals.OrderByDescending(x => x.SucursalId).Select(x => x.SucursalId).FirstOrDefaultAsync();
                    int newID = (lastID.HasValue ? lastID.Value : 0) + 1;
                    sucursal.SucursalId = newID;
                    sucursal.FechaHoraReg = DateTime.Now;
                    sucursal.FechaHoraAct = DateTime.Now;

                    respuesta.Cod = "000";
                    respuesta.Data = sucursal;
                    respuesta.Mensaje = "Ok";

                    await _context.Sucursals.AddAsync(sucursal);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    respuesta.Cod = "999";
                    respuesta.Mensaje = "sucursalID ya se encuentra registrado";
                }
            }
            catch (Exception ex)
            {

                respuesta.Cod = "000";
                respuesta.Mensaje = ex.Message; respuesta.Mensaje = $"Se presento un error, comunicarse con el departamento de sistemas";
                log.LogErrorMetodos("SucursalService", "PostSucursal", ex.Message);
            }
            return respuesta;
        }

        public async Task<Respuesta> PutSucursal(Sucursal sucursal)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                bool existSucursal = await _context.Sucursals.AnyAsync(x => x.SucursalId == sucursal.SucursalId);

                if (existSucursal)
                {
                    sucursal.FechaHoraAct = DateTime.Now;
                    respuesta.Cod = "000";
                    respuesta.Data = sucursal;
                    respuesta.Mensaje = "OK";

                    _context.Sucursals.Update(sucursal);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    respuesta.Cod = "000";
                    respuesta.Mensaje = "sucursalID no existente, no se puedo realizar cambios";
                }
            }
            catch (Exception ex)
            {

                respuesta.Cod = "000";
                respuesta.Mensaje = ex.Message; respuesta.Mensaje = $"Se presento un error, comunicarse con el departamento de sistemas";
                log.LogErrorMetodos("SucursalService", "PutSucursal", ex.Message);
            }
            return respuesta;
        }

        public async Task<Respuesta> DeleteSucursal(int sucursalID)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                bool existSucursal = await _context.Sucursals.AnyAsync(x => x.SucursalId == sucursalID);

                if (existSucursal)
                {
                    Sucursal? sucursalToDelete = await _context.Sucursals.Where(x => x.SucursalId == sucursalID).FirstOrDefaultAsync();

                    if (sucursalToDelete is not null)
                    {
                        sucursalToDelete.EstadoId = 2; 

                        respuesta.Cod = "000";
                        respuesta.Data = sucursalToDelete;
                        respuesta.Mensaje = "OK";

                        _context.Sucursals.Update(sucursalToDelete);
                        await _context.SaveChangesAsync();
                    }
                }
                else
                {
                    respuesta.Cod = "999";
                    respuesta.Mensaje = "sucursalID no existe, no se puede realizar cambios";
                }
            }
            catch (Exception ex)
            {

                respuesta.Cod = "000";
                respuesta.Mensaje = ex.Message; respuesta.Mensaje = $"Se presento un error, comunicarse con el departamento de sistemas";
                log.LogErrorMetodos("SucursalService", "DeleteSucursal", ex.Message);
            }
            return respuesta;
        }

         
    }
}
