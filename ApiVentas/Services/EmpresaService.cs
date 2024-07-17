using ApiVentas.Interfaces;
using ApiVentas.Models;
using ApiVentas.Utilitarios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace ApiVentas.Services
{
    public class EmpresaService : IEmpresa
    {
        private readonly BaseErpContext _context;
        private readonly ControlError Log = new ControlError();

        public EmpresaService(BaseErpContext context)
        {
            this._context = context;
        }

        public async Task<Respuesta> GetEmpresa()
        {
            var respuesta = new Respuesta();

            try
            {
                respuesta.Cod = "000";
                respuesta.Data = await _context.Empresas.ToListAsync();
                respuesta.Mensaje = "OK";
            }
            catch (Exception ex)
            {
                respuesta.Cod = "999";
                respuesta.Mensaje = $"Se presentó una novedad, comunicarse con el departamento de sistemas";
                Log.LogErrorMetodos("EmpresaService", "GetEmpresa", ex.Message);
            }

            return respuesta;
        }

        public async Task<Respuesta> PostEmpresa(Empresa empresa)
        {
            var respuesta = new Respuesta();
            try
            {
                var query = await _context.Empresas.OrderByDescending(x => x.EmpresaId).Select(x => x.EmpresaId).FirstOrDefaultAsync();
                empresa.EmpresaId = query == 0 ? 1 : query + 1;
                empresa.FechaHoraReg = DateTime.Now;

                _context.Empresas.Add(empresa);
                await _context.SaveChangesAsync();

                respuesta.Cod = "000";
                respuesta.Mensaje = "Se insertó correctamente";
            }
            catch (Exception ex)
            {
                respuesta.Cod = "999";
                respuesta.Mensaje = $"Se presentó una novedad, comunicarse con el departamento de sistemas";
                Log.LogErrorMetodos("EmpresaService", "PostEmpresa", ex.Message);
            }
            return respuesta;
        }

        public async Task<Respuesta> PutEmpresa(Empresa empresa)
        {
            var respuesta = new Respuesta();
            try
            {
                bool existingEmpresa = await _context.Empresas.AnyAsync(x => x.EmpresaId == empresa.EmpresaId);
                if (existingEmpresa)
                {
                    empresa.FechaHoraAct = DateTime.Now;

                    _context.Empresas.Update(empresa);
                    await _context.SaveChangesAsync();

                    respuesta.Cod = "000";
                    respuesta.Mensaje = "Se actualizó correctamente";
                }
                else
                {
                    respuesta.Cod = "999";
                    respuesta.Mensaje = "La empresa no existe";
                }
            }
            catch (Exception ex)
            {
                respuesta.Cod = "999";
                respuesta.Mensaje = $"Se presentó una novedad, comunicarse con el departamento de sistemas";
                Log.LogErrorMetodos("EmpresaService", "PutEmpresa", ex.Message);
            }
            return respuesta;
        }

        public async Task<Respuesta> DeleteEmpresa(Empresa empresa)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                bool existingEmpresa = await _context.Empresas.AnyAsync(x => x.EmpresaId == empresa.EmpresaId);
                if (existingEmpresa)
                {
                    empresa.FechaHoraAct = DateTime.Now;

                    empresa.Estado = 0;
                    _context.Empresas.Update(empresa);
                    await _context.SaveChangesAsync();

                    respuesta.Cod = "000";
                    respuesta.Mensaje = "Se ha eliminado correctamente";
                }
                else
                {
                    respuesta.Cod = "999";
                    respuesta.Mensaje = "La empresa no existe";
                }
            }
            catch (Exception ex)
            {
                respuesta.Cod = "999";
                respuesta.Mensaje = "Se presentó una novedad, comunicarse con el departamento de sistemas";
                Log.LogErrorMetodos("EmpresaService", "DeleteEmpresa", ex.Message);
            }
            return respuesta;
        }
    }
}
