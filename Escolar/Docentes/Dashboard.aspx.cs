using Microsoft.AspNet.Identity;
using System;
using System.Configuration;
using System.Data.SqlClient;

namespace Escolar.Docentes
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarNombreDocente();
                // Puedes agregar lógica adicional aquí si es necesario
            }
        }

        private void CargarNombreDocente()
        {
            string userId = User.Identity.GetUserId();  // Obtén el ID del usuario que ha iniciado sesión

            if (!string.IsNullOrEmpty(userId))
            {
                string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT nombre + ' ' + paterno + ' ' + materno AS NombreCompleto FROM docente WHERE idUsuario = @idUsuario";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@idUsuario", userId);

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            lblNombreDocente.Text = reader["NombreCompleto"].ToString();
                        }
                        else
                        {
                            lblNombreDocente.Text = "Docente no encontrado";
                        }

                        connection.Close();
                    }
                    catch (Exception ex)
                    {
                        lblNombreDocente.Text = "Error al cargar el nombre del docente: " + ex.Message;
                    }
                }
            }
            else
            {
                lblNombreDocente.Text = "Usuario no encontrado";
            }
        }
    }
}
