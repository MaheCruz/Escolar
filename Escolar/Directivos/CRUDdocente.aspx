<%@ Page Title="CRUD DOCENTE" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CRUDdocente.aspx.cs" Inherits="Escolar.Directivos.CRUDdocente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Gestión de Docentes</h1>
    <div class="container">
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
            <div class="col-md-4">
                <label for="ddlUsuarios">Usuario:</label>
                <asp:DropDownList ID="ddlUsuarios" runat="server" DataSourceID="SqlDataSourceUsuarios" DataTextField="UserName" DataValueField="Id" CssClass="form-control">
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSourceUsuarios" runat="server"
                    ConnectionString="<%$ ConnectionStrings:DefaultConnection %>"
                    SelectCommand="SELECT AspNetUsers.Id, AspNetUsers.UserName FROM AspNetUsers 
                                   JOIN AspNetUserRoles ON AspNetUsers.Id = AspNetUserRoles.UserId 
                                   JOIN AspNetRoles ON AspNetUserRoles.RoleId = AspNetRoles.Id 
                                   WHERE AspNetRoles.Name = 'Docente'">
                </asp:SqlDataSource>
            </div>
        </div>
        <div class="row mt-3">
            <div class="col-md-4">
                <asp:TextBox ID="txtIdDocente" runat="server" Placeholder="ID Docente" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="row mt-3">
            <div class="col-md-3">
                <asp:TextBox ID="txtNombre" runat="server" Placeholder="Nombre" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-3">
                <asp:TextBox ID="txtPaterno" runat="server" Placeholder="Apellido Paterno" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-3">
                <asp:TextBox ID="txtMaterno" runat="server" Placeholder="Apellido Materno" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-3">
                <asp:TextBox ID="txtTelefono" runat="server" Placeholder="Teléfono" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="row mt-3">
            <div class="col-md-4">
                <asp:TextBox ID="txtDireccion" runat="server" Placeholder="Dirección" CssClass="form-control"></asp:TextBox>
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
            <asp:GridView ID="GVDocentes" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSourceDocentes"
                CellPadding="4" CssClass="table table-striped" OnSelectedIndexChanged="GVDocentes_SelectedIndexChanged"
                DataKeyNames="idDocente">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:CommandField ShowSelectButton="True" ButtonType="Button" />
                    <asp:BoundField DataField="idDocente" HeaderText="ID Docente" SortExpression="idDocente" />
                    <asp:BoundField DataField="idUsuario" HeaderText="ID Usuario" SortExpression="idUsuario" />
                    <asp:BoundField DataField="nombre" HeaderText="Nombre" SortExpression="nombre" />
                    <asp:BoundField DataField="paterno" HeaderText="Paterno" SortExpression="paterno" />
                    <asp:BoundField DataField="materno" HeaderText="Materno" SortExpression="materno" />
                    <asp:BoundField DataField="telefono" HeaderText="Teléfono" SortExpression="telefono" />
                    <asp:BoundField DataField="direccion" HeaderText="Dirección" SortExpression="direccion" />
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSourceDocentes" runat="server"
                ConnectionString="<%$ ConnectionStrings:DefaultConnection %>"
                SelectCommand="SELECT * FROM docente">
            </asp:SqlDataSource>
        </div>
    </div>
</asp:Content>
