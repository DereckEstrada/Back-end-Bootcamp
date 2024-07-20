using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;

namespace ApiVentas.DTOs
{
    public class ProveedorDTO
    {
        public int ProvId { get; set; }
        public string? ProvRuc { get; set; }
        public string? ProvNomComercial { get; set; }
        public string? ProvRazon { get; set; }
        public string? ProvDireccion { get; set; }
        public int? ProvTelefono { get; set; }
        public int? CiudadId { get; set; }
        public string? CiudadDescrip{ get; set; }
        public string? Estado { get; set; }
        public DateTime? FechaHoraReg { get; set; }
        public DateTime? FechaHoraAct { get; set; }
        public int? UsuIdReg { get; set; }
        public string? UsuRegDescrip { get; set; }
        public int? UsuIdAct { get; set; }
        public string? UsuActDescrip { get; set; }
        public Expression<Func<ProveedorDTO, bool>> DictionaryProveedor(string?opcion, string?data)
        {
            bool validarOpcion = opcion.IsNullOrEmpty();
            bool validarData = data.IsNullOrEmpty();
            int.TryParse(data, out int in1);
            Expression<Func<ProveedorDTO, bool>> query;
            if (validarOpcion) {
                query = x => x.Estado.Equals("A");
            }
            else
            {
                var DictionaryProveedor = new Dictionary<string, Expression<Func<ProveedorDTO, bool>>>();
                DictionaryProveedor.Add("id", x => x.ProvId == in1 && x.Estado.Equals("A"));
                DictionaryProveedor.Add("ruc", x => x.ProvRuc.Equals(data) && x.Estado.Equals("A"));
                DictionaryProveedor.Add("nombre", x => x.ProvNomComercial.ToLower().Equals(data.ToLower()) && x.Estado.Equals("A"));
                DictionaryProveedor.Add("razon", x => x.ProvRazon.ToLower().Equals(data.ToLower()) && x.Estado.Equals("A"));
                DictionaryProveedor.Add("direccion", x => x.ProvDireccion.ToLower().Equals(data.ToLower()) && x.Estado.Equals("A"));
                DictionaryProveedor.Add("telefono", x => x.ProvTelefono == in1 && x.Estado.Equals("A"));
                DictionaryProveedor.Add("ciudad", x => x.CiudadId == in1 && x.Estado.Equals("A"));
                DictionaryProveedor.Add("estado", x => x.Estado.ToLower().Equals(data.ToLower()));
                query = !validarOpcion ? DictionaryProveedor.ContainsKey(opcion.ToLower()) && !validarData ? DictionaryProveedor[opcion.ToLower()]:null : null;
            }
            return query;
        }
    }
}
