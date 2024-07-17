using ApiVentas.Interfaces;
using ApiVentas.Models;
using ApiVentas.Utilitarios;
using Microsoft.EntityFrameworkCore;

namespace ApiVentas.Services
{
    public class CategoriaServices : ICategoria
    {
        private readonly BaseErpContext _context;
        private ControlError Log = new ControlError();

        public CategoriaServices(BaseErpContext context)
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
                Log.LogErrorMetodos("CategoriaServices", "GetCategoria", ex.Message);
            }

            return respuesta;
        }

        public async Task<Respuesta> PostCategoria(Categorium categoria)
        {
            var respuesta = new Respuesta();
            try
            {
                var query = _context.Categoria.OrderByDescending(x => x.CategoriaId).Select(x => x.CategoriaId).FirstOrDefault();
                categoria.CategoriaId = Convert.ToInt32(query) + 1;

                _context.Categoria.Add(categoria);
                await _context.SaveChangesAsync();

                respuesta.Cod = "000";
                respuesta.Mensaje = "Se insertó correctamente";
            }
            catch (Exception ex)
            {
                respuesta.Cod = "999";
                respuesta.Mensaje = $"Se presentó una novedad, comunicarse con el departamento de sistemas";
                Log.LogErrorMetodos("CategoriaServices", "PostCategoria", ex.Message);
            }
            return respuesta;
        }

        public async Task<Respuesta> PutCategoria(Categorium categoria)
        {
            var respuesta = new Respuesta();
            try
            {
                var existingCategoria = await _context.Categoria.FindAsync(categoria.CategoriaId);
                if (existingCategoria != null)
                {
                    _context.Entry(existingCategoria).CurrentValues.SetValues(categoria);
                    _context.Entry(existingCategoria).State = EntityState.Modified;

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
                Log.LogErrorMetodos("CategoriaServices", "PutCategoria", ex.Message);
            }
            return respuesta;
        }

        public async Task<Respuesta> DeleteCategoria(Categorium categoria)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                var existingCategoria = await _context.Categoria.FindAsync(categoria.CategoriaId);

                if (existingCategoria != null)
                {
                    _context.Entry(existingCategoria).CurrentValues.SetValues(categoria);
                    _context.Entry(existingCategoria).State = EntityState.Modified;

                    categoria.Estado = 0;
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
                Log.LogErrorMetodos("CategoriaServices", "DeleteCategoria", ex.Message);
            }
            return respuesta;
        }
    }
}
