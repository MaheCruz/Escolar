using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;

namespace Escolar.Docentes
{
    public partial class VistaEstudiantes : System.Web.UI.Page
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
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    // Suponiendo que el ID del docente es pasado o está en el contexto de la sesión.
                    string idDocente = "ID_DEL_DOCENTE"; // Reemplazar con el ID del docente actual

                    SqlCommand cmd = new SqlCommand("SELECT idMateria, nombre FROM materia WHERE idDocente = @idDocente", con);
                    cmd.Parameters.AddWithValue("@idDocente", idDocente);

                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    ddlMateria.Items.Clear();
                    ddlMateria.Items.Add(new ListItem("Seleccione una materia", ""));
                    while (dr.Read())
                    {
                        ddlMateria.Items.Add(new ListItem(dr["nombre"].ToString(), dr["idMateria"].ToString()));
                    }

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                Console.WriteLine("Error al cargar materias: " + ex.Message);
            }
        }

        protected void ddlMateria_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlMateria.SelectedValue))
            {
                LoadEstudiantes();
            }
        }

        private void LoadEstudiantes()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(@"SELECT e.matricula, 
                                                             CONCAT(e.nombre, ' ', e.paterno, ' ', e.materno) AS nombreCompleto,
                                                             c.calificacion1,
                                                             c.calificacion2,
                                                             c.calificacion3,
                                                             c.promedioMateria
                                                      FROM estudiante e
                                                      JOIN calificacion c ON e.matricula = c.idEstudiante
                                                      WHERE c.idMateria = @idMateria", con);
                    cmd.Parameters.AddWithValue("@idMateria", ddlMateria.SelectedValue);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    gvEstudiantes.DataSource = dt;
                    gvEstudiantes.DataBind();
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                Console.WriteLine("Error al cargar estudiantes: " + ex.Message);
            }
        }

        protected void btnGenerarPDF_Click(object sender, EventArgs e)
        {
            // Implementar lógica para generar PDF
        }

        protected void btnGenerarExcel_Click(object sender, EventArgs e)
        {
            // Implementar lógica para generar Excel
        }
    }
}
