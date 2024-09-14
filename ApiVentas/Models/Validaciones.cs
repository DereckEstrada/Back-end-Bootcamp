using ApiVentas.Utilitarios;

namespace ApiVentas.Models
{
    public class Validaciones
    {
       public bool validacionRequest(Request request)
        {
            bool validadciones = true;
            try
            {
                if (!request.Usuario)
                {
                    validadciones = false;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return validadciones;
    }
}
    }
