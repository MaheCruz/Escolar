<%@ Page Title="ASIGNAR MATERIAS A GRUPO" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AsigGrupoMateria.aspx.cs" Inherits="Escolar.Directivos.AsigGrupoMateria" %>
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
        <h2>Gestión de Grupos y Materias</h2>

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

        <!-- Sección de Grupo -->
        <div class="row">
            <div class="col-md-6 form-group">
                <h3>Crear Grupo</h3>
                <asp:TextBox ID="txtIdGrupo" runat="server" CssClass="form-control" Placeholder="ID del Grupo"></asp:TextBox>
                <asp:TextBox ID="txtNombreGrupo" runat="server" CssClass="form-control" Placeholder="Nombre del Grupo"></asp:TextBox>
                <asp:TextBox ID="txtDescripcionGrupo" runat="server" CssClass="form-control" Placeholder="Descripción"></asp:TextBox>
                <asp:TextBox ID="txtInicioGrupo" runat="server" CssClass="form-control" Placeholder="Fecha de Inicio" TextMode="Date"></asp:TextBox>
                <asp:TextBox ID="txtFinGrupo" runat="server" CssClass="form-control" Placeholder="Fecha de Fin" TextMode="Date"></asp:TextBox>
                <asp:Button ID="btnCrearGrupo" runat="server" CssClass="btn-custom" Text="Crear Grupo" OnClick="btnCrearGrupo_Click" />
                <asp:Button ID="btnModificarGrupo" runat="server" CssClass="btn-custom" Text="Modificar Grupo" OnClick="btnModificarGrupo_Click" />
                <asp:Button ID="btnEliminarGrupo" runat="server" CssClass="btn-custom" Text="Eliminar Grupo" OnClick="btnEliminarGrupo_Click" />
            </div>
            <div class="col-md-6 form-group">
                <h3>Grupos</h3>
                <asp:GridView ID="GVGrupos" runat="server" AutoGenerateColumns="False" CssClass="table table-striped" DataKeyNames="idGrupo"
                    DataSourceID="SqlDataSourceGrupos" OnSelectedIndexChanged="GVGrupos_SelectedIndexChanged">
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" />
                        <asp:BoundField DataField="idGrupo" HeaderText="ID Grupo" />
                        <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                        <asp:BoundField DataField="descripcion" HeaderText="Descripción" />
                        <asp:BoundField DataField="inicio" HeaderText="Fecha de Inicio" />
                        <asp:BoundField DataField="fin" HeaderText="Fecha de Fin" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSourceGrupos" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>"
                    SelectCommand="SELECT idGrupo, nombre, descripcion, inicio, fin FROM grupo"></asp:SqlDataSource>
            </div>
        </div>

        <!-- Sección de Materia -->
        <div class="row mt-5">
            <div class="col-md-6 form-group">
                <h3>Crear Materia</h3>
                <asp:TextBox ID="txtIdMateria" runat="server" CssClass="form-control" Placeholder="ID de la Materia"></asp:TextBox>
                <asp:TextBox ID="txtNombreMateria" runat="server" CssClass="form-control" Placeholder="Nombre de la Materia"></asp:TextBox>
                <asp:TextBox ID="txtDescripcionMateria" runat="server" CssClass="form-control" Placeholder="Descripción"></asp:TextBox>
                <asp:DropDownList ID="ddlDocente" runat="server" CssClass="form-control" DataSourceID="SqlDataSourceDocentes" DataTextField="nombre" DataValueField="idDocente"></asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSourceDocentes" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT idDocente, nombre FROM docente"></asp:SqlDataSource>
                <asp:Button ID="btnCrearMateria" runat="server" CssClass="btn-custom" Text="Crear Materia" OnClick="btnCrearMateria_Click" />
                <asp:Button ID="btnModificarMateria" runat="server" CssClass="btn-custom" Text="Modificar Materia" OnClick="btnModificarMateria_Click" />
                <asp:Button ID="btnEliminarMateria" runat="server" CssClass="btn-custom" Text="Eliminar Materia" OnClick="btnEliminarMateria_Click" />
            </div>
            <div class="col-md-6 form-group">
                <h3>Materias</h3>
                <asp:GridView ID="GVMaterias" runat="server" AutoGenerateColumns="False" CssClass="table table-striped" DataKeyNames="idMateria"
                    DataSourceID="SqlDataSourceMaterias" OnSelectedIndexChanged="GVMaterias_SelectedIndexChanged">
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" />
                        <asp:BoundField DataField="idMateria" HeaderText="ID Materia" />
                        <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                        <asp:BoundField DataField="descripcion" HeaderText="Descripción" />
                        <asp:BoundField DataField="idDocente" HeaderText="ID Docente" Visible="false" />
                        <asp:BoundField DataField="nombreDocente" HeaderText="Nombre del Docente" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSourceMaterias" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>"
                    SelectCommand="SELECT m.idMateria, m.nombre, m.descripcion, m.idDocente, d.nombre AS nombreDocente FROM materia m LEFT JOIN docente d ON m.idDocente = d.idDocente"></asp:SqlDataSource>
            </div>
        </div>

        <!-- Sección de Asignación -->
        <div class="row mt-5">
            <div class="col-md-12 form-group">
                <h3>Asignar Materia a Grupo</h3>
                <asp:Button ID="btnAsignarMateria" runat="server" CssClass="btn-custom" Text="Asignar Materia" OnClick="btnAsignarMateria_Click" />
                <br />
                <p>Grupo Seleccionado: <asp:Label ID="lblGrupoSeleccionado" runat="server" Text=""></asp:Label></p>
                <p>Materia Seleccionada: <asp:Label ID="lblMateriaSeleccionada" runat="server" Text=""></asp:Label></p>

                <h3>Asignaciones</h3>
                <asp:GridView ID="GVGrupoMateria" runat="server" AutoGenerateColumns="False" CssClass="table table-striped" DataKeyNames="idGrupo,idMateria"
                    DataSourceID="SqlDataSourceGrupoMateria">
                    <Columns>
                        <asp:BoundField DataField="idGrupo" HeaderText="ID Grupo" Visible="false" />
                        <asp:BoundField DataField="idMateria" HeaderText="ID Materia" Visible="false" />
                        <asp:BoundField DataField="nombreGrupo" HeaderText="Nombre del Grupo" />
                        <asp:BoundField DataField="nombreMateria" HeaderText="Nombre de la Materia" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnEliminarAsignacion" runat="server" Text="Eliminar" CssClass="btn-custom" CommandArgument='<%# Eval("idGrupo") + "," + Eval("idMateria") %>' OnClick="btnEliminarAsignacion_Click" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSourceGrupoMateria" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>"
                    SelectCommand="SELECT gm.idGrupo, gm.idMateria, g.nombre AS nombreGrupo, m.nombre AS nombreMateria FROM grupoMateria gm JOIN grupo g ON gm.idGrupo = g.idGrupo JOIN materia m ON gm.idMateria = m.idMateria"></asp:SqlDataSource>
            </div>
        </div>
    </div>
</asp:Content>
