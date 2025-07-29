using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Negocio
{
    class AccesoDB
    {
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector;
        public SqlDataReader Lector
        {
            get { return lector; }
        }


        public AccesoDB() //Abrir la conexion con la base de datos DISCOS_DB
        {
            conexion = new SqlConnection("data source=.\\SQLEXPRESS; initial catalog=CATALOGO_DB; integrated security=true");
        }

        public void setearConsulta(string consulta)
        {
            comando = new SqlCommand();
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = consulta;
        }

        public void ejecutarConsulta()//Se usa cuando le pida datos a la DB
        {
            comando.Connection = conexion;
            conexion.Open();
            lector = comando.ExecuteReader();
        }

        public void ejecutarAccion()//Se usa cuando no necesite que la DB devuelva datos como en un inset update y eso..
        {
            comando.Connection = conexion;
            conexion.Open();
            comando.ExecuteNonQuery();
        }
        public void setearParametro(string nombre,object valor)//se usa en los insert update etc...
        {
            comando.Parameters.AddWithValue(nombre,valor);
        }
        public void cerrarConexion()//Tal cual su titulo
        {
            conexion.Close();
            if (lector != null)
                lector.Close();
        }

    }
}
