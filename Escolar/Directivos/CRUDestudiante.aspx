<%@ Page Title="CRUD ESTUDIANTE" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CRUDestudiante.aspx.cs" Inherits="Escolar.Directivos.CRUDestudiante" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Gestión de Estudiantes</h1>
    <div class="container mt-5">
        <!-- Mensajes de éxito y error -->
        <asp:Panel ID="PanelExito" runat="server" CssClass="alert alert-success alert-dismissible" Visible="false">
            <button type="button" class="close" data-dismissible="alert">&times;</button>
            <strong>Éxito!</strong> <asp:Label ID="lblMensajeExito" runat="server" Text=""></asp:Label>
        </asp:Panel>
        <asp:Panel ID="PanelError" runat="server" CssClass="alert alert-danger alert-dismissible" Visible="false">
            <button type="button" class="close" data-dismissible="alert">&times;</button>
            <strong>Error!</strong> <asp:Label ID="lblMensajeError" runat="server" Text=""></asp:Label>
        </asp:Panel>

        <!-- Controles de formulario -->
        <div class="row">
            <div class="col-md-6">
                <label for="ddlUsuarios">Usuario:</label>
                <asp:DropDownList ID="ddlUsuarios" runat="server" DataSourceID="SqlDataSourceUsuarios" DataTextField="UserName" DataValueField="Id" CssClass="form-control">
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSourceUsuarios" runat="server"
                    ConnectionString="<%$ ConnectionStrings:DefaultConnection %>"
                    SelectCommand="SELECT AspNetUsers.Id, AspNetUsers.UserName FROM AspNetUsers 
                                   JOIN AspNetUserRoles ON AspNetUsers.Id = AspNetUserRoles.UserId 
                                   JOIN AspNetRoles ON AspNetUserRoles.RoleId = AspNetRoles.Id 
                                   WHERE AspNetRoles.Name = 'Estudiante'">
                </asp:SqlDataSource>
            </div>
            <div class="col-md-6">
                <label for="txtMatricula">Matrícula:</label>
                <asp:TextBox ID="txtMatricula" runat="server" Placeholder="Matrícula" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="row mt-3">
            <div class="col-md-4">
                <label for="txtNombre">Nombre:</label>
                <asp:TextBox ID="txtNombre" runat="server" Placeholder="Nombre" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-4">
                <label for="txtPaterno">Apellido Paterno:</label>
                <asp:TextBox ID="txtPaterno" runat="server" Placeholder="Apellido Paterno" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-4">
                <label for="txtMaterno">Apellido Materno:</label>
                <asp:TextBox ID="txtMaterno" runat="server" Placeholder="Apellido Materno" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="row mt-3">
            <div class="col-md-4">
                <label for="txtCURP">CURP:</label>
                <asp:TextBox ID="txtCURP" runat="server" Placeholder="CURP" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-4">
                <label for="txtTelefono">Teléfono:</label>
                <asp:TextBox ID="txtTelefono" runat="server" Placeholder="Teléfono" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-4">
                <label for="txtDireccion">Dirección:</label>
                <asp:TextBox ID="txtDireccion" runat="server" Placeholder="Dirección" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="row mt-3">
            <div class="col-md-4">
                <label for="ddlTipoSangre">Tipo de Sangre:</label>
                <asp:DropDownList ID="ddlTipoSangre" runat="server" CssClass="form-control">
                    <asp:ListItem Text="O+" Value="O+"></asp:ListItem>
                    <asp:ListItem Text="O-" Value="O-"></asp:ListItem>
                    <asp:ListItem Text="A+" Value="A+"></asp:ListItem>
                    <asp:ListItem Text="A-" Value="A-"></asp:ListItem>
                    <asp:ListItem Text="B+" Value="B+"></asp:ListItem>
                    <asp:ListItem Text="B-" Value="B-"></asp:ListItem>
                    <asp:ListItem Text="AB+" Value="AB+"></asp:ListItem>
                    <asp:ListItem Text="AB-" Value="AB-"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-md-4">
                <label for="txtPromGeneral">Promedio General:</label>
                <asp:TextBox ID="txtPromGeneral" runat="server" Placeholder="Promedio General" CssClass="form-control" Enabled="False"></asp:TextBox>
            </div>
        </div>
        <div class="row mt-3">
            <div class="col-md-4">
                <asp:Button ID="btnInsertar" runat="server" Text="Insertar" OnClick="btnInsertar_Click" CssClass="btn btn-primary btn-block" />
            </div>
            <div class="col-md-4">
                <asp:Button ID="btnModificar" runat="server" Text="Modificar" OnClick="btnModificar_Click" CssClass="btn btn-warning btn-block" />
            </div>
            <div class="col-md-4">
                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" OnClick="btnEliminar_Click" CssClass="btn btn-danger btn-block" />
            </div>
        </div>
        <div class="gridview-container mt-5">
            <asp:GridView ID="GVEstudiantes" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSourceEstudiantes" CellPadding="4" CssClass="table table-striped" OnSelectedIndexChanged="GVEstudiantes_SelectedIndexChanged">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:CommandField ShowSelectButton="True" ButtonType="Button" />
                    <asp:BoundField DataField="NombreUsuario" HeaderText="Nombre de Usuario" SortExpression="NombreUsuario" />
                    <asp:BoundField DataField="matricula" HeaderText="Matrícula" SortExpression="matricula" />
                    <asp:BoundField DataField="nombre" HeaderText="Nombre" SortExpression="nombre" />
                    <asp:BoundField DataField="paterno" HeaderText="Paterno" SortExpression="paterno" />
                    <asp:BoundField DataField="materno" HeaderText="Materno" SortExpression="materno" />
                    <asp:BoundField DataField="curp" HeaderText="CURP" SortExpression="curp" />
                    <asp:BoundField DataField="telefono" HeaderText="Teléfono" SortExpression="telefono" />
                    <asp:BoundField DataField="direccion" HeaderText="Dirección" SortExpression="direccion" />
                    <asp:BoundField DataField="tipoSangre" HeaderText="Tipo de Sangre" SortExpression="tipoSangre" />
                    <asp:BoundField DataField="promGeneral" HeaderText="Promedio General" SortExpression="promGeneral" />
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSourceEstudiantes" runat="server"
                ConnectionString="<%$ ConnectionStrings:DefaultConnection %>"
                SelectCommand="SELECT estudiante.idUsuario, AspNetUsers.UserName as NombreUsuario, estudiante.matricula, estudiante.nombre, estudiante.paterno, estudiante.materno, estudiante.curp, estudiante.telefono, estudiante.direccion, estudiante.tipoSangre, estudiante.promGeneral, estudiante.idTutor FROM estudiante JOIN AspNetUsers ON estudiante.idUsuario = AspNetUsers.Id">
            </asp:SqlDataSource>
        </div>
    </div>
</asp:Content>
