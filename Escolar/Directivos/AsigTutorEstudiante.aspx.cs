using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Escolar.Directivos
{
    public partial class AsigTutorEstudiante : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LimpiarMensajes();
            }
        }

        protected void btnAsignar_Click(object sender, EventArgs e)
        {
            LimpiarMensajes();
            string idEstudiante = ddlEstudiantes.SelectedValue;
            string idTutor = ddlTutores.SelectedValue;

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE estudiante SET idTutor = @IdTutor WHERE matricula = @IdEstudiante";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@IdTutor", idTutor);
                command.Parameters.AddWithValue("@IdEstudiante", idEstudiante);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    connection.Close();

                    if (rowsAffected > 0)
                    {
                        MostrarMensajeExito("Tutor asignado correctamente.");
                        gvRelaciones.DataBind(); // Actualizar la tabla de relaciones
                    }
                    else
                    {
                        MostrarMensajeError("No se encontró el estudiante o ya tiene un tutor asignado.");
                    }
                }
                catch (Exception ex)
                {
                    MostrarMensajeError("Error al asignar el tutor: " + ex.Message);
                }
            }
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            string idEstudiante = ((Button)sender).CommandArgument;
            ddlEstudiantes.SelectedValue = idEstudiante;

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT idTutor FROM estudiante WHERE matricula = @IdEstudiante";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@IdEstudiante", idEstudiante);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    connection.Close();

                    if (result != null)
                    {
                        ddlTutores.SelectedValue = result.ToString();
                    }
                }
                catch (Exception ex)
                {
                    MostrarMensajeError("Error al cargar el tutor del estudiante: " + ex.Message);
                }
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            LimpiarMensajes();
            string idEstudiante = ((Button)sender).CommandArgument;

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE estudiante SET idTutor = NULL WHERE matricula = @IdEstudiante";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@IdEstudiante", idEstudiante);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    connection.Close();

                    if (rowsAffected > 0)
                    {
                        MostrarMensajeExito("Relación de tutor eliminada correctamente.");
                        gvRelaciones.DataBind(); // Actualizar la tabla de relaciones
                    }
                    else
                    {
                        MostrarMensajeError("No se encontró el estudiante o no tiene un tutor asignado.");
                    }
                }
                catch (Exception ex)
                {
                    MostrarMensajeError("Error al eliminar la relación de tutor: " + ex.Message);
                }
            }
        }

        private void LimpiarMensajes()
        {
            PanelExito.Visible = false;
            PanelError.Visible = false;
        }

        private void MostrarMensajeExito(string mensaje)
        {
            PanelExito.Visible = true;
        }

        private void MostrarMensajeError(string mensaje)
        {
            PanelError.Visible = true;
        }
    }
}