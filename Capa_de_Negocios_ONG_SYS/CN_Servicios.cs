using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Capa_de_Datos_ONG_SYS;


namespace Capa_de_Negocios_ONG_SYS
{
    public class CN_Servicios
    {
        private CD_Servicios objetoCD = new CD_Servicios();
        public DataTable MostrarServ()
        {
            DataTable tabla = new DataTable();
            tabla = objetoCD.Mostrar();
            return tabla;



        }
        public DataTable MostarTipoS()
        {
            DataTable tblTS = new DataTable();
            tblTS = objetoCD.MostrarTiposervicio();
            return tblTS;

        }
        public void InsertarServ(int tipoServicio, string nombreServicio, string valorServicio)
        {
            objetoCD.InsertarServicio(Convert.ToInt32(tipoServicio), nombreServicio, Convert.ToDouble(valorServicio));



        }
        public DataTable BuscarS(string nombreS)
        {
            DataTable dtb = new DataTable();
            dtb = objetoCD.buscarS(nombreS);
            return dtb;



        }
        public void EditarServ(string nombreServicio, string valorServicio, int tipoServicio, string idServicio)
        {


            objetoCD.EditarServicio(nombreServicio, Convert.ToDouble(valorServicio), Convert.ToInt32(tipoServicio), Convert.ToInt32(idServicio));

        }
        public void EliminarServ(string idServicio)
        {


            objetoCD.EliminarServicio(Convert.ToInt32(idServicio));

        }


       


    }
}
