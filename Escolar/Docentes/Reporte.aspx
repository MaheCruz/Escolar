<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reporte.aspx.cs" Inherits="Escolar.Docentes.Reporte" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .form-container {
            max-width: 600px;
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
        }
        .form-container table td {
            padding: 10px;
        }
        .form-container table td:first-child {
            text-align: right;
            font-weight: bold;
            color: #555;
        }
        .form-container table td:last-child {
            text-align: left;
        }
        .form-container input[type="text"],
        .form-container select,
        .form-container textarea {
            width: 100%;
            padding: 8px;
            border: 1px solid #ccc;
            border-radius: 5px;
        }
        .form-container input[type="text"]:focus,
        .form-container select:focus,
        .form-container textarea:focus {
            border-color: #66afe9;
            outline: none;
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
        .form-container .message {
            text-align: center;
            font-weight: bold;
            color: green;
        }
        .form-container .validation-summary {
            color: red;
            text-align: center;
            margin-bottom: 10px;
        }
        .form-container .gridview-container {
            margin-top: 20px;
        }
        .form-container .gridview-container table {
            width: 100%;
            border-collapse: collapse;
        }
        .form-container .gridview-container th,
        .form-container .gridview-container td {
            border: 1px solid #ddd;
            padding: 8px;
            text-align: left;
        }
        .form-container .gridview-container th {
            background-color: #f2f2f2;
            color: #333;
        }
        .form-container .gridview-container tr:hover {
            background-color: #f1f1f1;
        }
    </style>

    <div class="form-container">
        <h2>Gestión de Reportes</h2>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="validation-summary" HeaderText="Por favor, corrija los siguientes errores:" />
        <asp:Label ID="lblMessage" runat="server" CssClass="message" Text=""></asp:Label>
        <table>
            <tr>
                <td>Tipo de Reporte:</td>
                <td>
                    <asp:DropDownList ID="ddlTipoReporte" runat="server">
                        <asp:ListItem Text="Seleccione un tipo" Value="" />
                        <asp:ListItem Text="Reunión" Value="Reunión" />
                        <asp:ListItem Text="Suspensión" Value="Suspensión" />
                        <asp:ListItem Text="Observación" Value="Observación" />
                        <asp:ListItem Text="Progreso Académico" Value="Progreso Académico" />
                        <asp:ListItem Text="Comportamiento" Value="Comportamiento" />
                        <asp:ListItem Text="Asistencia" Value="Asistencia" />
                        <asp:ListItem Text="Accidentes" Value="Accidentes" />
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvTipoReporte" runat="server" ControlToValidate="ddlTipoReporte" InitialValue="" ErrorMessage="El tipo de reporte es obligatorio." CssClass="validation-message" />
                </td>
            </tr>
            <tr>
                <td>Detalle:</td>
                <td>
                    <asp:TextBox ID="txtDetalle" runat="server" TextMode="MultiLine" Rows="4" Columns="50"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvDetalle" runat="server" ControlToValidate="txtDetalle" ErrorMessage="El detalle es obligatorio." CssClass="validation-message" />
                </td>
            </tr>
            <tr>
                <td>Fecha:</td>
                <td>
                    <asp:TextBox ID="txtFecha" runat="server"></asp:TextBox>
                    <asp:Calendar ID="calFecha" runat="server" OnSelectionChanged="calFecha_SelectionChanged"></asp:Calendar>
                    <asp:RequiredFieldValidator ID="rfvFecha" runat="server" ControlToValidate="txtFecha" ErrorMessage="La fecha es obligatoria." CssClass="validation-message" />
                </td>
            </tr>
            <tr>
                <td>Estudiante:</td>
                <td>
                    <asp:DropDownList ID="ddlEstudiante" runat="server">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvEstudiante" runat="server" ControlToValidate="ddlEstudiante" InitialValue="" ErrorMessage="El estudiante es obligatorio." CssClass="validation-message" />
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center;">
                    <asp:Button ID="btnSave" runat="server" Text="Guardar" CssClass="btn" OnClick="btnSave_Click" />
                    <asp:Button ID="btnUpdate" runat="server" Text="Actualizar" CssClass="btn" OnClick="btnUpdate_Click" />
                    <asp:Button ID="btnDelete" runat="server" Text="Eliminar" CssClass="btn" OnClick="btnDelete_Click" />
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hfIdReporte" runat="server" />
        <div class="gridview-container">
            <asp:GridView ID="gvReportes" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="gvReportes_SelectedIndexChanged">
                <Columns>
                    <asp:BoundField DataField="idReporte" HeaderText="ID" />
                    <asp:BoundField DataField="tipo" HeaderText="Tipo" />
                    <asp:BoundField DataField="detalle" HeaderText="Detalle" />
                    <asp:BoundField DataField="fecha" HeaderText="Fecha" />
                    <asp:BoundField DataField="nombreCompleto" HeaderText="Estudiante" />
                    <asp:CommandField ShowSelectButton="True" />
                </Columns>
            </asp:GridView>
        </div>
    </div>

    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
</asp:Content>

