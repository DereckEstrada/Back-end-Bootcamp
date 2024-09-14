using ApiVentas.DTOs;
using ApiVentas.Interfaces;
using ApiVentas.Models;
using ApiVentas.Utilitarios;
using ApiVentas.Utilitarios.Dictionaries;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Linq.Expressions;
using System.Security;

namespace ApiVentas.Services
{
    public class UsuarioServices : IUsuarioServices, IServices<Usuario>, IServices<UsuarioRol>, IServices<UsuarioPermiso>, IServices<Opcion>, IServices<Modulo>, IServices<Rol>, IServices<UsuarioAutenticacion>
    {
        private readonly BaseErpContext _context;
        private ControlError log = new ControlError();
        private DynamicEmpty dynamicEmpty = new DynamicEmpty();
        public UsuarioServices(BaseErpContext context)
        {
            this._context = context;
        }

        public async Task<Respuesta> GetUsuario(DataQuery dataQuery)
        {
            Respuesta result = new Respuesta();
            try
            {
                result.Data = await _context.Usuarios
                                            .Include(usuario => usuario.Empresa)
                                            .Include(usuario => usuario.Estado)
                                            .Include(usuario => usuario.UsuIdRegNavigation)
                                            .Where(UsuarioDictionary.GetExpression(dataQuery))
                                            .Select(usuario => new UsuarioDTO
                                            {
                                                UsuId = usuario.UsuId,
                                                UsuNombre = usuario.UsuNombre,
                                                EmpresaId = usuario.EmpresaId,
                                                EmpresaDescripcion = usuario.Empresa.EmpresaNombre,
                                                EstadoId = usuario.EstadoId,
                                                EstadoDescripcion = usuario.Estado.EstadoDescrip,
                                                FechaHoraReg = usuario.FechaHoraReg,
                                                UsuIdReg = usuario.UsuIdReg,
                                                UsuRegName = usuario.UsuIdRegNavigation.UsuNombre,
                                            }).ToListAsync();

                result.Code = dynamicEmpty.IsEmpty(result.Data) ? "204" : "200";
                result.Message = dynamicEmpty.IsEmpty(result.Data) ? $"No se encontro registro con opcion:                              '{dataQuery.OpcionData}' con data: '{dataQuery.DataFirstQuery}'" : "Ok";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "GetUsuario", ex.Message);
            }
            return result;
        }

