using ApiVentas.Interfaces;
using ApiVentas.Models;
using ApiVentas.Utilitarios;
using ApiVentas.Interfaces;
using ApiVentas.Models;
using ApiVentas.Utilitarios;
using Microsoft.EntityFrameworkCore;
using ApiVentas.DTOs;

namespace ejemploEntity.Services
{
    public class PaisServices : IPais
    {
        public readonly BaseErpContext _context;
        public ControlError err = new ControlError();
        public string clase = "PaisServices";

        public PaisServices(BaseErpContext context) { _context = context; }
        public async Task<Respuesta> getListaPais(int PaisId, string? nombrePais)
        {
            var resp = new Respuesta();
            var metodo = "getListaPais";

            var qryPais = _context.Pais;
            var qryUsu = _context.Usuarios;

            try
            {
                IQueryable<PaiDto> q = (from p in qryPais
                                        select new PaiDto
                                        {
                                            PaisId = p.PaisId,
                                            PaisNombre = p.PaisNombre,
                                            Estado = p.EstadoId,
                                            FechaHoraReg = p.FechaHoraReg,
                                            FechaHoraAct = p.FechaHoraAct,
                                            UsuReg = (from u in qryUsu where p.UsuIdReg == u.UsuId select u.UsuNombre).FirstOrDefault().ToString(),
                                            UsuAct = (from u in qryUsu where p.UsuIdAct == u.UsuId select u.UsuNombre).FirstOrDefault().ToString()
                                        }).AsQueryable();

                if (PaisId == 0 && (nombrePais == null || nombrePais == ""))
                {
                    resp.Cod = "200";
                    resp.Data = await q.Where(x => x.Estado == 1).ToListAsync();
                    resp.Mensaje = "OK";
                }
                else if (PaisId > 0 && (nombrePais == null || nombrePais == ""))
                {
                    resp.Cod = "200";
                    resp.Data = await q.Where(x => x.Estado == 1 && x.PaisId.Equals(PaisId)).ToListAsync();
                    resp.Mensaje = "OK";
                }
                else if (PaisId == 0 && (nombrePais != null || nombrePais != ""))
                {
                    resp.Cod = "200";
                    resp.Data = await q.Where(x => x.Estado == 1 && x.PaisNombre.Equals(nombrePais)).ToListAsync();
                    resp.Mensaje = "OK";
                }
                else if (PaisId > 0 && nombrePais != null && nombrePais != "")
                {
                    resp.Cod = "200";
                    resp.Data = await q.Where(x => x.Estado == 1 && x.PaisId.Equals(PaisId) && x.PaisNombre.Equals(nombrePais)).ToListAsync();
                    resp.Mensaje = "OK";
                }
            }
            catch (Exception ex)
            {
                resp.Cod = "400";
                resp.Mensaje = $"Error en {clase}: {ex.Message}";
                err.LogErrorMetodos(clase, metodo, ex.Message);
            }

            return resp;
        }
        public async Task<Respuesta> postPais(Pai Pais)
        {

            var resp = new Respuesta();
            var qry = _context.Pais;
            var metodo = "postPais";

            try
            {
                Pais.PaisId = qry.Max(x => x.PaisId) + 1;
                Pais.FechaHoraReg = DateTime.Now;

                qry.Add(Pais);
                await _context.SaveChangesAsync();

                resp.Cod = "200";
                resp.Data = Pais;
                resp.Mensaje = "Registrado exitosamente";

            }
            catch (Exception ex)
            {
                resp.Cod = "400";
                resp.Mensaje = $"Error en {clase}: {ex.Message}";
                err.LogErrorMetodos(clase, metodo, ex.Message);
            }

            return resp;
        }
        public async Task<Respuesta> putPais(Pai Pais)
        {

            var resp = new Respuesta();
            var pa = new Pai();
            var qry = _context.Pais;
            var metodo = "putPais";

            try
            {
                pa = qry.Where(x => x.PaisId == Pais.PaisId).FirstOrDefault();

                if (pa.PaisId == null || pa.PaisId == 0)
                {
                    resp.Cod = "400";
                    resp.Data = Pais;
                    resp.Mensaje = "No existe el Pais";
                }
                else
                {

                    pa.PaisNombre = Pais.PaisNombre;
                    pa.PaisId = Pais.PaisId;
                    pa.FechaHoraReg = DateTime.Now;
                    pa.Estado = Pais.Estado;

                    qry.Update(pa);
                    await _context.SaveChangesAsync();

                    resp.Cod = "200";
                    resp.Data = pa;
                    resp.Mensaje = "Actualizado exitosamente";
                }

            }
            catch (Exception ex)
            {
                resp.Cod = "400";
                resp.Mensaje = $"Error en {clase}: {ex.Message}";
                err.LogErrorMetodos(clase, metodo, ex.Message);
            }

            return resp;
        }
        public async Task<Respuesta> deletePais(int PaisId)
        {

            var resp = new Respuesta();
            var pa = new Pai();
            var qry = _context.Pais;
            var metodo = "deletePais";

            try
            {
                pa = qry.Where(x => x.PaisId == PaisId && x.EstadoId == 1).FirstOrDefault();

                if (pa.PaisId == null || pa.PaisId == 0)
                {
                    resp.Cod = "400";
                    resp.Data = PaisId;
                    resp.Mensaje = "No existe o ya fue eliminada la Pais";
                }
                else
                {

                    pa.FechaHoraReg = DateTime.Now;
                    pa.EstadoId = 0;

                    qry.Update(pa);
                    await _context.SaveChangesAsync();

                    resp.Cod = "200";
                    resp.Data = pa;
                    resp.Mensaje = "Eliminado exitosamente";
                }

            }
            catch (Exception ex)
            {
                resp.Cod = "400";
                resp.Mensaje = $"Error en {clase}: {ex.Message}";
                err.LogErrorMetodos(clase, metodo, ex.Message);
            }

            return resp;
        }
    }
}
