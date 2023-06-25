﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Conexion
{
    public class AccesoDatos
    {
        // declaro objetos sin iniciar
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector;

       //propiedad Lector
       public SqlDataReader Lector
        {
            get { return lector; }
        }


        //Contrutctor e inicio objetos conexion y comando
        public AccesoDatos()
        {
            conexion = new SqlConnection("Server=DESKTOP-74BHH28; database=CATALOGO_DB; integrated security=true");
            comando = new SqlCommand();


        }

        //Método de consulta en la base de datos
        public void SetConsulta(string consulta)
        {
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = consulta;
        }
        public void SetParametros(string nombre, object valor)
        {
            comando.Parameters.AddWithValue(nombre, valor);
        }


        //Método de lectura de la base de datos e inicio objeto lector
        public void EjecutarLectura()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                lector = comando.ExecuteReader();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void EjecutarAccion()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
        public void CerrarConexion()
        {
            if (lector != null)
            {
                lector.Close();
            }
            conexion.Close();
        }

    }
}
