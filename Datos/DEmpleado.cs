using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp3;

namespace Datos
{
    public class DEmpleado
    {
        public List<Empleado> ListarEmpleados(string nombre)
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
                command.Parameters.AddWithValue("@Nombre", nombre);

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
                //dataGrid.ItemsSource = empleados;
                
                //dgvDemo.DataSource = clientes;


            }
            catch (Exception ex)
            {
               

                //throw;
            }

            return empleados;

        }
    }
}
