<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AsigTutorEstudiante.aspx.cs" Inherits="Escolar.Directivos.AsigTutorEstudiante" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .container-custom {
            padding: 20px;
            background-color: #f9f9f9;
            border-radius: 8px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }

        .btn-custom {
            background-color: #28a745;
            border-color: #28a745;
            color: white;
            padding: 10px 20px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 16px;
            margin: 4px 2px;
            cursor: pointer;
            border-radius: 4px;
            transition: background-color 0.3s ease;
        }

        .btn-custom:hover {
            background-color: #218838;
        }

        .btn-warning-custom {
            background-color: #ffc107;
            border-color: #ffc107;
            color: white;
            padding: 10px 20px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 16px;
            margin: 4px 2px;
            cursor: pointer;
            border-radius: 4px;
            transition: background-color 0.3s ease;
        }

        .btn-warning-custom:hover {
            background-color: #e0a800;
        }

        .btn-danger-custom {
            background-color: #dc3545;
            border-color: #dc3545;
            color: white;
            padding: 10px 20px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 16px;
            margin: 4px 2px;
            cursor: pointer;
            border-radius: 4px;
            transition: background-color 0.3s ease;
        }

        .btn-danger-custom:hover {
            background-color: #c82333;
        }

        .form-group {
            margin-bottom: 15px;
        }

        .alert-dismissible .close {
            position: absolute;
            top: 0;
            right: 10px;
            padding: 10px 20px;
            color: inherit;
        }

        .alert {
            margin-top: 15px;
        }

        .table th {
            background-color: #343a40;
            color: white !important;
        }
    </style>
    <div class="container-custom">
        <h2>Asignar Tutor a Estudiante</h2>
        <div class="form-group">
            <asp:Label ID="lblSeleccionarEstudiante" runat="server" Text="Seleccionar Estudiante:" AssociatedControlID="ddlEstudiantes"></asp:Label>
            <asp:DropDownList ID="ddlEstudiantes" runat="server" CssClass="form-control" DataSourceID="SqlDataSourceEstudiantes" DataTextField="NombreCompleto" DataValueField="IdEstudiante">
            </asp:DropDownList>
            <asp:SqlDataSource ID="SqlDataSourceEstudiantes" runat="server" 
                ConnectionString='<%$ ConnectionStrings:DefaultConnection %>'
                SelectCommand="SELECT matricula AS IdEstudiante, CONCAT(nombre, ' ', paterno, ' ', materno) AS NombreCompleto FROM estudiante">
            </asp:SqlDataSource>
        </div>
        <div class="form-group">
            <asp:Label ID="lblSeleccionarTutor" runat="server" Text="Seleccionar Tutor:" AssociatedControlID="ddlTutores"></asp:Label>
            <asp:DropDownList ID="ddlTutores" runat="server" CssClass="form-control" DataSourceID="SqlDataSourceTutores" DataTextField="NombreCompleto" DataValueField="IdTutor">
            </asp:DropDownList>
            <asp:SqlDataSource ID="SqlDataSourceTutores" runat="server" 
                ConnectionString='<%$ ConnectionStrings:DefaultConnection %>'
                SelectCommand="SELECT idTutor AS IdTutor, CONCAT(nombre, ' ', paterno, ' ', materno) AS NombreCompleto FROM tutor">
            </asp:SqlDataSource>
        </div>
        <asp:Button ID="btnAsignar" runat="server" Text="Asignar Tutor" CssClass="btn-custom" OnClick="btnAsignar_Click" />
        <asp:Panel ID="PanelExito" runat="server" CssClass="alert alert-success alert-dismissible" Visible="false">
            <button type="button" class="close" data-dismissible="alert">&times;</button>
            <strong>Éxito!</strong> Tutor asignado correctamente.
        </asp:Panel>
        <asp:Panel ID="PanelError" runat="server" CssClass="alert alert-danger alert-dismissible" Visible="false">
            <button type="button" class="close" data-dismissible="alert">&times;</button>
            <strong>Error!</strong> No se pudo asignar el tutor.
        </asp:Panel>

        <h3>Relaciones de Tutor y Estudiante</h3>
        <asp:GridView ID="gvRelaciones" runat="server" AutoGenerateColumns="False" DataKeyNames="IdEstudiante" DataSourceID="SqlDataSourceRelaciones" CssClass="table table-striped">
            <Columns>
                <asp:BoundField DataField="NombreEstudiante" HeaderText="Nombre del Estudiante" ReadOnly="True"></asp:BoundField>
                <asp:BoundField DataField="NombreTutor" HeaderText="Nombre del Tutor" ReadOnly="True"></asp:BoundField>
                <asp:BoundField DataField="Relacion" HeaderText="Relación" ReadOnly="True"></asp:BoundField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="btnModificar" runat="server" Text="Modificar" CssClass="btn-warning-custom" CommandArgument='<%# Eval("IdEstudiante") %>' OnClick="btnModificar_Click" />
                        <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn-danger-custom" CommandArgument='<%# Eval("IdEstudiante") %>' OnClick="btnEliminar_Click" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSourceRelaciones" runat="server" 
            ConnectionString='<%$ ConnectionStrings:DefaultConnection %>'
            SelectCommand="SELECT e.matricula AS IdEstudiante, CONCAT(e.nombre, ' ', e.paterno, ' ', e.materno) AS NombreEstudiante, CONCAT(t.nombre, ' ', t.paterno, ' ', t.materno) AS NombreTutor, t.relacion AS Relacion FROM estudiante e LEFT JOIN tutor t ON e.idTutor = t.idTutor">
        </asp:SqlDataSource>
    </div>
</asp:Content>