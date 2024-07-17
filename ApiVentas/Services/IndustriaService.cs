using ApiVentas.Interfaces;
using ApiVentas.Models;
using ApiVentas.Utilitarios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace ApiVentas.Services
{
    public class IndustriaService : IIndustria
    {
        private readonly BaseErpContext _context;
        private readonly ControlError Log = new ControlError();

        public IndustriaService(BaseErpContext context)
        {
            this._context = context;
        }

        public async Task<Respuesta> GetIndustria()
        {
            var respuesta = new Respuesta();

            try
            {
                respuesta.Cod = "000";
                respuesta.Data = await _context.Industria.ToListAsync();
                respuesta.Mensaje = "OK";
            }
            catch (Exception ex)
            {
                respuesta.Cod = "999";
                respuesta.Mensaje = $"Se presentó una novedad, comunicarse con el departamento de sistemas";
                Log.LogErrorMetodos("IndustriaService", "GetIndustria", ex.Message);
            }

            return respuesta;
        }

        public async Task<Respuesta> PostIndustria(Industrium industria)
        {
            var respuesta = new Respuesta();
            try
            {
                var query = await _context.Industria.OrderByDescending(x => x.IndustriaId).Select(x => x.IndustriaId).FirstOrDefaultAsync();
                industria.IndustriaId = query == 0 ? 1 : query + 1;
                industria.FechaHoraReg = DateTime.Now;

                _context.Industria.Add(industria);
                await _context.SaveChangesAsync();

                respuesta.Cod = "000";
                respuesta.Mensaje = "Se insertó correctamente";
            }
            catch (Exception ex)
            {
                respuesta.Cod = "999";
                respuesta.Mensaje = $"Se presentó una novedad, comunicarse con el departamento de sistemas";
                Log.LogErrorMetodos("IndustriaService", "PostIndustria", ex.Message);
            }
            return respuesta;
        }

        public async Task<Respuesta> PutIndustria(Industrium industria)
        {
            var respuesta = new Respuesta();
            try
            {
                bool existingIndustria = await _context.Industria.AnyAsync(x => x.IndustriaId == industria.IndustriaId);
                if (existingIndustria)
                {
                    industria.FechaHoraAct = DateTime.Now;

                    _context.Industria.Update(industria);
                    await _context.SaveChangesAsync();

                    respuesta.Cod = "000";
                    respuesta.Mensaje = "Se actualizó correctamente";
                }
                else
                {
                    respuesta.Cod = "999";
                    respuesta.Mensaje = "La industria no existe";
                }
            }
            catch (Exception ex)
            {
                respuesta.Cod = "999";
                respuesta.Mensaje = $"Se presentó una novedad, comunicarse con el departamento de sistemas";
                Log.LogErrorMetodos("IndustriaService", "PutIndustria", ex.Message);
            }
            return respuesta;
        }

        public async Task<Respuesta> DeleteIndustria(Industrium industria)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                bool existingIndustria = await _context.Industria.AnyAsync(x => x.IndustriaId == industria.IndustriaId);
                if (existingIndustria)
                {
                    industria.FechaHoraAct = DateTime.Now;

                    industria.Estado = 0;
                    _context.Industria.Update(industria);
                    await _context.SaveChangesAsync();

                    respuesta.Cod = "000";
                    respuesta.Mensaje = "Se ha eliminado correctamente";
                }
                else
                {
                    respuesta.Cod = "999";
                    respuesta.Mensaje = "La industria no existe";
                }
            }
            catch (Exception ex)
            {
                respuesta.Cod = "999";
                respuesta.Mensaje = "Se presentó una novedad, comunicarse con el departamento de sistemas";
                Log.LogErrorMetodos("IndustriaService", "DeleteIndustria", ex.Message);
            }
            return respuesta;
        }
    }
}
