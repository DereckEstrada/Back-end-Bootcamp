using ApiVentas.DTOs;
using ApiVentas.Interfaces;
using ApiVentas.Models;
using ApiVentas.Utilitarios;
using Microsoft.EntityFrameworkCore;
using System;

namespace ApiVentas.Services
{
    public class UsuarioService : IUsuario
    {
        private readonly BaseErpContext _context;
        private ControlError log = new ControlError();
        public UsuarioService(BaseErpContext context)
        {
            this._context = context;
        }

        public async Task<Respuesta> GetUsuario(int? usuID, string? usuNombre)
        {
            Respuesta respuesta  = new Respuesta();
            try
            {
                IQueryable<UsuarioDTO> query =
                    from u in _context.Usuarios
                    join es in _context.Estados on u.EstadoId equals es.EstadoId
                    join emp in _context.Empresas on u.EmpresaId equals emp.EmpresaId
                    select new UsuarioDTO
                    {
                        UsuId = u.UsuId,
                        UsuNombre = u.UsuNombre,
                        EmpresaNombre = emp.EmpresaNombre,
                        EstadoDescrip = es.EstadoDescrip,
                        FechaHoraReg = u.FechaHoraReg,
                        FechaHoraAct = u.FechaHoraAct,
                        UsuIdReg = u.UsuIdReg,
                        UsuIdAct = u.UsuIdAct
                    };

                if (usuID is null && usuNombre is null)
                {
                    respuesta.Cod = "000";
                    respuesta.Data = await query.ToListAsync();
                    respuesta.Mensaje = "OK";
                }
                else if (usuID is not null && usuNombre is null)
                {
                    respuesta.Cod = "000";
                    respuesta.Data = await (from q in query
                                            where q.UsuId == usuID
                                            select q).ToListAsync();
                    respuesta.Mensaje = "OK";
                }
                else if (usuID is null && usuNombre is not null)
                {
                    respuesta.Cod = "000";
                    respuesta.Data = await (from q in query
                                            where q.UsuNombre == usuNombre
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
                log.LogErrorMetodos("UsuarioService", "GetUsuario", ex.Message);
            }
            return respuesta;
        }

        public async Task<Respuesta> PostUsuario(Usuario usuario)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                bool existUsuario = await _context.Usuarios.AnyAsync(x => x.UsuId == usuario.UsuId);

                if (!existUsuario)
                {
                    int? lastID = await _context.Usuarios.OrderByDescending(x => x.UsuId).Select(x => x.UsuId).FirstOrDefaultAsync();
                    int newID = (lastID.HasValue ? lastID.Value : 0) + 1;
                    usuario.UsuId = newID;
                    //usuario.FechaHoraReg = DateTime.Now;
                    //usuario.FechaHoraAct = DateTime.Now;

                    respuesta.Cod = "000";
                    respuesta.Data = usuario;
                    respuesta.Mensaje = "Ok";

                    await _context.Usuarios.AddAsync(usuario);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    respuesta.Cod = "999";
                    respuesta.Mensaje = "usuarioID ya se encuentra registrado";
                }
            }
            catch (Exception ex)
            {

                respuesta.Cod = "000";
                respuesta.Mensaje = ex.Message; respuesta.Mensaje = $"Se presento un error, comunicarse con el departamento de sistemas";
                log.LogErrorMetodos("UsuarioService", "PosttUsuario", ex.Message);
            }
            return respuesta;
        }

        public async Task<Respuesta> PutUsuario(Usuario usuario)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                bool existUsuario = await _context.Usuarios.AnyAsync(x => x.UsuId == usuario.UsuId);

                if (existUsuario)
                {
                    //usuario.FechaHoraAct = DateTime.Now;
                    respuesta.Cod = "000";
                    respuesta.Data = usuario;
                    respuesta.Mensaje = "OK";

                    _context.Usuarios.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    respuesta.Cod = "000";
                    respuesta.Mensaje = "UsuID no existente, no se puedo realizar cambios";
                }
            }
            catch (Exception ex)
            {

                respuesta.Cod = "000";
                respuesta.Mensaje = ex.Message; respuesta.Mensaje = $"Se presento un error, comunicarse con el departamento de sistemas";
                log.LogErrorMetodos("UsuarioService", "PutUsuario", ex.Message);
            }
            return respuesta;
        }

        public async Task<Respuesta> DeleteUsuario(int usuID)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                bool existUsuarioRol = await _context.Usuarios.AnyAsync(x => x.UsuId == usuID);

                if (existUsuarioRol)
                {
                    Usuario? usuarioToDelete = await _context.Usuarios.Where(x => x.UsuId == usuID).FirstOrDefaultAsync();

                    if (usuarioToDelete is not null)
                    {
                        usuarioToDelete.EstadoId = 2;

                        respuesta.Cod = "000";
                        respuesta.Data = usuarioToDelete;
                        respuesta.Mensaje = "OK";

                        _context.Usuarios.Update(usuarioToDelete);
                        await _context.SaveChangesAsync();
                    }
                }
                else
                {
                    respuesta.Cod = "999";
                    respuesta.Mensaje = "usuID no existe, no se puede realizar cambios";
                }
            }
            catch (Exception ex)
            {

                respuesta.Cod = "000";
                respuesta.Mensaje = ex.Message; respuesta.Mensaje = $"Se presento un error, comunicarse con el departamento de sistemas";
                log.LogErrorMetodos("UsuarioService", "DeleteUsuario", ex.Message);
            }
            return respuesta;
        }
    }
}
