using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Escolar.Directivos
{
    public partial class CRUDdocente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LimpiarMensajes();
            }
        }

        protected void btnInsertar_Click(object sender, EventArgs e)
        {
            LimpiarMensajes();
            if (ValidarCampos())
            {
                string idDocente = txtIdDocente.Text.Trim();
                string idUsuario = ddlUsuarios.SelectedValue;
                string nombre = txtNombre.Text.Trim();
                string paterno = txtPaterno.Text.Trim();
                string materno = txtMaterno.Text.Trim();
                string telefono = txtTelefono.Text.Trim();
                string direccion = txtDireccion.Text.Trim();

                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Verificar si el idUsuario ya existe
                    string checkQuery = "SELECT COUNT(*) FROM docente WHERE idUsuario = @idUsuario";
                    SqlCommand checkCommand = new SqlCommand(checkQuery, connection);
                    checkCommand.Parameters.AddWithValue("@idUsuario", idUsuario);

                    try
                    {
                        connection.Open();
                        int userExists = (int)checkCommand.ExecuteScalar();
                        if (userExists > 0)
                        {
                            MostrarMensajeError("El usuario ya tiene un registro en la tabla de docentes.");
                            connection.Close();
                            return;
                        }

                        string query = "INSERT INTO docente (idDocente, idUsuario, nombre, paterno, materno, telefono, direccion) " +
                                       "VALUES (@idDocente, @idUsuario, @nombre, @paterno, @materno, @telefono, @direccion)";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@idDocente", idDocente);
                        command.Parameters.AddWithValue("@idUsuario", idUsuario);
                        command.Parameters.AddWithValue("@nombre", nombre);
                        command.Parameters.AddWithValue("@paterno", paterno);
                        command.Parameters.AddWithValue("@materno", materno);
                        command.Parameters.AddWithValue("@telefono", telefono);
                        command.Parameters.AddWithValue("@direccion", direccion);

                        command.ExecuteNonQuery();
                        connection.Close();

                        MostrarMensajeExito("Docente insertado correctamente.");
                        GVDocentes.DataBind();
                    }
                    catch (Exception ex)
                    {
                        MostrarMensajeError("Error al insertar el docente: " + ex.Message);
                    }
                }
            }
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            LimpiarMensajes();
            if (ValidarCampos())
            {
                string idDocente = txtIdDocente.Text.Trim();
                string nombre = txtNombre.Text.Trim();
                string paterno = txtPaterno.Text.Trim();
                string materno = txtMaterno.Text.Trim();
                string telefono = txtTelefono.Text.Trim();
                string direccion = txtDireccion.Text.Trim();

                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE docente SET nombre = @nombre, paterno = @paterno, materno = @materno, " +
                                   "telefono = @telefono, direccion = @direccion " +
                                   "WHERE idDocente = @idDocente";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@idDocente", idDocente);
                    command.Parameters.AddWithValue("@nombre", nombre);
                    command.Parameters.AddWithValue("@paterno", paterno);
                    command.Parameters.AddWithValue("@materno", materno);
                    command.Parameters.AddWithValue("@telefono", telefono);
                    command.Parameters.AddWithValue("@direccion", direccion);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();

                        MostrarMensajeExito("Docente modificado correctamente.");
                        GVDocentes.DataBind();
                    }
                    catch (Exception ex)
                    {
                        MostrarMensajeError("Error al modificar el docente: " + ex.Message);
                    }
                }
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            LimpiarMensajes();
            string idDocente = txtIdDocente.Text.Trim();

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM docente WHERE idDocente = @idDocente";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@idDocente", idDocente);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();

                    MostrarMensajeExito("Docente eliminado correctamente.");
                    GVDocentes.DataBind();
                }
                catch (Exception ex)
                {
                    MostrarMensajeError("Error al eliminar el docente: " + ex.Message);
                }
            }
        }

        protected void GVDocentes_SelectedIndexChanged(object sender, EventArgs e)
        {
            LimpiarMensajes();
            GridViewRow row = GVDocentes.SelectedRow;
            txtIdDocente.Text = row.Cells[1].Text;
            ddlUsuarios.SelectedValue = row.Cells[2].Text;
            txtNombre.Text = row.Cells[3].Text;
            txtPaterno.Text = row.Cells[4].Text;
            txtMaterno.Text = row.Cells[5].Text;
            txtTelefono.Text = row.Cells[6].Text;
            txtDireccion.Text = row.Cells[7].Text;
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtIdDocente.Text))
            {
                MostrarMensajeError("El ID del docente es obligatorio.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || !System.Text.RegularExpressions.Regex.IsMatch(txtNombre.Text, @"^[a-zA-Z\s]+$"))
            {
                MostrarMensajeError("El nombre es obligatorio y solo puede contener letras.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtPaterno.Text) || !System.Text.RegularExpressions.Regex.IsMatch(txtPaterno.Text, @"^[a-zA-Z\s]+$"))
            {
                MostrarMensajeError("El apellido paterno es obligatorio y solo puede contener letras.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtMaterno.Text) || !System.Text.RegularExpressions.Regex.IsMatch(txtMaterno.Text, @"^[a-zA-Z\s]+$"))
            {
                MostrarMensajeError("El apellido materno es obligatorio y solo puede contener letras.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtTelefono.Text) || !System.Text.RegularExpressions.Regex.IsMatch(txtTelefono.Text, @"^\d{10}$"))
            {
                MostrarMensajeError("El número de teléfono es obligatorio y debe contener exactamente 10 dígitos.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtDireccion.Text))
            {
                MostrarMensajeError("La dirección es obligatoria.");
                return false;
            }

            return true;
        }

        private void LimpiarMensajes()
        {
            PanelExito.Visible = false;
            PanelError.Visible = false;
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
    }
}
