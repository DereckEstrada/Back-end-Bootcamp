﻿using ApiVentas.DTOs;
using ApiVentas.Interfaces;
using ApiVentas.Models;
using ApiVentas.Utilitarios;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ApiVentas.Services
{
    public class RolServices : IRolServices
    {
        private BaseErpContext _context;
        private RolDTO dto = new RolDTO();
        private ControlError log = new ControlError();
        private DynamicEmpty empty = new DynamicEmpty();
        public RolServices(BaseErpContext context)
        {
            this._context = context;
        }
        public async Task<Respuesta> DeleteRol(int id)
        {
            var result = new Respuesta();
            try
            {
                var rolDelete = await _context.Rols.FirstOrDefaultAsync(x => x.RolId == id);
                if (rolDelete != null)
                {
                    rolDelete.EstadoId = 2;
                    _context.Rols.Update(rolDelete);
                    await _context.SaveChangesAsync();
                }
                result.Cod = rolDelete != null ? "000" : "111";
                result.Mensaje = rolDelete != null ? "OK" : $"No se encontro registro con id: '{id}'";

            }
            catch (Exception ex)
            {
                result.Cod = "999";
                result.Mensaje = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "DeleteRol", ex.Message);

            }
            return result;
        }

        public async Task<Respuesta> GetRol(string? opcion, string? Data)
        {
            var result = new Respuesta();
            Expression<Func<RolDTO, bool>> query = dto.DictionaryRol(opcion, Data);
            try
            {
                if (query != null)
                {
                    result.Data = await (from rol in _context.Rols
                                         join userReg in _context.Usuarios on rol.UsuIdReg equals userReg.UsuId
                                         join est in _context.Estados on rol.EstadoId equals est.EstadoId
                                         select new RolDTO
                                         {
                                             RolId = rol.RolId,
                                             RolDescripcion = rol.RolDescripcion,
                                             EstadoId = rol.EstadoId,
                                             EstadoDescrip=est.EstadoDescrip,
                                             FechaHoraReg = rol.FechaHoraReg,
                                             FechaHoraAct = rol.FechaHoraAct,
                                             UsuIdReg = rol.UsuIdReg,
                                             UsuRegDescrip = userReg.UsuNombre,
                                             UsuIdAct = rol.UsuIdAct,
                                         }).Where(query).ToListAsync();
                }
                result.Cod = empty.IsEmpty(result.Data) ? "111" : "000";
                result.Mensaje = empty.IsEmpty(result.Data) ? $"No se encontro registro con opcion: '{opcion}' con Data: '{Data}'" : "OK";
            }
            catch (Exception ex)
            {
                result.Cod = "999";
                result.Mensaje = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "GetRol", ex.Message);

            }
            return result;
        }

        public async Task<Respuesta> PostRol(Rol rol)
        {
            var result = new Respuesta();
            try
            {
                var id = await _context.Rols.OrderByDescending(x => x.RolId).Select(x => x.RolId).FirstOrDefaultAsync() + 1;
                rol.RolId = id;
                rol.FechaHoraReg = DateTime.Now;
                var validar = rol.UsuIdReg != null;
                if (validar)
                {
                    _context.Rols.Add(rol);
                    await _context.SaveChangesAsync();
                }
                result.Cod = validar ? "000" : "111";
                result.Mensaje = validar ? "Ok" : "No se puede ingresar registro sin datos del usuario";
            }
            catch (Exception ex)
            {
                result.Cod = "999";
                result.Mensaje = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "PostRol", ex.Message);

            }
            return result;
        }

        public async Task<Respuesta> PutRol(Rol rol)
        {
            var result = new Respuesta();
            try
            {
                var validar = await _context.Rols.AnyAsync(x => x.RolId == rol.RolId);
                var usuarioEdit = rol.UsuIdAct;
                if (validar && usuarioEdit != null)
                {
                    result.Cod = "000";
                    result.Mensaje = "OK";
                    rol.UsuIdReg = await _context.Rols.Where(x => x.RolId == rol.RolId).Select(x => x.UsuIdReg).FirstOrDefaultAsync();
                    rol.FechaHoraReg = await _context.Rols.Where(x => x.RolId == rol.RolId).Select(x => x.FechaHoraReg).FirstOrDefaultAsync();
                    rol.FechaHoraAct = DateTime.Now;
                    _context.Rols.Update(rol);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    result.Cod = "111";
                    result.Mensaje = usuarioEdit != null ? $"No se encontro registro con id: '{rol.RolId}'" : "No se puede actualizar registro sin los datos del usuario";
                }
            }
            catch (Exception ex)
            {
                result.Cod = "999";
                result.Mensaje = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "PutPuntoEmisionSri", ex.Message);
            }
            return result;
        }
    }
}
