using ApiVentas.Interfaces;
using ApiVentas.Models;
using ApiVentas.Utilitarios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ApiVentas.Services
{
    public class FormaPagoServices : IFormaPago
    {
        private readonly BaseErpContext _context;
        private ControlError Log = new ControlError();

        public FormaPagoServices(BaseErpContext context)
        {
            this._context = context;
        }

        public async Task<Respuesta> GetFormaPago()
        {
            var respuesta = new Respuesta();

            try
            {
                respuesta.Cod = "000";
                respuesta.Data = await _context.FormaPagos.ToListAsync();
                respuesta.Mensaje = "OK";
            }
            catch (Exception ex)
            {
                respuesta.Cod = "999";
                respuesta.Mensaje = $"Se presentó una novedad, comunicarse con el departamento de sistemas";
                Log.LogErrorMetodos("FormaPagoServices", "GetFormaPago", ex.Message);
            }

            return respuesta;
        }

        public async Task<Respuesta> PostFormaPago(FormaPago formaPago)
        {
            var respuesta = new Respuesta();
            try
            {
                var query = _context.FormaPagos.OrderByDescending(x => x.FpagoId).Select(x => x.FpagoId).FirstOrDefault();
                formaPago.FpagoId = Convert.ToInt32(query) + 1;

                _context.FormaPagos.Add(formaPago);
                await _context.SaveChangesAsync();

                respuesta.Cod = "000";
                respuesta.Mensaje = "Se insertó correctamente";
            }
            catch (Exception ex)
            {
                respuesta.Cod = "999";
                respuesta.Mensaje = $"Se presentó una novedad, comunicarse con el departamento de sistemas";
                Log.LogErrorMetodos("FormaPagoServices", "PostFormaPago", ex.Message);
            }
            return respuesta;
        }

        public async Task<Respuesta> PutFormaPago(FormaPago formaPago)
        {
            var respuesta = new Respuesta();
            try
            {
                var existingFormaPago = await _context.FormaPagos.FindAsync(formaPago.FpagoId);
                if (existingFormaPago != null)
                {
                    _context.Entry(existingFormaPago).CurrentValues.SetValues(formaPago);
                    _context.Entry(existingFormaPago).State = EntityState.Modified;

                    await _context.SaveChangesAsync();

                    respuesta.Cod = "000";
                    respuesta.Mensaje = "Se actualizó correctamente";
                }
                else
                {
                    respuesta.Cod = "999";
                    respuesta.Mensaje = "La forma de pago no existe";
                }
            }
            catch (Exception ex)
            {
                respuesta.Cod = "999";
                respuesta.Mensaje = $"Se presentó una novedad, comunicarse con el departamento de sistemas";
                Log.LogErrorMetodos("FormaPagoServices", "PutFormaPago", ex.Message);
            }
            return respuesta;
        }

        public async Task<Respuesta> DeleteFormaPago(FormaPago formaPago)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                var existingFormaPago = await _context.FormaPagos.FindAsync(formaPago.FpagoId);

                if (existingFormaPago != null)
                {
                    _context.Entry(existingFormaPago).CurrentValues.SetValues(formaPago);
                    _context.Entry(existingFormaPago).State = EntityState.Modified;

                    formaPago.Estado = 0;
                    await _context.SaveChangesAsync();

                    respuesta.Cod = "000";
                    respuesta.Mensaje = "Se ha eliminado correctamente";
                }
                else
                {
                    respuesta.Cod = "999";
                    respuesta.Mensaje = "La forma de pago no existe";
                }
            }
            catch (Exception ex)
            {
                respuesta.Cod = "999";
                respuesta.Mensaje = "Se presentó una novedad, comunicarse con el departamento de sistemas";
                Log.LogErrorMetodos("FormaPagoServices", "DeleteFormaPago", ex.Message);
            }
            return respuesta;
        }
    }
}
