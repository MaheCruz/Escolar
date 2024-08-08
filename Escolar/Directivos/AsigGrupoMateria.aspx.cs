using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Escolar.Directivos
{
    public partial class AsigGrupoMateria : System.Web.UI.Page
    {
            protected void Page_Load(object sender, EventArgs e)
            {
                if (!IsPostBack)
                {
                    LimpiarMensajes();
                    CargarDocentes();
                }
            }

            private void CargarDocentes()
            {
                ddlDocente.DataBind();
            }

            protected void btnCrearGrupo_Click(object sender, EventArgs e)
            {
                LimpiarMensajes();
                try
                {
                    // Lógica para crear grupo
                    string idGrupo = txtIdGrupo.Text.Trim();
                    string nombre = txtNombreGrupo.Text.Trim();
                    string descripcion = txtDescripcionGrupo.Text.Trim();
                    DateTime inicio = DateTime.Parse(txtInicioGrupo.Text);
                    DateTime fin = DateTime.Parse(txtFinGrupo.Text);

                    // Conexión y ejecución
                    using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                    {
                        conn.Open();
                        string query = "INSERT INTO grupo (idGrupo, nombre, descripcion, inicio, fin) VALUES (@idGrupo, @nombre, @descripcion, @inicio, @fin)";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@idGrupo", idGrupo);
                        cmd.Parameters.AddWithValue("@nombre", nombre);
                        cmd.Parameters.AddWithValue("@descripcion", descripcion);
                        cmd.Parameters.AddWithValue("@inicio", inicio);
                        cmd.Parameters.AddWithValue("@fin", fin);
                        cmd.ExecuteNonQuery();
                    }

                    GVGrupos.DataBind();
                    MostrarMensajeExito("Grupo creado con éxito.");
                }
                catch (Exception ex)
                {
                    MostrarMensajeError($"Error al crear el grupo: {ex.Message}");
                }
            }

            protected void btnModificarGrupo_Click(object sender, EventArgs e)
            {
                LimpiarMensajes();
                try
                {
                    // Lógica para modificar grupo
                    string idGrupo = txtIdGrupo.Text.Trim();
                    string nombre = txtNombreGrupo.Text.Trim();
                    string descripcion = txtDescripcionGrupo.Text.Trim();
                    DateTime inicio = DateTime.Parse(txtInicioGrupo.Text);
                    DateTime fin = DateTime.Parse(txtFinGrupo.Text);

                    // Conexión y ejecución
                    using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                    {
                        conn.Open();
                        string query = "UPDATE grupo SET nombre = @nombre, descripcion = @descripcion, inicio = @inicio, fin = @fin WHERE idGrupo = @idGrupo";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@idGrupo", idGrupo);
                        cmd.Parameters.AddWithValue("@nombre", nombre);
                        cmd.Parameters.AddWithValue("@descripcion", descripcion);
                        cmd.Parameters.AddWithValue("@inicio", inicio);
                        cmd.Parameters.AddWithValue("@fin", fin);
                        cmd.ExecuteNonQuery();
                    }

                    GVGrupos.DataBind();
                    MostrarMensajeExito("Grupo modificado con éxito.");
                }
                catch (Exception ex)
                {
                    MostrarMensajeError($"Error al modificar el grupo: {ex.Message}");
                }
            }

            protected void btnEliminarGrupo_Click(object sender, EventArgs e)
            {
                LimpiarMensajes();
                try
                {
                    // Lógica para eliminar grupo
                    string idGrupo = txtIdGrupo.Text.Trim();

                    // Conexión y ejecución
                    using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                    {
                        conn.Open();
                        string query = "DELETE FROM grupo WHERE idGrupo = @idGrupo";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@idGrupo", idGrupo);
                        cmd.ExecuteNonQuery();
                    }

                    GVGrupos.DataBind();
                    MostrarMensajeExito("Grupo eliminado con éxito.");
                }
                catch (Exception ex)
                {
                    MostrarMensajeError($"Error al eliminar el grupo: {ex.Message}");
                }
            }

            protected void btnCrearMateria_Click(object sender, EventArgs e)
            {
                LimpiarMensajes();
                try
                {
                    // Lógica para crear materia
                    string idMateria = txtIdMateria.Text.Trim();
                    string nombre = txtNombreMateria.Text.Trim();
                    string descripcion = txtDescripcionMateria.Text.Trim();
                    string idDocente = ddlDocente.SelectedValue;

                    // Conexión y ejecución
                    using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                    {
                        conn.Open();
                        string query = "INSERT INTO materia (idMateria, nombre, descripcion, idDocente) VALUES (@idMateria, @nombre, @descripcion, @idDocente)";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@idMateria", idMateria);
                        cmd.Parameters.AddWithValue("@nombre", nombre);
                        cmd.Parameters.AddWithValue("@descripcion", descripcion);
                        cmd.Parameters.AddWithValue("@idDocente", idDocente);
                        cmd.ExecuteNonQuery();
                    }

                    GVMaterias.DataBind();
                    MostrarMensajeExito("Materia creada con éxito.");
                }
                catch (Exception ex)
                {
                    MostrarMensajeError($"Error al crear la materia: {ex.Message}");
                }
            }

            protected void btnModificarMateria_Click(object sender, EventArgs e)
            {
                LimpiarMensajes();
                try
                {
                    // Lógica para modificar materia
                    string idMateria = txtIdMateria.Text.Trim();
                    string nombre = txtNombreMateria.Text.Trim();
                    string descripcion = txtDescripcionMateria.Text.Trim();
                    string idDocente = ddlDocente.SelectedValue;

                    // Conexión y ejecución
                    using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                    {
                        conn.Open();
                        string query = "UPDATE materia SET nombre = @nombre, descripcion = @descripcion, idDocente = @idDocente WHERE idMateria = @idMateria";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@idMateria", idMateria);
                        cmd.Parameters.AddWithValue("@nombre", nombre);
                        cmd.Parameters.AddWithValue("@descripcion", descripcion);
                        cmd.Parameters.AddWithValue("@idDocente", idDocente);
                        cmd.ExecuteNonQuery();
                    }

                    GVMaterias.DataBind();
                    MostrarMensajeExito("Materia modificada con éxito.");
                }
                catch (Exception ex)
                {
                    MostrarMensajeError($"Error al modificar la materia: {ex.Message}");
                }
            }

            protected void btnEliminarMateria_Click(object sender, EventArgs e)
            {
                LimpiarMensajes();
                try
                {
                    // Lógica para eliminar materia
                    string idMateria = txtIdMateria.Text.Trim();

                    // Conexión y ejecución
                    using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                    {
                        conn.Open();
                        string query = "DELETE FROM materia WHERE idMateria = @idMateria";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@idMateria", idMateria);
                        cmd.ExecuteNonQuery();
                    }

                    GVMaterias.DataBind();
                    MostrarMensajeExito("Materia eliminada con éxito.");
                }
                catch (Exception ex)
                {
                    MostrarMensajeError($"Error al eliminar la materia: {ex.Message}");
                }
            }

            protected void btnAsignarMateria_Click(object sender, EventArgs e)
            {
                LimpiarMensajes();
                try
                {
                    string idGrupo = lblGrupoSeleccionado.Text.Trim();
                    string idMateria = lblMateriaSeleccionada.Text.Trim();

                    // Verificar si la asignación ya existe
                    using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                    {
                        conn.Open();
                        string queryCheck = "SELECT COUNT(*) FROM grupoMateria WHERE idGrupo = @idGrupo AND idMateria = @idMateria";
                        SqlCommand cmdCheck = new SqlCommand(queryCheck, conn);
                        cmdCheck.Parameters.AddWithValue("@idGrupo", idGrupo);
                        cmdCheck.Parameters.AddWithValue("@idMateria", idMateria);
                        int count = (int)cmdCheck.ExecuteScalar();

                        if (count > 0)
                        {
                            MostrarMensajeError("La materia ya está asignada a este grupo.");
                            return;
                        }

                        // Verificar que el idGrupo y el idMateria existen
                        string queryGrupo = "SELECT COUNT(*) FROM grupo WHERE idGrupo = @idGrupo";
                        SqlCommand cmdGrupo = new SqlCommand(queryGrupo, conn);
                        cmdGrupo.Parameters.AddWithValue("@idGrupo", idGrupo);
                        int grupoExiste = (int)cmdGrupo.ExecuteScalar();

                        string queryMateria = "SELECT COUNT(*) FROM materia WHERE idMateria = @idMateria";
                        SqlCommand cmdMateria = new SqlCommand(queryMateria, conn);
                        cmdMateria.Parameters.AddWithValue("@idMateria", idMateria);
                        int materiaExiste = (int)cmdMateria.ExecuteScalar();

                        if (grupoExiste == 0 || materiaExiste == 0)
                        {
                            MostrarMensajeError("El grupo o la materia no existen.");
                            return;
                        }

                        // Insertar la asignación
                        string query = "INSERT INTO grupoMateria (idGrupo, idMateria) VALUES (@idGrupo, @idMateria)";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@idGrupo", idGrupo);
                        cmd.Parameters.AddWithValue("@idMateria", idMateria);
                        cmd.ExecuteNonQuery();
                    }

                    GVGrupoMateria.DataBind();
                    MostrarMensajeExito("Materia asignada al grupo con éxito.");
                }
                catch (Exception ex)
                {
                    MostrarMensajeError($"Error al asignar la materia al grupo: {ex.Message}");
                }
            }

            protected void GVGrupos_SelectedIndexChanged(object sender, EventArgs e)
            {
                // Lógica para seleccionar grupo y rellenar el formulario
                GridViewRow row = GVGrupos.SelectedRow;
                txtIdGrupo.Text = row.Cells[1].Text;
                txtNombreGrupo.Text = row.Cells[2].Text;
                txtDescripcionGrupo.Text = row.Cells[3].Text;
                txtInicioGrupo.Text = DateTime.Parse(row.Cells[4].Text).ToString("yyyy-MM-dd");
                txtFinGrupo.Text = DateTime.Parse(row.Cells[5].Text).ToString("yyyy-MM-dd");
                lblGrupoSeleccionado.Text = row.Cells[1].Text; // Actualizado para mostrar el ID en lugar del nombre
            }

            protected void GVMaterias_SelectedIndexChanged(object sender, EventArgs e)
            {
                // Lógica para seleccionar materia y rellenar el formulario
                GridViewRow row = GVMaterias.SelectedRow;
                txtIdMateria.Text = row.Cells[1].Text;
                txtNombreMateria.Text = row.Cells[2].Text;
                txtDescripcionMateria.Text = row.Cells[3].Text;
                string idDocente = row.Cells[4].Text;
                if (ddlDocente.Items.FindByValue(idDocente) != null)
                {
                    ddlDocente.SelectedValue = idDocente;
                }
                lblMateriaSeleccionada.Text = row.Cells[1].Text; // Actualizado para mostrar el ID en lugar del nombre
            }

            protected void btnEliminarAsignacion_Click(object sender, EventArgs e)
            {
                LimpiarMensajes();
                var args = ((Button)sender).CommandArgument.Split(',');
                string idGrupo = args[0];
                string idMateria = args[1];

                try
                {
                    // Conexión y ejecución
                    using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                    {
                        conn.Open();
                        string query = "DELETE FROM grupoMateria WHERE idGrupo = @idGrupo AND idMateria = @idMateria";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@idGrupo", idGrupo);
                        cmd.Parameters.AddWithValue("@idMateria", idMateria);
                        cmd.ExecuteNonQuery();
                    }

                    GVGrupoMateria.DataBind();
                    MostrarMensajeExito("Asignación eliminada con éxito.");
                }
                catch (Exception ex)
                {
                    MostrarMensajeError($"Error al eliminar la asignación: {ex.Message}");
                }
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
