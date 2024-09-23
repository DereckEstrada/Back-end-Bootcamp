using ApiVentas.DTOs;
using ApiVentas.Models;
using ApiVentas.Utilitarios.Dictionaries;
using ApiVentas.Utilitarios;
using ApiVentas.Interfaces;

namespace ApiVentas.Services
{
    public class DashboardServices: IDashboardServices, IServices<Dashboard>, IServices<VentasMe>
    {
        private BaseErpContext _context;
        private ControlError log = new ControlError();
        private DynamicEmpty dynamicEmpty = new DynamicEmpty();
        public DashboardServices(BaseErpContext context)
        {
            this._context = context;
        }

        public async Task<Respuesta> GetDashboard()
        {
            var result = new Respuesta();
            try
            {
                result.Data = _context.Dashboards;

                result.Code = dynamicEmpty.IsEmpty(result.Data) ? "204" : "200";
                result.Message = dynamicEmpty.IsEmpty(result.Data) ? $"No se encontro registro" : "Ok";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "GetDashboard", ex.Message);
            }

            return result;
        }
        public async Task<Respuesta> GetVentasMensuales()
        {
            var result = new Respuesta();
            try
            {
                result.Data = _context.VentasMes;

                result.Code = dynamicEmpty.IsEmpty(result.Data) ? "204" : "200";
                result.Message = dynamicEmpty.IsEmpty(result.Data) ? $"No se encontro registro" : "Ok";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "GetDashboard", ex.Message);
            }

            return result;
        }
    }
}
