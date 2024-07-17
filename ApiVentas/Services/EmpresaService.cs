using ApiVentas.Interfaces;
using ApiVentas.Models;
using ApiVentas.Utilitarios;
using Microsoft.EntityFrameworkCore;

namespace ApiVentas.Services
{
    public class EmpresaServices : IEmpresa
    {
        private readonly BaseErpContext _context;
        private ControlError Log = new ControlError();

        public EmpresaServices(BaseErpContext context)
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
                Log.LogErrorMetodos("EmpresaServices", "GetEmpresa", ex.Message);
            }

            return respuesta;
        }

        public async Task<Respuesta> PostEmpresa(Empresa empresa)
        {
            var respuesta = new Respuesta();
            try
            {
                var query = _context.Empresas.OrderByDescending(x => x.EmpresaId).Select(x => x.EmpresaId).FirstOrDefault();
                empresa.EmpresaId = Convert.ToInt32(query) + 1;

                _context.Empresas.Add(empresa);
                await _context.SaveChangesAsync();

                respuesta.Cod = "000";
                respuesta.Mensaje = "Se insertó correctamente";
            }
            catch (Exception ex)
            {
                respuesta.Cod = "999";
                respuesta.Mensaje = $"Se presentó una novedad, comunicarse con el departamento de sistemas";
                Log.LogErrorMetodos("EmpresaServices", "PostEmpresa", ex.Message);
            }
            return respuesta;
        }

        public async Task<Respuesta> PutEmpresa(Empresa empresa)
        {
            var respuesta = new Respuesta();
            try
            {
                var existingEmpresa = await _context.Empresas.FindAsync(empresa.EmpresaId);
                if (existingEmpresa != null)
                {
                    _context.Entry(existingEmpresa).CurrentValues.SetValues(empresa);
                    _context.Entry(existingEmpresa).State = EntityState.Modified;

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
                Log.LogErrorMetodos("EmpresaServices", "PutEmpresa", ex.Message);
            }
            return respuesta;
        }

        public async Task<Respuesta> DeleteEmpresa(Empresa empresa)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                var existingEmpresa = await _context.Empresas.FindAsync(empresa.EmpresaId);

                if (existingEmpresa != null)
                {
                    _context.Entry(existingEmpresa).CurrentValues.SetValues(empresa);
                    _context.Entry(existingEmpresa).State = EntityState.Modified;

                    empresa.Estado = 0;
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
                Log.LogErrorMetodos("EmpresaServices", "DeleteEmpresa", ex.Message);
            }
            return respuesta;
        }
    }
}
