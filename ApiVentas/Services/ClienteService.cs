using ApiVentas.Interfaces;
using ApiVentas.Models;
using ApiVentas.Utilitarios;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace ApiVentas.Services
{
    public class ClienteServices : ICliente
    {
        private readonly BaseErpContext _context;
        private ControlError Log = new ControlError();

        public ClienteServices(BaseErpContext context)
        {
            this._context = context;
        }

        public async Task<Respuesta> GetCliente()
        {
            var respuesta = new Respuesta();

            try
            {
                respuesta.Cod = "000";
                respuesta.Data = await _context.Clientes.ToListAsync();
                respuesta.Mensaje = "OK";
            }
            catch (Exception ex)
            {
                respuesta.Cod = "999";
                respuesta.Mensaje = $"Se presentó una novedad, comunicarse con el departamento de sistemas";
                Log.LogErrorMetodos("ClienteServices", "GetCliente", ex.Message);
            }

            return respuesta;
        }

        public async Task<Respuesta> PostCliente(Cliente cliente)
        {
            var respuesta = new Respuesta();
            try
            {
                var query = _context.Clientes.OrderByDescending(x => x.ClienteId).Select(x => x.ClienteId).FirstOrDefault();
                cliente.ClienteId = query == 0 ? 1 : Convert.ToInt32(query) + 1;
                cliente.FechaHoraReg = DateTime.Now;

                _context.Clientes.Add(cliente);
                await _context.SaveChangesAsync();

                respuesta.Cod = "000";
                respuesta.Mensaje = "Se insertó correctamente";
            }
            catch (Exception ex)
            {
                respuesta.Cod = "999";
                respuesta.Mensaje = $"Se presentó una novedad, comunicarse con el departamento de sistemas";
                Log.LogErrorMetodos("ClienteServices", "PostCliente", ex.Message);
            }
            return respuesta;
        }


        public async Task<Respuesta> PutCliente(Cliente cliente)
        {
            var respuesta = new Respuesta();
            try
            {
                bool existingCliente = await _context.Clientes.AnyAsync();
                if (existingCliente)
                {

                    var fechaActualString = DateTime.Now.ToString("dd-MM-yyyy");
                    DateOnly fechaActualDateOnly = DateOnly.ParseExact(fechaActualString, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    cliente.FechaHoraAct = fechaActualDateOnly;

                    _context.Clientes.Update(cliente);
                    await _context.SaveChangesAsync();

                    respuesta.Cod = "000";
                    respuesta.Mensaje = "Se actualizó correctamente";
                }
                else
                {
                    respuesta.Cod = "999";
                    respuesta.Mensaje = "El cliente no existe";
                }
            }
            catch (Exception ex)
            {
                respuesta.Cod = "999";
                respuesta.Mensaje = $"Se presentó una novedad, comunicarse con el departamento de sistemas";
                Log.LogErrorMetodos("ClienteServices", "PutCliente", ex.Message);
            }
            return respuesta;
        }

        public async Task<Respuesta> DeleteCliente(Cliente cliente)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                bool existingCliente = await _context.Clientes.AnyAsync();
                if (existingCliente)
                {

                    var fechaActualString = DateTime.Now.ToString("dd-MM-yyyy");
                    DateOnly fechaActualDateOnly = DateOnly.ParseExact(fechaActualString, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    cliente.FechaHoraAct = fechaActualDateOnly;

                    cliente.Estado = 0;
                    
                    _context.Clientes.Update(cliente);
                    await _context.SaveChangesAsync();

                    respuesta.Cod = "000";
                    respuesta.Mensaje = "Se ha eliminado correctamente";
                }
                else
                {
                    respuesta.Cod = "999";
                    respuesta.Mensaje = "El cliente no existe";
                }
            }
            catch (Exception ex)
            {
                respuesta.Cod = "999";
                respuesta.Mensaje = "Se presentó una novedad, comunicarse con el departamento de sistemas";
                Log.LogErrorMetodos("ClienteServices", "DeleteCliente", ex.Message);
            }
            return respuesta;
        }
    }
}
