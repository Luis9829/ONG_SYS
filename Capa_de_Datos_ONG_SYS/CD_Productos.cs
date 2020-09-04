using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Capa_de_Datos_ONG_SYS
{
    public class CD_Productos
    {
        private Conexion_DB con = new Conexion_DB();
        private SqlCommand comando = new SqlCommand();
        private SqlDataReader leerfilas;

        public DataTable ListarTipoProductos()
        {

            DataTable TablaP = new DataTable();
            comando.Connection = con.AbrirConexion();
            comando.CommandText = "MostrarTipoProductos";
            comando.CommandType = CommandType.StoredProcedure;
            leerfilas = comando.ExecuteReader();
            TablaP.Load(leerfilas);
            leerfilas.Close();
            con.CerrarConexion();
            return TablaP;
        }

        public DataTable ListarProductos()
        {

            DataTable TablaPro = new DataTable();
            comando.Connection = con.AbrirConexion();
            comando.CommandText = "Mostrar_Productos";
            comando.CommandType = CommandType.StoredProcedure;
            leerfilas = comando.ExecuteReader();
            TablaPro.Load(leerfilas);
            leerfilas.Close();
            con.CerrarConexion();
            return TablaPro;
        }

        public void Insertar(string NombreProducto, int idTipoProducto, int idProveedor, string marca, float precio, int stock)
        {
            comando.Connection = con.AbrirConexion();
            comando.CommandText = "IngresarProductos";
            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue("@NombreProducto", NombreProducto);
            comando.Parameters.AddWithValue("@idTipoProducto", idTipoProducto);
            comando.Parameters.AddWithValue("@idProveedor", idProveedor);
            comando.Parameters.AddWithValue("@Marca", marca);
            comando.Parameters.AddWithValue("@Precio", precio);
            comando.Parameters.AddWithValue("@Stock", stock);
            comando.ExecuteNonQuery();
            comando.Parameters.Clear();
            con.CerrarConexion();

        }

        public void Actualizar(string NombreProducto, int idTipoProducto, int idProveedor, string marca, float precio, int stock,int id)
        {
            comando.Connection = con.AbrirConexion();
            comando.CommandText = "EditarProductos";
            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue("@NombreProducto", NombreProducto);
            comando.Parameters.AddWithValue("@idTipoProducto", idTipoProducto);
            comando.Parameters.AddWithValue("@idProveedor", idProveedor);
            comando.Parameters.AddWithValue("@Marca", marca);
            comando.Parameters.AddWithValue("@Precio", precio);
            comando.Parameters.AddWithValue("@Stock", stock);
            comando.Parameters.AddWithValue("@idProductoaActualizar", id);
            comando.ExecuteNonQuery();
            comando.Parameters.Clear();
            con.CerrarConexion();

        }

        public void EliminarProducto(int idProducto)
        {
            comando.Connection = con.AbrirConexion();
            comando.CommandText = "EliminarProducto";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@idProducto", idProducto);
            comando.ExecuteNonQuery();
            comando.Parameters.Clear();
            con.CerrarConexion();
        }





    }
}
