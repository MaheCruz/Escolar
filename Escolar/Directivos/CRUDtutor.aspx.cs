using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Escolar.Directivos
{
    public partial class CRUDtutor : System.Web.UI.Page
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
                string idTutor = txtIdTutor.Text.Trim();
                string idUsuario = ddlUsuarios.SelectedValue;
                string nombre = txtNombre.Text.Trim();
                string paterno = txtPaterno.Text.Trim();
                string materno = txtMaterno.Text.Trim();
                string movil = txtMovil.Text.Trim();
                string telefono = txtTelefono.Text.Trim();
                string direccion = txtDireccion.Text.Trim();
                string relacion = txtRelacion.Text.Trim();

                if (TutorExiste(idTutor))
                {
                    MostrarMensajeError("El ID Tutor ya existe.");
                    return;
                }

                if (UsuarioTieneTutor(idUsuario))
                {
                    MostrarMensajeError("El Usuario ya tiene un tutor registrado.");
                    return;
                }

                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO tutor (idTutor, idUsuario, nombre, paterno, materno, movil, telefono, direccion, relacion) " +
                                   "VALUES (@idTutor, @idUsuario, @nombre, @paterno, @materno, @movil, @telefono, @direccion, @relacion)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@idTutor", idTutor);
                    command.Parameters.AddWithValue("@idUsuario", idUsuario);
                    command.Parameters.AddWithValue("@nombre", nombre);
                    command.Parameters.AddWithValue("@paterno", paterno);
                    command.Parameters.AddWithValue("@materno", materno);
                    command.Parameters.AddWithValue("@movil", movil);
                    command.Parameters.AddWithValue("@telefono", telefono);
                    command.Parameters.AddWithValue("@direccion", direccion);
                    command.Parameters.AddWithValue("@relacion", relacion);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();

                        MostrarMensajeExito("Tutor insertado correctamente.");
                        GVTutores.DataBind();
                    }
                    catch (Exception ex)
                    {
                        MostrarMensajeError("Error al insertar el tutor: " + ex.Message);
                    }
                }
            }
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            LimpiarMensajes();
            if (ValidarCampos())
            {
                string idTutor = txtIdTutor.Text.Trim();
                string nombre = txtNombre.Text.Trim();
                string paterno = txtPaterno.Text.Trim();
                string materno = txtMaterno.Text.Trim();
                string movil = txtMovil.Text.Trim();
                string telefono = txtTelefono.Text.Trim();
                string direccion = txtDireccion.Text.Trim();
                string relacion = txtRelacion.Text.Trim();

                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE tutor SET nombre = @nombre, paterno = @paterno, materno = @materno, " +
                                   "movil = @movil, telefono = @telefono, direccion = @direccion, relacion = @relacion " +
                                   "WHERE idTutor = @idTutor";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@idTutor", idTutor);
                    command.Parameters.AddWithValue("@nombre", nombre);
                    command.Parameters.AddWithValue("@paterno", paterno);
                    command.Parameters.AddWithValue("@materno", materno);
                    command.Parameters.AddWithValue("@movil", movil);
                    command.Parameters.AddWithValue("@telefono", telefono);
                    command.Parameters.AddWithValue("@direccion", direccion);
                    command.Parameters.AddWithValue("@relacion", relacion);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();

                        MostrarMensajeExito("Tutor modificado correctamente.");
                        GVTutores.DataBind();
                    }
                    catch (Exception ex)
                    {
                        MostrarMensajeError("Error al modificar el tutor: " + ex.Message);
                    }
                }
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            LimpiarMensajes();
            string idTutor = txtIdTutor.Text.Trim();

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM tutor WHERE idTutor = @idTutor";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@idTutor", idTutor);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();

                    MostrarMensajeExito("Tutor eliminado correctamente.");
                    GVTutores.DataBind();
                }
                catch (Exception ex)
                {
                    MostrarMensajeError("Error al eliminar el tutor: " + ex.Message);
                }
            }
        }

        protected void GVTutores_SelectedIndexChanged(object sender, EventArgs e)
        {
            LimpiarMensajes();
            GridViewRow row = GVTutores.SelectedRow;
            txtIdTutor.Text = row.Cells[1].Text;
            ddlUsuarios.SelectedValue = row.Cells[2].Text;
            txtNombre.Text = row.Cells[3].Text;
            txtPaterno.Text = row.Cells[4].Text;
            txtMaterno.Text = row.Cells[5].Text;
            txtMovil.Text = row.Cells[6].Text;
            txtTelefono.Text = row.Cells[7].Text;
            txtDireccion.Text = row.Cells[8].Text;
            txtRelacion.Text = row.Cells[9].Text;
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtIdTutor.Text))
            {
                MostrarMensajeError("El ID Tutor es obligatorio.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || !System.Text.RegularExpressions.Regex.IsMatch(txtNombre.Text, "^[a-zA-Z]+$"))
            {
                MostrarMensajeError("El nombre es obligatorio y debe contener solo letras.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtPaterno.Text) || !System.Text.RegularExpressions.Regex.IsMatch(txtPaterno.Text, "^[a-zA-Z]+$"))
            {
                MostrarMensajeError("El apellido paterno es obligatorio y debe contener solo letras.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtMaterno.Text) || !System.Text.RegularExpressions.Regex.IsMatch(txtMaterno.Text, "^[a-zA-Z]+$"))
            {
                MostrarMensajeError("El apellido materno es obligatorio y debe contener solo letras.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtMovil.Text) || !System.Text.RegularExpressions.Regex.IsMatch(txtMovil.Text, "^[0-9]{10}$"))
            {
                MostrarMensajeError("El número de móvil es obligatorio y debe contener 10 dígitos.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtTelefono.Text) || !System.Text.RegularExpressions.Regex.IsMatch(txtTelefono.Text, "^[0-9]{10}$"))
            {
                MostrarMensajeError("El número de teléfono es obligatorio y debe contener 10 dígitos.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtDireccion.Text))
            {
                MostrarMensajeError("La dirección es obligatoria.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtRelacion.Text))
            {
                MostrarMensajeError("La relación es obligatoria.");
                return false;
            }
            return true;
        }

        private bool TutorExiste(string idTutor)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM tutor WHERE idTutor = @idTutor";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@idTutor", idTutor);
                connection.Open();
                int count = (int)command.ExecuteScalar();
                connection.Close();
                return count > 0;
            }
        }

        private bool UsuarioTieneTutor(string idUsuario)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM tutor WHERE idUsuario = @idUsuario";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@idUsuario", idUsuario);
                connection.Open();
                int count = (int)command.ExecuteScalar();
                connection.Close();
                return count > 0;
            }
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
