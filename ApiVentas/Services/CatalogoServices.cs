using ApiVentas.DTOs;
using ApiVentas.Interfaces;
using ApiVentas.Models;
using ApiVentas.Utilitarios;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;

namespace ApiVentas.Services
{
    public class CatalogoServices : ICatalogoServices, IServices<Categorium>, IServices<Ciudad>, IServices<Pai>, IServices<FormaPago>, IServices<TipoMovimiento>, IServices<Industrium>, IServices<TarjetaCredito>
    {
        private readonly BaseErpContext _context;
        private ControlError log = new ControlError();
        private DynamicEmpty dynamicEmpty = new DynamicEmpty();
        public CatalogoServices(BaseErpContext context)
        {
            this._context = context;
        }

        public async Task<Respuesta> GetCategoria(DataQuery dataQuery)
        {
            var result = new Respuesta();
            try
            {
                result.Data = await _context.Categoria
                                            .Include(categoria => categoria.Estado)
                                            .Include(categoria => categoria.UsuIdRegNavigation)
                                            .Where(categoria =>categoria.CategoriaDescrip.Contains(dataQuery.DataFirstQuery)
                                                                                                 && categoria.EstadoId == 1)
                                            .Select(categoria => new CategoriaDTO
                                            {
                                                CategoriaId = categoria.CategoriaId,
                                                CategoriaDescrip= categoria.CategoriaDescrip,
                                                EstadoId = categoria.EstadoId,
                                                EstadoDescripcion = categoria.Estado.EstadoDescrip,
                                                FechaHoraReg = categoria.FechaHoraReg,
                                                UsuIdReg = categoria.UsuIdReg,
                                                UsuRegName = categoria.UsuIdRegNavigation.UsuNombre,
                                            }).ToListAsync();

                result.Code = dynamicEmpty.IsEmpty(result.Data) ? "204" : "200";
                result.Message = dynamicEmpty.IsEmpty(result.Data) ? "No existen registros" : "Ok";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "GetCategoria", ex.Message);
            }

