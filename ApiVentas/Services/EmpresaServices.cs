using ApiVentas.DTOs;
using ApiVentas.Interfaces;
using ApiVentas.Models;
using ApiVentas.Utilitarios;
using ApiVentas.Utilitarios.Dictionaries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ApiVentas.Services
{
    public class EmpresaServices : IEmpresaServices, IServices<Empresa>
    {
        private readonly BaseErpContext _context;
        private ControlError log = new ControlError();
        private DynamicEmpty dynamicEmpty= new DynamicEmpty();  
        public EmpresaServices(BaseErpContext context)
        {
            this._context = context;
        }
        public async Task<Respuesta> GetEmpresa(DataQuery dataQuery)
        {
            var result = new Respuesta();
            try
            {
                result.Data= await _context.Empresas
                                            .Include(empresa=>empresa.Ciudad)
                                            .Include(empresa=>empresa.Estado)
                                            .Include(empresa=>empresa.UsuIdRegNavigation)
                                            .Where(EmpresaDictionary.GetExpression(dataQuery))
                                            .Select(empresa=>new EmpresaDTO
                                            {
                                                EmpresaId = empresa.EmpresaId,
                                                EmpresaRuc = empresa.EmpresaRuc,
                                                EmpresaNombre = empresa.EmpresaNombre,
                                                EmpresaRazon = empresa.EmpresaRazon,
                                                EmpresaDireccionMatriz = empresa.EmpresaDireccionMatriz,
                                                EmpresaTelefonoMatriz = empresa.EmpresaTelefonoMatriz,
                                                CiudadId = empresa.CiudadId,
                                                CiudadDescripcion = empresa.Ciudad.CiudadNombre,
                                                EstadoId = empresa.EstadoId,
                                                EstadoDescripcion = empresa.Estado.EstadoDescrip,
                                                FechaHoraReg = empresa.FechaHoraReg,
                                                UsuIdReg = empresa.UsuIdReg,
                                                UsuRegName = empresa.UsuIdRegNavigation.UsuNombre,
                                            }).ToListAsync();   

                result.Code = dynamicEmpty.IsEmpty(result.Data) ? "204" : "200";
                result.Message = dynamicEmpty.IsEmpty(result.Data) ? $"No se encontro registro con opcion:                              '{dataQuery.OpcionData}' con data: '{dataQuery.DataFirstQuery}'" : "Ok";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "GetEmpresa", ex.Message);
            }

            return result;
        }

        public async Task<Respuesta> PostEmpresa(Empresa empresa)
        {
            var result = new Respuesta();
            try
            {
                var query = await _context.Empresas.OrderByDescending(x => x.EmpresaId)
                                                      .Select(x => x.EmpresaId).FirstOrDefaultAsync()+1;
                empresa.EmpresaId = query;
                empresa.FechaHoraReg = DateTime.Now;

                _context.Empresas.Add(empresa);
                await _context.SaveChangesAsync();

                result.Code = "200";
                result.Data = empresa;
                result.Message = "Ok";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "PostEmpresa", ex.Message);
            }
            return result;
        }

        public async Task<Respuesta> PutEmpresa(Empresa empresa)
        {
            var result = new Respuesta();
            try
            {
                bool existEmpresa = await _context.Empresas.AnyAsync(x => x.EmpresaId == empresa.EmpresaId);
                if (existEmpresa)
                {
                    empresa.FechaHoraAct = DateTime.Now;

                    _context.Empresas.Update(empresa);
                    await _context.SaveChangesAsync();
                    result.Data = empresa;
                }
                result.Code = existEmpresa ? "200" : "204";
                result.Message = existEmpresa ? "Ok" : $"No existe registro con id: '{empresa.EmpresaId}'";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "PutEmpresa", ex.Message);
            }
            return result;
        }
        public async Task<Respuesta> DeleteEmpresa(Empresa empresa)
        {
            Respuesta result = new Respuesta();
            try
            {
                bool existEmpresa = await _context.Empresas.AnyAsync(x => x.EmpresaId == empresa.EmpresaId);

                if (existEmpresa)
                {
                    empresa.FechaHoraAct = DateTime.Now;
                    empresa.EstadoId = 2;

                    _context.Empresas.Update(empresa);
                    await _context.SaveChangesAsync();
                }
                result.Code = existEmpresa ? "200" : "204";
                result.Message = existEmpresa ? "Ok" : $"No existe registro con id: '{empresa.EmpresaId}'";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "DeleteEmpresa", ex.Message);
            }
            return result;
        }
    }
}
