using Microsoft.AspNet.Identity;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace Escolar.Tutor
{
    public partial class Reportes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarEstudiantes();
            }
        }

        private void CargarEstudiantes()
        {
            string tutorId = ObtenerIdTutor();

            if (!string.IsNullOrEmpty(tutorId))
            {
                string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT matricula, nombre + ' ' + paterno + ' ' + materno AS NombreCompleto FROM estudiante WHERE idTutor = @idTutor";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@idTutor", tutorId);

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            ddlEstudiantes.DataSource = reader;
                            ddlEstudiantes.DataTextField = "NombreCompleto";
                            ddlEstudiantes.DataValueField = "matricula";
                            ddlEstudiantes.DataBind();
                        }
                        else
                        {
                            lblError.Text = "No se encontraron estudiantes asignados a este tutor.";
                        }
                        connection.Close();
                    }
                    catch (Exception ex)
                    {
                        lblError.Text = "Error al cargar estudiantes: " + ex.Message;
                    }
                }
            }
        }

        protected void ddlEstudiantes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlEstudiantes.SelectedValue != "")
            {
                CargarReportesEstudiante(ddlEstudiantes.SelectedValue);
            }
        }

        private void CargarReportesEstudiante(string matricula)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT tipo, detalle, fecha FROM reporte WHERE idEstudiante = @idEstudiante ORDER BY fecha DESC";
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
                        lblError.Text = ""; // Limpiar mensaje de error si se cargan los datos correctamente
                    }
                    else
                    {
                        gvReportes.DataSource = null;
                        gvReportes.DataBind();
                        lblError.Text = "El estudiante no tiene reportes asignados.";
                    }

                    connection.Close();
                }
                catch (Exception ex)
                {
                    lblError.Text = "Error al cargar reportes: " + ex.Message;
                }
            }
        }

        private string ObtenerIdTutor()
        {
            string userId = User.Identity.GetUserId();

            if (!string.IsNullOrEmpty(userId))
            {
                string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT idTutor FROM tutor WHERE idUsuario = @idUsuario";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@idUsuario", userId);

                    try
                    {
                        connection.Open();
                        string tutorId = command.ExecuteScalar()?.ToString();
                        connection.Close();

                        if (!string.IsNullOrEmpty(tutorId))
                        {
                            Session["TutorID"] = tutorId;
                            return tutorId;
                        }
                        else
                        {
                            lblError.Text = "No se encontró el ID del tutor asociado al usuario.";
                            return string.Empty;
                        }
                    }
                    catch (Exception ex)
                    {
                        lblError.Text = "Error al obtener el ID del tutor: " + ex.Message;
                        return string.Empty;
                    }
                }
            }
            else
            {
                lblError.Text = "No se encontró el ID del usuario en la sesión.";
                return string.Empty;
            }
        }
    }
}
