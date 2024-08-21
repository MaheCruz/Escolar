using Microsoft.AspNet.Identity;
using System;
using System.Configuration;
using System.Data.SqlClient;

namespace Escolar.Estudiantes
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarGrupoYPromedio();
                CargarPromedios();
                 CargarDatosEstudiante();
            }
        }

    private void CargarDatosEstudiante() //metodo para insertar los datos del estudiante
            {
                string userId = User.Identity.GetUserId();
                if (!string.IsNullOrEmpty(userId))
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        string query = "SELECT e.nombre + ' ' + e.paterno + ' ' + e.materno AS NombreCompleto, g.nombre AS Grupo, e.promGeneral " +
                                       "FROM estudiante e " +
                                       "JOIN grupoEstudiante ge ON e.matricula = ge.idEstudiante " +
                                       "JOIN grupo g ON ge.idGrupo = g.idGrupo " +
                                       "WHERE e.idUsuario = @userId";

                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@userId", userId);

                        try
                        {
                            connection.Open();
                            SqlDataReader reader = command.ExecuteReader();
                            if (reader.Read())
                            {
                                lblNombreEstudiante.Text = reader["NombreCompleto"].ToString();
                                lblGrupo.Text = reader["Grupo"].ToString();
                                lblPromedioGeneral.Text = reader["promGeneral"].ToString();
                            }
                            else
                            {
                                lblNombreEstudiante.Text = "Estudiante no encontrado";
                            }
                            connection.Close();
                        }
                        catch (Exception ex)
                        {
                            // Manejo de errores
                            lblNombreEstudiante.Text = "Error: " + ex.Message;
                        }
                    }
                }
            }
    private void CargarGrupoYPromedio() //Metodo para cargar grupo y promedio
        {
            string estudianteId = ObtenerIdEstudiante();

            if (!string.IsNullOrEmpty(estudianteId))
            {
                string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT g.nombre AS NombreGrupo, e.promGeneral AS PromedioGeneral " +
                                   "FROM grupo g " +
                                   "INNER JOIN grupoEstudiante ge ON g.idGrupo = ge.idGrupo " +
                                   "INNER JOIN estudiante e ON ge.idEstudiante = e.matricula " +
                                   "WHERE ge.idEstudiante = @idEstudiante";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@idEstudiante", estudianteId);

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            lblGrupo.Text = reader["NombreGrupo"].ToString();
                            lblPromedioGeneral.Text = reader["PromedioGeneral"] != DBNull.Value ? reader["PromedioGeneral"].ToString() : "N/A";
                        }
                        else
                        {
                            lblGrupo.Text = "No se encontró grupo asignado.";
                            lblPromedioGeneral.Text = "N/A";
                        }
                        connection.Close();
                    }
                    catch (Exception ex)
                    {
                        lblGrupo.Text = "Error al cargar el grupo: " + ex.Message;
                        lblPromedioGeneral.Text = "N/A";
                    }
                }
            }
        }

        private void CargarPromedios()
        {
            string estudianteId = ObtenerIdEstudiante();

            if (!string.IsNullOrEmpty(estudianteId))
            {
                string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT m.nombre AS NombreMateria, c.promedioMateria AS Promedio " +
                                   "FROM calificacion c INNER JOIN materia m ON c.idMateria = m.idMateria " +
                                   "WHERE c.idEstudiante = @idEstudiante";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@idEstudiante", estudianteId);

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            gvPromedios.DataSource = reader;
                            gvPromedios.DataBind();
                            lblNoPromedios.Visible = false;
                        }
                        else
                        {
                            gvPromedios.DataSource = null;
                            gvPromedios.DataBind();
                            lblNoPromedios.Visible = true;
                        }
                        connection.Close();
                    }
                    catch (Exception ex)
                    {
                        lblNoPromedios.Text = "Error al cargar los promedios: " + ex.Message;
                        lblNoPromedios.Visible = true;
                    }
                }
            }
        }

        private string ObtenerIdEstudiante()
        {
            string userId = User.Identity.GetUserId();

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
                            lblGrupo.Text = "No se encontró el ID del estudiante asociado al usuario.";
                            return string.Empty;
                        }
                    }
                    catch (Exception ex)
                    {
                        lblGrupo.Text = "Error al obtener el ID del estudiante: " + ex.Message;
                        return string.Empty;
                    }
                }
            }
            else
            {
                lblGrupo.Text = "No se encontró el ID del usuario en la sesión.";
                return string.Empty;
            }
        }
    }
}

