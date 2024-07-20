using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;

namespace ApiVentas.DTOs
{
    public class BodegaDTO
    {
        public int BodegaId { get; set; }
        public string? BodegaNombre { get; set; }
        public string? BodegaDireccion { get; set; }
        public string? BodegaTelefono { get; set; }
        public int? SucursalId { get; set; }
        public string? SucursalDescrip { get; set; }
        public int? EstadoId { get; set; }
        public string? EstadoDescrip { get; set; }
        public DateTime? FechaHoraReg { get; set; }
        public DateTime? FechaHoraAct { get; set; }
        public int? UsuIdReg { get; set; }
        public string? UsuRegDescrip { get; set; }
        public int? UsuIdAct { get; set; }
        public string? UsuActDescrip { get; set; }
        public Expression<Func<BodegaDTO, bool>>? DictionaryBodega(string? opcion, string? data)
        {
            bool validarOpcion = opcion.IsNullOrEmpty();
            bool validarData = data.IsNullOrEmpty();
            int.TryParse(data, out int in1);
            Expression<Func<BodegaDTO, bool>> query;
            if (validarOpcion)
            {
                query = x => x.EstadoId == 1;
            }
            else
            {
                var DictionaryBodega = new Dictionary<string, Expression<Func<BodegaDTO, bool>>>();
                DictionaryBodega.Add("id", x => x.BodegaId == in1 && x.EstadoId == 1);
                DictionaryBodega.Add("nombre", x => x.BodegaNombre.Equals(data) && x.EstadoId == 1);
                DictionaryBodega.Add("direccion", x => x.BodegaDireccion.Equals(data) && x.EstadoId == 1);
                DictionaryBodega.Add("telefono", x => x.BodegaTelefono.Equals(data) && x.EstadoId == 1);
                DictionaryBodega.Add("sucursal", x => x.SucursalId==in1&& x.EstadoId == 1);
                DictionaryBodega.Add("estado", x => x.EstadoId == in1);
                query = !validarOpcion ? DictionaryBodega.ContainsKey(opcion.ToLower()) && !validarData ? DictionaryBodega[opcion] : null : null;
            }
            return query;
        }
    }
}
