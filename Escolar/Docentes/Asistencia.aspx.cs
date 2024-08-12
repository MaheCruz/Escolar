using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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
                lblFechaHora.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            }
        }

        private void LoadMaterias()
        {
            string userEmail = User.Identity.Name;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT m.idMateria, m.nombre FROM materia m JOIN docente d ON m.idDocente = d.idDocente JOIN AspNetUsers u ON d.idUsuario = u.Id WHERE u.Email = @Email", con);
                cmd.Parameters.AddWithValue("@Email", userEmail);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                ddlMateria.Items.Clear();
                ddlMateria.Items.Add(new System.Web.UI.WebControls.ListItem("Seleccione una materia", "")); // Opción por defecto
                while (dr.Read())
                {
                    ddlMateria.Items.Add(new System.Web.UI.WebControls.ListItem(dr["nombre"].ToString(), dr["idMateria"].ToString()));
                }

                con.Close();
            }
        }

        protected void ddlMateria_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAlumnos();
            LoadHistorialAsistencia();
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

        private void LoadHistorialAsistencia()
        {
            string idMateria = ddlMateria.SelectedValue;
            if (!string.IsNullOrEmpty(idMateria))
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(@"
                        SELECT 
                            e.matricula, 
                            CONCAT(e.nombre, ' ', e.paterno, ' ', e.materno) AS nombreCompleto, 
                            a.dia, 
                            CASE WHEN a.asistencia = 1 THEN 'x' ELSE '' END AS Asistio
                        FROM 
                            asistencia a 
                            JOIN estudiante e ON a.idEstudiante = e.matricula
                        WHERE 
                            a.idMateria = @idMateria
                        ORDER BY 
                            e.matricula, a.dia", con);

                    cmd.Parameters.AddWithValue("@idMateria", idMateria);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    var pivotTable = new DataTable();

                    // Agregar columna de nombre
                    pivotTable.Columns.Add("Nombre del Estudiante");

                    // Obtener las fechas distintas
                    var fechas = dt.AsEnumerable()
                                   .Select(row => row.Field<DateTime>("dia").ToString("dd/MM/yyyy"))
                                   .Distinct();

                    // Agregar columnas de fecha
                    foreach (var fecha in fechas)
                    {
                        pivotTable.Columns.Add(fecha);
                    }

                    pivotTable.Columns.Add("% Asistencia");

                    // Llenar la tabla pivot con los estudiantes y su asistencia
                    var estudiantes = dt.AsEnumerable()
                                        .Select(row => new
                                        {
                                            Matricula = row.Field<string>("matricula"),
                                            NombreCompleto = row.Field<string>("nombreCompleto")
                                        })
                                        .Distinct();

                    foreach (var estudiante in estudiantes)
                    {
                        var newRow = pivotTable.NewRow();
                        newRow["Nombre del Estudiante"] = estudiante.NombreCompleto;

                        var asistencias = dt.AsEnumerable()
                                            .Where(row => row.Field<string>("matricula") == estudiante.Matricula)
                                            .GroupBy(row => row.Field<DateTime>("dia").ToString("dd/MM/yyyy"))
                                            .ToDictionary(g => g.Key, g => g.First().Field<string>("Asistio"));

                        int totalDias = fechas.Count();
                        int diasAsistidos = 0;

                        foreach (var fecha in fechas)
                        {
                            newRow[fecha] = asistencias.ContainsKey(fecha) ? asistencias[fecha] : "";
                            if (asistencias.ContainsKey(fecha) && asistencias[fecha] == "x")
                            {
                                diasAsistidos++;
                            }
                        }

                        newRow["% Asistencia"] = (diasAsistidos * 100) / totalDias;
                        pivotTable.Rows.Add(newRow);
                    }

                    gvHistorialAsistencia.DataSource = pivotTable;
                    gvHistorialAsistencia.DataBind();
                }
            }
            else
            {
                gvHistorialAsistencia.DataSource = null;
                gvHistorialAsistencia.DataBind();
            }
        }

        protected void btnGuardarAsistencia_Click(object sender, EventArgs e)
        {
            string idMateria = ddlMateria.SelectedValue;
            DateTime fecha = DateTime.Now;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // Verificación de si la asistencia ya fue registrada para la fecha
                SqlCommand cmdCheck = new SqlCommand("SELECT COUNT(*) FROM asistencia WHERE idMateria = @idMateria AND dia = @dia", con);
                cmdCheck.Parameters.AddWithValue("@idMateria", idMateria);
                cmdCheck.Parameters.AddWithValue("@dia", fecha.Date);

                int count = Convert.ToInt32(cmdCheck.ExecuteScalar());

                if (count > 0)
                {
                    lblMessage.Text = "Asistencia ya registrada para esta fecha.";
                    LoadHistorialAsistencia();
                    return;
                }

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
            LoadHistorialAsistencia();
        }

        protected void btnGenerarPDF_Click(object sender, EventArgs e)
        {

        }
    }
}
