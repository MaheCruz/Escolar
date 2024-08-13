using System;
using System.Configuration;
using System.Data.SqlClient;

namespace Escolar.Directivos
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Cargar los totales y los promedios
                CargarTotalesYPromedios();
                EjecutarProcedimientoCalcularPromedio();
            }
        }

        private void EjecutarProcedimientoCalcularPromedio()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("CalcularPromedioGeneralPorEstudiante", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    // Manejo de errores
                    Response.Write("Error: " + ex.Message);
                }
            }
        }

    private void CargarTotalesYPromedios()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // Contar estudiantes
                SqlCommand cmdEstudiantes = new SqlCommand("SELECT COUNT(*) FROM estudiante", con);
                lblTotalEstudiantes.Text = cmdEstudiantes.ExecuteScalar().ToString();

                // Contar docentes
                SqlCommand cmdDocentes = new SqlCommand("SELECT COUNT(*) FROM docente", con);
                lblTotalDocentes.Text = cmdDocentes.ExecuteScalar().ToString();

                // Contar tutores
                SqlCommand cmdTutores = new SqlCommand("SELECT COUNT(*) FROM tutor", con);
                lblTotalTutores.Text = cmdTutores.ExecuteScalar().ToString();

                // Obtener promedio general de calificaciones
                SqlCommand cmdPromedioGeneral = new SqlCommand("SELECT AVG(promGeneral) FROM estudiante", con);
                lblPromedioGeneral.Text = Convert.ToDecimal(cmdPromedioGeneral.ExecuteScalar()).ToString("F2");

                // Obtener promedio general de asistencias
                SqlCommand cmdPromedioAsistencias = new SqlCommand("SELECT AVG(CAST(asistencia AS FLOAT)) FROM asistencia", con);
                lblPromedioAsistencias.Text = Convert.ToDecimal(cmdPromedioAsistencias.ExecuteScalar()).ToString("F2");
            }
        }
    }
}
