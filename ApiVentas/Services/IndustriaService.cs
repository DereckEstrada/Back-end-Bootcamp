using ApiVentas.Interfaces;
using ApiVentas.Models;
using ApiVentas.Utilitarios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ApiVentas.Services
{
    public class IndustriaServices : IIndustria
    {
        private readonly BaseErpContext _context;
        private readonly ControlError Log = new ControlError();

        public IndustriaServices(BaseErpContext context)
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
                respuesta.Mensaje = "Se presentó una novedad, comunicarse con el departamento de sistemas";
                Log.LogErrorMetodos("IndustriaServices", "GetIndustria", ex.Message);
            }

            return respuesta;
        }

        public async Task<Respuesta> PostIndustria(Industrium industria)
        {
            var respuesta = new Respuesta();
            try
            {
                var query = _context.Industria.OrderByDescending(x => x.IndustriaId).Select(x => x.IndustriaId).FirstOrDefault();
                industria.IndustriaId = Convert.ToInt32(query) + 1;

                _context.Industria.Add(industria);
                await _context.SaveChangesAsync();

                respuesta.Cod = "000";
                respuesta.Mensaje = "Se insertó correctamente";
            }
            catch (Exception ex)
            {
                respuesta.Cod = "999";
                respuesta.Mensaje = "Se presentó una novedad, comunicarse con el departamento de sistemas";
                Log.LogErrorMetodos("IndustriaServices", "PostIndustria", ex.Message);
            }
            return respuesta;
        }

        public async Task<Respuesta> PutIndustria(Industrium industria)
        {
            var respuesta = new Respuesta();
            try
            {
                var existingIndustria = await _context.Industria.FindAsync(industria.IndustriaId);
                if (existingIndustria != null)
                {
                    _context.Entry(existingIndustria).CurrentValues.SetValues(industria);
                    _context.Entry(existingIndustria).State = EntityState.Modified;

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
                respuesta.Mensaje = "Se presentó una novedad, comunicarse con el departamento de sistemas";
                Log.LogErrorMetodos("IndustriaServices", "PutIndustria", ex.Message);
            }
            return respuesta;
        }

        public async Task<Respuesta> DeleteIndustria(Industrium industria)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                var existingIndustrium = await _context.Industria.FindAsync(industria.IndustriaId);

                if (existingIndustrium != null)
                {
                    _context.Entry(existingIndustrium).CurrentValues.SetValues(industria);
                    _context.Entry(existingIndustrium).State = EntityState.Modified;

                    industria.Estado = 0;
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
                Log.LogErrorMetodos("IndustriaServices", "DeleteIndustria", ex.Message);
            }
            return respuesta;
        }
    }
}
