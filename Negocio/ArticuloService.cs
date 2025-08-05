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

                    if (!(db.Lector["Codigo"] is DBNull))
                        art.codigo = (string)db.Lector["Codigo"];

                    if (!(db.Lector["Nombre"] is DBNull))
                        art.nombre = (string)db.Lector["Nombre"];

                    if (!(db.Lector["Descripcion"] is DBNull))
                        art.descripcion = (string)db.Lector["Descripcion"];

                    art.marca = new Marca();
                    if (!(db.Lector["IdMarca"] is DBNull))
                        art.marca.id = (int)db.Lector["IdMarca"];
                    if (!(db.Lector["Marca"] is DBNull))
                        art.marca.descripcion = (string)db.Lector["Marca"];

                    art.categoria = new Categoria();
                    if (!(db.Lector["IdCategoria"] is DBNull))
                        art.categoria.id = (int)db.Lector["IdCategoria"];
                    if (!(db.Lector["Categoria"] is DBNull))
                        art.categoria.descripcion = (string)db.Lector["Categoria"];

                    if (!(db.Lector["ImagenUrl"] is DBNull))
                        art.urlImg = (string)db.Lector["ImagenUrl"];

                    if (!(db.Lector["Precio"] is DBNull))
                        art.precio = (decimal)db.Lector["Precio"];//No andaba por que lo tenia como float JAJAJA

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
