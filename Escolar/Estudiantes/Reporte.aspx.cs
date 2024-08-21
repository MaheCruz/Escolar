using Microsoft.AspNet.Identity;
using System;
using System.Configuration;
using System.Data.SqlClient;

namespace Escolar.Estudiantes
{
    public partial class Reporte : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarReportes();
            }
        }

        private void CargarReportes()
        {
            string estudianteId = ObtenerIdEstudiante();

            if (!string.IsNullOrEmpty(estudianteId))
            {
                MostrarReportes(estudianteId);
            }
        }
        //Metodo mostrar reportes
        private void MostrarReportes(string matricula)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT tipo, fecha, detalle FROM reporte WHERE idEstudiante = @idEstudiante ORDER BY fecha DESC";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@idEstudiante", matricula);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        gvReportes.DataSource = reader;
                        gvReportes.DataBind();
                        lblNoReportes.Visible = false;
                    }
                    else
                    {
                        gvReportes.DataSource = null;
                        gvReportes.DataBind();
                        lblNoReportes.Visible = true;
                    }

                    connection.Close();
                }
                catch (Exception ex)
                {
                    lblNoReportes.Text = "Error al cargar los reportes: " + ex.Message;
                    lblNoReportes.Visible = true;
                }
            }
        }

        private string ObtenerIdEstudiante()
        {
            string userId = User.Identity.GetUserId();  // Obtén el ID del usuario que ha iniciado sesión

            if (!string.IsNullOrEmpty(userId))
            {
                string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT matricula FROM estudiante WHERE idUsuario = @idUsuario";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@idUsuario", userId);

                    try
                    {
                        connection.Open();
                        string estudianteId = command.ExecuteScalar()?.ToString();
                        connection.Close();

                        if (!string.IsNullOrEmpty(estudianteId))
                        {
                            return estudianteId;
                        }
                        else
                        {
                            lblNoReportes.Text = "No se encontró el ID del estudiante asociado al usuario.";
                            lblNoReportes.Visible = true;
                            return string.Empty;
                        }
                    }
                    catch (Exception ex)
                    {
                        lblNoReportes.Text = "Error al obtener el ID del estudiante: " + ex.Message;
                        lblNoReportes.Visible = true;
                        return string.Empty;
                    }
                }
            }
            else
            {
                lblNoReportes.Text = "No se encontró el ID del usuario en la sesión."; //mensaje de error
                lblNoReportes.Visible = true;
                return string.Empty;
            }
        }
    }
}
