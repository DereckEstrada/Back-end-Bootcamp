using ApiVentas.Interfaces;
using ApiVentas.Models;
using ApiVentas.Utilitarios;
using Microsoft.EntityFrameworkCore;
using ApiVentas.DTOs;
using ApiVentas.Utilitarios.Dictionaries;

namespace ejemploEntity.Services
{
    public class MovimientoServices : IMovimientosServices, IServices<MovimientoCab>, IServices<MovimientoDetPago>, IServices<MovimientoDetProducto>
    {
        private readonly BaseErpContext _context;
        private ControlError log = new ControlError();
        private DynamicEmpty dynamicEmpty = new DynamicEmpty();
        public MovimientoServices(BaseErpContext context) { _context = context; }

        public async Task<Respuesta> GetMovimientoCab(DataQuery dataQuery)
        {
            var result = new Respuesta();
            try
            {
                result.Data = await _context.MovimientoCabs
                                            .Include(movimientoCab => movimientoCab.Tipomov)
                                            .Include(movimientoCab => movimientoCab.Empresa)
                                            .Include(movimientoCab => movimientoCab.Sucursal)
                                            .Include(movimientoCab => movimientoCab.Bodega)
                                            .Include(movimientoCab => movimientoCab.Cliente)
                                            .Include(movimientoCab => movimientoCab.Puntovta)
                                            .Include(movimientoCab => movimientoCab.Proveedor)
                                            .Include(movimientoCab => movimientoCab.Estado)
                                            .Include(movimientoCab => movimientoCab.UsuIdRegNavigation)
                                            .Where(MovimientoCabDictionary.GetExpression(dataQuery))
                                            .Select(movimientoCab => new MovimientoCabDTO
                                            {
                                                MovicabId = movimientoCab.MovicabId,
                                                TipomovId = movimientoCab.TipomovId,
                                                TipomovDescripcion = movimientoCab.Tipomov.TipomovDescrip,
                                                TipomovIngEgr = movimientoCab.TipomovIngEgr,
                                                EmpresaId = movimientoCab.EmpresaId,
                                                EmpresaDescripcion = movimientoCab.Empresa.EmpresaNombre,
                                                SucursalId = movimientoCab.SucursalId,
                                                SucursalDescripcion = movimientoCab.Sucursal.SucursalNombre,
                                                BodegaId = movimientoCab.BodegaId,
                                                BodegaDescripcion = movimientoCab.Bodega.BodegaNombre,
                                                SecuenciaFactura = movimientoCab.SecuenciaFactura,
                                                AutorizacionSri = movimientoCab.AutorizacionSri,
                                                ClaveAcceso = movimientoCab.ClaveAcceso,
                                                ClienteId = movimientoCab.ClienteId,
                                                ClienteName = movimientoCab.Cliente.ClienteNombre1
                                                + movimientoCab.Cliente.ClienteNombre2
                                                + movimientoCab.Cliente.ClienteApellido1
                                                + movimientoCab.Cliente.ClienteApellido2,
                                                PuntovtaId = movimientoCab.PuntovtaId,
                                                PuntovtaDescripcion = movimientoCab.Puntovta.PuntovtaNombre,
                                                ProveedorId = movimientoCab.ProveedorId,
                                                ProveedorDescripcion = movimientoCab.Proveedor.ProvRuc,
                                                EstadoId = movimientoCab.EstadoId,
                                                EstadoDescripcion = movimientoCab.Estado.EstadoDescrip,
                                                FechaHoraReg = movimientoCab.FechaHoraReg,
                                                UsuIdReg = movimientoCab.UsuIdReg,
                                                UsuRegName = movimientoCab.UsuIdRegNavigation.UsuNombre,
                                            }).ToListAsync();

                result.Code = dynamicEmpty.IsEmpty(result.Data) ? "204" : "200";
                result.Message = dynamicEmpty.IsEmpty(result.Data) ? $"No se encontro registro con opcion:                              '{dataQuery.OpcionData}' con data: '{dataQuery.DataFirstQuery}'" : "Ok";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "GetMovimientoCab", ex.Message);
            }

