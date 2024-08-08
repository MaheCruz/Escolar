using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Services;
using System.Web.Script.Serialization;

namespace Escolar.Directivos
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDatos();
            }
        }

        private void CargarDatos()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Total de estudiantes
                string queryEstudiantes = "SELECT COUNT(*) FROM estudiante";
                using (SqlCommand cmd = new SqlCommand(queryEstudiantes, conn))
                {
                    lblTotalEstudiantes.Text = cmd.ExecuteScalar().ToString();
                }

                // Total de docentes
                string queryDocentes = "SELECT COUNT(*) FROM docente";
                using (SqlCommand cmd = new SqlCommand(queryDocentes, conn))
                {
                    lblTotalDocentes.Text = cmd.ExecuteScalar().ToString();
                }

                // Total de materias
                string queryMaterias = "SELECT COUNT(*) FROM materia";
                using (SqlCommand cmd = new SqlCommand(queryMaterias, conn))
                {
                    lblTotalMaterias.Text = cmd.ExecuteScalar().ToString();
                }
            }
        }

        [WebMethod]
        public static string GetEstudiantesPorGrupo()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            var labels = new List<string>();
            var values = new List<int>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT g.nombre, COUNT(e.matricula) AS cantidad FROM grupo g LEFT JOIN grupoEstudiante ge ON g.idGrupo = ge.idGrupo LEFT JOIN estudiante e ON ge.idEstudiante = e.matricula GROUP BY g.nombre";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        labels.Add(reader["nombre"].ToString());
                        values.Add(Convert.ToInt32(reader["cantidad"]));
                    }
                }
            }

            var result = new { labels = labels.ToArray(), values = values.ToArray() };
            return new JavaScriptSerializer().Serialize(result);
        }

        [WebMethod]
        public static string GetDocentesPorMateria()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            var labels = new List<string>();
            var values = new List<int>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT m.nombre, COUNT(d.idDocente) AS cantidad FROM materia m LEFT JOIN grupoMateria gm ON m.idMateria = gm.idMateria LEFT JOIN docente d ON gm.idDocente = d.idDocente GROUP BY m.nombre";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        labels.Add(reader["nombre"].ToString());
                        values.Add(Convert.ToInt32(reader["cantidad"]));
                    }
                }
            }

            var result = new { labels = labels.ToArray(), values = values.ToArray() };
            return new JavaScriptSerializer().Serialize(result);
        }
    }
}
