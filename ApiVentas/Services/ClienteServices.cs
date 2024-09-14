using ApiVentas.Interfaces;
using ApiVentas.Models;
using ApiVentas.Utilitarios;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using ApiVentas.DTOs;
using Azure.Core.Pipeline;
using ApiVentas.Utilitarios.Dictionaries;

namespace ApiVentas.Services
{
    public class ClienteServices : IClienteServices, IServices<Cliente>
    {
        private BaseErpContext _context;
        private ControlError log = new ControlError();
        private DynamicEmpty dynamicEmpty = new DynamicEmpty();
        public ClienteServices(BaseErpContext context)
        {
            this._context = context;
        }

        public async Task<Respuesta> GetCliente(DataQuery dataQuery)
        {
            var result = new Respuesta();
            try
            {
                result.Data = await _context.Clientes
                                            .Include(cliente => cliente.Estado)
                                            .Include(cliente => cliente.UsuIdRegNavigation)
                                            .Where(ClienteDictionary.GetExpression(dataQuery))
                                            .Select(cliente => new ClienteDTO
                                            {
                                                ClienteId = cliente.ClienteId,
                                                ClienteRuc = cliente.ClienteRuc,
                                                ClienteNombre1 = cliente.ClienteNombre1,
                                                ClienteNombre2 = cliente.ClienteNombre2,
                                                ClienteApellido1 = cliente.ClienteApellido1,
                                                ClienteApellido2 = cliente.ClienteApellido2,
                                                ClienteEmail = cliente.ClienteEmail,
                                                ClienteTelefono = cliente.ClienteTelefono,
                                                ClienteDireccion = cliente.ClienteTelefono,
                                                EstadoId = cliente.EstadoId,
                                                EstadoDescripcion = cliente.Estado.EstadoDescrip,
                                                FechaHoraReg = cliente.FechaHoraReg,
                                                UsuIdReg = cliente.UsuIdReg,
                                                UsuRegName = cliente.UsuIdRegNavigation.UsuNombre,
                                            }).ToListAsync();

                result.Code = dynamicEmpty.IsEmpty(result.Data) ? "204" : "200";
                result.Message = dynamicEmpty.IsEmpty(result.Data) ? $"No se encontro registro con opcion:'{dataQuery.OpcionData}' con data: '{dataQuery.DataFirstQuery}'" : "Ok";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "GetCliente", ex.Message);
            }

            return result;
        }


        public async Task<Respuesta> PostCliente(Cliente cliente)
        {
            var result = new Respuesta();
            try
            {
                var query = _context.Clientes.OrderByDescending(clienteDB => clienteDB.ClienteId)
                                                .Select(idDB => idDB.ClienteId).FirstOrDefault() + 1;
                cliente.ClienteId = query;
                cliente.FechaHoraReg = DateTime.Now;

                _context.Clientes.Add(cliente);
                await _context.SaveChangesAsync();

                result.Code = "200";
                result.Data = cliente;
                result.Message = "Ok";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "PostCliente", ex.Message);
            }
            return result;
        }
        public async Task<Respuesta> PutCliente(Cliente cliente)
        {
            var result = new Respuesta();
            try
            {
                bool existCliente = await _context.Clientes.AnyAsync(clienteDB => clienteDB.ClienteId == cliente.ClienteId);
                if (existCliente)
                {
                    cliente.FechaHoraAct = DateTime.Now;

                    _context.Clientes.Update(cliente);
                    await _context.SaveChangesAsync();
                    result.Data = cliente;
                }
                result.Code = existCliente ? "200" : "204";
                result.Message = existCliente ? "Ok" : $"No existe registro con id: '{cliente.ClienteId}'";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "PutCliente", ex.Message);
            }
            return result;
        }

        public async Task<Respuesta> DeleteCliente(Cliente cliente)
        {
            Respuesta result = new Respuesta();
            try
            {
                bool existCliente = await _context.Clientes.AnyAsync(clienteDB => clienteDB.ClienteId == cliente.ClienteId);

                if (existCliente)
                {
                    cliente.FechaHoraAct = DateTime.Now;
                    cliente.EstadoId = 2;

                    _context.Clientes.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                result.Code = existCliente ? "200" : "204";
                result.Message = existCliente ? "Ok" : $"No existe registro con id: '{cliente.ClienteId}'";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "DeleteCliente", ex.Message);
            }
            return result;
        }
    }
}
