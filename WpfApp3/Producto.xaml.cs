using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    /// Lógica de interacción para Producto.xaml
    /// </summary>
    public partial class Producto : Window
    {
        public Producto()
        {
            InitializeComponent();
        }

        //    private void MostrarProductos_Click(object sender, RoutedEventArgs e)
        //    {
        //        string connectionString = "Data Source=LAPTOP-0ORAP2JU\\SQLEXPRESS; Initial Catalog=Neptuno; User Id=userXavier; Password=123456";

        //        try
        //        {
        //            SqlConnection connection = new SqlConnection(connectionString);
        //            connection.Open();


        //            SqlCommand comand = new SqlCommand("select * from productos", connection);

        //            SqlDataAdapter adapter = new SqlDataAdapter(comand);

        //            DataTable dataTable = new DataTable();

        //            adapter.Fill(dataTable);

        //            dataGrid.ItemsSource = dataTable.DefaultView;


        //            connection.Close();
        //        }
        //        catch (Exception)
        //        {
        //            MessageBox.Show("Conexion pipipi");
        //        }

        //    }
        //}
        string connectionString = "Data Source=LAPTOP-0ORAP2JU\\SQLEXPRESS; Initial Catalog=Neptuno; User Id=userXavier; Password=123456";

        private void MostrarCliente_Click(object sender, RoutedEventArgs e)
        {
            List<Cliente> clientes = new List<Cliente>();
            try
            {
                //Cadena de conexión
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();


                //Comandos de TRANSACT SQL
                SqlCommand command = new SqlCommand("USP_GetCliente", connection);
                command.CommandType = CommandType.StoredProcedure;

                //CONECTADA
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string idCliente = reader.GetString("idCliente");
                    string NombreCompañia = reader.GetString("NombreCompañia");
                    string NombreContacto = reader.GetString("NombreContacto");
                    string CargoContacto = reader.GetString("CargoContacto");
                    string Ciudad = reader.GetString("Ciudad");

                    clientes.Add(new Cliente { idCliente = idCliente, NombreCompañia = NombreCompañia, NombreContacto = NombreContacto, CargoContacto = CargoContacto, Ciudad = Ciudad });

                }

                connection.Close();
                dataGrid.ItemsSource = clientes;

                //dgvDemo.DataSource = clientes;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                //throw;
            }
        }
        private void MostrarProveedor_Click(object sender, RoutedEventArgs e)
        {
            List<Proveedor> proveedores = new List<Proveedor>();
            try
            {
                //Cadena de conexión
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();


                //Comandos de TRANSACT SQL
                SqlCommand command = new SqlCommand("USP_GetProveedores", connection);
                command.CommandType = CommandType.StoredProcedure;

                //CONECTADA
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int idProveedor = reader.GetInt32("idProveedor");
                    string NombreCompañia = reader.GetString("NombreCompañia");
                    string cargocontacto = reader.GetString("cargocontacto");
                    string ciudad = reader.GetString("ciudad");
                    string codPostal = reader.GetString("codPostal");

                    proveedores.Add(new Proveedor { idProveedor = idProveedor, nombreCompañia = NombreCompañia, cargocontacto = cargocontacto, ciudad = ciudad, codPostal = codPostal });

                }

                connection.Close();
                dataGrid2.ItemsSource = proveedores;

                //dgvDemo.DataSource = clientes;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                //throw;
            }
        }
    }
}
