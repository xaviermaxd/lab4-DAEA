using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Windows.Shapes;
using Datos;

namespace WpfApp3
{
    /// <summary>
    /// Lógica de interacción para UpdateCliente.xaml
    /// </summary>
    public partial class UpdateCliente : Window
    {
        public UpdateCliente()
        {
            InitializeComponent();
        }
        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(BaseDatos.connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("USP_UpdateEmpleado", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IdEmpleado", txtIdEmpleado.Text);
                    command.Parameters.AddWithValue("@Apellidos", txtApellidos.Text);
                    command.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                    command.Parameters.AddWithValue("@cargo", txtcargo.Text);
                    command.Parameters.AddWithValue("@Tratamiento", txtTratamiento.Text);

                    command.ExecuteNonQuery();
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
