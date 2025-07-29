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
        public List<Articulo> listarArt()//Esta funcion devuelve una lista
        {
            List<Articulo> listaArticulo = new List<Articulo>();
            AccesoDB db = new AccesoDB();
            try
            {
                string listarQuery = "select A.Id,A.Codigo,a.Nombre,A.Descripcion,A.IdMarca,A.IdCategoria,B.Descripcion as Marca,C.Descripcion as Categoria,A.ImagenUrl,A.Precio from ARTICULOS A ,MARCAS B,CATEGORIAS C WHERE B.Id = A.IdMarca AND C.Id = A.IdCategoria;";
                db.setearConsulta(listarQuery);
                db.ejecutarConsulta();
                while (db.Lector.Read())
                {
                    Articulo art = new Articulo();
                    art.id = (int)db.Lector["Id"];
                    art.codigo = (string)db.Lector["Codigo"];
                    art.nombre = (string)db.Lector["Nombre"];
                    art.descripcion = (string)db.Lector["Descripcion"];
                    art.marca = new Marca();
                    art.marca.id = (int)db.Lector["IdMarca"];
                    art.marca.descripcion = (string)db.Lector["Marca"];
                    art.categoria = new Categoria();
                    art.categoria.id = (int)db.Lector["IdCategoria"];
                    art.categoria.descripcion = (string)db.Lector["Categoria"];
                    art.urlImg = (string)db.Lector["ImagenUrl"];
                    art.precio = (decimal)(db.Lector["Precio"]);//No andaba por que lo tenia como float JAJAJA

                    listaArticulo.Add(art);

                }

               
                return listaArticulo;
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
        public List<Categoria> listarCategoria() //creo que me podria ahorrar hacer esto si las dos clases eredarian de una clase abstacta seria algo como list<Elemento> y retornarla , aunque deberia agregar otro parametro para manejar las 2 query con un if y hacer la consulta a la db
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
            catch (Exception)
            {

                throw;
            }
            finally
            {
                db.cerrarConexion();
            }
        }
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

        public void agregar(Articulo aux)
        {
            AccesoDB db = new AccesoDB();
            try
            {
                string agregarQuery = "INSERT INTO ARTICULOS (Codigo,Nombre,Descripcion,IdMarca,IdCategoria,ImagenUrl,Precio) VALUES (@Codigo,@Nombre,@Descripcion,@IdMarca,@IdCategoria,@ImagenUrl,@Precio)";
                db.setearConsulta(agregarQuery);

                db.setearParametro("@Codigo", aux.codigo);
                db.setearParametro("@Nombre", aux.nombre);
                db.setearParametro("@Descripcion", aux.descripcion);
                db.setearParametro("@IdMarca", aux.marca.id);
                db.setearParametro("@IdCategoria", aux.categoria.id);
                db.setearParametro("@ImagenUrl", aux.urlImg);
                db.setearParametro("@Precio", aux.precio);
               
                db.ejecutarAccion();
                
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
        public void modificar(Articulo aux)
        {
            AccesoDB db = new AccesoDB();
            try
            {
                string updateQuery = "UPDATE ARTICULOS SET Codigo = @Codigo, Nombre = @Nombre, Descripcion = @Descripcion, IdMarca = @IdMarca, IdCategoria = @IdCategoria, ImagenUrl = @ImagenUrl, Precio = @Precio WHERE Id = @Id";

                db.setearConsulta(updateQuery);
                db.setearParametro("@Codigo", aux.codigo);
                db.setearParametro("@Nombre", aux.nombre);
                db.setearParametro("@Descripcion", aux.descripcion);
                db.setearParametro("@IdMarca", aux.marca.id);
                db.setearParametro("@IdCategoria", aux.categoria.id);
                db.setearParametro("@ImagenUrl", aux.urlImg);
                db.setearParametro("@Precio", aux.precio);
                db.setearParametro("@Id", aux.id);
                db.ejecutarAccion();

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
        public void eliminar(Articulo aux)
        {
            AccesoDB db = new AccesoDB();
            try
            {
                string eliminarQuery = "DELETE FROM ARTICULOS WHERE Id = @Id";
                db.setearConsulta(eliminarQuery);
                db.setearParametro("@Id", aux.id);
                db.ejecutarAccion();
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