        public async Task<Respuesta> PostUsuario(Usuario usuario)
        {
            Respuesta result = new Respuesta();
            try
            {
                var query = await _context.Usuarios.OrderByDescending(usuarioDB => usuarioDB.UsuId)
                                                       .Select(idDB => idDB.UsuId).FirstOrDefaultAsync() + 1;
                usuario.UsuId = query;
                usuario.FechaHoraReg = DateTime.Now;

                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();

                result.Code = "200";
                result.Data = usuario;
                result.Message = "Ok";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "PostUsuario", ex.Message);
            }
            return result;
        }

        public async Task<Respuesta> PutUsuario(Usuario usuario)
        {
            Respuesta result = new Respuesta();
            try
            {
                bool existUsuario = await _context.Usuarios.AnyAsync(usuarioDB => usuarioDB.UsuId == usuario.UsuId);

                if (existUsuario)
                {
                    usuario.FechaHoraAct = DateTime.Now;

                    _context.Usuarios.Update(usuario);
                    await _context.SaveChangesAsync();
                    result.Data = usuario;
                }
                result.Code = existUsuario ? "200" : "204";
                result.Message = existUsuario ? "Ok" : $"No existe registro con id: '{usuario.UsuId}'";
            }
            catch (Exception ex)
            {

                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "PutUsuario", ex.Message);
            }
            return result;
        }

        public async Task<Respuesta> DeleteUsuario(Usuario usuario)
        {
            Respuesta result = new Respuesta();
            try
            {
                bool existUsuario = await _context.Usuarios.AnyAsync(usuarioDB => usuarioDB.UsuId == usuario.UsuId);

                if (existUsuario)
                {
                    usuario.FechaHoraAct = DateTime.Now;
                    usuario.EstadoId = 2;

                    _context.Usuarios.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                result.Code = existUsuario ? "200" : "204";
                result.Message = existUsuario ? "Ok" : $"No existe registro con id: '{usuario.UsuId}'";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "DeleteUsuario", ex.Message);
            }
            return result;
        }


        public async Task<Respuesta> GetUsuarioRol()
        {
            Respuesta result = new Respuesta();

            try
            {
                result.Data = await _context.UsuarioRols
                                            .Include(usarioRol=> usarioRol.Rol)
                                            .Include(usarioRol=> usarioRol.Usu)
                                            .Include(usarioRol=> usarioRol.Estado)
                                            .Include(usarioRol=> usarioRol.UsuIdRegNavigation)
                                            .Where(usuarioRol=>usuarioRol.EstadoId==1)
                                            .Select(usuarioRol=>new UsuarioRolDTO
                                            {
                                                UsuRolId = usuarioRol.UsuRolId,
                                                UsuId = usuarioRol.UsuId,
                                                UsuDescripcion = usuarioRol.Usu.UsuNombre,
                                                RolId = usuarioRol.RolId,
                                                RolDescripcion = usuarioRol.Rol.RolDescripcion,
                                                EstadoId = usuarioRol.EstadoId,
                                                EstadoDescripcion = usuarioRol.Estado.EstadoDescrip,
                                                FechaHoraReg = usuarioRol.FechaHoraReg,
                                                UsuIdReg = usuarioRol.UsuIdReg,
                                                UsuRegName = usuarioRol.UsuIdRegNavigation.UsuNombre,
                                            }).ToListAsync();   

                result.Code = dynamicEmpty.IsEmpty(result.Data) ? "204" : "200";
                result.Message = dynamicEmpty.IsEmpty(result.Data) ? "No existen registros" : "Ok";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "GetUsuarioRol", ex.Message);

            }

            return result;
        }
        public async Task<Respuesta> PostUsuarioRol(UsuarioRol usuarioRol)
        {
            var result = new Respuesta();

            try
            {
                var query = await _context.UsuarioRols.OrderByDescending(usuarioRolDB => usuarioRolDB.UsuRolId)
                                                        .Select(idDB => idDB.UsuRolId).FirstOrDefaultAsync() + 1;
                usuarioRol.UsuRolId = query;
                usuarioRol.FechaHoraReg = DateTime.Now;

                _context.UsuarioRols.Add(usuarioRol);
                await _context.SaveChangesAsync();

                result.Code = "400";
                result.Data = usuarioRol;
                result.Message = "Ok";
            }
            catch (Exception ex)
            {

                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "PostUsuarioRol", ex.Message);
            }

            return result;
        }
        public async Task<Respuesta> PutUsuarioRol(UsuarioRol usuarioRol)
        {
            var result = new Respuesta();

            try
            {
                bool existUsuarioRol = await _context.UsuarioRols.AnyAsync(usuarioRolDB =>
                                                                                usuarioRolDB.UsuRolId == usuarioRol.UsuRolId);
                if (existUsuarioRol)
                {
                    usuarioRol.FechaHoraAct = DateTime.Now;

                    _context.UsuarioRols.Update(usuarioRol);
                    await _context.SaveChangesAsync();
                    result.Data = usuarioRol;
                }
                result.Code = existUsuarioRol ? "200" : "204";
                result.Message = existUsuarioRol ? "Ok" : $"No existe registro con id: '{usuarioRol.UsuId}'";
            }
            catch (Exception ex)
            {

                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "PutUsuarioRol", ex.Message);
            }

            return result;
        }
        public async Task<Respuesta> DeleteUsuarioRol(UsuarioRol usuarioRol)
        {
            var result = new Respuesta();
            try
            {
                bool existUsuarioRol = await _context.UsuarioRols.AnyAsync(usuarioRolDB => 
                                                                                usuarioRolDB.UsuRolId == usuarioRol.UsuRolId);
                if (existUsuarioRol)
                {
                    usuarioRol.FechaHoraAct = DateTime.Now;
                    usuarioRol.EstadoId = 2;

                    _context.UsuarioRols.Update(usuarioRol);
                    await _context.SaveChangesAsync();
                }
                result.Code = existUsuarioRol ? "200" : "204";
                result.Message = existUsuarioRol ? "Ok" : $"No existe registro con id: '{usuarioRol.UsuRolId}'";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "DeleteUsuarioRol", ex.Message);
            }
            return result;
        }


        public async Task<Respuesta> GetUsuarioPermiso(DataQuery dataQuery)
        {
            Respuesta result = new Respuesta();
            try
            {
                result.Data = _context.UsuarioPermisos
                                      .Include(usuarioPermiso => usuarioPermiso.Usu)
                                      .Include(usuarioPermiso => usuarioPermiso.Opcion)
                                      .Include(usuarioPermiso => usuarioPermiso.Modulo)
                                      .Include(usuarioPermiso => usuarioPermiso.Estado)
                                      .Include(usuarioPermiso => usuarioPermiso.UsuIdRegNavigation)
                                      .Where(PermisoDictionary.GetExpression(dataQuery))
                                      .Select(usuarioPermiso => new UsuarioPermisoDTO
                                      {
                                          PermisoId = usuarioPermiso.PermisoId,
                                          ModuloId = usuarioPermiso.ModuloId,
                                          ModuloDescripcion = usuarioPermiso.Modulo.ModuloDescripcion,
                                          OpcionId = usuarioPermiso.OpcionId,
                                          OpcionDescripcion = usuarioPermiso.Opcion.OpcionDescripcion,
                                          UsuId = usuarioPermiso.UsuId,
                                          UsuDescripcion = usuarioPermiso.Usu.UsuNombre,
                                          EstadoId = usuarioPermiso.EstadoId,
                                          EstadoDescripcion = usuarioPermiso.Estado.EstadoDescrip,
                                          FechaHoraReg = usuarioPermiso.FechaHoraReg,
                                          UsuIdReg = usuarioPermiso.UsuIdReg,
                                          UsuRegName = usuarioPermiso.UsuIdRegNavigation.UsuNombre,
                                      }).ToListAsync();

                result.Code = dynamicEmpty.IsEmpty(result.Data) ? "204" : "200";
                result.Message = dynamicEmpty.IsEmpty(result.Data) ? "No existen registros" : "Ok";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "GetUsuarioPermiso", ex.Message);
            }
            return result;
        }
        public async Task<Respuesta> PostUsuarioPermiso(UsuarioPermiso usuarioPermiso)
        {
            Respuesta result = new Respuesta();
            try
            {
                var query = await _context.UsuarioPermisos.OrderByDescending(usuarioPermisoDB => usuarioPermisoDB.PermisoId)
                                                            .Select(idDB => idDB.PermisoId).FirstOrDefaultAsync() + 1;
                usuarioPermiso.FechaHoraReg = DateTime.Now;

                _context.UsuarioPermisos.Add(usuarioPermiso);
                await _context.SaveChangesAsync();

                result.Code = "2004";
                result.Data = usuarioPermiso;
                result.Message = "Ok";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "PostUsuarioPermiso", ex.Message);
            }
            return result;
        }
        public async Task<Respuesta> PutUsuarioPermiso(UsuarioPermiso usuarioPermiso)
        {
            Respuesta result = new Respuesta();
            try
            {
                bool existUsuarioPermiso = await _context.UsuarioPermisos.AnyAsync(usuarioPermisoDB =>
                                                                        usuarioPermisoDB.PermisoId == usuarioPermiso.PermisoId);

                if (existUsuarioPermiso)
                {
                    usuarioPermiso.FechaHoraAct = DateTime.Now;


                    _context.UsuarioPermisos.Update(usuarioPermiso);
                    await _context.SaveChangesAsync();
                    result.Data = usuarioPermiso;
                }
                result.Code = existUsuarioPermiso ? "200" : "204";
                result.Message = existUsuarioPermiso ? "Ok" : $"No existe registro con id: '{usuarioPermiso.PermisoId}'";
            }
            catch (Exception ex)
            {

                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "PutUsuarioPermiso", ex.Message);
            }
            return result;
        }
        public async Task<Respuesta> DeleteUsuarioPermiso(UsuarioPermiso usuarioPermiso)
        {
            Respuesta result = new Respuesta();
            try
            {
                bool existUsuarioPermiso = await _context.UsuarioPermisos.AnyAsync(usuarioPermisoDB =>
                                                                        usuarioPermisoDB.PermisoId == usuarioPermiso.PermisoId);

                if (existUsuarioPermiso)
                {
                    usuarioPermiso.FechaHoraAct = DateTime.Now;
                    usuarioPermiso.EstadoId = 2;

                    _context.UsuarioPermisos.Update(usuarioPermiso);
                    await _context.SaveChangesAsync();
                }
                result.Code = existUsuarioPermiso ? "200" : "204";
                result.Message = existUsuarioPermiso ? "Ok" : $"No existe registro con id: '{usuarioPermiso.PermisoId}'";
            }
            catch (Exception ex)
            {

                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "DeleteUsuarioPermiso", ex.Message);
            }
            return result;
        }


        public async Task<Respuesta> GetOpcion()
        {
            var result = new Respuesta();
            try
            {
                result.Data = await _context.Opcions
                                            .Include(opcion=>opcion.Modulo)
                                            .Include(opcion=>opcion.Estado)
                                            .Include(opcion=>opcion.UsuIdRegNavigation)
                                            .Where(opcion=>opcion.EstadoId==1)
                                            .Select(opcion=>new OpcionDTO
                                            {
                                                OpcionId = opcion.OpcionId,
                                                OpcionDescripcion = opcion.OpcionDescripcion,
                                                ModuloId = opcion.ModuloId,
                                                ModuloDescripcion = opcion.Modulo.ModuloDescripcion,
                                                EstadoId = opcion.EstadoId,
                                                EstadoDescripcion = opcion.Estado.EstadoDescrip,
                                                FechaHoraReg = opcion.FechaHoraReg,
                                                UsuIdReg = opcion.UsuIdReg,
                                                UsuRegName = opcion.UsuIdRegNavigation.UsuNombre,
                                            }).ToListAsync();
                                            
                result.Code = dynamicEmpty.IsEmpty(result.Data) ? "204" : "200";
                result.Message = dynamicEmpty.IsEmpty(result.Data) ? "No existen registros" : "Ok";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "GetOpcion", ex.Message);
            }

            return result;
        }
        public async Task<Respuesta> PostOpcion(Opcion opcion)
        {
            var result = new Respuesta();
            try
            {
                var query = await _context.Opcions.OrderByDescending(opcionDB => opcionDB.OpcionId)
                                                    .Select(idDB => idDB.OpcionId).FirstOrDefaultAsync() + 1;
                opcion.OpcionId = query;
                opcion.FechaHoraReg = DateTime.Now;

                _context.Opcions.Add(opcion);
                await _context.SaveChangesAsync();

                result.Code = "200";
                result.Data = opcion;
                result.Message = "Ok";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "PostOpcion", ex.Message);
            }

            return result;
        }
        public async Task<Respuesta> PutOpcion(Opcion opcion)
        {
            var result = new Respuesta();
            try
            {
                bool existOpcion = await _context.Opcions.AnyAsync(opcionDB => opcionDB.OpcionId == opcion.OpcionId);

                if (existOpcion)
                {
                    _context.Opcions.Update(opcion);
                    await _context.SaveChangesAsync();
                    result.Data = opcion;
                }
                result.Code = existOpcion ? "200" : "204";
                result.Message = existOpcion ? "Ok" : $"No existe registro con id: '{opcion.OpcionId}'";

            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "PutOpcion", ex.Message);
            }

            return result;
        }
        public async Task<Respuesta> DeleteOpcion(Opcion opcion)
        {
            var result = new Respuesta();
            try
            {
                bool existOpcion = await _context.Opcions.AnyAsync(opcionDB => opcionDB.OpcionId == opcion.OpcionId);
                if (existOpcion)
                {
                    opcion.FechaHoraAct = DateTime.Now;
                    opcion.EstadoId = 2;

                    _context.Opcions.Update(opcion);
                    await _context.SaveChangesAsync();
                }
                result.Code = existOpcion ? "200" : "204";
                result.Message = existOpcion ? "Ok" : $"No existe registro con id: '{opcion.OpcionId}'";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "DeleteOpcion", ex.Message);
            }

            return result;
        }


        public async Task<Respuesta> GetModulo()
        {
            var result = new Respuesta();

            try
            {
                result.Data = await _context.Modulos
                                            .Include(modulo=>modulo.Estado)
                                            .Include(modulo=>modulo.UsuIdRegNavigation)
                                            .Where(modulo=>modulo.EstadoId==1)
                                            .Select(modulo=>new ModuloDTO
                                            {
                                                ModuloId = modulo.ModuloId,
                                                ModuloDescripcion = modulo.ModuloDescripcion,
                                                EstadoId = modulo.EstadoId,
                                                EstadoDescripcion = modulo.Estado.EstadoDescrip,
                                                FechaHoraReg = modulo.FechaHoraReg,
                                                UsuIdReg = modulo.UsuIdReg,
                                                UsuRegName = modulo.UsuIdRegNavigation.UsuNombre
                                            }).ToListAsync();

                result.Code = dynamicEmpty.IsEmpty(result.Data) ? "204" : "200";
                result.Message = dynamicEmpty.IsEmpty(result.Data) ? "No existen registros" : "Ok";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "GetModulo", ex.Message);
            }

            return result;
        }
        public async Task<Respuesta> PostModulo(Modulo modulo)
        {
            var result = new Respuesta();
            try
            {
                var query = await _context.Modulos.OrderByDescending(moduloDB => moduloDB.ModuloId)
                                        .Select(idDB => idDB.ModuloId).FirstOrDefaultAsync() + 1;
                modulo.ModuloId = query;
                modulo.FechaHoraReg = DateTime.Now;

                _context.Modulos.Add(modulo);
                await _context.SaveChangesAsync();

                result.Code = "200";
                result.Data = modulo;
                result.Message = "Ok";

            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "PostModulo", ex.Message);
            }

            return result;
        }
        public async Task<Respuesta> PutModulo(Modulo modulo)
        {
            var result = new Respuesta();
            try
            {
                var existModulo = await _context.Modulos.AnyAsync(moduloDB =>moduloDB.ModuloId == modulo.ModuloId);

                if (existModulo)
                {
                    modulo.FechaHoraAct = DateTime.Now;

                    _context.Modulos.Update(modulo);
                    await _context.SaveChangesAsync();
                    result.Data = modulo;
                }
                result.Code = existModulo ? "200" : "204";
                result.Message = existModulo ? "Ok" : $"No existe registro con id: '{modulo.ModuloId}'";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "PutModulo", ex.Message);
            }

            return result;
        }
        public async Task<Respuesta> DeleteModulo(Modulo modulo)
        {
            var result = new Respuesta();
            try
            {
                var existModulo = await _context.Modulos.AnyAsync(moduloDB => moduloDB.ModuloId == modulo.ModuloId);
                if (existModulo)
                {

                    modulo.FechaHoraAct = DateTime.Now;
                    modulo.EstadoId = 2;

                    _context.Modulos.Update(modulo);
                    await _context.SaveChangesAsync();
                }
                result.Code = existModulo ? "200" : "204";
                result.Message = existModulo ? "Ok" : $"No existe registro con id: '{modulo.ModuloId}'";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "DeleteModulo", ex.Message);
            }

            return result;
        }


        public async Task<Respuesta> GetRol()
        {
            var result = new Respuesta();
            try
            {
                result.Data = await _context.Rols
                                            .Include(rol=>rol.Estado)
                                            .Include(rol=>rol.UsuIdRegNavigation)
                                            .Where(rol=>rol.EstadoId==1)
                                            .Select(rol=>new RolDTO
                                            {
                                                RolId = rol.RolId,
                                                RolDescripcion = rol.RolDescripcion,
                                                EstadoId = rol.EstadoId,
                                                EstadoDescripcion = rol.Estado.EstadoDescrip,
                                                FechaHoraReg = rol.FechaHoraReg,
                                                UsuIdReg = rol.UsuIdReg,
                                                UsuRegName = rol.UsuIdRegNavigation.UsuNombre,
                                            }).ToListAsync();

                result.Code = dynamicEmpty.IsEmpty(result.Data) ? "204" : "200";
                result.Message = dynamicEmpty.IsEmpty(result.Data) ? "No existen registros" : "Ok";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "GetRol", ex.Message);

            }
            return result;
        }
        public async Task<Respuesta> PostRol(Rol rol)
        {
            var result = new Respuesta();
            try
            {
                var query = await _context.Rols.OrderByDescending(rolDB => rolDB.RolId)
                                                    .Select(idDB => idDB.RolId).FirstOrDefaultAsync() + 1;
                rol.RolId = query;
                rol.FechaHoraReg = DateTime.Now;

                _context.Rols.Add(rol);
                await _context.SaveChangesAsync();

                result.Code = "200";
                result.Data = rol;
                result.Message = "Ok";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "PostRol", ex.Message);

            }
            return result;
        }
        public async Task<Respuesta> PutRol(Rol rol)
        {
            var result = new Respuesta();
            try
            {
                var existRol = await _context.Rols.AnyAsync(rolDB => rolDB.RolId == rol.RolId);
                if (existRol)
                {
                    rol.FechaHoraAct = DateTime.Now;

                    _context.Rols.Update(rol);
                    await _context.SaveChangesAsync();
                    result.Data = rol;
                }
                result.Code = existRol ? "200" : "204";
                result.Message = existRol ? "Ok" : $"No existe registro con id: '{rol.RolId}'";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "PutRol", ex.Message);
            }
            return result;
        }
        public async Task<Respuesta> DeleteRol(Rol rol)
        {
            var result = new Respuesta();
            try
            {
                var existRol = await _context.Rols.AnyAsync(rolDB => rolDB.RolId == rol.RolId);
                if (existRol)
                {
                    rol.FechaHoraAct = DateTime.Now;
                    rol.EstadoId = 2;

                    _context.Rols.Update(rol);
                    await _context.SaveChangesAsync();
                }
                result.Code = existRol ? "200" : "204";
                result.Message = existRol ? "Ok" : $"No se encontro registro con id: '{rol.RolId}'";

            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "DeleteRol", ex.Message);

            }
            return result;
        }


        public async Task<Respuesta> GetUsuarioAutenticacion(DataUsuarioAutenticacion dataUsuarioAutenticacion)
        {
            var result = new Respuesta();
            try
            {
                result.Data = await (from usuarioAutenticacion in _context.UsuarioAutenticacions
                                     where usuarioAutenticacion.Username.Equals(dataUsuarioAutenticacion.Username)
                                                          && usuarioAutenticacion.Userpassword.Equals(dataUsuarioAutenticacion.Password)
                                     select new UsuarioAutenticationDTO
                                     {
                                         Username = usuarioAutenticacion.Username,
                                         Userpassword = usuarioAutenticacion.Userpassword,
                                     }).ToListAsync();

                result.Code = dynamicEmpty.IsEmpty(result.Data) ? "204" : "200";
                result.Message = dynamicEmpty.IsEmpty(result.Data) ? $"No se encontro registro con username: '{dataUsuarioAutenticacion.Username}'": "Ok";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "GetStock", ex.Message);

            }
            return result;
        }
        public async Task<Respuesta> PostUsuarioAutenticacion(UsuarioAutenticacion usuarioAutenticacion)
        {
            var result = new Respuesta();
            try
            {
                _context.UsuarioAutenticacions.Add(usuarioAutenticacion);
                await _context.SaveChangesAsync();

                result.Code = "200";
                result.Message = "Ok";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "PostUsuarioAutenticacion", ex.Message);

            }
            return result;
        }
        public async Task<Respuesta> PutUsuarioAutenticacion(UsuarioAutenticacion usuarioAutenticacion)
        {
            var result = new Respuesta();
            try
            {
                bool existUsuarioAutenticacion = await _context.UsuarioAutenticacions.AnyAsync(usuarioAutenticacionDB => usuarioAutenticacionDB.UsuId == usuarioAutenticacion.UsuId);
                if (existUsuarioAutenticacion)
                {
                    _context.UsuarioAutenticacions.Update(usuarioAutenticacion);
                    await _context.SaveChangesAsync();
                    result.Data= usuarioAutenticacion;
                }

                result.Code = existUsuarioAutenticacion ? "200" : "204";
                result.Message = existUsuarioAutenticacion ? "Ok" : $"No existe registro con id: '{usuarioAutenticacion.UsuId}'";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "PutUsuarioAutenticacion", ex.Message);
            }
            return result;
        }
    }
}
