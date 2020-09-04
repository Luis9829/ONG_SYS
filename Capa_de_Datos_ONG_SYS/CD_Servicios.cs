using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Collections;

namespace Capa_de_Datos_ONG_SYS
{
    public class CD_Servicios
    {
        private Conexion_DB conexion = new Conexion_DB();
        SqlDataReader leer;

        DataTable tabla = new DataTable();
        DataTable tabla2 = new DataTable();
        SqlCommand Comandos = new SqlCommand();
        public DataTable Mostrar()
        {
            Comandos.Connection = conexion.AbrirConexion();
            Comandos.CommandText = "Mostrarservicios";
            Comandos.CommandType = CommandType.StoredProcedure;
            leer = Comandos.ExecuteReader();
            tabla.Load(leer);
            conexion.CerrarConexion();
            return tabla;

        }
        public DataTable MostrarTiposervicio()
        {
            Comandos.Connection = conexion.AbrirConexion();
            Comandos.CommandText = "MostrarTipoServicio";
            Comandos.CommandType = CommandType.StoredProcedure;
            leer = Comandos.ExecuteReader();
            tabla2.Load(leer);
            conexion.CerrarConexion();
            return tabla2;

        }

        
        public void InsertarServicio(int tipoServicio, string nombreServicio, double valorServicio)
        {
            Comandos.Connection = conexion.AbrirConexion();
            Comandos.CommandText = "InsertarServicios";
            Comandos.CommandType = CommandType.StoredProcedure;
            Comandos.Parameters.AddWithValue("@tipoServicio", tipoServicio);
            Comandos.Parameters.AddWithValue("@nombreServicio", nombreServicio);
            Comandos.Parameters.AddWithValue("@valorServicio", valorServicio);

            Comandos.ExecuteNonQuery();
            Comandos.Parameters.Clear();
            conexion.CerrarConexion();



        }
        public void EditarServicio(string nombreServicio, double valorServicio, int tipoServicio, int idServicio)
        {
            Comandos.Connection = conexion.AbrirConexion();
            Comandos.CommandText = "EditarServicio";
            Comandos.CommandType = CommandType.StoredProcedure;
            Comandos.Parameters.AddWithValue("@nombreServicio", nombreServicio);
            Comandos.Parameters.AddWithValue("@valorServicio", valorServicio);
            Comandos.Parameters.AddWithValue("@tipoServicio", tipoServicio);
            Comandos.Parameters.AddWithValue("@idServicio", idServicio);
            Comandos.ExecuteNonQuery();
            Comandos.Parameters.Clear();
            conexion.CerrarConexion();



        }
        public void EliminarServicio(int idServicio)
        {
            Comandos.Connection = conexion.AbrirConexion();
            Comandos.CommandText = "EliminarServicio";
            Comandos.CommandType = CommandType.StoredProcedure;
            Comandos.Parameters.AddWithValue("@idServicio", idServicio);
            Comandos.ExecuteNonQuery();
            Comandos.Parameters.Clear();
            conexion.CerrarConexion();



        }
        public DataTable buscarS(string nombre)
        {
            Comandos.Connection = conexion.AbrirConexion();
            Comandos.CommandType = CommandType.Text;
            //Comandos.CommandText = "select idServicio, nombreTiposervicio, nombreServicio,valorServicio from tblServicio join tblTipoServicio on tblServicio.idTiposervicio = tblTipoServicio.idTiposervicio like('" + nombre + "%')";
            Comandos.CommandText = "select*from VMostrar where nombreServicio like('" + nombre + "%')   ";
            Comandos.ExecuteNonQuery();
            DataTable dta = new DataTable();
            SqlDataAdapter sqd = new SqlDataAdapter(Comandos);
            sqd.Fill(dta);
            conexion.CerrarConexion();
            return dta;


        }
        


    }
}

