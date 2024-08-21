using Microsoft.AspNet.Identity;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace Escolar.Tutor
{
    public partial class Calificaciones : System.Web.UI.Page
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
                CargarCalificacionesEstudiante(ddlEstudiantes.SelectedValue);
                CalcularPromedioGeneral(ddlEstudiantes.SelectedValue);
            }
        }
        //Metodo para insertar las calificaciones de los estudiantes
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
                        lblError.Text = ""; // Limpiar el mensaje de error si los datos se cargan correctamente
                    }
                    else
                    {
                        gvCalificaciones.DataSource = null;
                        gvCalificaciones.DataBind();
                        lblError.Text = "El estudiante aún no tiene calificaciones asignadas.";
                    }

                    connection.Close();
                }
                catch (Exception ex) //Mensaje de error al cargar las calificaciones
                {
                    lblError.Text = "Error al cargar calificaciones: " + ex.Message;
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

                    lblPromedioGeneral.Text = resultado != null ? "Promedio General: " + resultado.ToString() : "Promedio General: N/A";
                }
                catch (Exception ex)
                {
                    lblError.Text = "Error al obtener el promedio general: " + ex.Message;
                }
            }
        }

        private string ObtenerIdTutor() //Metodo obtener id del tutor
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

        protected void btnRecargar_Click(object sender, EventArgs e)
        {
            if (ddlEstudiantes.SelectedValue != "")
            {
                CargarCalificacionesEstudiante(ddlEstudiantes.SelectedValue);
                CalcularPromedioGeneral(ddlEstudiantes.SelectedValue);
            }
        }

        protected System.Void btnRecargar_Click(System.Object sender, System.EventArgs e)
        {

        }
    }
}
