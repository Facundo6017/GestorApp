using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class CategoriaService
    {
        public List<Categoria> listarCategoria()
        {
            List<Categoria> listaCategoria = new List<Categoria>();
            AccesoDB db = new AccesoDB();
            try
            {
                string categoriaQuery = "SELECT Id,Descripcion FROM CATEGORIAS";
                db.setearConsulta(categoriaQuery);
                db.ejecutarConsulta();
                while (db.Lector.Read())
                {
                    Categoria aux = new Categoria();
                    aux.id = (int)db.Lector["Id"];
                    aux.descripcion = (string)db.Lector["Descripcion"];

                    listaCategoria.Add(aux);
                }

                return listaCategoria;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db.cerrarConexion();
            }
        }

    }
}
