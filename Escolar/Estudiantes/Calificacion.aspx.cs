using Microsoft.AspNet.Identity;
using System;
using System.Configuration;
using System.Data.SqlClient;

namespace Escolar.Estudiantes
{
    public partial class Calificacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCalificaciones();
            }
        }

        private void CargarCalificaciones()
        {
            string estudianteId = ObtenerIdEstudiante();

            if (!string.IsNullOrEmpty(estudianteId))
            {
                CargarCalificacionesEstudiante(estudianteId);
                CalcularPromedioGeneral(estudianteId);
            }
        }

        private void CargarCalificacionesEstudiante(string matricula)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "EXEC VerCalificacionesEstudiante @idEstudiante";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@idEstudiante", matricula);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        gvCalificaciones.DataSource = reader;
                        gvCalificaciones.DataBind();
                    }
                    else
                    {
                        lblPromedioGeneral.Text = "No se encontraron calificaciones.";
                    }

                    connection.Close();
                }
                catch (Exception ex)
                {
                    lblPromedioGeneral.Text = "Error al cargar calificaciones: " + ex.Message;
                }
            }
        }

        private void CalcularPromedioGeneral(string estudianteId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT promGeneral FROM estudiante WHERE matricula = @idEstudiante";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@idEstudiante", estudianteId);

                try
                {
                    connection.Open();
                    object resultado = command.ExecuteScalar();
                    connection.Close();

                    lblPromedioGeneral.Text = resultado != null
                        ? "Promedio General: " + resultado.ToString()
                        : "Promedio General: N/A";
                }
                catch (Exception ex)
                {
                    lblPromedioGeneral.Text = "Error: " + ex.Message;
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
                            lblPromedioGeneral.Text = "No se encontró el ID del estudiante asociado al usuario.";
                            return string.Empty;
                        }
                    }
                    catch (Exception ex)
                    {
                        lblPromedioGeneral.Text = "Error al obtener el ID del estudiante: " + ex.Message;
                        return string.Empty;
                    }
                }
            }
            else
            {
                lblPromedioGeneral.Text = "No se encontró el ID del usuario en la sesión.";
                return string.Empty;
            }
        }
    }
}
