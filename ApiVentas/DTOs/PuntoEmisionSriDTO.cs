using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;

namespace ApiVentas.DTOs
{
    public class PuntoEmisionSriDTO
    {
        public int PuntoEmisionId { get; set; }
        public string? PuntoEmision { get; set; }
        public int? EmpresaId { get; set; }
        public string? EmpresaDescrip{ get; set; }
        public int? SucursalId { get; set; }
        public string? SucursalDescrip{ get; set; }
        public string? CodEstablecimientoSri { get; set; }
        public int? UltSecuencia { get; set; }
        public short? Estado { get; set; }
        public DateTime? FechaHoraReg { get; set; }
        public DateTime? FechaHoraAct { get; set; }
        public int? UsuIdReg { get; set; }
        public string? UsuRegDescrip { get; set; }
        public int? UsuIdAct { get; set; }
        public string? UsuActDescrip { get; set; }
        public Expression<Func<PuntoEmisionSriDTO, bool>> DictionaryPuntoSri(string? opcion, string? data)
        {
            bool validarOpcion = opcion.IsNullOrEmpty();
            bool validarData = data.IsNullOrEmpty();
            int.TryParse(data, out int in1);
            Expression<Func<PuntoEmisionSriDTO, bool>> query;
            if (validarOpcion)
            {
                query = x => x.Estado==1;
            }
            else
            {
                var DictionaryPuntoSri= new Dictionary<string, Expression<Func<PuntoEmisionSriDTO, bool>>>();
                DictionaryPuntoSri.Add("id", x => x.PuntoEmisionId== in1 && x.Estado==1);
                DictionaryPuntoSri.Add("puntoemision", x => x.PuntoEmision.ToLower().Equals(data.ToLower()) && x.Estado == 1);
                DictionaryPuntoSri.Add("empresa", x => x.EmpresaId==in1 && x.Estado == 1);
                DictionaryPuntoSri.Add("sucursal", x => x.SucursalId==in1 && x.Estado== 1);
                DictionaryPuntoSri.Add("establecimiento", x => x.CodEstablecimientoSri.ToLower().Equals(data.ToLower()) && x.Estado == 1);
                DictionaryPuntoSri.Add("utl", x => x.UltSecuencia== in1 && x.Estado == 1);
                DictionaryPuntoSri.Add("estado", x => x.Estado.Equals(data.ToUpper()));
                query = !validarOpcion ? DictionaryPuntoSri.ContainsKey(opcion.ToLower()) && !validarData ? DictionaryPuntoSri[opcion.ToLower()] : null : null;
            }
            return query;
        }
    }
}
