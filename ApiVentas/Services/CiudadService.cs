using ApiVentas.Interfaces;
using ApiVentas.Models;
using ApiVentas.Utilitarios;
using Microsoft.EntityFrameworkCore;

namespace ApiVentas.Services
{
    public class CiudadServices : ICiudad
    {
        private readonly BaseErpContext _context;
        private ControlError Log = new ControlError();

        public CiudadServices(BaseErpContext context)
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
                Log.LogErrorMetodos("CiudadServices", "GetCiudad", ex.Message);
            }

            return respuesta;
        }

        public async Task<Respuesta> PostCiudad(Ciudad ciudad)
        {
            var respuesta = new Respuesta();
            try
            {
                var query = _context.Ciudads.OrderByDescending(x => x.CiudadId).Select(x => x.CiudadId).FirstOrDefault();
                ciudad.CiudadId = Convert.ToInt32(query) + 1;

                _context.Ciudads.Add(ciudad);
                await _context.SaveChangesAsync();

                respuesta.Cod = "000";
                respuesta.Mensaje = "Se insertó correctamente";
            }
            catch (Exception ex)
            {
                respuesta.Cod = "999";
                respuesta.Mensaje = $"Se presentó una novedad, comunicarse con el departamento de sistemas";
                Log.LogErrorMetodos("CiudadServices", "PostCiudad", ex.Message);
            }
            return respuesta;
        }

        public async Task<Respuesta> PutCiudad(Ciudad ciudad)
        {
            var respuesta = new Respuesta();
            try
            {
                var existingCiudad = await _context.Ciudads.FindAsync(ciudad.CiudadId);
                if (existingCiudad != null)
                {
                    _context.Entry(existingCiudad).CurrentValues.SetValues(ciudad);
                    _context.Entry(existingCiudad).State = EntityState.Modified;

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
                Log.LogErrorMetodos("CiudadServices", "PutCiudad", ex.Message);
            }
            return respuesta;
        }

        public async Task<Respuesta> DeleteCiudad(Ciudad ciudad)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                var existingCiudad = await _context.Ciudads.FindAsync(ciudad.CiudadId);

                if (existingCiudad != null)
                {
                    _context.Entry(existingCiudad).CurrentValues.SetValues(ciudad);
                    _context.Entry(existingCiudad).State = EntityState.Modified;

                    ciudad.Estado = 0;
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
                Log.LogErrorMetodos("CiudadServices", "DeleteCiudad", ex.Message);
            }
            return respuesta;
        }
    }
}
