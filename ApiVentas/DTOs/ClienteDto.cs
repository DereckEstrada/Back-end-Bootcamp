namespace ApiVentas.DTOs
{
    public class ClienteDTO
    {
        public int ClienteId { get; set; }
        public string? ClienteRuc { get; set; }
        public string? ClienteNombre1 { get; set; }
        public string? ClienteNombre2 { get; set; }
        public string? ClienteApellido1 { get; set; }
        public string? ClienteApellido2 { get; set; }
        public string? ClienteEmail { get; set; }
        public string? ClienteTelefono { get; set; }
        public string? ClienteDireccion { get; set; }
        public int? EstadoId { get; set; }
        public string? EstadoDescripcion { get; set; }
        public DateTime? FechaHoraReg { get; set; }
        public int? UsuIdReg { get; set; }
        public string? UsuRegName { get; set; }
    }
}
