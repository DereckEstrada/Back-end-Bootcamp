using ApiVentas.Interfaces;
using ApiVentas.Models;
using ApiVentas.Utilitarios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace ApiVentas.Services
{
    public class CategoriaService : ICategoria
    {
        private readonly BaseErpContext _context;
        private readonly ControlError Log = new ControlError();

        public CategoriaService(BaseErpContext context)
        {
            this._context = context;
        }

        public async Task<Respuesta> GetCategoria()
        {
            var respuesta = new Respuesta();

            try
            {
                respuesta.Cod = "000";
                respuesta.Data = await _context.Categoria.ToListAsync();
                respuesta.Mensaje = "OK";
            }
            catch (Exception ex)
            {
                respuesta.Cod = "999";
                respuesta.Mensaje = $"Se presentó una novedad, comunicarse con el departamento de sistemas";
                Log.LogErrorMetodos("CategoriaService", "GetCategoria", ex.Message);
            }

            return respuesta;
        }

        public async Task<Respuesta> PostCategoria(Categorium categoria)
        {
            var respuesta = new Respuesta();
            try
            {
                var query = await _context.Categoria.OrderByDescending(x => x.CategoriaId).Select(x => x.CategoriaId).FirstOrDefaultAsync();
                categoria.CategoriaId = query == 0 ? 1 : query + 1;
                categoria.FechaHoraReg = DateTime.Now;

                _context.Categoria.Add(categoria);
                await _context.SaveChangesAsync();

                respuesta.Cod = "000";
                respuesta.Mensaje = "Se insertó correctamente";
            }
            catch (Exception ex)
            {
                respuesta.Cod = "999";
                respuesta.Mensaje = $"Se presentó una novedad, comunicarse con el departamento de sistemas";
                Log.LogErrorMetodos("CategoriaService", "PostCategoria", ex.Message);
            }
            return respuesta;
        }

        public async Task<Respuesta> PutCategoria(Categorium categoria)
        {
            var respuesta = new Respuesta();
            try
            {
                bool existingCategoria = await _context.Categoria.AnyAsync(x => x.CategoriaId == categoria.CategoriaId);
                if (existingCategoria)
                {
                    categoria.FechaHoraAct = DateTime.Now;

                    _context.Categoria.Update(categoria);
                    await _context.SaveChangesAsync();

                    respuesta.Cod = "000";
                    respuesta.Mensaje = "Se actualizó correctamente";
                }
                else
                {
                    respuesta.Cod = "999";
                    respuesta.Mensaje = "La categoría no existe";
                }
            }
            catch (Exception ex)
            {
                respuesta.Cod = "999";
                respuesta.Mensaje = $"Se presentó una novedad, comunicarse con el departamento de sistemas";
                Log.LogErrorMetodos("CategoriaService", "PutCategoria", ex.Message);
            }
            return respuesta;
        }

        public async Task<Respuesta> DeleteCategoria(Categorium categoria)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                bool existingCategoria = await _context.Categoria.AnyAsync(x => x.CategoriaId == categoria.CategoriaId);
                if (existingCategoria)
                {
                    categoria.FechaHoraAct = DateTime.Now;

                    categoria.Estado = 0;
                    _context.Categoria.Update(categoria);
                    await _context.SaveChangesAsync();

                    respuesta.Cod = "000";
                    respuesta.Mensaje = "Se ha eliminado correctamente";
                }
                else
                {
                    respuesta.Cod = "999";
                    respuesta.Mensaje = "La categoría no existe";
                }
            }
            catch (Exception ex)
            {
                respuesta.Cod = "999";
                respuesta.Mensaje = "Se presentó una novedad, comunicarse con el departamento de sistemas";
                Log.LogErrorMetodos("CategoriaService", "DeleteCategoria", ex.Message);
            }
            return respuesta;
        }
    }
}
