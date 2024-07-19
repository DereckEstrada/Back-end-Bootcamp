namespace ApiVentas.DTOs
{
    public class EmpresaDto
    {
        public int EmpID { get; set; }

        public string? EmpRuc { get; set; }

        public string? EmpNombre { get; set; }

        public string? EmpRazon { get; set; }

        public string? EmpDirMatriz { get; set; }

        public string? EmpTelMatriz { get; set; }

        public string? CiudadDescrip { get; set; }

        public short? Estado { get; set; }

        public DateTime? FecHoraReg { get; set; }

        public DateTime? FecHoraAct { get; set; }

    }
}
