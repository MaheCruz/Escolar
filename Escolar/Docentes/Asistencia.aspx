<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Asistencia.aspx.cs" Inherits="Escolar.Docentes.Asistencia" %>
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
        <h2>Registro de Asistencia</h2>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="validation-summary" HeaderText="Por favor, corrija los siguientes errores:" />
        <asp:Label ID="lblMessage" runat="server" CssClass="message" Text=""></asp:Label>
        <table>
            <tr>
                <td>Materia:</td>
                <td>
                    <asp:DropDownList ID="ddlMateria" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMateria_SelectedIndexChanged">
                        <asp:ListItem Text="Seleccione una materia" Value="" />
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvMateria" runat="server" ControlToValidate="ddlMateria" InitialValue="" ErrorMessage="La materia es obligatoria." CssClass="validation-message" />
                </td>
            </tr>
        </table>
        <asp:Button ID="btnCargarAlumnos" runat="server" Text="Cargar Alumnos" CssClass="btn" OnClick="btnCargarAlumnos_Click" />
        <asp:Label ID="lblCargarAlumnos" runat="server" CssClass="message" Text=""></asp:Label>
        <div class="gridview-container">
            <asp:GridView ID="gvAlumnos" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No hay alumnos en esta materia">
                <Columns>
                    <asp:BoundField DataField="matricula" HeaderText="Matrícula" />
                    <asp:BoundField DataField="nombreCompleto" HeaderText="Nombre del Estudiante" />
                    <asp:TemplateField HeaderText="Asistencia">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkAsistencia" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <asp:Button ID="btnGuardarAsistencia" runat="server" Text="Guardar Asistencia" CssClass="btn" OnClick="btnGuardarAsistencia_Click" />
    </div>

    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
</asp:Content>

