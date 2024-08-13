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
        private string GetMateriaIdFromGridView(GridView gvCalificaciones)
        {
            // El GridView está dentro de un RepeaterItem, necesitamos obtener ese item
            RepeaterItem repeaterItem = (RepeaterItem)gvCalificaciones.NamingContainer;

            // Encontramos el HiddenField que contiene el idMateria
            HiddenField hfMateriaId = (HiddenField)repeaterItem.FindControl("hfMateriaId");

            // Validamos si el HiddenField se encontró
            if (hfMateriaId != null)
            {
                return hfMateriaId.Value; // Devolvemos el valor del HiddenField
            }
            else
            {
                throw new Exception("No se encontró el HiddenField hfMateriaId. Asegúrate de que existe en el Repeater y tiene el ID correcto.");
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

            // Verificación y conversión de las calificaciones
            string calificacion1 = ((TextBox)gvCalificaciones.Rows[e.RowIndex].FindControl("txtCalificacion1")).Text;
            string calificacion2 = ((TextBox)gvCalificaciones.Rows[e.RowIndex].FindControl("txtCalificacion2")).Text;
            string calificacion3 = ((TextBox)gvCalificaciones.Rows[e.RowIndex].FindControl("txtCalificacion3")).Text;

            if (!EsCalificacionValida(calificacion1) || !EsCalificacionValida(calificacion2) || !EsCalificacionValida(calificacion3))
            {
                Response.Write("<script>alert('Las calificaciones deben ser numéricas y no mayores a 10.');</script>");
                return;
            }

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

        protected void btnAgregarCalificacion_Click(object sender, EventArgs e)
        {
            string idMateria = ((Button)sender).CommandArgument;
            // Aquí puedes agregar lógica para mostrar un formulario modal o un cuadro de diálogo para seleccionar un estudiante y añadir una calificación
            // También puedes redirigir a una página de inserción de calificaciones
        }

        protected void btnGenerarExcel_Click(object sender, EventArgs e)
        {
            string idMateria = ((Button)sender).CommandArgument;
            string nombreMateria = GetNombreMateria(idMateria);
            if (!string.IsNullOrEmpty(nombreMateria))
            {
                ExportToExcel(idMateria, nombreMateria);
            }
        }

        protected void btnGenerarPDF_Click(object sender, EventArgs e)
        {
            string idMateria = ((Button)sender).CommandArgument;
            string nombreMateria = GetNombreMateria(idMateria);
            if (!string.IsNullOrEmpty(nombreMateria))
            {
                ExportToPDF(idMateria, nombreMateria);
            }
        }

        private string GetNombreMateria(string idMateria)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT nombre FROM materia WHERE idMateria = @idMateria";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@idMateria", idMateria);

                con.Open();
                return cmd.ExecuteScalar()?.ToString();
            }
        }

        private bool EsCalificacionValida(string calificacion)
        {
            if (string.IsNullOrWhiteSpace(calificacion)) return true; // Campo vacío permitido
            if (decimal.TryParse(calificacion, out decimal cal))
            {
                return cal >= 0 && cal <= 10;
            }
            return false; // No es numérico
        }

        private void ExportToPDF(string idMateria, string nombreMateria)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                Document document = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);

                try
                {
                    document.Open();

                    // Agregar el encabezado
                    document.Add(new Paragraph("Reporte de calificaciones " + nombreMateria));
                    document.Add(new Paragraph("Materia: " + nombreMateria));
                    document.Add(new Paragraph("Generado por: " + User.Identity.Name));
                    document.Add(new Paragraph("Fecha: " + DateTime.Now.ToString()));
                    document.Add(new Paragraph(" ")); // Espacio

                    // Crear la tabla PDF con las columnas necesarias
                    PdfPTable pdfTable = new PdfPTable(5); // Número de columnas: Nombre + 3 parciales + promedio

                    // Agregar los encabezados de las columnas
                    pdfTable.AddCell("Nombre del Estudiante");
                    pdfTable.AddCell("Calificación 1er Parcial");
                    pdfTable.AddCell("Calificación 2do Parcial");
                    pdfTable.AddCell("Calificación 3er Parcial");
                    pdfTable.AddCell("Promedio General");

                    // Consultar la base de datos para obtener las calificaciones
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        string queryCalificaciones = @"
                        SELECT 
                            CONCAT(e.nombre, ' ', e.paterno, ' ', e.materno) AS nombreCompleto,
                            c.calificacion1, c.calificacion2, c.calificacion3, c.promedioMateria
                        FROM calificacion c
                        JOIN estudiante e ON c.idEstudiante = e.matricula
                        WHERE c.idMateria = @idMateria";

                        SqlCommand cmdCalificaciones = new SqlCommand(queryCalificaciones, con);
                        cmdCalificaciones.Parameters.AddWithValue("@idMateria", idMateria);

                        con.Open();
                        SqlDataReader reader = cmdCalificaciones.ExecuteReader();

                        // Agregar las filas de datos al PDF
                        while (reader.Read())
                        {
                            pdfTable.AddCell(reader["nombreCompleto"].ToString());
                            pdfTable.AddCell(reader["calificacion1"] != DBNull.Value ? reader["calificacion1"].ToString() : "N/A");
                            pdfTable.AddCell(reader["calificacion2"] != DBNull.Value ? reader["calificacion2"].ToString() : "N/A");
                            pdfTable.AddCell(reader["calificacion3"] != DBNull.Value ? reader["calificacion3"].ToString() : "N/A");
                            pdfTable.AddCell(reader["promedioMateria"] != DBNull.Value ? reader["promedioMateria"].ToString() : "N/A");
                        }

                        reader.Close();
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

        private void ExportToExcel(string idMateria, string nombreMateria)
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                // Crear DataTable
                DataTable dt = new DataTable();
                dt.Columns.Add("Nombre del Estudiante");
                dt.Columns.Add("Calificación 1er Parcial");
                dt.Columns.Add("Calificación 2do Parcial");
                dt.Columns.Add("Calificación 3er Parcial");
                dt.Columns.Add("Promedio");

                // Consultar la base de datos para obtener las calificaciones
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string queryCalificaciones = @"
                SELECT 
                    CONCAT(e.nombre, ' ', e.paterno, ' ', e.materno) AS nombreCompleto,
                    c.calificacion1, c.calificacion2, c.calificacion3, c.promedioMateria
                FROM calificacion c
                JOIN estudiante e ON c.idEstudiante = e.matricula
                WHERE c.idMateria = @idMateria";

                    SqlCommand cmdCalificaciones = new SqlCommand(queryCalificaciones, con);
                    cmdCalificaciones.Parameters.AddWithValue("@idMateria", idMateria);

                    con.Open();
                    SqlDataReader reader = cmdCalificaciones.ExecuteReader();

                    // Llenar el DataTable con los datos obtenidos
                    while (reader.Read())
                    {
                        DataRow dr = dt.NewRow();
                        dr["Nombre del Estudiante"] = reader["nombreCompleto"].ToString();
                        dr["Calificación 1er Parcial"] = reader["calificacion1"] != DBNull.Value ? reader["calificacion1"].ToString() : "N/A";
                        dr["Calificación 2do Parcial"] = reader["calificacion2"] != DBNull.Value ? reader["calificacion2"].ToString() : "N/A";
                        dr["Calificación 3er Parcial"] = reader["calificacion3"] != DBNull.Value ? reader["calificacion3"].ToString() : "N/A";
                        dr["Promedio"] = reader["promedioMateria"] != DBNull.Value ? reader["promedioMateria"].ToString() : "N/A";
                        dt.Rows.Add(dr);
                    }

                    reader.Close();
                }

                // Añadir DataTable a Workbook
                var ws = wb.Worksheets.Add(dt, "Calificaciones");

                // Configuración de la respuesta HTTP para descarga del archivo
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", $"attachment;filename={nombreMateria}_Calificaciones.xlsx");
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    wb.SaveAs(memoryStream);
                    memoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }

    }
}