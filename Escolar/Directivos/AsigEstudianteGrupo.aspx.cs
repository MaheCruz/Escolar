using System;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Escolar.Directivos
{
    public partial class AsigEstudianteGrupo : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LimpiarMensajes();
            }
        }

        protected void btnAsignarEstudiante_Click(object sender, EventArgs e)
        {
            LimpiarMensajes();
            try
            {
                string idEstudiante = ddlEstudiantes.SelectedValue;
                string idGrupo = ddlGrupos.SelectedValue;

                // Verificar si el estudiante ya está asignado a un grupo
                using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    conn.Open();
                    string queryCheck = "SELECT COUNT(*) FROM grupoEstudiante WHERE idEstudiante = @idEstudiante";
                    SqlCommand cmdCheck = new SqlCommand(queryCheck, conn);
                    cmdCheck.Parameters.AddWithValue("@idEstudiante", idEstudiante);
                    int count = (int)cmdCheck.ExecuteScalar();

                    if (count > 0)
                    {
                        MostrarMensajeError("El estudiante ya está asignado a un grupo.");
                        return;
                    }

                    // Insertar la asignación
                    string query = "INSERT INTO grupoEstudiante (idEstudiante, idGrupo, promedio) VALUES (@idEstudiante, @idGrupo, NULL)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@idEstudiante", idEstudiante);
                    cmd.Parameters.AddWithValue("@idGrupo", idGrupo);
                    cmd.ExecuteNonQuery();
                }

                GVGrupoEstudiante.DataBind();
                MostrarMensajeExito("Estudiante asignado al grupo con éxito.");
            }
            catch (Exception ex)
            {
                MostrarMensajeError($"Error al asignar el estudiante al grupo: {ex.Message}");
            }
        }

        protected void btnModificarAsignacion_Click(object sender, EventArgs e)
        {
            LimpiarMensajes();
            try
            {
                string idEstudianteOriginal = hfIdEstudianteOriginal.Value;
                string idGrupoOriginal = hfIdGrupoOriginal.Value;
                string idEstudianteNuevo = ddlEstudiantes.SelectedValue;
                string idGrupoNuevo = ddlGrupos.SelectedValue;

                // Verificar si la nueva asignación ya existe
                using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    conn.Open();
                    string queryCheck = "SELECT COUNT(*) FROM grupoEstudiante WHERE idEstudiante = @idEstudianteNuevo AND idGrupo = @idGrupoNuevo";
                    SqlCommand cmdCheck = new SqlCommand(queryCheck, conn);
                    cmdCheck.Parameters.AddWithValue("@idEstudianteNuevo", idEstudianteNuevo);
                    cmdCheck.Parameters.AddWithValue("@idGrupoNuevo", idGrupoNuevo);
                    int count = (int)cmdCheck.ExecuteScalar();

                    if (count > 0 && (idEstudianteNuevo != idEstudianteOriginal || idGrupoNuevo != idGrupoOriginal))
                    {
                        MostrarMensajeError("El estudiante ya está asignado a otro grupo.");
                        return;
                    }

                    // Actualizar la asignación
                    string query = "UPDATE grupoEstudiante SET idEstudiante = @idEstudianteNuevo, idGrupo = @idGrupoNuevo WHERE idEstudiante = @idEstudianteOriginal AND idGrupo = @idGrupoOriginal";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@idEstudianteNuevo", idEstudianteNuevo);
                    cmd.Parameters.AddWithValue("@idGrupoNuevo", idGrupoNuevo);
                    cmd.Parameters.AddWithValue("@idEstudianteOriginal", idEstudianteOriginal);
                    cmd.Parameters.AddWithValue("@idGrupoOriginal", idGrupoOriginal);
                    cmd.ExecuteNonQuery();
                }

                GVGrupoEstudiante.DataBind();
                MostrarMensajeExito("Asignación modificada con éxito.");
            }
            catch (Exception ex)
            {
                MostrarMensajeError($"Error al modificar la asignación: {ex.Message}");
            }
        }

        protected void btnEliminarAsignacion_Click(object sender, EventArgs e)
        {
            LimpiarMensajes();
            var args = ((Button)sender).CommandArgument.Split(',');
            string idEstudiante = args[0];
            string idGrupo = args[1];

            try
            {
                // Conexión y ejecución
                using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM grupoEstudiante WHERE idEstudiante = @idEstudiante AND idGrupo = @idGrupo";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@idEstudiante", idEstudiante);
                    cmd.Parameters.AddWithValue("@idGrupo", idGrupo);
                    cmd.ExecuteNonQuery();
                }

                GVGrupoEstudiante.DataBind();
                MostrarMensajeExito("Asignación eliminada con éxito.");
            }
            catch (Exception ex)
            {
                MostrarMensajeError($"Error al eliminar la asignación: {ex.Message}");
            }
        }

        protected void GVGrupoEstudiante_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Lógica para seleccionar una asignación y rellenar el formulario para modificación
            GridViewRow row = GVGrupoEstudiante.SelectedRow;
            hfIdEstudianteOriginal.Value = row.Cells[1].Text;
            hfIdGrupoOriginal.Value = row.Cells[2].Text;
            ddlEstudiantes.SelectedValue = row.Cells[1].Text;
            ddlGrupos.SelectedValue = row.Cells[2].Text;
        }

        private void MostrarMensajeExito(string mensaje)
        {
            lblMensajeExito.Text = mensaje;
            PanelExito.Visible = true;
        }

        private void MostrarMensajeError(string mensaje)
        {
            lblMensajeError.Text = mensaje;
            PanelError.Visible = true;
        }

        private void LimpiarMensajes()
        {
            PanelExito.Visible = false;
            PanelError.Visible = false;
        }
    }
}
