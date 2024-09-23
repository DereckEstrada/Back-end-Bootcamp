using ApiVentas.Utilitarios;

namespace ApiVentas.Interfaces
{
    public interface IDashboardServices
    {
        Task<Respuesta> GetDashboard();
        Task<Respuesta> GetVentasMensuales();
    }
}
