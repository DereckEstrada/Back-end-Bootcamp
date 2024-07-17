using ApiVentas.Interfaces;
using ApiVentas.Models;
using ApiVentas.Utilitarios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace ApiVentas.Services
{
    public class CiudadService : ICiudad
    {
        private readonly BaseErpContext _context;
        private readonly ControlError Log = new ControlError();

        public CiudadService(BaseErpContext context)
        {
            this._context = context;
        }

        public async Task<Respuesta> GetCiudad()
        {
            var respuesta = new Respuesta();

            try
            {
                respuesta.Cod = "000";
                respuesta.Data = await _context.Ciudads.ToListAsync();
                respuesta.Mensaje = "OK";
            }
            catch (Exception ex)
            {
                respuesta.Cod = "999";
                respuesta.Mensaje = $"Se presentó una novedad, comunicarse con el departamento de sistemas";
                Log.LogErrorMetodos("CiudadService", "GetCiudad", ex.Message);
            }

            return respuesta;
        }

        public async Task<Respuesta> PostCiudad(Ciudad ciudad)
        {
            var respuesta = new Respuesta();
            try
            {
                var query = await _context.Ciudads.OrderByDescending(x => x.CiudadId).Select(x => x.CiudadId).FirstOrDefaultAsync();
                ciudad.CiudadId = query == 0 ? 1 : query + 1;
                ciudad.FechaHoraReg = DateTime.Now;

                _context.Ciudads.Add(ciudad);
                await _context.SaveChangesAsync();

                respuesta.Cod = "000";
                respuesta.Mensaje = "Se insertó correctamente";
            }
            catch (Exception ex)
            {
                respuesta.Cod = "999";
                respuesta.Mensaje = $"Se presentó una novedad, comunicarse con el departamento de sistemas";
                Log.LogErrorMetodos("CiudadService", "PostCiudad", ex.Message);
            }
            return respuesta;
        }

        public async Task<Respuesta> PutCiudad(Ciudad ciudad)
        {
            var respuesta = new Respuesta();
            try
            {
                bool existingCiudad = await _context.Ciudads.AnyAsync(x => x.CiudadId == ciudad.CiudadId);
                if (existingCiudad)
                {
                    ciudad.FechaHoraAct = DateTime.Now;

                    _context.Ciudads.Update(ciudad);
                    await _context.SaveChangesAsync();

                    respuesta.Cod = "000";
                    respuesta.Mensaje = "Se actualizó correctamente";
                }
                else
                {
                    respuesta.Cod = "999";
                    respuesta.Mensaje = "La ciudad no existe";
                }
            }
            catch (Exception ex)
            {
                respuesta.Cod = "999";
                respuesta.Mensaje = $"Se presentó una novedad, comunicarse con el departamento de sistemas";
                Log.LogErrorMetodos("CiudadService", "PutCiudad", ex.Message);
            }
            return respuesta;
        }

        public async Task<Respuesta> DeleteCiudad(Ciudad ciudad)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                bool existingCiudad = await _context.Ciudads.AnyAsync(x => x.CiudadId == ciudad.CiudadId);
                if (existingCiudad)
                {
                    ciudad.FechaHoraAct = DateTime.Now;

                    ciudad.Estado = 0;
                    _context.Ciudads.Update(ciudad);
                    await _context.SaveChangesAsync();

                    respuesta.Cod = "000";
                    respuesta.Mensaje = "Se ha eliminado correctamente";
                }
                else
                {
                    respuesta.Cod = "999";
                    respuesta.Mensaje = "La ciudad no existe";
                }
            }
            catch (Exception ex)
            {
                respuesta.Cod = "999";
                respuesta.Mensaje = "Se presentó una novedad, comunicarse con el departamento de sistemas";
                Log.LogErrorMetodos("CiudadService", "DeleteCiudad", ex.Message);
            }
            return respuesta;
        }
    }
}
