using ApiVentas.DTOs;
using ApiVentas.Interfaces;
using ApiVentas.Models;
using ApiVentas.Utilitarios;
using ApiVentas.Utilitarios.Dictionaries;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;
using System.Threading.Tasks.Dataflow;

namespace ApiVentas.Services
{
    public class ProveedorServices : IProveedorServices, IServices<Proveedor>
    {
        private BaseErpContext _context;
        private ControlError log = new ControlError();
        private DynamicEmpty dynamicEmpty = new DynamicEmpty();
        public ProveedorServices(BaseErpContext context)
        {
            this._context = context;
        }
        public async Task<Respuesta> GetProveedor(DataQuery dataQuery)
        {
            var result = new Respuesta();
            try
            {
                result.Data =await _context.Proveedors
                                     .Include(proveedor => proveedor.Ciudad)
                                     .Include(proveedor => proveedor.Estado)
                                     .Include(proveedor => proveedor.UsuIdRegNavigation)
                                     .Include(proveedor => proveedor.Ciudad)
                                     .Where(ProveedorDictionary.GetExpression(dataQuery))
                                     .Select(proveedor => new ProveedorDTO
                                     {
                                         ProvId = proveedor.ProvId,
                                         ProvRuc = proveedor.ProvRuc,
                                         ProvNomComercial = proveedor.ProvNomComercial,
                                         ProvRazon = proveedor.ProvRazon,
                                         ProvDireccion = proveedor.ProvDireccion,
                                         ProvTelefono = proveedor.ProvTelefono,
                                         CiudadId = proveedor.CiudadId,
                                         CiudadDescripcion = proveedor.Ciudad.CiudadNombre,
                                         EstadoId = proveedor.EstadoId,
                                         EstadoDescripcion = proveedor.Estado.EstadoDescrip,
                                         FechaHoraReg = proveedor.FechaHoraReg,
                                         UsuIdReg = proveedor.UsuIdReg,
                                         UsuRegName = proveedor.UsuIdRegNavigation.UsuNombre,
                                     }).ToListAsync();

                result.Code = dynamicEmpty.IsEmpty(result.Data) ? "204" : "200";
                result.Message = dynamicEmpty.IsEmpty(result.Data) ? $"No se encontro registro con opcion:                              '{dataQuery.OpcionData}' con data: '{dataQuery.DataFirstQuery}'" : "Ok";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "GetProveedor", ex.Message);

            }
            return result;
        }
        public async Task<Respuesta> PostProveedor(Proveedor proveedor)
        {
            var result = new Respuesta();
            try
            {
                var query = await _context.Proveedors.OrderByDescending(proveedorDB => proveedorDB.ProvId)
                                                    .Select(idDB => idDB.ProvId).FirstOrDefaultAsync() + 1;
                proveedor.ProvId = query;
                proveedor.FechaHoraReg = DateTime.Now;

                _context.Proveedors.Add(proveedor);
                await _context.SaveChangesAsync();

                result.Code = "200";
                result.Data = proveedor;
                result.Message = "Ok";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "PostProveedor", ex.Message);

            }
            return result;
        }
        public async Task<Respuesta> PutProveedor(Proveedor proveedor)
        {
            var result = new Respuesta();
            try
            {
                var existProveedor = await _context.Proveedors.AnyAsync(proveedorDB=> proveedorDB.ProvId == proveedor.ProvId);
                if (existProveedor)
                {
                    proveedor.FechaHoraAct = DateTime.Now;

                    _context.Proveedors.Update(proveedor);
                    await _context.SaveChangesAsync();
                    result.Data = proveedor;
                }
                result.Code = existProveedor ? "200" : "204";
                result.Message = existProveedor ? "Ok" : $"No existe registro con id: '{proveedor.ProvId}'";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "PutProveedor", ex.Message);
            }
            return result;
        }
        public async Task<Respuesta> DeleteProveedor(Proveedor proveedor)
        {
            var result = new Respuesta();
            try
            {
                var existProveedor = await _context.Proveedors.AnyAsync(proveedorDB => proveedorDB.ProvId == proveedor.ProvId);
                if (existProveedor)
                {
                    proveedor.FechaHoraAct = DateTime.Now;
                    proveedor.EstadoId = 2;

                    _context.Proveedors.Update(proveedor);
                    await _context.SaveChangesAsync();
                }
                result.Code = existProveedor ? "200" : "204";
                result.Message = existProveedor ? "Ok" : $"No existe registro con id: '{proveedor.ProvId}'";

            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "DeleteProveedor", ex.Message);
            }
            return result;
        }
    }
}
