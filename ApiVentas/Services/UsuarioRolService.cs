using ApiVentas.DTOs;
using ApiVentas.Interfaces;
using ApiVentas.Models;
using ApiVentas.Utilitarios;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace ApiVentas.Services
{
    public class UsuarioRolService : IUsuarioRol
    {
        private readonly BaseErpContext _context;
        private ControlError log = new ControlError();
        public UsuarioRolService(BaseErpContext context)
        {
            _context = context;
        }

        public async Task<Respuesta> GetUsuarioRol(int? usuRolID, string? usuNombre, string? rolDescrip)
        {
            Respuesta respuesta = new Respuesta();

            try
            {
                IQueryable<UsuarioRolDTO> query =
                    from ur in _context.UsuarioRols
                    join r in _context.Rols on ur.RolId equals r.RolId
                    join es in _context.Estados on ur.EstadoId equals es.EstadoId
                    join u in _context.Usuarios on ur.UsuId equals u.UsuId
                    select new UsuarioRolDTO
                    {
                        UsuarioRolID = ur.UsuRolId,
                        UsuarioNombre = u.UsuNombre,
                        RolDescripcion = r.RolDescripcion,
                        Estado = es.EstadoDescrip,
                        FechaRegistro = ur.FechaHoraReg,
                        FechaActualizacion = ur.FechaHoraAct,
                        UsuIdReg = ur.UsuIdReg,
                        UsuIdIdAct = ur.UsuIdAct
                    };

                if (usuRolID is null && usuNombre is null && rolDescrip is null)
                {
                    respuesta.Cod = "000";
                    respuesta.Data = await query.ToListAsync();
                    respuesta.Mensaje = "OK";
                }
                else if (usuRolID is not null && usuNombre is null && rolDescrip is null)
                {
                    respuesta.Cod = "000";
                    respuesta.Data = await (from q in query
                                            where q.UsuarioRolID == usuRolID
                                            select q).ToListAsync();

                    respuesta.Mensaje = "OK";
                }
                else if (usuRolID is null && usuNombre is not null && rolDescrip is null)
                {
                    respuesta.Cod = "000";
                    respuesta.Data = await (from q in query
                                            where q.UsuarioNombre.ToLower().Trim() == usuNombre.ToLower().Trim()
                                            select q).ToListAsync();
                    respuesta.Mensaje = "OK";
                }
                else if (usuRolID is null && usuNombre is null && rolDescrip is not null)
                {
                    respuesta.Cod = "000";
                    respuesta.Data = await (from q in query
                                            where q.RolDescripcion.ToLower().Trim() == rolDescrip.ToLower().Trim()
                                            select q).ToListAsync();
                    respuesta.Mensaje = "OK";
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
                log.LogErrorMetodos("UsuarioRolService", "GetUsuarioRol", ex.Message);

            }

            return respuesta;


        }

        public async Task<Respuesta> PostUsuarioRol(UsuarioRol usuarioRol)
        {
            Respuesta respuesta = new Respuesta();

            try
            {
                bool existUsuarioRol = await _context.UsuarioRols.AnyAsync(x => x.UsuRolId == usuarioRol.UsuRolId);

                if (!existUsuarioRol)
                {
                    int? lastID = await _context.UsuarioRols.OrderByDescending(x => x.UsuRolId).Select(x => x.UsuRolId).FirstOrDefaultAsync();
                    int newID = (lastID.HasValue ? lastID.Value : 0) + 1;
                    usuarioRol.UsuRolId = newID;
                    usuarioRol.FechaHoraReg = DateTime.Now;
                    usuarioRol.FechaHoraAct = DateTime.Now;

                    respuesta.Cod = "000";
                    respuesta.Data = usuarioRol;
                    respuesta.Mensaje = "Ok";

                    await _context.UsuarioRols.AddAsync(usuarioRol);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    respuesta.Cod = "999";
                    respuesta.Mensaje = "UsuarioRolID ya se encuentra registrado";
                }
            }
            catch (Exception ex)
            {

                respuesta.Cod = "000";
                respuesta.Mensaje = $"Se presento un error, comunicarse con el departamento de sistemas";
                log.LogErrorMetodos("UsuarioRolService", "PostUsuarioRol", ex.Message);
            }

            return respuesta;
        }

        public async Task<Respuesta> PutUsuarioRol(UsuarioRol usuarioRol)
        {
            Respuesta respuesta = new Respuesta();

            try
            {
                bool existUsuarioRol = await _context.UsuarioRols.AnyAsync(x => x.UsuRolId == usuarioRol.UsuRolId);

                if (existUsuarioRol)
                {
                    usuarioRol.FechaHoraAct = DateTime.Now;
                    respuesta.Cod = "000";
                    respuesta.Data = usuarioRol;
                    respuesta.Mensaje = "OK";

                    _context.UsuarioRols.Update(usuarioRol);
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
                log.LogErrorMetodos("UsuarioRolService", "PutUsuarioRol", ex.Message);
            }

            return respuesta;
        }
        public async Task<Respuesta> DeleteUsuarioRol(int usuRolID)
        {
            Respuesta respuesta = new Respuesta();

            try
            {
                bool existUsuarioRol = await _context.UsuarioRols.AnyAsync(x => x.UsuRolId == usuRolID);

                if (existUsuarioRol)
                {
                    UsuarioRol? usuarioRolToDelete = await _context.UsuarioRols.Where(x => x.UsuRolId == usuRolID).FirstOrDefaultAsync();

                    if (usuarioRolToDelete is not null)
                    {
                        usuarioRolToDelete.EstadoId = 2;

                        respuesta.Cod = "000";
                        respuesta.Data = usuarioRolToDelete;
                        respuesta.Mensaje = "OK";

                        _context.UsuarioRols.Update(usuarioRolToDelete);
                        await _context.SaveChangesAsync();
                    }
                }
                else
                {
                    respuesta.Cod = "999";
                    respuesta.Mensaje = "usuRolID no existe, no se puede realizar cambios";
                }
            }
            catch (Exception ex)
            {

                respuesta.Cod = "000";
                respuesta.Mensaje = $"Se presento un error, comunicarse con el departamento de sistemas";
                log.LogErrorMetodos("UsuarioRolService", "DeleteUsuarioRol", ex.Message);
            }

            return respuesta;
        }
    }
}
