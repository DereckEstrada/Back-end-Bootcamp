using ApiVentas.Interfaces;
using ApiVentas.Models;
using ApiVentas.Utilitarios;
using Microsoft.EntityFrameworkCore;

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
                cliente.ClienteId = Convert.ToInt32(query) + 1;

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
                var existingCliente = await _context.Clientes.FindAsync(cliente.ClienteId);
                if (existingCliente != null)
                {
                    _context.Entry(existingCliente).CurrentValues.SetValues(cliente);
                    _context.Entry(existingCliente).State = EntityState.Modified;

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
                var existingCliente = await _context.Clientes.FindAsync(cliente.ClienteId);

                if (existingCliente != null)
                {
                    _context.Entry(existingCliente).CurrentValues.SetValues(cliente);
                    _context.Entry(existingCliente).State = EntityState.Modified;

                    cliente.Estado = 0;
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
