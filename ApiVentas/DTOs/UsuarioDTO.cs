namespace ApiVentas.DTOs
{
    public class UsuarioDTO
    {
        public int UsuId { get; set; }

        public string? UsuNombre { get; set; }

        public string? EmpresaNombre { get; set; }

        public string? EstadoDescrip { get; set; }

        public DateTime? FechaHoraReg { get; set; }

        public DateTime? FechaHoraAct { get; set; }

        public int? UsuIdReg { get; set; }

        public int? UsuIdAct { get; set; }
    }
}
