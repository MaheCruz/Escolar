using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Escolar.Docentes
{
    public partial class Asistencia : Page
    {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            protected void Page_Load(object sender, EventArgs e)
            {
                if (!IsPostBack)
                {
                    LoadMaterias();
                }
            }

            private void LoadMaterias()
            {
                string userEmail = User.Identity.Name; // Suponiendo que el email del usuario es usado para identificar al docente
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT m.idMateria, m.nombre FROM materia m JOIN docente d ON m.idDocente = d.idDocente JOIN AspNetUsers u ON d.idUsuario = u.Id WHERE u.Email = @Email", con);
                    cmd.Parameters.AddWithValue("@Email", userEmail);
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    ddlMateria.Items.Clear();
                    ddlMateria.Items.Add(new ListItem("Seleccione una materia", "")); // Opción por defecto
                    while (dr.Read())
                    {
                        ddlMateria.Items.Add(new ListItem(dr["nombre"].ToString(), dr["idMateria"].ToString()));
                    }

                    con.Close();
                }
            }

            protected void ddlMateria_SelectedIndexChanged(object sender, EventArgs e)
            {
                LoadAlumnos();
            }

            protected void btnCargarAlumnos_Click(object sender, EventArgs e)
            {
                LoadAlumnos();
            }

            private void LoadAlumnos()
            {
                string idMateria = ddlMateria.SelectedValue;
                if (!string.IsNullOrEmpty(idMateria))
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        SqlCommand cmd = new SqlCommand("SELECT e.matricula, CONCAT(e.nombre, ' ', e.paterno, ' ', e.materno) AS nombreCompleto FROM estudiante e JOIN grupoEstudiante ge ON e.matricula = ge.idEstudiante JOIN grupoMateria gm ON ge.idGrupo = gm.idGrupo WHERE gm.idMateria = @idMateria", con);
                        cmd.Parameters.AddWithValue("@idMateria", idMateria);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        gvAlumnos.DataSource = dt;
                        gvAlumnos.DataBind();
                    }
                }
                else
                {
                    gvAlumnos.DataSource = null;
                    gvAlumnos.DataBind();
                }
            }

            protected void btnGuardarAsistencia_Click(object sender, EventArgs e)
            {
                string idMateria = ddlMateria.SelectedValue;
                DateTime fecha = DateTime.Now;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    foreach (GridViewRow row in gvAlumnos.Rows)
                    {
                        string matricula = row.Cells[0].Text;
                        bool asistencia = ((CheckBox)row.FindControl("chkAsistencia")).Checked;

                        SqlCommand cmd = new SqlCommand("INSERT INTO asistencia (idAsistencia, dia, asistencia, idMateria, idEstudiante) VALUES (@idAsistencia, @dia, @asistencia, @idMateria, @idEstudiante)", con);
                        cmd.Parameters.AddWithValue("@idAsistencia", Guid.NewGuid().ToString());
                        cmd.Parameters.AddWithValue("@dia", fecha);
                        cmd.Parameters.AddWithValue("@asistencia", asistencia);
                        cmd.Parameters.AddWithValue("@idMateria", idMateria);
                        cmd.Parameters.AddWithValue("@idEstudiante", matricula);

                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                }

                lblMessage.Text = "Asistencia guardada exitosamente.";
            }
        }
    }
