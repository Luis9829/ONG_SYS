using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Capa_de_Datos_ONG_SYS
{
    public class CD_Facturacion
    {

        private Conexion_DB conexion = new Conexion_DB();
        SqlDataReader leer;

        DataTable tabla = new DataTable();
        DataTable tabla2 = new DataTable();
        SqlCommand Comandos = new SqlCommand();
        public DataTable MostrarDetalle()
        {
            Comandos.Connection = conexion.AbrirConexion();
            Comandos.CommandText = "Mostrardetalle";
            Comandos.CommandType = CommandType.StoredProcedure;
            leer = Comandos.ExecuteReader();
            tabla.Load(leer);
            conexion.CerrarConexion();
            return tabla;

        }
        public DataTable MostrarSIT(int idF)
        {
            Comandos.Connection = conexion.AbrirConexion();
            Comandos.CommandText = "MostrarSIT";
            Comandos.CommandType = CommandType.StoredProcedure;
            Comandos.Parameters.AddWithValue("@id", idF);
            leer = Comandos.ExecuteReader();
            tabla2.Load(leer);
            conexion.CerrarConexion();
            return tabla2;



        }
        public void Crearfactura(int idCliente, int idUsuario)
        {
            Comandos.Connection = conexion.AbrirConexion();
            Comandos.CommandText = "sp_NuevaFactura";
            Comandos.CommandType = CommandType.StoredProcedure;
            Comandos.Parameters.AddWithValue("@clienteID ", idCliente);
            Comandos.Parameters.AddWithValue("@usuarioID", idUsuario);
            Comandos.ExecuteNonQuery();
            Comandos.Parameters.Clear();
            conexion.CerrarConexion();

        }
    }
}
