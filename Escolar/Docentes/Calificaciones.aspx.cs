using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;
using ClosedXML.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Web.UI;
using System.Web;

namespace Escolar.Docentes
{
    public partial class Calificaciones : System.Web.UI.Page
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
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string queryMaterias = @"
                    SELECT m.idMateria, m.nombre AS nombreMateria
                    FROM materia m
                    JOIN docente d ON m.idDocente = d.idDocente
                    JOIN AspNetUsers u ON d.idUsuario = u.Id
                    WHERE u.Email = @Email";

                SqlCommand cmdMaterias = new SqlCommand(queryMaterias, con);
                cmdMaterias.Parameters.AddWithValue("@Email", User.Identity.Name);

                SqlDataAdapter da = new SqlDataAdapter(cmdMaterias);
                DataTable dt = new DataTable();
                da.Fill(dt);

                rptMaterias.DataSource = dt;
                rptMaterias.DataBind();
            }
        }

        protected void rptMaterias_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                GridView gvCalificaciones = (GridView)e.Item.FindControl("gvCalificaciones");
                string idMateria = DataBinder.Eval(e.Item.DataItem, "idMateria").ToString();
                LoadCalificaciones(idMateria, gvCalificaciones);
            }
        }

        private void LoadCalificaciones(string idMateria, GridView gvCalificaciones)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string queryCalificaciones = @"
            SELECT c.idCalificacion, 
                   CONCAT(e.nombre, ' ', e.paterno, ' ', e.materno) AS nombreCompleto,
                   c.calificacion1, c.calificacion2, c.calificacion3, c.promedioMateria
            FROM calificacion c
            JOIN estudiante e ON c.idEstudiante = e.matricula
            WHERE c.idMateria = @idMateria";

                SqlCommand cmdCalificaciones = new SqlCommand(queryCalificaciones, con);
                cmdCalificaciones.Parameters.AddWithValue("@idMateria", idMateria);

                SqlDataAdapter da = new SqlDataAdapter(cmdCalificaciones);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Depurar datos en consola
                foreach (DataRow row in dt.Rows)
                {
                    Console.WriteLine("Estudiante: " + row["nombreCompleto"]);
                    Console.WriteLine("Calificación 1: " + row["calificacion1"]);
                    Console.WriteLine("Calificación 2: " + row["calificacion2"]);
                    Console.WriteLine("Calificación 3: " + row["calificacion3"]);
                    Console.WriteLine("Promedio: " + row["promedioMateria"]);
                }

                gvCalificaciones.DataSource = dt;
                gvCalificaciones.DataBind();
            }
        }

        protected void gvCalificaciones_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView gvCalificaciones = (GridView)sender;
            gvCalificaciones.EditIndex = e.NewEditIndex;
            LoadCalificaciones(GetMateriaIdFromGridView(gvCalificaciones), gvCalificaciones);
        }

        protected void gvCalificaciones_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridView gvCalificaciones = (GridView)sender;
            string idCalificacion = gvCalificaciones.DataKeys[e.RowIndex].Value.ToString();
            string calificacion1 = ((TextBox)gvCalificaciones.Rows[e.RowIndex].FindControl("txtCalificacion1")).Text;
            string calificacion2 = ((TextBox)gvCalificaciones.Rows[e.RowIndex].FindControl("txtCalificacion2")).Text;
            string calificacion3 = ((TextBox)gvCalificaciones.Rows[e.RowIndex].FindControl("txtCalificacion3")).Text;

            decimal? cal1 = string.IsNullOrWhiteSpace(calificacion1) ? (decimal?)null : decimal.Parse(calificacion1);
            decimal? cal2 = string.IsNullOrWhiteSpace(calificacion2) ? (decimal?)null : decimal.Parse(calificacion2);
            decimal? cal3 = string.IsNullOrWhiteSpace(calificacion3) ? (decimal?)null : decimal.Parse(calificacion3);

            decimal promedio = ((cal1 ?? 0) + (cal2 ?? 0) + (cal3 ?? 0)) / 3;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string queryUpdate = @"
                    UPDATE calificacion 
                    SET calificacion1 = @calificacion1, 
                        calificacion2 = @calificacion2, 
                        calificacion3 = @calificacion3, 
                        promedioMateria = @promedioMateria
                    WHERE idCalificacion = @idCalificacion";

                SqlCommand cmdUpdate = new SqlCommand(queryUpdate, con);
                cmdUpdate.Parameters.AddWithValue("@calificacion1", (object)cal1 ?? DBNull.Value);
                cmdUpdate.Parameters.AddWithValue("@calificacion2", (object)cal2 ?? DBNull.Value);
                cmdUpdate.Parameters.AddWithValue("@calificacion3", (object)cal3 ?? DBNull.Value);
                cmdUpdate.Parameters.AddWithValue("@promedioMateria", promedio);
                cmdUpdate.Parameters.AddWithValue("@idCalificacion", idCalificacion);

                con.Open();
                cmdUpdate.ExecuteNonQuery();
                con.Close();
            }

            gvCalificaciones.EditIndex = -1;
            LoadCalificaciones(GetMateriaIdFromGridView(gvCalificaciones), gvCalificaciones);
        }

        protected void gvCalificaciones_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView gvCalificaciones = (GridView)sender;
            gvCalificaciones.EditIndex = -1;
            LoadCalificaciones(GetMateriaIdFromGridView(gvCalificaciones), gvCalificaciones);
        }

        //protected void btnAgregarCalificacion_Click(object sender, EventArgs e)
        //{
        //    string idMateria = ((Button)sender).CommandArgument;
        //    // Aquí puedes agregar lógica para mostrar un formulario modal o un cuadro de diálogo para seleccionar un estudiante y añadir una calificación
        //    // También puedes redirigir a una página de inserción de calificaciones
        //}

        //protected void btnGenerarExcel_Click(object sender, EventArgs e)
        //{
        //    string idMateria = ((Button)sender).CommandArgument;
        //    GridView gvCalificaciones = GetGridViewByMateriaId(idMateria);
        //    if (gvCalificaciones != null)
        //    {
        //        ExportToExcel(gvCalificaciones, idMateria);
        //    }
        //    else
        //    {
        //        // Puedes agregar un mensaje de error para indicar que el GridView no se encontró
        //        Response.Write("<script>alert('GridView no encontrado.');</script>");
        //    }
        //}

        //private void ExportToPDF(GridView gv, string nombreMateria)
        //{
        //    using (MemoryStream memoryStream = new MemoryStream())
        //    {
        //        Document document = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
        //        PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);

        //        try
        //        {
        //            document.Open();

        //            // Agregar el encabezado
        //            document.Add(new Paragraph("Reporte de Calificaciones"));
        //            document.Add(new Paragraph("Materia: " + nombreMateria));
        //            document.Add(new Paragraph("Generado por: " + User.Identity.Name));
        //            document.Add(new Paragraph("Fecha: " + DateTime.Now.ToString()));
        //            document.Add(new Paragraph(" ")); // Espacio

        //            PdfPTable pdfTable = new PdfPTable(gv.HeaderRow.Cells.Count - 1); // Excluye la columna de Acciones

        //            // Agregar encabezados al PDF
        //            foreach (TableCell headerCell in gv.HeaderRow.Cells)
        //            {
        //                if (headerCell.Text != "Acciones")
        //                {
        //                    PdfPCell pdfCell = new PdfPCell(new Phrase(headerCell.Text));
        //                    pdfTable.AddCell(pdfCell);
        //                }
        //            }

        //            // Agregar filas al PDF
        //            foreach (GridViewRow gridViewRow in gv.Rows)
        //            {
        //                for (int i = 0; i < gridViewRow.Cells.Count - 1; i++) // Excluye la última columna (Acciones)
        //                {
        //                    string cellText = gridViewRow.Cells[i].Text.Trim();
        //                    Console.WriteLine($"Cell Text[{i}]: {cellText}"); // Depurar
        //                    PdfPCell pdfCell = new PdfPCell(new Phrase(string.IsNullOrWhiteSpace(cellText) ? " " : cellText));
        //                    pdfTable.AddCell(pdfCell);
        //                }
        //            }

        //            document.Add(pdfTable);
        //        }
        //        catch (Exception ex)
        //        {
        //            // Manejo de errores
        //            Console.WriteLine("Error al generar PDF: " + ex.Message);
        //        }
        //        finally
        //        {
        //            document.Close();
        //        }

        //        Response.ContentType = "application/pdf";
        //        Response.AddHeader("content-disposition", "attachment;filename=" + nombreMateria + "_Calificaciones.pdf");
        //        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //        Response.BinaryWrite(memoryStream.ToArray());
        //        Response.End();
        //    }
        //}




        private string GetMateriaIdFromGridView(GridView gvCalificaciones)
        {
            RepeaterItem repeaterItem = (RepeaterItem)gvCalificaciones.NamingContainer;
            HiddenField hfMateriaId = (HiddenField)repeaterItem.FindControl("hfMateriaId");

            if (hfMateriaId != null)
            {
                return hfMateriaId.Value;
            }
            else
            {
                throw new Exception("No se encontró el HiddenField hfMateriaId. Asegúrate de que existe en el Repeater y tiene el ID correcto.");
            }
        }



        private GridView GetGridViewByMateriaId(string idMateria)
        {
            foreach (RepeaterItem item in rptMaterias.Items)
            {
                HiddenField hfMateriaId = (HiddenField)item.FindControl("hfMateriaId");
                if (hfMateriaId != null && hfMateriaId.Value == idMateria)
                {
                    return (GridView)item.FindControl("gvCalificaciones");
                }
            }
            return null;
        }
        //private void ExportToExcel(GridView gv, string nombreMateria)
        //{
        //    using (XLWorkbook wb = new XLWorkbook())
        //    {
        //        // Crear DataTable desde GridView
        //        DataTable dt = new DataTable();
        //        foreach (TableCell cell in gv.HeaderRow.Cells)
        //        {
        //            dt.Columns.Add(cell.Text);
        //        }
        //        foreach (GridViewRow row in gv.Rows)
        //        {
        //            DataRow dr = dt.NewRow();
        //            for (int i = 0; i < row.Cells.Count; i++)
        //            {
        //                dr[i] = row.Cells[i].Text.Trim();
        //            }
        //            dt.Rows.Add(dr);
        //        }

        //        // Añadir DataTable a Workbook
        //        wb.Worksheets.Add(dt, nombreMateria);

        //        // Configuración de la respuesta HTTP para descarga del archivo
        //        Response.Clear();
        //        Response.Buffer = true;
        //        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //        Response.AddHeader("content-disposition", $"attachment;filename={nombreMateria}_Calificaciones.xlsx");
        //        using (MemoryStream memoryStream = new MemoryStream())
        //        {
        //            wb.SaveAs(memoryStream);
        //            memoryStream.WriteTo(Response.OutputStream);
        //            Response.Flush();
        //            Response.End();
        //        }
        //    }
        //}

      private void ExportToPDF(GridView gv, string nombreMateria)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                Document document = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);

                try
                {
                    document.Open();

                    // Agregar el encabezado
                    document.Add(new Paragraph("Reporte de Calificaciones"));
                    document.Add(new Paragraph("Materia: " + nombreMateria));
                    document.Add(new Paragraph("Generado por: " + User.Identity.Name));
                    document.Add(new Paragraph("Fecha: " + DateTime.Now.ToString()));
                    document.Add(new Paragraph(" ")); // Espacio

                    PdfPTable pdfTable = new PdfPTable(gv.HeaderRow.Cells.Count - 1); // Excluye la columna de Acciones

                    // Agregar encabezados al PDF
                    foreach (TableCell headerCell in gv.HeaderRow.Cells)
                    {
                        if (headerCell.Text != "Acciones")
                        {
                            PdfPCell pdfCell = new PdfPCell(new Phrase(headerCell.Text));
                            pdfTable.AddCell(pdfCell);
                        }
                    }

                    // Agregar filas al PDF
                    foreach (GridViewRow gridViewRow in gv.Rows)
                    {
                        for (int i = 0; i < gridViewRow.Cells.Count - 1; i++) // Excluye la última columna (Acciones)
                        {
                            string cellText = gridViewRow.Cells[i].Text.Trim();
                            PdfPCell pdfCell = new PdfPCell(new Phrase(string.IsNullOrWhiteSpace(cellText) ? " " : cellText));
                            pdfTable.AddCell(pdfCell);
                        }
                    }

                    document.Add(pdfTable);
                }
                catch (Exception ex)
                {
                    // Manejo de errores
                    Console.WriteLine("Error al generar PDF: " + ex.Message);
                }
                finally
                {
                    document.Close();
                }

                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=" + nombreMateria + "_Calificaciones.pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite(memoryStream.ToArray());
                Response.End();
            }
        }


    }
}