            return result;
        }
        public async Task<Respuesta> PostCategoria(Categorium categoria)
        {
            var result = new Respuesta();
            try
            {
                var query = await _context.Categoria.OrderByDescending(categoriaDB => categoriaDB.CategoriaId)
                                                        .Select(idDB => idDB.CategoriaId).FirstOrDefaultAsync() + 1;
                categoria.CategoriaId = query;
                categoria.FechaHoraReg = DateTime.Now;

                _context.Categoria.Add(categoria);
                await _context.SaveChangesAsync();

                result.Code = "200";
                result.Data = categoria;
                result.Message = "Ok";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "PostCategoria", ex.Message);
            }
            return result;
        }
        public async Task<Respuesta> PutCategoria(Categorium categoria)
        {
            var result = new Respuesta();
            try
            {
                bool existCategoria = await _context.Categoria.AnyAsync(categoriaDB =>
                                                                        categoriaDB.CategoriaId == categoria.CategoriaId);
                if (existCategoria)
                {
                    categoria.FechaHoraAct = DateTime.Now;

                    _context.Categoria.Update(categoria);
                    await _context.SaveChangesAsync();
                    result.Data = categoria;
                }
                result.Code = existCategoria ? "200" : "204";
                result.Message = existCategoria ? "Ok" : $"No existe registro con id: '{categoria.CategoriaId}'";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "PutCategoria", ex.Message);
            }
            return result;
        }
        public async Task<Respuesta> DeleteCategoria(Categorium categoria)
        {
            Respuesta result = new Respuesta();
            try
            {
                var existCategoria = await _context.Categoria.AnyAsync(categoriaDB =>
                                                                categoriaDB.CategoriaId == categoria.CategoriaId);
                if (existCategoria)
                {
                    categoria.FechaHoraAct = DateTime.Now;
                    categoria.EstadoId = 2;

                    _context.Categoria.Update(categoria);
                    await _context.SaveChangesAsync();
                }
                result.Code = existCategoria ? "200" : "204";
                result.Message = existCategoria ? "Ok" : $"No existe registro con id: '{categoria.CategoriaId}'";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "DeleteCategoria", ex.Message);
            }
            return result;
        }


        public async Task<Respuesta> GetCiudad(DataQuery dataQuery)
        {
            var result = new Respuesta();
            try
            {
                result.Data = await _context.Ciudads
                                          .Include(ciudad => ciudad.Pais)
                                          .Include(ciudad => ciudad.Estado)
                                          .Include(ciudad => ciudad.UsuIdRegNavigation)
                                          .Where(ciudad => ciudad.CiudadNombre.Contains(dataQuery.DataFirstQuery)
                                                                                         && ciudad.EstadoId == 1)
                                          .Select(ciudad => new CiudadDTO
                                          {
                                              CiudadId = ciudad.CiudadId,
                                              CiudadNombre = ciudad.CiudadNombre,
                                              PaisId = ciudad.PaisId,
                                              PaisDescripcion = ciudad.Pais.PaisNombre,
                                              EstadoId = ciudad.EstadoId,
                                              EstadoDescripcion = ciudad.Estado.EstadoDescrip,
                                              FechaHoraReg = ciudad.FechaHoraReg,
                                              UsuIdReg = ciudad.UsuIdReg,
                                              UsuRegName = ciudad.UsuIdRegNavigation.UsuNombre,
                                          }).ToListAsync();

                result.Code = dynamicEmpty.IsEmpty(result.Data) ? "204" : "200";
                result.Message = dynamicEmpty.IsEmpty(result.Data) ? "No existen registros" : "Ok";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "GetCiudad", ex.Message);
            }

            return result;
        }
        public async Task<Respuesta> PostCiudad(Ciudad ciudad)
        {
            var result = new Respuesta();
            try
            {
                var query = await _context.Ciudads.OrderByDescending(ciudadDB => ciudadDB.CiudadId)
                                                    .Select(idDB => idDB.CiudadId).FirstOrDefaultAsync() + 1;
                ciudad.CiudadId = query;
                ciudad.FechaHoraReg = DateTime.Now;

                _context.Ciudads.Add(ciudad);
                await _context.SaveChangesAsync();

                result.Code = "200";
                result.Data = ciudad;
                result.Message = "Ok";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "PostCiudad", ex.Message);
            }
            return result;
        }
        public async Task<Respuesta> PutCiudad(Ciudad ciudad)
        {
            var result = new Respuesta();
            try
            {
                bool existCiudad = await _context.Ciudads.AnyAsync(ciudadDB => ciudadDB.CiudadId == ciudad.CiudadId);
                if (existCiudad)
                {
                    ciudad.FechaHoraAct = DateTime.Now;

                    _context.Ciudads.Update(ciudad);
                    await _context.SaveChangesAsync();
                    result.Data = ciudad;
                }
                result.Code = existCiudad ? "200" : "204";
                result.Message = existCiudad ? "Ok" : $"No existe registro con id: '{ciudad.CiudadId}'";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "PutCiudad", ex.Message);
            }
            return result;
        }
        public async Task<Respuesta> DeleteCiudad(Ciudad ciudad)
        {
            Respuesta result = new Respuesta();
            try
            {
                var existCiudad = await _context.Ciudads.AnyAsync(ciudadDB => ciudadDB.CiudadId == ciudad.CiudadId);
                if (existCiudad)
                {
                    ciudad.FechaHoraAct = DateTime.Now;
                    ciudad.EstadoId = 2;

                    _context.Ciudads.Update(ciudad);
                    await _context.SaveChangesAsync();
                }
                result.Code = existCiudad ? "200" : "204";
                result.Message = existCiudad ? "Ok" : $"No existe registro con id: '{ciudad.CiudadId}'";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "DeleteCiudad", ex.Message);
            }
            return result;
        }


        public async Task<Respuesta> GetPais(DataQuery dataQuery)
        {
            var result = new Respuesta();
            try
            {
                result.Data = await _context.Pais
                                            .Include(pais => pais.Estado)
                                            .Include(pais => pais.UsuIdRegNavigation)
                                            .Where(pais => pais.PaisNombre.Contains(dataQuery.DataFirstQuery) && pais.EstadoId == 1)
                                            .Select(pais => new PaiDTO
                                            {
                                                PaisId = pais.PaisId,
                                                PaisNombre = pais.PaisNombre,
                                                EstadoId = pais.EstadoId,
                                                EstadoDescripcion = pais.Estado.EstadoDescrip,
                                                FechaHoraReg = pais.FechaHoraReg,
                                                UsuIdReg = pais.UsuIdReg,
                                                UsuRegName = pais.UsuIdRegNavigation.UsuNombre,
                                            }).ToListAsync();

                result.Code = dynamicEmpty.IsEmpty(result.Data) ? "204" : "200";
                result.Message = dynamicEmpty.IsEmpty(result.Data) ? "No existen registros" : "Ok";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "GetPais", ex.Message);
            }

            return result;
        }
        public async Task<Respuesta> PostPais(Pai pais)
        {
            var result = new Respuesta();
            try
            {
                var query = await _context.Pais.OrderByDescending(paisDB => pais.PaisId)
                                                    .Select(idDB => idDB.PaisId).FirstOrDefaultAsync() + 1;
                pais.PaisId = query;
                pais.FechaHoraReg = DateTime.Now;

                _context.Pais.Add(pais);
                await _context.SaveChangesAsync();

                result.Code = "200";
                result.Data = pais;
                result.Message = "Ok";

            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "PostPais", ex.Message);
            }

            return result;
        }
        public async Task<Respuesta> PutPais(Pai pais)
        {
            var result = new Respuesta();
            try
            {
                bool existPais = await _context.Pais.AnyAsync(paisDB => paisDB.PaisId == pais.PaisId);
                if (existPais)
                {
                    pais.FechaHoraAct = DateTime.Now;

                    _context.Pais.Update(pais);
                    await _context.SaveChangesAsync();
                    result.Data = pais;
                }

                result.Code = existPais ? "200" : "204";
                result.Message = existPais ? "Ok" : $"No existe registro con id: '{pais.PaisId}'";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "PutPais", ex.Message);
            }

            return result;
        }
        public async Task<Respuesta> DeletePais(Pai pais)
        {
            var result = new Respuesta();
            try
            {
                bool existPais = await _context.Pais.AnyAsync(paisDB => paisDB.PaisId == pais.PaisId);
                if (existPais)
                {
                    pais.FechaHoraAct = DateTime.Now;
                    pais.EstadoId = 2;

                    _context.Pais.Update(pais);
                    await _context.SaveChangesAsync();
                }
                result.Code = existPais ? "200" : "204";
                result.Message = existPais ? "Ok" : $"No existe registro con id: '{pais.PaisId}'";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "DeletePais", ex.Message);
            }
            return result;
        }


        public async Task<Respuesta> GetFormaPago()
        {
            var result = new Respuesta();
            try
            {
                result.Data = await _context.FormaPagos
                                            .Include(formaPagos => formaPagos.Estado)
                                            .Include(formaPagos => formaPagos.UsuIdRegNavigation)
                                            .Where(formaPago => formaPago.EstadoId == 1)
                                            .Select(formaPago => new FormaPagoDTO
                                            {
                                                FpagoId = formaPago.FpagoId,
                                                FpagoDescripcion = formaPago.FpagoDescripcion,
                                                EstadoId = formaPago.EstadoId,
                                                EstadoDescripcion = formaPago.Estado.EstadoDescrip,
                                                FechaHoraReg = formaPago.FechaHoraReg,
                                                UsuIdReg = formaPago.UsuIdReg,
                                                UsuRegName = formaPago.UsuIdRegNavigation.UsuNombre,
                                            }).ToListAsync();

                result.Code = dynamicEmpty.IsEmpty(result.Data) ? "204" : "200";
                result.Message = dynamicEmpty.IsEmpty(result.Data) ? "No existen registros" : "Ok";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "GetFormaPago", ex.Message);
            }

            return result;
        }
        public async Task<Respuesta> PostFormaPago(FormaPago formaPago)
        {
            var result = new Respuesta();
            try
            {
                var query = await _context.FormaPagos.OrderByDescending(formaPagoDB => formaPagoDB.FpagoId)
                                                        .Select(idDB => idDB.FpagoId).FirstOrDefaultAsync() + 1;
                formaPago.FpagoId = query;
                formaPago.FechaHoraReg = DateTime.Now;

                _context.FormaPagos.Add(formaPago);
                await _context.SaveChangesAsync();

                result.Code = "200";
                result.Data = formaPago;
                result.Message = "Ok";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "PostFormaPago", ex.Message);
            }
            return result;
        }
        public async Task<Respuesta> PutFormaPago(FormaPago formaPago)
        {
            var result = new Respuesta();
            try
            {
                bool existFormaPago = await _context.FormaPagos.AnyAsync(formaPagoDB => formaPagoDB.FpagoId == formaPago.FpagoId);
                if (existFormaPago)
                {
                    formaPago.FechaHoraAct = DateTime.Now;

                    _context.FormaPagos.Update(formaPago);
                    await _context.SaveChangesAsync();
                    result.Data = formaPago;
                }
                result.Code = existFormaPago ? "200" : "204";
                result.Message = existFormaPago ? "Ok" : $"No existe registro con id: '{formaPago.FpagoId}'";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "PutFormaPago", ex.Message);
            }
            return result;
        }
        public async Task<Respuesta> DeleteFormaPago(FormaPago formaPago)
        {
            Respuesta result = new Respuesta();
            try
            {
                bool existFormaPago = await _context.FormaPagos.AnyAsync(formaPagoDB => formaPagoDB.FpagoId == formaPago.FpagoId);
                if (existFormaPago)
                {
                    formaPago.FechaHoraAct = DateTime.Now;
                    formaPago.EstadoId = 2;

                    _context.FormaPagos.Update(formaPago);
                    await _context.SaveChangesAsync();
                }
                result.Code = existFormaPago ? "200" : "204";
                result.Message = existFormaPago ? "Ok" : $"No existe registro con id: '{formaPago.FpagoId}'";

            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "DeleteFormaPago", ex.Message);
            }
            return result;
        }


        public async Task<Respuesta> GetTipoMovimiento()
        {
            Respuesta result = new Respuesta();
            try
            {
                result.Data = await _context.TipoMovimientos
                                            .Include(tipoMovieminto => tipoMovieminto.Estado)
                                            .Include(tipoMovieminto => tipoMovieminto.UsuIdRegNavigation)
                                            .Where(tipoMovimiento => tipoMovimiento.EstadoId == 1)
                                            .Select(tipoMovimiento => new TipoMovimientoDTO
                                            {
                                                TipomovId = tipoMovimiento.TipomovId,
                                                TipomovDescrip= tipoMovimiento.TipomovDescrip,
                                                TipomovIngEgr = tipoMovimiento.TipomovIngEgr,
                                                EstadoId = tipoMovimiento.EstadoId,
                                                EstadoDescripcion = tipoMovimiento.Estado.EstadoDescrip,
                                                FechaHoraReg = tipoMovimiento.FechaHoraReg,
                                                UsuIdReg = tipoMovimiento.UsuIdReg,
                                                UsuRegName = tipoMovimiento.UsuIdRegNavigation.UsuNombre
                                            }).ToListAsync();

                result.Code = dynamicEmpty.IsEmpty(result.Data) ? "204" : "200";
                result.Message = dynamicEmpty.IsEmpty(result.Data) ? "No existen registros" : "Ok";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "GetTipoMovimiento", ex.Message);
            }
            return result;
        }
        public async Task<Respuesta> PostTipoMovimiento(TipoMovimiento tipoMovimiento)
        {
            Respuesta result = new Respuesta();
            try
            {
                var query = await _context.TipoMovimientos.OrderByDescending(tipoMovimientoDB => tipoMovimiento.TipomovId)
                                                            .Select(idDB => idDB.TipomovId).FirstOrDefaultAsync() + 1;
                tipoMovimiento.TipomovId = query;
                tipoMovimiento.FechaHoraReg = DateTime.Now;

                _context.TipoMovimientos.Add(tipoMovimiento);
                await _context.SaveChangesAsync();

                result.Code = "200";
                result.Data = tipoMovimiento;
                result.Message = "Ok";
            }
            catch (Exception ex)
            {

                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "PostTipoMovimiento", ex.Message);
            }
            return result;
        }
        public async Task<Respuesta> PutTipoMovimiento(TipoMovimiento tipoMovimiento)
        {
            Respuesta result = new Respuesta();
            try
            {
                bool existTipoMovimiento = await _context.TipoMovimientos.AnyAsync(tipoMovimientoDB => 
                                                                            tipoMovimientoDB.TipomovId == tipoMovimiento.TipomovId);

                if (existTipoMovimiento)
                {
                    tipoMovimiento.FechaHoraAct = DateTime.Now;

                    _context.TipoMovimientos.Update(tipoMovimiento);
                    await _context.SaveChangesAsync();
                    result.Data = tipoMovimiento;
                }
                result.Code = existTipoMovimiento ? "200" : "204";
                result.Message = existTipoMovimiento ? "Ok" : $"No existe registro con id: '{tipoMovimiento.TipomovId}'";
            }
            catch (Exception ex)
            {

                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "PutTipoMovimiento", ex.Message);
            }
            return result;
        }
        public async Task<Respuesta> DeleteTipoMovimiento(TipoMovimiento tipoMovimiento)
        {
            Respuesta result = new Respuesta();
            try
            {
                bool existTipoMovimiento = await _context.TipoMovimientos.AnyAsync(tipoMovimientoDB =>
                                                                            tipoMovimientoDB.TipomovId == tipoMovimiento.TipomovId);
                if (existTipoMovimiento)
                {
                    tipoMovimiento.FechaHoraAct = DateTime.Now;
                    tipoMovimiento.EstadoId = 2;

                    _context.TipoMovimientos.Update(tipoMovimiento);
                    await _context.SaveChangesAsync();
                }
                result.Code = existTipoMovimiento ? "200" : "204";
                result.Message = existTipoMovimiento ? "Ok" : $"No existe registro con id: '{tipoMovimiento.TipomovId}'";
            }
            catch (Exception ex)
            {

                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "DeleteTipoMovimiento", ex.Message);
            }
            return result;
        }


        public async Task<Respuesta> GetIndustria(DataQuery dataQuery)
        {
            var result = new Respuesta();
            try
            {
                result.Data = await _context.Industria
                                          .Include(industria => industria.Estado)
                                          .Include(industria => industria.UsuIdRegNavigation)
                                          .Where(industria => industria.IndustriaDescripcion.Contains(dataQuery.DataFirstQuery)
                                                                                                    && industria.EstadoId == 1)
                                          .Select(industria => new IndustriaDTO
                                          {
                                              IndustriaId = industria.IndustriaId,
                                              IndustriaDescripcion = industria.IndustriaDescripcion,
                                              EstadoId = industria.EstadoId,
                                              EstadoDescripcion = industria.Estado.EstadoDescrip,
                                              FechaHoraReg = industria.FechaHoraReg,
                                              UsuIdReg = industria.UsuIdReg,
                                              UsuRegName = industria.UsuIdRegNavigation.UsuNombre,
                                          }).ToListAsync();

                result.Code = dynamicEmpty.IsEmpty(result.Data) ? "204" : "200";
                result.Message = dynamicEmpty.IsEmpty(result.Data) ? "No existen registros" : "Ok";
            }
            catch (Exception ex)
            {

                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "GetIndustria", ex.Message);
            }

            return result;
        }
        public async Task<Respuesta> PostIndustria(Industrium industria)
        {
            var result = new Respuesta();
            try
            {
                var query = await _context.Industria.OrderByDescending(industriaDB => industriaDB.IndustriaId).Select(idDB => idDB.IndustriaId).FirstOrDefaultAsync() + 1;
                industria.IndustriaId = query;
                industria.FechaHoraReg = DateTime.Now;

                _context.Industria.Add(industria);
                await _context.SaveChangesAsync();

                result.Code = "200";
                result.Data = industria;
                result.Message = "Ok";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "PostIndustria", ex.Message);
            }
            return result;
        }
        public async Task<Respuesta> PutIndustria(Industrium industria)
        {
            var result = new Respuesta();
            try
            {
                bool existIndustria = await _context.Industria.AnyAsync(industriaDB=> 
                                                                            industriaDB.IndustriaId == industria.IndustriaId);
                if (existIndustria)
                {
                    industria.FechaHoraAct = DateTime.Now;

                    _context.Industria.Update(industria);
                    await _context.SaveChangesAsync();
                    result.Data = industria;
                }
                result.Code = existIndustria ? "200" : "204";
                result.Message = existIndustria ? "Ok" : $"No existe registro con id: '{industria.IndustriaId}'";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "PutIndustria", ex.Message);
            }
            return result;
        }
        public async Task<Respuesta> DeleteIndustria(Industrium industria)
        {
            Respuesta result = new Respuesta();
            try
            {
                bool existIndustria = await _context.Industria.AnyAsync(industriaDB =>
                                                                            industriaDB.IndustriaId == industria.IndustriaId); 
                if (existIndustria)
                {
                    industria.FechaHoraAct = DateTime.Now;
                    industria.EstadoId = 2;

                    _context.Industria.Update(industria);
                    await _context.SaveChangesAsync();
                }
                result.Code = existIndustria ? "200" : "204";
                result.Message = existIndustria ? "Ok" : $"No existe registro con id: '{industria.IndustriaId}'";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "DeleteIndustria", ex.Message);
            }
            return result;
        }


        public async Task<Respuesta> GetTarjetaCredito()
        {
            Respuesta result = new Respuesta();
            try
            {
                result.Data = await _context.TarjetaCreditos
                                            .Include(tarjetaCredito => tarjetaCredito.Industria)
                                            .Include(tarjetaCredito => tarjetaCredito.Estado)
                                            .Include(tarjetaCredito => tarjetaCredito.UsuIdRegNavigation)
                                            .Where(tarjetaCredito => tarjetaCredito.EstadoId == 1)
                                            .Select(tarjetaCredito => new TarjetaCreditoDTO
                                            {
                                                TarjetacredId = tarjetaCredito.TarjetacredId,
                                                TarjetacredDescripcion = tarjetaCredito.TarjetacredDescripcion,
                                                IndustriaId = tarjetaCredito.IndustriaId,
                                                IndustriaDescripcion = tarjetaCredito.Industria.IndustriaDescripcion,
                                                EstadoId = tarjetaCredito.EstadoId,
                                                EstadoDescripcion = tarjetaCredito.Estado.EstadoDescrip,
                                                FechaHoraReg = tarjetaCredito.FechaHoraReg,
                                                UsuIdReg = tarjetaCredito.UsuIdReg,
                                                UsuRegName = tarjetaCredito.UsuIdRegNavigation.UsuNombre,
                                            }).ToListAsync();

                result.Code = dynamicEmpty.IsEmpty(result.Data) ? "204" : "200";
                result.Message = dynamicEmpty.IsEmpty(result.Data) ? "No existen registros" : "Ok";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "GetTarjetaCredito", ex.Message);
            }
            return result;
        }
        public async Task<Respuesta> PostTarjetaCredito(TarjetaCredito tarjetaCredito)
        {
            Respuesta result = new Respuesta();
            try
            {
                var query = await _context.TarjetaCreditos.OrderByDescending(tarjetaCreditoDB => tarjetaCreditoDB.TarjetacredId)
                                                            .Select(idDB => idDB.TarjetacredId).FirstOrDefaultAsync() + 1;

                tarjetaCredito.TarjetacredId = query;
                tarjetaCredito.FechaHoraReg = DateTime.Now;

                result.Code = "200";
                result.Data = tarjetaCredito;
                result.Message = "Ok";

                await _context.TarjetaCreditos.AddAsync(tarjetaCredito);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "PostTarjetaCredito", ex.Message);
            }
            return result;
        }
        public async Task<Respuesta> PutTarjetaCredito(TarjetaCredito tarjetaCredito)
        {
            Respuesta result = new Respuesta();
            try
            {
                bool existTarjetaCredito = await _context.TarjetaCreditos
                                                    .AnyAsync(tarjetaCreditoDB => tarjetaCreditoDB.TarjetacredId
                                                                                            == tarjetaCredito.TarjetacredId);

                if (existTarjetaCredito)
                {
                    tarjetaCredito.FechaHoraAct = DateTime.Now;

                    _context.TarjetaCreditos.Update(tarjetaCredito);
                    await _context.SaveChangesAsync();
                    result.Data = tarjetaCredito;
                }
                result.Code = existTarjetaCredito ? "200" : "204";
                result.Message = existTarjetaCredito ? "Ok" : $"No existe registro con id: '{tarjetaCredito.TarjetacredId}'";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "PutTarjetaCredito", ex.Message);
            }
            return result;
        }
        public async Task<Respuesta> DeleteTarjetaCredito(TarjetaCredito tarjetaCredito)
        {
            Respuesta result = new Respuesta();
            try
            {
                bool existTarjetaCredito = await _context.TarjetaCreditos
                                                    .AnyAsync(tarjetaCreditoDB => tarjetaCreditoDB.TarjetacredId
                                                                                            == tarjetaCredito.TarjetacredId);

                if (existTarjetaCredito)
                {
                    tarjetaCredito.FechaHoraAct = DateTime.Now;
                    tarjetaCredito.EstadoId = 2;

                    _context.TarjetaCreditos.Update(tarjetaCredito);
                    await _context.SaveChangesAsync();
                    result.Data = tarjetaCredito;
                }
                result.Code = existTarjetaCredito ? "200" : "204";
                result.Message = existTarjetaCredito ? "Ok" : $"No existe registro con id: '{tarjetaCredito.TarjetacredId}'";

            }
            catch (Exception ex)
            {

                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "DeleteTarjetaCredito", ex.Message);
            }
            return result;
        }
    }
}



