using ApiVentas.Interfaces;
using ApiVentas.Models;
using ApiVentas.Utilitarios;
using Microsoft.EntityFrameworkCore;

namespace ApiVentas.Services
{
    public class BodegaServices : IBodega
    {
        private readonly BaseErpContext _context;
        private ControlError Log = new ControlError();

        public BodegaServices(BaseErpContext context)
        {
            this._context = context;
        }

        public async Task<Respuesta> GetBodega()
        {
            var respuesta = new Respuesta();

            try
            {
                respuesta.Cod = "000";
                respuesta.Data = await _context.Bodegas.ToListAsync();
                respuesta.Mensaje = "OK";
            }
            catch (Exception ex)
            {
                respuesta.Cod = "999";
                respuesta.Mensaje = $"Se presentó una novedad, comunicarse con el departamento de sistemas";
                Log.LogErrorMetodos("BodegaServices", "GetBodega", ex.Message);
            }

            return respuesta;
        }

        public async Task<Respuesta> PostBodega(Bodega bodega)
        {
            var respuesta = new Respuesta();
            try
            {
                var query = _context.Bodegas.OrderByDescending(x => x.BodegaId).Select(x => x.BodegaId).FirstOrDefault();
                bodega.BodegaId = Convert.ToInt32(query) + 1;

                _context.Bodegas.Add(bodega);
                await _context.SaveChangesAsync();

                respuesta.Cod = "000";
                respuesta.Mensaje = "Se insertó correctamente";
            }
            catch (Exception ex)
            {
                respuesta.Cod = "999";
                respuesta.Mensaje = $"Se presentó una novedad, comunicarse con el departamento de sistemas";
                Log.LogErrorMetodos("BodegaServices", "PostBodega", ex.Message);
            }
            return respuesta;
        }

        public async Task<Respuesta> PutBodega(Bodega bodega)
        {
            var respuesta = new Respuesta();
            try
            {
                var existingBodega = await _context.Bodegas.FindAsync(bodega.BodegaId);
                if (existingBodega != null)
                {
                    _context.Entry(existingBodega).CurrentValues.SetValues(bodega);
                    _context.Entry(existingBodega).State = EntityState.Modified;

                    await _context.SaveChangesAsync();

                    respuesta.Cod = "000";
                    respuesta.Mensaje = "Se actualizó correctamente";
                }
                else
                {
                    respuesta.Cod = "999";
                    respuesta.Mensaje = "La bodega no existe";
                }
            }
            catch (Exception ex)
            {
                respuesta.Cod = "999";
                respuesta.Mensaje = $"Se presentó una novedad, comunicarse con el departamento de sistemas";
                Log.LogErrorMetodos("BodegaServices", "PutBodega", ex.Message);
            }
            return respuesta;
        }

        public async Task<Respuesta> DeleteBodega(Bodega bodega)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                var existingBodega = await _context.Bodegas.FindAsync(bodega.BodegaId);

                if (existingBodega != null)
                {
                    _context.Entry(existingBodega).CurrentValues.SetValues(bodega);
                    _context.Entry(existingBodega).State = EntityState.Modified;

                    bodega.Estado = 0;
                    await _context.SaveChangesAsync();

                    respuesta.Cod = "000";
                    respuesta.Mensaje = "Se ha eliminado correctamente";
                }
                else
                {
                    respuesta.Cod = "999";
                    respuesta.Mensaje = "La bodega no existe";
                }
            }
            catch (Exception ex)
            {
                respuesta.Cod = "999";
                respuesta.Mensaje = "Se presentó una novedad, comunicarse con el departamento de sistemas";
                Log.LogErrorMetodos("BodegaServices", "DeleteBodega", ex.Message);
            }
            return respuesta;
        }
    }
}
