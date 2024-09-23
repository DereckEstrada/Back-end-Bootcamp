using ApiVentas.Interfaces;
using ApiVentas.Models;
using ApiVentas.Utilitarios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ApiVentas.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardServices _dashboardServices;
        private ControlError Log = new ControlError();

        public DashboardController(IDashboardServices dashboard)
        {
            this._dashboardServices = dashboard;
        }
        [HttpPost]
        [Route("Dashboard")]
        public async Task<Respuesta> GetDashboard([FromBody] Request request)
        {
            var result = new Respuesta();
            try
            {
                result = await _dashboardServices.GetDashboard();
            }
            catch (Exception ex)
            {
                Log.LogErrorMetodos(this.GetType().Name, "GetDashboard", ex.Message);
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
            }
            return result;
        }
        [HttpPost]
        [Route("VentasMensuales")]
        public async Task<Respuesta> GetVentasMensuales([FromBody] Request request)
        {
            var result = new Respuesta();
            try
            {
                result = await _dashboardServices.GetVentasMensuales();
            }
            catch (Exception ex)
            {
                Log.LogErrorMetodos(this.GetType().Name, "GetVentasMensuales", ex.Message);
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
            }
            return result;
        }
    }
}
