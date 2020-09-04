using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using Capa_de_Negocios_ONG_SYS;

namespace ONG_SYS
{
    /// <summary>
    /// Lógica de interacción para FRM_Administracion_de_servicios.xaml
    /// </summary>
    public partial class FRM_Administracion_de_servicios : Window
    {
        public FRM_Administracion_de_servicios()
        {
            InitializeComponent();
        }

        private void btn_Regresar_S_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            FRM_MENU_PRINCIPAL paginaanteriorP = new FRM_MENU_PRINCIPAL();
            paginaanteriorP.ShowDialog();
            this.Close();
        }
        CN_Servicios objCN = new CN_Servicios();
        private string idServicio = null;
        private bool Editar = false;
        //List<Servicios> LServ:






        public object DataSource { get; set; }


        //private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        //{
        //    MostrarServicios();
        //}

        private void MostrarServicios()
        {
            CN_Servicios objec = new CN_Servicios();
            dtGLS.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = objec.MostrarServ() });

        }
       

        private void limpiarAdministracionServicios()
        {
            // CBTipoServicio.Clear();

            TXT_Nombre_Servicio.Clear();
            TXT_valor_servicio.Clear();
        }

        

        private void btn_Agregar_NS_Click(object sender, RoutedEventArgs e)
        {
          
            try
            {
                objCN.InsertarServ(CBTipoServicio.SelectedIndex + 1, TXT_Nombre_Servicio.Text, TXT_valor_servicio.Text);
                MessageBox.Show("Se guardó correctamente");
                MostrarServicios();
                limpiarAdministracionServicios();


            }
            catch (Exception ex)
            {
                MessageBox.Show("No es posible insertar los datos" + ex);

            }

        }

        private void btn_ACTUALIZAR_S_Click(object sender, RoutedEventArgs e)
        {
            if (Editar == false)
            {
                try
                {
                    objCN.EditarServ(TXT_Nombre_Servicio.Text, TXT_valor_servicio.Text, CBTipoServicio.SelectedIndex + 1, idServicio);
                    MessageBox.Show("Se actualizo correctamente");

                    MostrarServicios();

                    limpiarAdministracionServicios();


                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se ha hactualizado los dato por : " + ex);


                }


            }
        }

        private void btn_ELIMINAR_S_Click(object sender, RoutedEventArgs e)
        {

            if (dtGLS.SelectedItem != null)

            {
                objCN.EliminarServ(idServicio);
                MessageBox.Show("Se ha eliminado correctamente");
                MostrarServicios();
                limpiarAdministracionServicios();


            }
            else
            {
                MessageBox.Show("Eliga un dato");


            }

        }

        
        private void TXT_BUSCAR_Servicio_KeyUp(object sender, KeyEventArgs e)
        {
            //para buscaaaaaaar
            CN_Servicios objec1 = new CN_Servicios();

            dtGLS.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = objec1.BuscarS(TXT_BUSCAR_Servicio.Text) });
        }

        private void dtGLS_Loaded_1(object sender, RoutedEventArgs e)
        {
            MostrarServicios();

        }

        private void dtGLS_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dtGLS.SelectedItem != null)
            {
                //foreach (DataRow row in tbl.Rows)
                //{
                //    string dta = row["idtiposervicio"].ToString();
                //    string dtb = row["Nombreservicio"].ToString();
                //    string dtc = row["Valorservicio"].ToString();

                //    txtTS.Text = dta;
                //    txtNS.Text = dtb;
                //    txtVS.Text = dtc;
                //}

                DataGrid dataGrid = sender as DataGrid;
                DataRowView rowView = dataGrid.SelectedItem as DataRowView;
                try
                {
                    if (rowView.Row != null)
                    {
                        string dt1 = rowView.Row[1].ToString(); /* 1st Column on selected Row */
                        string dt2 = rowView.Row[2].ToString();
                        string dt3 = rowView.Row[3].ToString();
                        string dto = rowView.Row[0].ToString();
                        //txtTS.Text = dt1;
                        TXT_Nombre_Servicio.Text = dt2;
                        TXT_valor_servicio.Text = dt3;
                        idServicio = dto;
                    }
                    else
                    {
                        MessageBox.Show("Debe contener informacion");

                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("No puede seleccionar un campo vacio" + ex);


                }

            }
            else
            {

                //MessageBox.Show("Seleccione una fila!");
            }
        }
    }
}
