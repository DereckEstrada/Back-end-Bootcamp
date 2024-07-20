using ApiVentas.DTOs;
using ApiVentas.Interfaces;
using ApiVentas.Models;
using ApiVentas.Utilitarios;
using Microsoft.EntityFrameworkCore;

namespace ApiVentas.Services
{
    public class UsuarioPermisoService : IUsuarioPermiso
    {
        private readonly BaseErpContext _context;
        private ControlError log = new ControlError();

        public UsuarioPermisoService(BaseErpContext context)
        {
            this._context = context;
        }
        public async Task<Respuesta> GetUsuarioPermiso(int? permisoID, string? usuNombre, string? moduloDescrip)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                IQueryable<UsuarioPermisoDTO> query =
                    from up in _context.UsuarioPermisos
                    join u in _context.Usuarios on up.UsuId equals u.UsuId

                    join es in _context.Estados on up.EstadoId equals es.EstadoId
                    join op in _context.Opcions on up.OpcionId equals op.OpcionId
                    join m in _context.Modulos on op.ModuloId equals m.ModuloId
                    select new UsuarioPermisoDTO()
                    {
                        PermisoId = up.PermisoId,
                        Modulodescripcion = m.ModuloDescripcion,
                        OpcionDescripcion = op.OpcionDescripcion,
                        UsuarioNombre = u.UsuNombre,
                        Estado = es.EstadoDescrip,
                        FechaRegistro = up.FechaHoraReg,
                        FechaActualizacion = up.FechaHoraAct,
                        UsuIdReg = up.UsuIdReg,
                        UsuIdAct = up.UsuIdAct
                    };

                if (permisoID is null && usuNombre is null)
                {
                    respuesta.Cod = "000";
                    respuesta.Data = await query.ToListAsync();
                    respuesta.Mensaje = "OK";
                }
                else if (permisoID is not null && usuNombre is null && moduloDescrip is null)
                {
                    respuesta.Cod = "000";
                    respuesta.Data = await (from q in query
                                           where q.PermisoId == permisoID
                                           select q).ToListAsync();
                    respuesta.Mensaje = "OK";
                }
                else if (permisoID is  null && usuNombre is not null && moduloDescrip is null)
                {
                    respuesta.Cod = "000";
                    respuesta.Data = await (from q in query
                                            where q.UsuarioNombre == usuNombre
                                            select q).ToListAsync();
                    respuesta.Mensaje = "OK";
                }
                else if (permisoID is null && usuNombre is not null && moduloDescrip is not null)
                {
                    respuesta.Cod = "000";
                    respuesta.Data = await (from q in query
                                            where q.Modulodescripcion == moduloDescrip
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
                log.LogErrorMetodos("UsuarioPermisoService", "GetUsuarioPermiso", ex.Message);
            }
            return respuesta;
        }
        public async Task<Respuesta> PostUsuarioPermiso(UsuarioPermiso usuarioPermiso)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                bool existUsuarioPermiso = await _context.UsuarioPermisos.AnyAsync(x => x.PermisoId == usuarioPermiso.PermisoId);

                if (!existUsuarioPermiso)
                {
                    int? lastID = await _context.UsuarioPermisos.OrderByDescending(x => x.PermisoId).Select(x => x.PermisoId).FirstOrDefaultAsync();
                    int newID = (lastID.HasValue ? lastID.Value : 0) + 1;
                    usuarioPermiso.PermisoId = newID;
                    usuarioPermiso.FechaHoraReg = DateTime.Now;
                    usuarioPermiso.FechaHoraAct = DateTime.Now;

                    respuesta.Cod = "000";
                    respuesta.Data = usuarioPermiso;
                    respuesta.Mensaje = "Ok";

                    await _context.UsuarioPermisos.AddAsync(usuarioPermiso);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    respuesta.Cod = "999";
                    respuesta.Mensaje = "usuarioPermisoID ya se encuentra registrado";
                }
            }
            catch (Exception ex)
            {

                respuesta.Cod = "000";
                respuesta.Mensaje = $"Se presento un error, comunicarse con el departamento de sistemas";
                log.LogErrorMetodos("UsuarioPermisoService", "PostUsuarioPermiso", ex.Message);
            }
            return respuesta;
        }

        public async Task<Respuesta> PutUsuarioPermiso(UsuarioPermiso usuarioPermiso)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                bool existUsuarioPermiso = await _context.UsuarioPermisos.AnyAsync(x => x.PermisoId == usuarioPermiso.PermisoId);

                if (existUsuarioPermiso)
                {
                    usuarioPermiso.FechaHoraAct = DateTime.Now;
                    respuesta.Cod = "000";
                    respuesta.Data = usuarioPermiso;
                    respuesta.Mensaje = "OK";

                    _context.UsuarioPermisos.Update(usuarioPermiso);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    respuesta.Cod = "000";
                    respuesta.Mensaje = "UsuRolID no existente, no se puedo realizar cambios";
                }
            }
            catch (Exception ex)
            {

                respuesta.Cod = "000";
                respuesta.Mensaje = $"Se presento un error, comunicarse con el departamento de sistemas";
                log.LogErrorMetodos("UsuarioPermisoService", "PutUsuarioPermiso", ex.Message);
            }
            return respuesta;
        }
        public async Task<Respuesta> DeleteUsuarioPermiso(int permisoID)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                bool existUsuarioRol = await _context.UsuarioPermisos.AnyAsync(x => x.PermisoId == permisoID);

                if (existUsuarioRol)
                {
                    UsuarioPermiso? usuarioPermisoToDelete = await _context.UsuarioPermisos.Where(x => x.PermisoId == permisoID).FirstOrDefaultAsync();

                    if (usuarioPermisoToDelete is not null)
                    {
                        usuarioPermisoToDelete.EstadoId = 2;

                        respuesta.Cod = "000";
                        respuesta.Data = usuarioPermisoToDelete;
                        respuesta.Mensaje = "OK";

                        _context.UsuarioPermisos.Update(usuarioPermisoToDelete);
                        await _context.SaveChangesAsync();
                    }
                }
                else
                {
                    respuesta.Cod = "999";
                    respuesta.Mensaje = "permisoID no existe, no se puede realizar cambios";
                }
            }
            catch (Exception ex)
            {

                respuesta.Cod = "000";
                respuesta.Mensaje = $"Se presento un error, comunicarse con el departamento de sistemas";
                log.LogErrorMetodos("UsuarioPermisoService", "DeleteUsuarioPermiso", ex.Message);
            }
            return respuesta;
        }

    }
}
