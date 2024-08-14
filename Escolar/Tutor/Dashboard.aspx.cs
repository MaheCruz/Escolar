using Microsoft.AspNet.Identity;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

namespace Escolar.Tutor
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarPromediosPorHijo();
                CargarReporteReciente();
                CargarNombreTutor();
            }
        }

        private void CargarNombreTutor()
        {
            string userId = User.Identity.GetUserId();

            if (!string.IsNullOrEmpty(userId))
            {
                string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT nombre + ' ' + paterno + ' ' + materno AS NombreCompleto FROM tutor WHERE idUsuario = @idUsuario";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@idUsuario", userId);

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            lblNombreTutor.Text = reader["NombreCompleto"].ToString();
                        }
                        else
                        {
                            lblNombreTutor.Text = "Tutor no encontrado";
                        }

                        connection.Close();
                    }
                    catch (Exception ex)
                    {
                        lblNombreTutor.Text = "Error al cargar el nombre del tutor: " + ex.Message;
                    }
                }
            }
            else
            {
                lblNombreTutor.Text = "Usuario no encontrado";
            }
        }
        private void CargarPromediosPorHijo()
        {
            string tutorId = ObtenerIdTutor();

            if (!string.IsNullOrEmpty(tutorId))
            {
                string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT nombre + ' ' + paterno + ' ' + materno AS NombreCompleto, promGeneral FROM estudiante WHERE idTutor = @idTutor";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@idTutor", tutorId);

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        lblPromedioGeneral.Text = string.Empty;

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                string nombreCompleto = reader["NombreCompleto"].ToString();
                                string promedio = reader["promGeneral"] != DBNull.Value ? Convert.ToDouble(reader["promGeneral"]).ToString("F2") : "N/A";
                                lblPromedioGeneral.Text += $"{nombreCompleto}: {promedio}<br/>";
                            }
                        }
                        else
                        {
                            lblPromedioGeneral.Text = "No se encontraron promedios.";
                        }

                        connection.Close();
                    }
                    catch (Exception ex)
                    {
                        lblPromedioGeneral.Text = "Error al cargar el promedio: " + ex.Message;
                    }
                }
            }
            else
            {
                lblPromedioGeneral.Text = "No se encontró el ID del tutor.";
            }
        }

        private void CargarReporteReciente()
        {
            string tutorId = ObtenerIdTutor();

            if (!string.IsNullOrEmpty(tutorId))
            {
                string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"
                        SELECT TOP 1 r.tipo, r.fecha, r.detalle, 
                               e.nombre + ' ' + e.paterno + ' ' + e.materno AS NombreCompleto
                        FROM reporte r
                        INNER JOIN estudiante e ON r.idEstudiante = e.matricula
                        WHERE e.idTutor = @idTutor
                        ORDER BY r.fecha DESC";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@idTutor", tutorId);

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            lblTipo.Text = reader["tipo"].ToString();
                            lblFecha.Text = Convert.ToDateTime(reader["fecha"]).ToString("dd/MM/yyyy");
                            lblDetalle.Text = reader["detalle"].ToString();
                            lblNombreCompleto.Text = reader["NombreCompleto"].ToString();
                        }
                        else
                        {
                            lblTipo.Text = "No hay reportes recientes.";
                            lblFecha.Text = "";
                            lblDetalle.Text = "";
                            lblNombreCompleto.Text = "";
                        }
                        connection.Close();
                    }
                    catch (Exception ex)
                    {
                        lblTipo.Text = "Error al cargar el reporte: " + ex.Message;
                    }
                }
            }
        }

        private string ObtenerIdTutor()
        {
            string userId = User.Identity.GetUserId();  // Obtén el ID del usuario que ha iniciado sesión

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
                            return tutorId;
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write("Error al obtener el ID del tutor: " + ex.Message);
                    }
                }
            }
            return null;
        }
    }
}
