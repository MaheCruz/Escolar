<%@ Page Title="ASIGNAR ESTUDIANTES A UN GRUPO" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AsigEstudianteGrupo.aspx.cs" Inherits="Escolar.Directivos.AsigEstudianteGrupo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .btn-custom {
            background-color: #007bff;
            border-color: #007bff;
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
            background-color: #0056b3;
        }

        .alert-dismissible .close {
            position: absolute;
            top: 0;
            right: 10px;
            padding: 10px 20px;
            color: inherit;
        }

        .form-group {
            margin-bottom: 15px;
        }

        .alert {
            margin-top: 15px;
        }

        .container-custom {
            padding: 20px;
            background-color: #f9f9f9;
            border-radius: 8px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }

        .table th {
            background-color: #343a40;
            color: white !important; /* Forzar color blanco para el texto */
        }

        .table tr:nth-child(even) {
            background-color: #f2f2f2;
        }
    </style>
    <div class="container-custom">
        <h2>Asignar Estudiantes a Grupos</h2>

        <!-- Panel para mensajes de éxito -->
        <asp:Panel ID="PanelExito" runat="server" CssClass="alert alert-success alert-dismissible" Visible="false">
            <button type="button" class="close" data-dismiss="alert">&times;</button>
            <strong>¡Éxito!</strong> <asp:Label ID="lblMensajeExito" runat="server" Text=""></asp:Label>
        </asp:Panel>

        <!-- Panel para mensajes de error -->
        <asp:Panel ID="PanelError" runat="server" CssClass="alert alert-danger alert-dismissible" Visible="false">
            <button type="button" class="close" data-dismiss="alert">&times;</button>
            <strong>¡Error!</strong> <asp:Label ID="lblMensajeError" runat="server" Text=""></asp:Label>
        </asp:Panel>

        <!-- Sección de Estudiantes -->
        <div class="row">
            <div class="col-md-6 form-group">
                <h3>Estudiantes</h3>
                <asp:DropDownList ID="ddlEstudiantes" runat="server" CssClass="form-control" DataSourceID="SqlDataSourceEstudiantes" DataTextField="nombreCompleto" DataValueField="matricula"></asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSourceEstudiantes" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>"
                    SelectCommand="SELECT matricula, (nombre + ' ' + paterno + ' ' + materno) AS nombreCompleto FROM estudiante"></asp:SqlDataSource>
            </div>
            <div class="col-md-6 form-group">
                <h3>Grupos</h3>
                <asp:DropDownList ID="ddlGrupos" runat="server" CssClass="form-control" DataSourceID="SqlDataSourceGrupos" DataTextField="nombre" DataValueField="idGrupo"></asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSourceGrupos" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" 
                    SelectCommand="SELECT idGrupo, nombre FROM grupo"></asp:SqlDataSource>
            </div>
        </div>

        <!-- Botones de asignación -->
        <div class="row mt-5">
            <div class="col-md-12 form-group">
                <asp:Button ID="btnAsignarEstudiante" runat="server" CssClass="btn-custom" Text="Asignar Estudiante" OnClick="btnAsignarEstudiante_Click" />
                <asp:Button ID="btnModificarAsignacion" runat="server" CssClass="btn-custom" Text="Modificar Asignación" OnClick="btnModificarAsignacion_Click" />
            </div>
        </div>

        <!-- Sección de Asignaciones -->
        <div class="row mt-5">
            <div class="col-md-12 form-group">
                <h3>Asignaciones</h3>
                <asp:GridView ID="GVGrupoEstudiante" runat="server" AutoGenerateColumns="False" CssClass="table table-striped" DataKeyNames="idEstudiante,idGrupo"
                    DataSourceID="SqlDataSourceGrupoEstudiante" OnSelectedIndexChanged="GVGrupoEstudiante_SelectedIndexChanged">
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" />
                        <asp:BoundField DataField="idEstudiante" HeaderText="ID Estudiante" />
                        <asp:BoundField DataField="idGrupo" HeaderText="ID Grupo" />
                        <asp:BoundField DataField="nombreCompleto" HeaderText="Nombre del Estudiante" />
                        <asp:BoundField DataField="nombreGrupo" HeaderText="Nombre del Grupo" />
                        <asp:BoundField DataField="promedio" HeaderText="Promedio" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnEliminarAsignacion" runat="server" Text="Eliminar" CssClass="btn-custom" CommandArgument='<%# Eval("idEstudiante") + "," + Eval("idGrupo") %>' OnClick="btnEliminarAsignacion_Click" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSourceGrupoEstudiante" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>"
                    SelectCommand="SELECT ge.idEstudiante, ge.idGrupo, (e.nombre + ' ' + e.paterno + ' ' + e.materno) AS nombreCompleto, g.nombre AS nombreGrupo, ge.promedio FROM grupoEstudiante ge JOIN estudiante e ON ge.idEstudiante = e.matricula JOIN grupo g ON ge.idGrupo = g.idGrupo"></asp:SqlDataSource>
            </div>
        </div>

        <!-- Campos ocultos para almacenar los valores seleccionados -->
        <asp:HiddenField ID="hfIdEstudianteOriginal" runat="server" />
        <asp:HiddenField ID="hfIdGrupoOriginal" runat="server" />
    </div>
</asp:Content>
