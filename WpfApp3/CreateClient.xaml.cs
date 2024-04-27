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

namespace WpfApp3
{
    /// <summary>
    /// Lógica de interacción para CreateClient.xaml
    /// </summary>
    public partial class CreateClient : Window
    {
        public CreateClient()
        {
            InitializeComponent();
        }
        string connectionString = "Data Source=LAPTOP-0ORAP2JU\\SQLEXPRESS; Initial Catalog=Neptuno; User Id=userXavier; Password=123456";

        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("USP_CreateEmpleado", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    //command.Parameters.AddWithValue("@IdEmpleado", txtIdEmpleado.Text);
                    command.Parameters.AddWithValue("@Apellidos", txtApellidos.Text);
                    command.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                    command.Parameters.AddWithValue("@cargo", txtcargo.Text);
                    command.Parameters.AddWithValue("@Tratamiento", txtTratamiento.Text);
                    command.Parameters.AddWithValue("@FechaNacimiento", DateTime.Parse(txtFechaNacimiento.Text));
                    command.Parameters.AddWithValue("@FechaContratacion", DateTime.Parse(txtFechaContratacion.Text));
                    command.Parameters.AddWithValue("@direccion", txtdireccion.Text);
                    command.Parameters.AddWithValue("@ciudad", txtciudad.Text);
                    command.Parameters.AddWithValue("@region", txtregion.Text);
                    command.Parameters.AddWithValue("@codPostal", txtcodPostal.Text);
                    command.Parameters.AddWithValue("@pais", txtpais.Text);
                    command.Parameters.AddWithValue("@TelDomicilio", txtTelDomicilio.Text);
                    command.Parameters.AddWithValue("@Extension", txtExtension.Text);
                    command.Parameters.AddWithValue("@notas", txtnotas.Text);
                    command.Parameters.AddWithValue("@Jefe", string.IsNullOrEmpty(txtJefe.Text) ? (object)DBNull.Value : int.Parse(txtJefe.Text));
                    command.Parameters.AddWithValue("@sueldoBasico", decimal.Parse(txtsueldoBasico.Text));
                    //command.Parameters.AddWithValue("@Enable", 1); Asumiendo que siempre habilitas al nuevo empleado


                    command.ExecuteNonQuery();
                    MessageBox.Show("Cliente creado con éxito.");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al crear el cliente: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
