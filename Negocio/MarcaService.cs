using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class MarcaService
    {
        public List<Marca> listarMarca()
        {
            List<Marca> listaMarca = new List<Marca>();
            AccesoDB db = new AccesoDB();
            try
            {
                string marcaQuery = "SELECT Id,Descripcion FROM MARCAS";
                db.setearConsulta(marcaQuery);
                db.ejecutarConsulta();
                while (db.Lector.Read())
                {
                    Marca aux = new Marca();
                    aux.id = (int)db.Lector["Id"];
                    aux.descripcion = (string)db.Lector["Descripcion"];

                    listaMarca.Add(aux);
                }

                return listaMarca;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                db.cerrarConexion();
            }
        }

    }
}
