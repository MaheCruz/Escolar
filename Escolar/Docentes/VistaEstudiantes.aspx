<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VistaEstudiantes.aspx.cs" Inherits="Escolar.Docentes.VistaEstudiantes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .form-container {
            max-width: 800px;
            margin: 0 auto;
            padding: 20px;
            border: 1px solid #ccc;
            border-radius: 10px;
            background-color: #f9f9f9;
        }
        .form-container h2 {
            text-align: center;
            color: #333;
        }
        .form-container table {
            width: 100%;
            margin-top: 20px;
        }
        .form-container table th,
        .form-container table td {
            border: 1px solid #ddd;
            padding: 8px;
            text-align: left;
        }
        .form-container table th {
            background-color: #f2f2f2;
            color: #333;
        }
        .form-container table tr:hover {
            background-color: #f1f1f1;
        }
        .form-container .btn {
            background-color: #007bff;
            color: white;
            border: none;
            border-radius: 5px;
            padding: 10px 20px;
            cursor: pointer;
            margin: 5px;
        }
        .form-container .btn:hover {
            background-color: #0056b3;
        }
    </style>
    <div class="form-container">
        <h2>Vista de Estudiantes y Calificaciones</h2>
        <table>
            <tr>
                <td>Materia:</td>
                <td>
                    <asp:DropDownList ID="ddlMateria" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMateria_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <div>
            <asp:GridView ID="gvEstudiantes" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="matricula" HeaderText="ID Estudiante" />
                    <asp:BoundField DataField="nombreCompleto" HeaderText="Nombre Completo" />
                    <asp:BoundField DataField="calificacion1" HeaderText="Calificación 1" />
                    <asp:BoundField DataField="calificacion2" HeaderText="Calificación 2" />
                    <asp:BoundField DataField="calificacion3" HeaderText="Calificación 3" />
                    <asp:BoundField DataField="promedioMateria" HeaderText="Promedio" />
                </Columns>
            </asp:GridView>
        </div>
        <div style="text-align: center; margin-top: 20px;">
            <asp:Button ID="btnGenerarPDF" runat="server" Text="Generar PDF" CssClass="btn" OnClick="btnGenerarPDF_Click" />
            <asp:Button ID="btnGenerarExcel" runat="server" Text="Generar Excel" CssClass="btn" OnClick="btnGenerarExcel_Click" />
        </div>
    </div>
</asp:Content>
