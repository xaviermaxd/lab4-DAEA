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
    /// Lógica de interacción para ClienteCRUD.xaml
    /// </summary>
    public partial class ClienteCRUD : Window
    {
        public ClienteCRUD()
        {
            InitializeComponent();
        }

       private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<Empleado> empleados = new List<Empleado>();
            try
            {
                //Cadena de conexión
                SqlConnection connection = new SqlConnection(BaseDatos.connectionString);
                connection.Open();


                //Comandos de TRANSACT SQL
                SqlCommand command = new SqlCommand("USP_GetEmpleadoFilter", connection);
                command.CommandType = CommandType.StoredProcedure;

                //Agrega el parámetro al comando
                command.Parameters.AddWithValue("@Nombre", txtFilter.Text);

                //CONECTADA
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int IdEmpleado = reader.GetInt32("IdEmpleado");
                    string Apellidos = reader.GetString("Apellidos");
                    string Nombre = reader.GetString("Nombre");
                    string cargo = reader.GetString("cargo");
                    string Tratamiento = reader.GetString("Tratamiento");

                    empleados.Add(new Empleado { IdEmpleado = IdEmpleado, Apellidos = Apellidos, Nombre = Nombre, cargo = cargo, Tratamiento = Tratamiento });

                }

                connection.Close();
                dataGrid.ItemsSource = empleados;

                //dgvDemo.DataSource = clientes;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                //throw;
            }


            
        }

        private void Editar_Click(object sender, RoutedEventArgs e)
        {
            Button editButton = sender as Button;
            Empleado empleadoSeleccionado = editButton.DataContext as Empleado;  

            if (empleadoSeleccionado != null)
            {
                UpdateCliente updateWindow = new UpdateCliente();  // Asegúrate de que esta ventana sea la correcta para editar empleados
                updateWindow.txtIdEmpleado.Text = empleadoSeleccionado.IdEmpleado.ToString();  // Asumiendo que IdEmpleado es un int
                updateWindow.txtNombre.Text = empleadoSeleccionado.Nombre;
                updateWindow.txtApellidos.Text = empleadoSeleccionado.Apellidos;
                updateWindow.txtcargo.Text = empleadoSeleccionado.cargo;
                updateWindow.txtTratamiento.Text = empleadoSeleccionado.Tratamiento;
                updateWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("No se pudo obtener el empleado seleccionado.");
            }
        }


        private void Eliminar_Click(object sender, RoutedEventArgs e)
        {
            // Obtener el objeto Cliente vinculado al botón que se presionó
            Button eliminarButton = sender as Button;
            Empleado Seleccionado = eliminarButton.DataContext as Empleado;

            if (Seleccionado != null)
            {
                MessageBoxResult result = MessageBox.Show("¿Estás seguro de que deseas eliminar este empleado?", "Confirmar Eliminación", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        using (SqlConnection connection = new SqlConnection(BaseDatos.connectionString))
                        {
                            connection.Open();
                            SqlCommand command = new SqlCommand("USP_DeleteEmpleado", connection);
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@IdEmpleado", Seleccionado.IdEmpleado);

                            command.ExecuteNonQuery();
                        }


                        MessageBox.Show("Empleado desactivado correctamente.", "Desactivado", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al desactivar el Empleado: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void Registrar_Click(object sender, RoutedEventArgs e)
        {
            CreateClient createClient = new CreateClient();
            createClient.ShowDialog();
        }
    }


}
