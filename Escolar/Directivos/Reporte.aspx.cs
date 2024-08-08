using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;

namespace Escolar.Directivos
{
    public partial class Reporte : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadReportes();
                LoadEstudiantes();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO reporte (idReporte, tipo, detalle, fecha, idEstudiante, creadoPor) VALUES (@idReporte, @tipo, @detalle, @fecha, @idEstudiante, @creadoPor)", con);
                    cmd.Parameters.AddWithValue("@idReporte", Guid.NewGuid().ToString());
                    cmd.Parameters.AddWithValue("@tipo", ddlTipoReporte.SelectedValue);
                    cmd.Parameters.AddWithValue("@detalle", txtDetalle.Text);
                    cmd.Parameters.AddWithValue("@fecha", txtFecha.Text);
                    cmd.Parameters.AddWithValue("@idEstudiante", ddlEstudiante.SelectedValue);
                    cmd.Parameters.AddWithValue("@creadoPor", User.Identity.Name);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    lblMessage.Text = "Reporte guardado exitosamente.";
                    LoadReportes();
                }
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("UPDATE reporte SET tipo = @tipo, detalle = @detalle, fecha = @fecha, idEstudiante = @idEstudiante WHERE idReporte = @idReporte AND creadoPor = @creadoPor", con);
                    cmd.Parameters.AddWithValue("@idReporte", hfIdReporte.Value);
                    cmd.Parameters.AddWithValue("@tipo", ddlTipoReporte.SelectedValue);
                    cmd.Parameters.AddWithValue("@detalle", txtDetalle.Text);
                    cmd.Parameters.AddWithValue("@fecha", txtFecha.Text);
                    cmd.Parameters.AddWithValue("@idEstudiante", ddlEstudiante.SelectedValue);
                    cmd.Parameters.AddWithValue("@creadoPor", User.Identity.Name);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    lblMessage.Text = "Reporte actualizado exitosamente.";
                    LoadReportes();
                }
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM reporte WHERE idReporte = @idReporte AND creadoPor = @creadoPor", con);
                cmd.Parameters.AddWithValue("@idReporte", hfIdReporte.Value);
                cmd.Parameters.AddWithValue("@creadoPor", User.Identity.Name);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                lblMessage.Text = "Reporte eliminado exitosamente.";
                LoadReportes();
            }
        }

        protected void gvReportes_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = gvReportes.SelectedRow;
            hfIdReporte.Value = row.Cells[0].Text;
            ddlTipoReporte.SelectedValue = row.Cells[1].Text;
            txtDetalle.Text = row.Cells[2].Text;
            txtFecha.Text = Convert.ToDateTime(row.Cells[3].Text).ToString("yyyy-MM-dd");
            string selectedEstudiante = row.Cells[4].Text;

            if (ddlEstudiante.Items.FindByText(selectedEstudiante) != null)
            {
                ddlEstudiante.SelectedValue = ddlEstudiante.Items.FindByText(selectedEstudiante).Value;
            }
        }

        protected void calFecha_SelectionChanged(object sender, EventArgs e)
        {
            txtFecha.Text = calFecha.SelectedDate.ToString("yyyy-MM-dd");
        }

        private void LoadReportes()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT idReporte, tipo, detalle, fecha, CONCAT(e.nombre, ' ', e.paterno, ' ', e.materno) AS nombreCompleto, creadoPor FROM reporte r JOIN estudiante e ON r.idEstudiante = e.matricula", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvReportes.DataSource = dt;
                gvReportes.DataBind();
            }
        }

        private void LoadEstudiantes()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT matricula, nombre + ' ' + paterno + ' ' + materno AS nombreCompleto FROM estudiante", con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                ddlEstudiante.Items.Clear();
                ddlEstudiante.Items.Add(new ListItem("Seleccione un estudiante", "")); // Opción por defecto
                while (dr.Read())
                {
                    ddlEstudiante.Items.Add(new ListItem(dr["nombreCompleto"].ToString(), dr["matricula"].ToString()));
                }

                con.Close();
            }
        }
    }
}
