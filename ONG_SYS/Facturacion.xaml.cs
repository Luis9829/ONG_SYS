using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Capa_de_Negocios_ONG_SYS;
namespace ONG_SYS
{
    /// <summary>
    /// Lógica de interacción para Facturacion.xaml
    /// </summary>
    public partial class Facturacion : Window
    {
        public bool variableCF = false;
        public string  idF= null; 
        public Facturacion()
        {
            InitializeComponent();
        }
        CN_facturacion objecF = new CN_facturacion();
        private void MostrarDetalle()
        {

            DT_Facturacion.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = objecF.MostrarDetalle() });

        }
        //private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        //{
        //    MostrarDetalle();
        //}

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FRM_Administracion_de_servicios admiS = new FRM_Administracion_de_servicios();

            admiS.ShowDialog();
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            MostrarDetalle();

            //CN_facturacion objF = new CN_facturacion();
            DataTable dt = new DataTable();
            //dt = objF.MostrarSIT(// ojo variable con el id de factura\\);
            //string dt1=dt.Rows[0]["Subtotal"].ToString();
            //string dt2 = dt.Rows[0]["IVA (12%)"].ToString();
            //string dt3 = dt.Rows[0]["Total"].ToString();

            //txtSt.Text = dt1;
            //txtIv.Text = dt2;
            //txtTt.Text = dt3;


        }

        private void btnSel_Click_1(object sender, RoutedEventArgs e)
        {
            ListaClientesFactura listaC = new ListaClientesFactura(this);

            listaC.ShowDialog();
        }

        private void btnRs_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            FRM_MENU_PRINCIPAL paginaanteriorP = new FRM_MENU_PRINCIPAL();
            paginaanteriorP.ShowDialog();
            this.Close();
        }




        //private void btnCF_Click(object sender, RoutedEventArgs e)
        //{
            
        //    if (variableCF == false)
        //    {
        //        FMR_AUTENTICACION au = new FMR_AUTENTICACION();
        //        try
        //        {
        //            objecF.CrearFactura(idF, au.valorid);
        //            MessageBox.Show("Se ha creado una factura");



        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("No es posible crear la factura por :" + ex);

        //        }



        //    }
        //    else { MessageBox.Show("Verifique sus datos"); }

        //}

        private void btnCF_Click_1(object sender, RoutedEventArgs e)
        {

            if (variableCF == false)
            {
              FMR_AUTENTICACION au = new FMR_AUTENTICACION();
                ListaClientesFactura ltc = new ListaClientesFactura();
                try
                {
                    objecF.CrearFactura(ltc.VA, au.valorid);
                    MessageBox.Show("Se ha creado una factura");



                }
                catch (Exception ex)
                {
                    MessageBox.Show("No es posible crear la factura por :" + ex);

                }



            }
            else { MessageBox.Show("Verifique sus datos"); }


        }
    }
}
