using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class ArticuloService
    {
        public List<Articulo> listar()
        {
            List<Articulo> listaArticulo = new List<Articulo>();
            try
            {
                return listaArticulo;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
