namespace ApiVentas.DTOs
{
    public class SucursalDTO
    {


        public int SucursalId { get; set; }

        public string? SucursalRuc { get; set; }

        public string? SucursalNombre { get; set; }

        public string? SucursalDireccion { get; set; }

        public string? SucursalTelefono { get; set; }

        public string? SucursalEstado { get; set; }

        public DateTime? FechaHoraReg { get; set; }

        public int? UsuIdReg { get; set; }

        public DateTime? FechaHoraAct { get; set; }

        public int? UsuIdAct { get; set; }

        public string? EmpresaNombre { get; set; }

        public string? CodEstablecimientoSri { get; set; }
    }
}
