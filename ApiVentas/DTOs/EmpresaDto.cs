namespace ApiVentas.DTOs
{
    public class EmpresaDTO
    {
        public int EmpresaId { get; set; }
        public string? EmpresaRuc { get; set; }
        public string? EmpresaNombre { get; set; }
        public string? EmpresaRazon { get; set; }
        public string? EmpresaDireccionMatriz { get; set; }
        public string? EmpresaTelefonoMatriz { get; set; }
        public int? CiudadId { get; set; }
        public string? CiudadDescripcion { get; set; }
        public int? EstadoId { get; set; }
        public string? EstadoDescripcion{ get; set; }
        public DateTime? FechaHoraReg { get; set; }
        public int? UsuIdReg { get; set; }
        public string? UsuRegName { get; set; }
    }
}
