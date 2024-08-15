using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.DataVisualization.Charting;

namespace Escolar.Directivos
{
    public partial class Dashboard : System.Web.UI.Page
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Cargar los totales y los promedios
                CargarTotalesYPromedios();
                EjecutarProcedimientoCalcularPromedio();
                GenerarGraficaAsistencia();
                GenerarGraficaCalificaciones();
            }
        }
        private void GenerarGraficaAsistencia()
        {
            DataTable dt = ObtenerDatosAsistencia();

            Chart chart = new Chart();
            chart.Width = 600;
            chart.Height = 400;

            ChartArea chartArea = new ChartArea();
            chart.ChartAreas.Add(chartArea);

            Series series = new Series();
            series.Name = "Asistencia";
            series.ChartType = SeriesChartType.Column;

            foreach (DataRow row in dt.Rows)
            {
                series.Points.AddXY(row["Dia"].ToString(), Convert.ToInt32(row["Total"]));
            }

            chart.Series.Add(series);

            string filePath = Server.MapPath("~/Images/GraficaAsistencia.png");
            chart.SaveImage(filePath, ChartImageFormat.Png);

            imgGraficaAsistencia.ImageUrl = "~/Images/GraficaAsistencia.png";
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
                catch (InvalidCastException ex)
                {
                    // Este bloque maneja específicamente el error de conversión
                    Response.Write("Error de conversión de tipos: " + ex.Message);
                }
                catch (Exception ex)
                {
                    // Manejo general de errores
                    Response.Write("Error: " + ex.Message);
                }
            }
        }

        private DataTable ObtenerDatosAsistencia()
        {
            string query = "SELECT Dia, COUNT(*) AS Total FROM asistencia WHERE Asistencia = 1 GROUP BY Dia";
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }

            return dt;
        }
        private void GenerarGraficaCalificaciones()
        {
            DataTable dt = ObtenerDatosCalificaciones();

            Chart chart = new Chart();
            chart.Width = 600;
            chart.Height = 400;

            ChartArea chartArea = new ChartArea();
            chart.ChartAreas.Add(chartArea);

            Series series = new Series();
            series.Name = "Calificaciones";
            series.ChartType = SeriesChartType.Bar;

            try
            {
                foreach (DataRow row in dt.Rows)
                {
                    decimal promedio = row["Promedio"] != DBNull.Value ? Convert.ToDecimal(row["Promedio"]) : 0;
                    series.Points.AddXY(row["NombreMateria"].ToString(), promedio);
                }

                chart.Series.Add(series);

                string filePath = Server.MapPath("~/Images/GraficaCalificaciones.png");
                chart.SaveImage(filePath, ChartImageFormat.Png);

                imgGraficaCalificaciones.ImageUrl = "~/Images/GraficaCalificaciones.png";
            }
            catch (InvalidCastException ex)
            {
                // Manejo específico del error de conversión
                Response.Write("Error de conversión al generar la gráfica de calificaciones: " + ex.Message);
            }
            catch (Exception ex)
            {
                // Manejo general de errores
                Response.Write("Error al generar la gráfica de calificaciones: " + ex.Message);
            }
        }

        private DataTable ObtenerDatosCalificaciones()
        {
            string query = "SELECT m.nombre AS NombreMateria, AVG(c.promedioMateria) AS Promedio FROM calificacion c INNER JOIN materia m ON c.idMateria = m.idMateria GROUP BY m.nombre";
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        con.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }
            }
            catch (SqlException ex)
            {
                // Manejo específico de errores SQL
                Response.Write("Error en la consulta de calificaciones: " + ex.Message);
            }
            catch (Exception ex)
            {
                // Manejo general de errores
                Response.Write("Error al obtener los datos de calificaciones: " + ex.Message);
            }

            return dt;
        }

        private void CargarTotalesYPromedios()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
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
                    object resultadoPromedioGeneral = cmdPromedioGeneral.ExecuteScalar();
                    lblPromedioGeneral.Text = resultadoPromedioGeneral != DBNull.Value ? Convert.ToDecimal(resultadoPromedioGeneral).ToString("F2") : "N/A";

                    // Obtener promedio general de asistencias
                    SqlCommand cmdPromedioAsistencias = new SqlCommand("SELECT AVG(CAST(asistencia AS FLOAT)) FROM asistencia", con);
                    object resultadoPromedioAsistencias = cmdPromedioAsistencias.ExecuteScalar();
                    lblPromedioAsistencias.Text = resultadoPromedioAsistencias != DBNull.Value ? Convert.ToDecimal(resultadoPromedioAsistencias).ToString("F2") : "N/A";
                }
                catch (InvalidCastException ex)
                {
                    // Este bloque maneja específicamente el error de conversión
                    Response.Write("Error de conversión de tipos: " + ex.Message);
                }
                catch (SqlException ex)
                {
                    // Manejo específico de errores SQL
                    Response.Write("Error en la base de datos: " + ex.Message);
                }
                catch (Exception ex)
                {
                    // Manejo general de errores
                    Response.Write("Error: " + ex.Message);
                }
            }
        }

    }

}