            return result;
        }
        public async Task<Respuesta> PostMovimientoCab(MovimientoCab movimientoCab)
        {
            var result = new Respuesta();
            try
            {
                var query = await _context.MovimientoCabs.OrderByDescending(movimientoCabDB => movimientoCabDB.MovicabId)
                                                            .Select(idDB => idDB.MovicabId).FirstOrDefaultAsync() + 1;
                movimientoCab.MovicabId = query;
                movimientoCab.FechaHoraReg = DateTime.Now;

                _context.MovimientoCabs.Add(movimientoCab);
                await _context.SaveChangesAsync();

                result.Code = "200";
                result.Data = movimientoCab;
                result.Message = "Ok";

            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "PostMovimientoCab", ex.Message);
            }

            return result;
        }
        public async Task<Respuesta> PutMovimientoCab(MovimientoCab movimientoCab)
        {
            var result = new Respuesta();
            try
            {
                var existMovimientoCab = await _context.MovimientoCabs.AnyAsync(movimientoCabDB =>
                                                                        movimientoCabDB.MovicabId == movimientoCab.MovicabId);
                if (existMovimientoCab)
                {
                    movimientoCab.FechaHoraReg = DateTime.Now;

                    _context.MovimientoCabs.Update(movimientoCab);
                    await _context.SaveChangesAsync();
                    result.Data = movimientoCab;
                }
                result.Code = existMovimientoCab ? "200" : "204";
                result.Message = existMovimientoCab ? "Ok" : $"No existe registro con id: '{movimientoCab.MovicabId}'";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "PutMovimientoCab", ex.Message);
            }

            return result;
        }
        public async Task<Respuesta> DeleteMovimientoCab(MovimientoCab movimientoCab)
        {
            var result = new Respuesta();
            try
            {
                var existMovimientoCab = await _context.MovimientoCabs.AnyAsync(movimientoCabDB =>
                                                                        movimientoCabDB.MovicabId == movimientoCab.MovicabId);
                if (existMovimientoCab)
                {
                    movimientoCab.FechaHoraReg = DateTime.Now;
                    movimientoCab.EstadoId = 2;

                    _context.MovimientoCabs.Update(movimientoCab);
                    await _context.SaveChangesAsync();
                }
                result.Code = existMovimientoCab ? "200" : "204";
                result.Message = existMovimientoCab ? "Ok" : $"No existe registro con id: '{movimientoCab.MovicabId}'";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "DeleteMovimientoCab", ex.Message);
            }

            return result;
        }


        public async Task<Respuesta> GetMovimientoDetPago(DataQuery dataQuery)
        {
            var result = new Respuesta();
            try
            {
                result.Data = await _context.MovimientoDetPagos
                                            .Include(movimientoDetPagos=> movimientoDetPagos.Movicab)
                                            .Include(movimientoDetPagos=> movimientoDetPagos.Fpago)
                                            .Include(movimientoDetPagos=> movimientoDetPagos.Industria)
                                            .Include(movimientoDetPagos=> movimientoDetPagos.Tarjetacred)
                                            .Include(movimientoDetPagos=> movimientoDetPagos.Cliente)
                                            .Include(movimientoDetPagos=> movimientoDetPagos.Estado)
                                            .Include(movimientoDetPagos => movimientoDetPagos.UsuIdRegNavigation)
                                            .Where(MovimientoDetPagosDictionary.GetExpression(dataQuery))
                                            .Select(movimientoDetPagos => new MovimientoDetPagoDTO
                                            {
                                                MovidetPagoId=movimientoDetPagos.MovidetPagoId,
                                                MovicabId=movimientoDetPagos.MovicabId,
                                                MovicabDescripcion=movimientoDetPagos.Movicab.SecuenciaFactura,
                                                FpagoId=movimientoDetPagos.FpagoId,
                                                FpagoDescripcion=movimientoDetPagos.Fpago.FpagoDescripcion,
                                                ValorPagado=movimientoDetPagos.ValorPagado,
                                                IndustriaId=movimientoDetPagos.IndustriaId,
                                                IndustriaDescripcion=movimientoDetPagos.Industria.IndustriaDescripcion,
                                                Lote=movimientoDetPagos.Lote,
                                                Voucher=movimientoDetPagos.Voucher,
                                                TarjetacredId=movimientoDetPagos.TarjetacredId,
                                                TarjetacredDescripcion=movimientoDetPagos.Tarjetacred.TarjetacredDescripcion,
                                                BancoId=movimientoDetPagos.BancoId,
                                                ComprobanteId=movimientoDetPagos.ComprobanteId,
                                                FechaPago=movimientoDetPagos.FechaPago,
                                                ClienteId=movimientoDetPagos.ClienteId,
                                                ClienteDescripcion=movimientoDetPagos.Cliente.ClienteRuc,
                                                EstadoId=movimientoDetPagos.EstadoId,
                                                EstadoDescripcion=movimientoDetPagos.Estado.EstadoDescrip,
                                                FechaHoraReg=movimientoDetPagos.FechaHoraReg,
                                                UsuIdReg=movimientoDetPagos.UsuIdReg,
                                                UsuRegName= movimientoDetPagos.UsuIdRegNavigation.UsuNombre,
                                            }).ToListAsync();

                result.Code = dynamicEmpty.IsEmpty(result.Data) ? "204" : "200";
                result.Message = dynamicEmpty.IsEmpty(result.Data) ? $"No se encontro registro con opcion:                              '{dataQuery.OpcionData}' con data: '{dataQuery.DataFirstQuery}'" : "Ok";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "GetMovimientoDetPago", ex.Message);
            }

            return result;
        }
        public async Task<Respuesta> PostMovimientoDetPago(MovimientoDetPago movimientoDetPago)
        {
            var result = new Respuesta();
            try
            {
                var query = await _context.MovimientoDetPagos.OrderByDescending(movimientoDetPagoDB => movimientoDetPagoDB.MovidetPagoId)
                                                            .Select(idDB => idDB.MovidetPagoId).FirstOrDefaultAsync() + 1;
                movimientoDetPago.MovidetPagoId = query;
                movimientoDetPago.FechaHoraReg = DateTime.Now;

                _context.MovimientoDetPagos.Add(movimientoDetPago);
                await _context.SaveChangesAsync();

                result.Code = "200";
                result.Data = movimientoDetPago;
                result.Message = "Ok";

            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "PostMovimientoDetPago", ex.Message);
            }

            return result;
        }
        public async Task<Respuesta> PutMovimientoDetPago(MovimientoDetPago movimientoDetPago)
        {
            var result = new Respuesta();
            try
            {
                var existMovimientoDetPago = await _context.MovimientoDetPagos.AnyAsync(movimientoDetPagoDB =>
                                                                        movimientoDetPagoDB.MovidetPagoId
                                                                       == movimientoDetPagoDB.MovidetPagoId);
                if (existMovimientoDetPago)
                {
                    movimientoDetPago.FechaHoraReg = DateTime.Now;

                    _context.MovimientoDetPagos.Update(movimientoDetPago);
                    await _context.SaveChangesAsync();
                    result.Data = movimientoDetPago;
                }
                result.Code = existMovimientoDetPago ? "200" : "204";
                result.Message = existMovimientoDetPago ? "Ok" : $"No existe registro con id: '{movimientoDetPago.MovidetPagoId}'";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "PutMovimientoDetPago", ex.Message);
            }

            return result;
        }
        public async Task<Respuesta> DeleteMovimientoDetPago(MovimientoDetPago movimientoDetPago)
        {
            var result = new Respuesta();
            try
            {
                var existMovimientoDetPago = await _context.MovimientoDetPagos.AnyAsync(movimientoDetPagoDB =>
                                                                        movimientoDetPagoDB.MovidetPagoId
                                                                       == movimientoDetPagoDB.MovidetPagoId);
                if (existMovimientoDetPago)
                {
                    movimientoDetPago.FechaHoraReg = DateTime.Now;
                    movimientoDetPago.EstadoId = 2;

                    _context.MovimientoDetPagos.Update(movimientoDetPago);
                    await _context.SaveChangesAsync();
                }
                result.Code = existMovimientoDetPago ? "200" : "204";
                result.Message = existMovimientoDetPago ? "Ok" : $"No existe registro con id: '{movimientoDetPago.MovidetPagoId}'";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "DeleteMovimientoDetPago", ex.Message);
            }

            return result;
        }


        public async Task<Respuesta> GetMovimientoDetProducto(DataQuery dataQuery)
        {
            var result = new Respuesta();
            try
            {
                result.Data = await _context.MovimientoDetProductos
                                            .Include(movimientoDetProducto=> movimientoDetProducto.Movicab)
                                            .Include(movimientoDetProducto=> movimientoDetProducto.Producto)                
                                            .Include(movimientoDetProducto=> movimientoDetProducto.Estado)
                                            .Include(movimientoDetProducto=> movimientoDetProducto.UsuIdRegNavigation)
                                            .Where(MovimientoDetProductoDictionary.GetExpression(dataQuery))
                                            .Select(movimientoDetProducto => new MovimientoDetProductoDTO
                                            {
                                                MovidetProdId=movimientoDetProducto.MovidetProdId,
                                                MovicabId=movimientoDetProducto.MovicabId,
                                                MovicabDescripcion=movimientoDetProducto.Movicab.SecuenciaFactura,
                                                ProductoId=movimientoDetProducto.ProductoId,
                                                ProductoDescripcion=movimientoDetProducto.Producto.ProdDescripcion,
                                                Cantidad=movimientoDetProducto.Cantidad,
                                                Precio=movimientoDetProducto.Precio,
                                                EstadoId=movimientoDetProducto.EstadoId,
                                                EstadoDescripcion=movimientoDetProducto.Estado.EstadoDescrip,
                                                FechaHoraReg=movimientoDetProducto.FechaHoraReg,
                                                UsuIdReg=movimientoDetProducto.UsuIdReg,
                                                UsuRegName= movimientoDetProducto.UsuIdRegNavigation.UsuNombre,
                                            }).ToListAsync();

                result.Code = dynamicEmpty.IsEmpty(result.Data) ? "204" : "200";
                result.Message = dynamicEmpty.IsEmpty(result.Data) ? $"No se encontro registro con opcion:                              '{dataQuery.OpcionData}' con data: '{dataQuery.DataFirstQuery}'" : "Ok";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "GetMovimientoDetPagos", ex.Message);
            }

            return result;
        }
        public async Task<Respuesta> PostMovimientoDetProducto(MovimientoDetProducto movimientoDetProducto)
        {
            var result = new Respuesta();
            try
            {
                var query = await _context.MovimientoDetProductos.OrderByDescending(movimientoDetProductoDB 
                                                             => movimientoDetProductoDB.MovidetProdId)
                                                            .Select(idDB => idDB.MovidetProdId).FirstOrDefaultAsync() + 1;
                movimientoDetProducto.MovidetProdId= query;
                movimientoDetProducto.FechaHoraReg = DateTime.Now;

                _context.MovimientoDetProductos.Add(movimientoDetProducto);
                await _context.SaveChangesAsync();

                result.Code = "200";
                result.Data = movimientoDetProducto;
                result.Message = "Ok";

            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "PostMovimientoDetProducto", ex.Message);
            }

            return result;
        }
        public async Task<Respuesta> PutMovimientoDetProducto (MovimientoDetProducto movimientoDetProducto)
        {
            var result = new Respuesta();
            try
            {
                var existMovimientoDetProducto = await _context.MovimientoDetProductos.AnyAsync(movimientoDetProductoDB =>
                                                                        movimientoDetProductoDB.MovidetProdId
                                                                       == movimientoDetProducto.MovidetProdId);
                if (existMovimientoDetProducto)
                {
                    movimientoDetProducto.FechaHoraReg = DateTime.Now;

                    _context.MovimientoDetProductos.Update(movimientoDetProducto);
                    await _context.SaveChangesAsync();
                    result.Data = movimientoDetProducto;
                }
                result.Code = existMovimientoDetProducto ? "200" : "204";
                result.Message = existMovimientoDetProducto ? "Ok" : $"No existe registro con id: '{movimientoDetProducto.MovidetProdId}'";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "PutMovimientoDetProducto", ex.Message);
            }

            return result;
        }
        public async Task<Respuesta> DeleteMovimientoDetProducto(MovimientoDetProducto movimientoDetProducto)
        {
            var result = new Respuesta();
            try
            {
                var existMovimientoDetProducto= await _context.MovimientoDetProductos.AnyAsync(movimientoDetProductoDB =>
                                                                        movimientoDetProductoDB.MovidetProdId
                                                                       == movimientoDetProducto.MovidetProdId);
                if (existMovimientoDetProducto)
                {
                    movimientoDetProducto.FechaHoraReg = DateTime.Now;
                    movimientoDetProducto.EstadoId = 2;

                    _context.MovimientoDetProductos.Update(movimientoDetProducto);
                    await _context.SaveChangesAsync();
                }
                result.Code = existMovimientoDetProducto ? "200" : "204";
                result.Message = existMovimientoDetProducto ? "Ok" : $"No existe registro con id: '{movimientoDetProducto.MovidetProdId}'";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "DeleteMovimientoDetProducto", ex.Message);
            }
            return result;
        }
    }
}
