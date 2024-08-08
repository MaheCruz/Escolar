using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Escolar.Directivos
{
    public partial class CRUDestudiante : System.Web.UI.Page
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
            if (ValidarCampos() && !EstudianteExiste(ddlUsuarios.SelectedValue))
            {
                string idUsuario = ddlUsuarios.SelectedValue;
                string matricula = txtMatricula.Text.Trim();
                string nombre = txtNombre.Text.Trim();
                string paterno = txtPaterno.Text.Trim();
                string materno = txtMaterno.Text.Trim();
                string curp = txtCURP.Text.Trim();
                string telefono = txtTelefono.Text.Trim();
                string direccion = txtDireccion.Text.Trim();
                string tipoSangre = ddlTipoSangre.SelectedValue;

                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO estudiante (idUsuario, matricula, nombre, paterno, materno, curp, telefono, direccion, tipoSangre) VALUES (@IdUsuario, @Matricula, @Nombre, @Paterno, @Materno, @CURP, @Telefono, @Direccion, @TipoSangre)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@IdUsuario", idUsuario);
                    command.Parameters.AddWithValue("@Matricula", matricula);
                    command.Parameters.AddWithValue("@Nombre", nombre);
                    command.Parameters.AddWithValue("@Paterno", paterno);
                    command.Parameters.AddWithValue("@Materno", materno);
                    command.Parameters.AddWithValue("@CURP", curp);
                    command.Parameters.AddWithValue("@Telefono", telefono);
                    command.Parameters.AddWithValue("@Direccion", direccion);
                    command.Parameters.AddWithValue("@TipoSangre", tipoSangre);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                        MostrarMensajeExito("Estudiante insertado correctamente.");
                        GVEstudiantes.DataBind();
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.Contains("PRIMARY KEY constraint"))
                        {
                            MostrarMensajeError("No se puede registrar el mismo estudiante más de una vez.");
                        }
                        else
                        {
                            MostrarMensajeError("Error al insertar estudiante: " + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MostrarMensajeError("El estudiante ya está registrado.");
            }
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            LimpiarMensajes();
            if (ValidarCampos())
            {
                string idUsuario = ddlUsuarios.SelectedValue;
                string matricula = txtMatricula.Text.Trim();
                string nombre = txtNombre.Text.Trim();
                string paterno = txtPaterno.Text.Trim();
                string materno = txtMaterno.Text.Trim();
                string curp = txtCURP.Text.Trim();
                string telefono = txtTelefono.Text.Trim();
                string direccion = txtDireccion.Text.Trim();
                string tipoSangre = ddlTipoSangre.SelectedValue;

                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE estudiante SET nombre = @Nombre, paterno = @Paterno, materno = @Materno, curp = @CURP, telefono = @Telefono, direccion = @Direccion, tipoSangre = @TipoSangre WHERE idUsuario = @IdUsuario AND matricula = @Matricula";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@IdUsuario", idUsuario);
                    command.Parameters.AddWithValue("@Matricula", matricula);
                    command.Parameters.AddWithValue("@Nombre", nombre);
                    command.Parameters.AddWithValue("@Paterno", paterno);
                    command.Parameters.AddWithValue("@Materno", materno);
                    command.Parameters.AddWithValue("@CURP", curp);
                    command.Parameters.AddWithValue("@Telefono", telefono);
                    command.Parameters.AddWithValue("@Direccion", direccion);
                    command.Parameters.AddWithValue("@TipoSangre", tipoSangre);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        connection.Close();

                        if (rowsAffected > 0)
                        {
                            MostrarMensajeExito("Estudiante modificado correctamente.");
                            GVEstudiantes.DataBind();
                        }
                        else
                        {
                            MostrarMensajeError("No se encontró el estudiante.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MostrarMensajeError("Error al modificar estudiante: " + ex.Message);
                    }
                }
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            LimpiarMensajes();
            string idUsuario = ddlUsuarios.SelectedValue;
            string matricula = txtMatricula.Text.Trim();

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM estudiante WHERE idUsuario = @IdUsuario AND matricula = @Matricula";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@IdUsuario", idUsuario);
                command.Parameters.AddWithValue("@Matricula", matricula);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    connection.Close();

                    if (rowsAffected > 0)
                    {
                        MostrarMensajeExito("Estudiante eliminado correctamente.");
                        GVEstudiantes.DataBind();
                    }
                    else
                    {
                        MostrarMensajeError("No se encontró el estudiante.");
                    }
                }
                catch (Exception ex)
                {
                    MostrarMensajeError("Error al eliminar estudiante: " + ex.Message);
                }
            }
        }

        protected void GVEstudiantes_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GVEstudiantes.SelectedRow;
            ddlUsuarios.SelectedValue = ObtenerIdUsuario(row.Cells[1].Text);
            txtMatricula.Text = row.Cells[2].Text;
            txtNombre.Text = row.Cells[3].Text;
            txtPaterno.Text = row.Cells[4].Text;
            txtMaterno.Text = row.Cells[5].Text;
            txtCURP.Text = row.Cells[6].Text;
            txtTelefono.Text = row.Cells[7].Text;
            txtDireccion.Text = row.Cells[8].Text;
            ddlTipoSangre.SelectedValue = row.Cells[9].Text;
            txtPromGeneral.Text = row.Cells[10].Text;
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtMatricula.Text) ||
                string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtPaterno.Text) ||
                string.IsNullOrWhiteSpace(txtCURP.Text) ||
                string.IsNullOrWhiteSpace(txtTelefono.Text) ||
                string.IsNullOrWhiteSpace(txtDireccion.Text) ||
                string.IsNullOrWhiteSpace(ddlTipoSangre.SelectedValue))
            {
                MostrarMensajeError("Todos los campos son obligatorios.");
                return false;
            }

            if (txtTelefono.Text.Length != 10 || !txtTelefono.Text.All(char.IsDigit))
            {
                MostrarMensajeError("El teléfono debe tener 10 números.");
                return false;
            }

            if (txtCURP.Text.Length != 18 || !txtCURP.Text.All(char.IsLetterOrDigit))
            {
                MostrarMensajeError("La CURP debe tener 18 caracteres alfanumericos.");
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

        private bool EstudianteExiste(string idUsuario)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM estudiante WHERE idUsuario = @IdUsuario";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@IdUsuario", idUsuario);

                try
                {
                    connection.Open();
                    int count = (int)command.ExecuteScalar();
                    connection.Close();

                    return count > 0;
                }
                catch
                {
                    return false;
                }
            }
        }

        private string ObtenerIdUsuario(string userName)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Id FROM AspNetUsers WHERE UserName = @UserName";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserName", userName);

                try
                {
                    connection.Open();
                    string idUsuario = command.ExecuteScalar()?.ToString();
                    connection.Close();

                    return idUsuario;
                }
                catch
                {
                    return string.Empty;
                }
            }
        }
    }
}
