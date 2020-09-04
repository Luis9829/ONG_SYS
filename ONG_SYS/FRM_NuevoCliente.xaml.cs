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
using Capa_de_Negocios_ONG_SYS;

namespace ONG_SYS
{
    /// <summary>
    /// Lógica de interacción para FRM_NuevoCliente.xaml
    /// </summary>
    public partial class FRM_NuevoCliente : Window
    {
        CN_Clientes objetoCN = new CN_Clientes();
        public FRM_NuevoCliente()
        {
            InitializeComponent();
        }

        private void btn_Regresar_C_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            FRM_Principal_Cliente paginaanteriorP = new FRM_Principal_Cliente();
            paginaanteriorP.ShowDialog();
            this.Close();
        }

        private void btn_Agregar_NC_Click(object sender, RoutedEventArgs e)
        {
            string identificacion = TXT_IDENTIFICACION_CLIENTE.Text;
            if (cmb_tipocliente.SelectedIndex == 0)
            {
                identificacion = identificacion + "001";
            }

            if (VERIFICA_IDENTIFICACION.VerificaIdentificacion(identificacion) == false || identificacion.Length < 13)
            {
                MessageBox.Show("Verifique identificación del cliente");
                return;

            }
            else if (string.IsNullOrEmpty(TXT_Nombre_cliente.Text))
            {
                MessageBox.Show("Verifique que el campo Nombre del cliente se encuentre lleno");
                return;

            }
            else if (string.IsNullOrEmpty(TXT_IDENTIFICACION_CLIENTE.Text))
            {
                MessageBox.Show("Verifique que el campo de identificación se encuentre lleno");
                return;

            }
            else if (cmb_tipocliente.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione el tipo de cliente");
                return;

            }
            else if (string.IsNullOrEmpty(TXT_APELLIDO_CLIENTE.Text))
            {
                MessageBox.Show("Verifique que el campo apellido se encuentre lleno");
                return;

            }
            else if (string.IsNullOrEmpty(TXT_DIRECCION.Text))
            {
                MessageBox.Show("Verifique que el campo Dirección se encuentre lleno");
                return;
            }
            else if (string.IsNullOrEmpty(TXT_TELEFONO.Text))
            {
                MessageBox.Show("Verifique que el campo Teléfono se encuentre lleno");
                return;

            }
            else if (string.IsNullOrEmpty(TXT_CORREO.Text))
            {
                MessageBox.Show("Verifique que el campo de correo electrónico se encuentre lleno");
                return;
            }
            else if (VERIFICA_IDENTIFICACION.VerificaIdentificacion(identificacion) == true)
            {
                try
                {
                    objetoCN.InsertarCliente(cmb_tipocliente.SelectedIndex + 1, TXT_Nombre_cliente.Text, TXT_APELLIDO_CLIENTE.Text, TXT_IDENTIFICACION_CLIENTE.Text, TXT_TELEFONO.Text, TXT_DIRECCION.Text, TXT_CORREO.Text);
                    MessageBox.Show("Guardado correctamente!");

                    limpiarForm();
                }

                catch (Exception ex)
                {
                    MessageBox.Show("Error Producido por: " + ex);
                }
            }
           
        }

        private void limpiarForm()
        {
            TXT_IDENTIFICACION_CLIENTE.Clear();
            TXT_Nombre_cliente.Clear();
            TXT_APELLIDO_CLIENTE.Clear();
            cmb_tipocliente.Text = "";
            TXT_DIRECCION.Clear();
            TXT_TELEFONO.Clear();
            TXT_CORREO.Clear();
        }
        private void TXT_IDENTIFICACION_CLIENTE_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }

        private void TXT_TELEFONO_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }

        private void TXT_Nombre_cliente_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.Text, "^[a-zA-Z]"))
            {
                e.Handled = true;
            }
        }

        private void TXT_APELLIDO_CLIENTE_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.Text, "^[a-zA-Z]"))
            {
                e.Handled = true;
            }
        }
    }
}
