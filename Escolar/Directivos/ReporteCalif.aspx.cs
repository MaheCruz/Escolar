using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using ClosedXML.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Web;

namespace Escolar.Directivos
{
    public partial class ReporteCalif : System.Web.UI.Page
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

            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                SqlCommand cmdCalificaciones = new SqlCommand(@"
                    SELECT 
                        e.nombre + ' ' + e.paterno + ' ' + e.materno AS NombreCompleto, 
                        c.calificacion1 AS Calificacion1, 
                        c.calificacion2 AS Calificacion2, 
                        c.calificacion3 AS Calificacion3, 
                        c.promedioMateria AS Promedio
                    FROM calificacion c
                    INNER JOIN estudiante e ON c.idEstudiante = e.matricula
                    WHERE c.idMateria = @idMateria", con);
                cmdCalificaciones.Parameters.AddWithValue("@idMateria", idMateria);

                SqlDataAdapter daCalificaciones = new SqlDataAdapter(cmdCalificaciones);
                daCalificaciones.Fill(dt);

                gvCalificaciones.DataSource = dt;
                gvCalificaciones.DataBind();

                // Guardar el DataTable en la sesión para exportación a Excel y PDF
                Session["CalificacionesDataTable"] = dt;
            }
        }

        protected void btnGenerarExcel_Click(object sender, EventArgs e)
        {
            DataTable dt = Session["CalificacionesDataTable"] as DataTable;

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Calificaciones");

                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=ReporteCalificaciones.xlsx");
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
            DataTable dt = Session["CalificacionesDataTable"] as DataTable;

            Document pdfDoc = new Document(PageSize.A4.Rotate(), 25f, 25f, 25f, 25f);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();

            string encabezado = $"Reporte de Calificaciones\nMateria: {ddlMateria.SelectedItem.Text}\nGenerado por: {User.Identity.Name}\nFecha y Hora: {DateTime.Now}\n";
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
            Response.AddHeader("content-disposition", "attachment;filename=ReporteCalificaciones.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Write(pdfDoc);
            Response.End();
        }
    }
}
