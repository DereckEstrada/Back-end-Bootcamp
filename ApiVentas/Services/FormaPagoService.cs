using ApiVentas.Interfaces;
using ApiVentas.Models;
using ApiVentas.Utilitarios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace ApiVentas.Services
{
    public class FormaPagoService : IFormaPago
    {
        private readonly BaseErpContext _context;
        private readonly ControlError Log = new ControlError();

        public FormaPagoService(BaseErpContext context)
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
                Log.LogErrorMetodos("FormaPagoService", "GetFormaPago", ex.Message);
            }

            return respuesta;
        }

        public async Task<Respuesta> PostFormaPago(FormaPago formaPago)
        {
            var respuesta = new Respuesta();
            try
            {
                var query = await _context.FormaPagos.OrderByDescending(x => x.FpagoId).Select(x => x.FpagoId).FirstOrDefaultAsync();
                formaPago.FpagoId = query == 0 ? 1 : query + 1;
                formaPago.FechaHoraReg = DateTime.Now;

                _context.FormaPagos.Add(formaPago);
                await _context.SaveChangesAsync();

                respuesta.Cod = "000";
                respuesta.Mensaje = "Se insertó correctamente";
            }
            catch (Exception ex)
            {
                respuesta.Cod = "999";
                respuesta.Mensaje = $"Se presentó una novedad, comunicarse con el departamento de sistemas";
                Log.LogErrorMetodos("FormaPagoService", "PostFormaPago", ex.Message);
            }
            return respuesta;
        }

        public async Task<Respuesta> PutFormaPago(FormaPago formaPago)
        {
            var respuesta = new Respuesta();
            try
            {
                bool existingFormaPago = await _context.FormaPagos.AnyAsync(x => x.FpagoId == formaPago.FpagoId);
                if (existingFormaPago)
                {
                    var fechaActualString = DateTime.Now.ToString("dd-MM-yyyy");
                    DateOnly fechaActualDateOnly = DateOnly.ParseExact(fechaActualString, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    formaPago.FechaHoraAct = DateTime.Now;

                    _context.FormaPagos.Update(formaPago);
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
                Log.LogErrorMetodos("FormaPagoService", "PutFormaPago", ex.Message);
            }
            return respuesta;
        }

        public async Task<Respuesta> DeleteFormaPago(FormaPago formaPago)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                bool existingFormaPago = await _context.FormaPagos.AnyAsync(x => x.FpagoId == formaPago.FpagoId);
                if (existingFormaPago)
                {
                    var fechaActualString = DateTime.Now.ToString("dd-MM-yyyy");
                    DateOnly fechaActualDateOnly = DateOnly.ParseExact(fechaActualString, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    formaPago.FechaHoraAct = DateTime.Now;

                    formaPago.Estado = 0;
                    _context.FormaPagos.Update(formaPago);
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
                Log.LogErrorMetodos("FormaPagoService", "DeleteFormaPago", ex.Message);
            }
            return respuesta;
        }
    }
}
