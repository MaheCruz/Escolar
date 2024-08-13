using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using ClosedXML.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Web;
using System.Linq;

namespace Escolar.Directivos
{
    public partial class ReporteAsist : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarMaterias();
            }
        }

        private void CargarMaterias()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT idMateria, nombre FROM materia", con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                ddlMateria.Items.Clear();
                ddlMateria.Items.Add(new System.Web.UI.WebControls.ListItem("Seleccione una materia", ""));
                while (dr.Read())
                {
                    ddlMateria.Items.Add(new System.Web.UI.WebControls.ListItem(dr["nombre"].ToString(), dr["idMateria"].ToString()));
                }

                con.Close();
            }
        }

        protected void btnGenerarReporte_Click(object sender, EventArgs e)
        {
            string idMateria = ddlMateria.SelectedValue;
            DateTime fechaInicio = Convert.ToDateTime(txtFechaInicio.Text);
            DateTime fechaFin = Convert.ToDateTime(txtFechaFin.Text);

            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // 1. Obtener las fechas únicas de asistencia dentro del rango
                SqlCommand cmdFechas = new SqlCommand("SELECT DISTINCT CONVERT(VARCHAR, dia, 103) AS Fecha FROM asistencia WHERE idMateria = @idMateria AND dia BETWEEN @fechaInicio AND @fechaFin", con);
                cmdFechas.Parameters.AddWithValue("@idMateria", idMateria);
                cmdFechas.Parameters.AddWithValue("@fechaInicio", fechaInicio);
                cmdFechas.Parameters.AddWithValue("@fechaFin", fechaFin);

                SqlDataAdapter daFechas = new SqlDataAdapter(cmdFechas);
                DataTable dtFechas = new DataTable();
                daFechas.Fill(dtFechas);

                // Ordenar las fechas en C#
                DataView dv = dtFechas.DefaultView;
                dv.Sort = "Fecha ASC";
                dtFechas = dv.ToTable();

                // 2. Construir la estructura del DataTable para incluir las columnas dinámicas
                dt.Columns.Add("NombreCompleto", typeof(string));
                foreach (DataRow dr in dtFechas.Rows)
                {
                    dt.Columns.Add(dr["Fecha"].ToString(), typeof(string));
                }
                dt.Columns.Add("% Asistencia", typeof(string));

                // 3. Obtener los datos de asistencia
                SqlCommand cmdAsistencia = new SqlCommand(@"
                    SELECT 
                        e.nombre + ' ' + e.paterno + ' ' + e.materno AS NombreCompleto, 
                        CONVERT(VARCHAR, a.dia, 103) AS Fecha, 
                        CASE WHEN a.asistencia = 1 THEN 'ASISTIÓ' ELSE 'FALTÓ' END AS Asistencia
                    FROM asistencia a
                    INNER JOIN estudiante e ON a.idEstudiante = e.matricula
                    WHERE a.idMateria = @idMateria AND a.dia BETWEEN @fechaInicio AND @fechaFin", con);
                cmdAsistencia.Parameters.AddWithValue("@idMateria", idMateria);
                cmdAsistencia.Parameters.AddWithValue("@fechaInicio", fechaInicio);
                cmdAsistencia.Parameters.AddWithValue("@fechaFin", fechaFin);

                SqlDataAdapter daAsistencia = new SqlDataAdapter(cmdAsistencia);
                DataTable dtAsistencia = new DataTable();
                daAsistencia.Fill(dtAsistencia);

                // 4. Rellenar el DataTable con los datos pivotados
                var estudiantes = dtAsistencia.AsEnumerable()
                    .GroupBy(r => r["NombreCompleto"]);

                foreach (var grupo in estudiantes)
                {
                    DataRow row = dt.NewRow();
                    row["NombreCompleto"] = grupo.Key;

                    int totalAsistencias = 0;
                    int totalClases = dtFechas.Rows.Count;

                    foreach (DataRow fechaRow in dtFechas.Rows)
                    {
                        string fecha = fechaRow["Fecha"].ToString();
                        var asistencia = grupo.FirstOrDefault(r => r["Fecha"].ToString() == fecha);

                        if (asistencia != null && asistencia["Asistencia"].ToString() == "ASISTIÓ")
                        {
                            row[fecha] = "ASISTIÓ";
                            totalAsistencias++;
                        }
                        else
                        {
                            row[fecha] = "FALTÓ";
                        }
                    }

                    row["% Asistencia"] = (totalAsistencias * 100.0 / totalClases).ToString("0.00") + " %";
                    dt.Rows.Add(row);
                }

                gvAsistencias.DataSource = dt;
                gvAsistencias.DataBind();

                // Guardar el DataTable en la sesión para exportación a Excel y PDF
                Session["AsistenciasDataTable"] = dt;
            }
        }

        protected void btnGenerarExcel_Click(object sender, EventArgs e)
        {
            DataTable dt = Session["AsistenciasDataTable"] as DataTable;

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Asistencias");

                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=ReporteAsistencias.xlsx");
                using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
                {
                    wb.SaveAs(memoryStream);
                    memoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }

        protected void btnGenerarPDF_Click(object sender, EventArgs e)
        {
            DataTable dt = Session["AsistenciasDataTable"] as DataTable;

            Document pdfDoc = new Document(PageSize.A4.Rotate(), 25f, 25f, 25f, 25f);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();

            string encabezado = $"Reporte de Asistencias\nMateria: {ddlMateria.SelectedItem.Text}\nGenerado por: {User.Identity.Name}\nFecha y Hora: {DateTime.Now}\n";
            pdfDoc.Add(new Paragraph(encabezado));

            PdfPTable pdfTable = new PdfPTable(dt.Columns.Count);
            pdfTable.WidthPercentage = 100;

            // Añadir encabezados
            foreach (DataColumn column in dt.Columns)
            {
                pdfTable.AddCell(new Phrase(column.ColumnName));
            }

            // Añadir filas
            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {
                    pdfTable.AddCell(new Phrase(row[column].ToString()));
                }
            }

            pdfDoc.Add(pdfTable);
            pdfDoc.Close();

            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=ReporteAsistencias.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Write(pdfDoc);
            Response.End();
        }
    }
}
