using ApiVentas.Interfaces;
using ApiVentas.Models;
using ApiVentas.Utilitarios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace ApiVentas.Services
{
    public class BodegaService : IBodega
    {
        private readonly BaseErpContext _context;
        private readonly ControlError Log = new ControlError();

        public BodegaService(BaseErpContext context)
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
                Log.LogErrorMetodos("BodegaService", "GetBodega", ex.Message);
            }

            return respuesta;
        }

        public async Task<Respuesta> PostBodega(Bodega bodega)
        {
            var respuesta = new Respuesta();
            try
            {
                var query = await _context.Bodegas.OrderByDescending(x => x.BodegaId).Select(x => x.BodegaId).FirstOrDefaultAsync();
                bodega.BodegaId = query == 0 ? 1 : query + 1;
                bodega.FechaHoraReg = DateTime.Now;

                _context.Bodegas.Add(bodega);
                await _context.SaveChangesAsync();

                respuesta.Cod = "000";
                respuesta.Mensaje = "Se insertó correctamente";
            }
            catch (Exception ex)
            {
                respuesta.Cod = "999";
                respuesta.Mensaje = $"Se presentó una novedad, comunicarse con el departamento de sistemas";
                Log.LogErrorMetodos("BodegaService", "PostBodega", ex.Message);
            }
            return respuesta;
        }

        public async Task<Respuesta> PutBodega(Bodega bodega)
        {
            var respuesta = new Respuesta();
            try
            {
                bool existingBodega = await _context.Bodegas.AnyAsync(x => x.BodegaId == bodega.BodegaId);
                if (existingBodega)
                {
                    bodega.FechaHoraAct = DateTime.Now;

                    _context.Bodegas.Update(bodega);
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
                Log.LogErrorMetodos("BodegaService", "PutBodega", ex.Message);
            }
            return respuesta;
        }

        public async Task<Respuesta> DeleteBodega(Bodega bodega)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                bool existingBodega = await _context.Bodegas.AnyAsync(x => x.BodegaId == bodega.BodegaId);
                if (existingBodega)
                {
                    bodega.FechaHoraAct = DateTime.Now;

                    bodega.Estado = 0;
                    _context.Bodegas.Update(bodega);
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
                Log.LogErrorMetodos("BodegaService", "DeleteBodega", ex.Message);
            }
            return respuesta;
        }
    }
}
