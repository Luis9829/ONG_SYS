using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_de_Datos_ONG_SYS;
using System.Data;

namespace Capa_de_Negocios_ONG_SYS
{
    public class CN_facturacion
    {

        CD_Facturacion objFc = new CD_Facturacion();

        public DataTable MostrarDetalle()
        {
            //CD_Facturacion objF = new CD_Facturacion();

            DataTable tabla = new DataTable();
            tabla = objFc.MostrarDetalle();
            return tabla;



        }
        public DataTable MostrarSIT(int idF)
        {

            DataTable tabla1 = new DataTable();
            tabla1 = objFc.MostrarSIT(Convert.ToInt32(idF));
            return tabla1;


        }
        public void CrearFactura(string idCliente, string  idUsuario)
        {
            objFc.Crearfactura(Convert.ToInt32(idCliente), Convert.ToInt32(idUsuario));



        }

    }
}
