using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Capa_de_Datos_ONG_SYS
{
    public class CD_Clientes
    {
        private Conexion_DB conexion = new Conexion_DB();
        SqlDataReader leerDatos;
        DataTable tabla = new DataTable();
        SqlCommand comando = new SqlCommand();

        public DataTable buscarC(string nombre)
        {
            comando.Connection = conexion.AbrirConexion();
            comando.CommandType = CommandType.Text;
            //Comandos.CommandText = "select idServicio, nombreTiposervicio, nombreServicio,valorServicio from tblServicio join tblTipoServicio on tblServicio.idTiposervicio = tblTipoServicio.idTiposervicio like('" + nombre + "%')";
            comando.CommandText = "select*from vClientes WHERE Cliente like('%" + nombre + "%') ";
            comando.ExecuteNonQuery();
            DataTable dta = new DataTable();
            SqlDataAdapter sqd = new SqlDataAdapter(comando);
            sqd.Fill(dta);
            conexion.CerrarConexion();
            return dta;

        }

        public DataTable Mostrar()
        {
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "sp_VerClientes";
            comando.CommandType = CommandType.StoredProcedure;
            leerDatos = comando.ExecuteReader();
            tabla.Load(leerDatos);
            conexion.CerrarConexion();
            return tabla;
        }


        public void InsertarCliente(int tipoCliente, string nombre, string apellido, string cedula, string telefono, string direccion, string correo)
        {
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "sp_IngresoCliente";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@TipoClienteID", tipoCliente);
            comando.Parameters.AddWithValue("@nombre", nombre);
            comando.Parameters.AddWithValue("@apellido", apellido);
            comando.Parameters.AddWithValue("@cedula", cedula);
            comando.Parameters.AddWithValue("@telefono", telefono);
            comando.Parameters.AddWithValue("@direccion", direccion);
            comando.Parameters.AddWithValue("@correo", correo);
            comando.ExecuteNonQuery();
        }
        public void Editar( int idCliente, int tipoCliente, string nombre, string apellido, string cedula, string telefono, string direccion, string correo)
        {
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "sp_ActualizarCliente";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@clienteID", idCliente );
            comando.Parameters.AddWithValue("@TipoClienteID", tipoCliente);
            comando.Parameters.AddWithValue("@nombre", nombre);
            comando.Parameters.AddWithValue("@apellido", apellido);
            comando.Parameters.AddWithValue("@cedula", cedula);
            comando.Parameters.AddWithValue("@telefono", telefono);
            comando.Parameters.AddWithValue("@direccion", direccion);
            comando.Parameters.AddWithValue("@correo", correo);
            comando.ExecuteNonQuery();
        }

        public void EliminarProveedor(int idCliente)
        {
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "sp_EliminarCliente";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@clienteID", idCliente);
            comando.ExecuteNonQuery();
            comando.Parameters.Clear();
            conexion.CerrarConexion();
        }

    }
}
