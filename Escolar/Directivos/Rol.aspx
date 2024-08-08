<%@ Page Title="ROLES" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Rol.aspx.cs" Inherits="Escolar.Directivos.Rol" %>
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
        <div class="row">
            <div class="col-sm-4 form-group">
                <asp:Label ID="lbRol" runat="server" Text="Rol:" AssociatedControlID="txtRolName"></asp:Label>
                <asp:TextBox ID="txtRolName" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:Button ID="btnCrearRol" runat="server" Text="Crear Rol" CssClass="btn-custom" OnClick="btnCrearRol_Click" />
                <asp:Button ID="btnEliminarRol" runat="server" Text="Eliminar Rol" CssClass="btn-custom" OnClick="btnEliminarRol_Click" />
                <asp:Button ID="btnEditarRol" runat="server" Text="Editar Rol" CssClass="btn-custom" OnClick="btnEditarRol_Click" />
                <asp:Label ID="lblIdRol" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="lblIdUser" runat="server" Visible="false"></asp:Label>
            </div>
            <div class="col-sm-4 form-group">
                <asp:Label ID="Label2" runat="server" Text="Roles:"></asp:Label>
                <asp:GridView ID="gvRolName" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" DataSourceID="SqlDataSource1" OnSelectedIndexChanged="gvRolName_SelectedIndexChanged" CellPadding="4" ForeColor="#333333" GridLines="None" CssClass="table table-striped">
                    <AlternatingRowStyle BackColor="White"></AlternatingRowStyle>
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" ButtonType="Button"></asp:CommandField>
                        <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True" SortExpression="Id"></asp:BoundField>
                        <asp:BoundField DataField="Name" HeaderText="Nombre del Rol" SortExpression="Name"></asp:BoundField>
                    </Columns>
                    <FooterStyle BackColor="#343a40" Font-Bold="True" ForeColor="White"></FooterStyle>
                    <HeaderStyle BackColor="#343a40" Font-Bold="True" ForeColor="White"></HeaderStyle>
                    <RowStyle BackColor="#EFF3FB"></RowStyle>
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>
                    <SortedAscendingCellStyle BackColor="#F5F7FB"></SortedAscendingCellStyle>
                    <SortedAscendingHeaderStyle BackColor="#6D95E1"></SortedAscendingHeaderStyle>
                    <SortedDescendingCellStyle BackColor="#E9EBEF"></SortedDescendingCellStyle>
                    <SortedDescendingHeaderStyle BackColor="#4870BE"></SortedDescendingHeaderStyle>
                </asp:GridView>
                <asp:SqlDataSource runat="server" ID="SqlDataSource1" ConnectionString='<%$ ConnectionStrings:DefaultConnection %>' SelectCommand="SELECT Id, Name FROM AspNetRoles"></asp:SqlDataSource>
            </div>
            <div class="col-sm-4 form-group">
                <asp:Panel ID="Panel1" runat="server" CssClass="alert alert-success alert-dismissible" Visible="false">
                    <button type="button" class="close" data-dismissible="alert">&times;</button>
                    <strong>Éxito!</strong> Operación ejecutada sobre el rol con éxito.
                </asp:Panel>
                <asp:Panel ID="Panel2" runat="server" CssClass="alert alert-danger alert-dismissible" Visible="false">
                    <button type="button" class="close" data-dismissible="alert">&times;</button>
                    <strong>Fracaso!</strong> Error.
                </asp:Panel>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-4 form-group">
                <asp:Label ID="Label3" runat="server" Text="Nombre del Usuario: " AssociatedControlID="txtUserName"></asp:Label>
                <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                <asp:Label ID="Label4" runat="server" Text="Ingresa correo electrónico: " AssociatedControlID="txtCorreo"></asp:Label>
                <asp:TextBox ID="txtCorreo" runat="server" CssClass="form-control" OnTextChanged="txtCorreo_TextChanged" AutoPostBack="true"></asp:TextBox>
                <asp:Label ID="Label5" runat="server" Text="Ingresa una contraseña: " AssociatedControlID="txtPass"></asp:Label>
                <asp:TextBox ID="txtPass" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                <asp:Label ID="Label6" runat="server" Text="Confirma la contraseña: " AssociatedControlID="txtConfirmar"></asp:Label>
                <asp:TextBox ID="txtConfirmar" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                <asp:Button ID="btnCrearUsuario" runat="server" Text="Crear Usuario" CssClass="btn-custom" OnClick="btnCrearUsuario_Click" />
                <asp:Button ID="btnBorrarUsuario" runat="server" Text="Borrar Usuario" CssClass="btn-custom" OnClick="btnBorrarUsuario_Click" />
            </div>
            <div class="col-sm-5 form-group">
                <asp:Label ID="Label7" runat="server" Text="Asignación de Roles"></asp:Label>
                <asp:GridView ID="gvUsuarios" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" DataSourceID="SqlDataSource2" OnSelectedIndexChanged="gvUsuarios_SelectedIndexChanged" CellPadding="4" ForeColor="#333333" GridLines="None" CssClass="table table-striped">
                    <AlternatingRowStyle BackColor="White"></AlternatingRowStyle>
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" ButtonType="Button"></asp:CommandField>
                        <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True" SortExpression="Id"></asp:BoundField>
                        <asp:BoundField DataField="UserName" HeaderText="Nombre de Usuario" SortExpression="UserName"></asp:BoundField>
                    </Columns>
                    <FooterStyle BackColor="#343a40" Font-Bold="True" ForeColor="White"></FooterStyle>
                    <HeaderStyle BackColor="#343a40" Font-Bold="True" ForeColor="White"></HeaderStyle>
                    <RowStyle BackColor="#EFF3FB"></RowStyle>
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>
                    <SortedAscendingCellStyle BackColor="#F5F7FB"></SortedAscendingCellStyle>
                    <SortedAscendingHeaderStyle BackColor="#6D95E1"></SortedAscendingHeaderStyle>
                    <SortedDescendingCellStyle BackColor="#E9EBEF"></SortedDescendingCellStyle>
                    <SortedDescendingHeaderStyle BackColor="#4870BE"></SortedDescendingHeaderStyle>
                </asp:GridView>
                <asp:SqlDataSource runat="server" ID="SqlDataSource2" ConnectionString='<%$ ConnectionStrings:DefaultConnection %>' SelectCommand="SELECT Id, UserName FROM AspNetUsers"></asp:SqlDataSource>
            </div>
            <div class="col-sm-3 form-group">
                <asp:Panel ID="Panel3" runat="server" CssClass="alert alert-success alert-dismissible" Visible="false">
                    <button type="button" class="close" data-dismissible="alert">&times;</button>
                    <strong>Éxito!</strong> Usuario creado correctamente.
                </asp:Panel>
                <asp:Panel ID="Panel4" runat="server" CssClass="alert alert-danger alert-dismissible" Visible="false">
                    <button type="button" class="close" data-dismissible="alert">&times;</button>
                    <strong>Fracaso!</strong> No se pudo crear al usuario.
                </asp:Panel>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-6 form-group">
                <asp:Label ID="Label1" runat="server" Text="Usuario: " AssociatedControlID="lblNomUser"></asp:Label>
                <asp:Label ID="lblNomUser" runat="server"></asp:Label><br />
                <asp:Label ID="Label8" runat="server" Text="Rol: " AssociatedControlID="lblNomRol"></asp:Label>
                <asp:Label ID="lblNomRol" runat="server"></asp:Label><br />
                <asp:Button ID="btnRelacion" runat="server" Text="Asignar a Rol" CssClass="btn-custom" OnClick="btnRelacion_Click" />
            </div>
            <div class="col-sm-6 form-group">
                <asp:Panel ID="Panel5" runat="server" CssClass="alert alert-success alert-dismissible" Visible="false">
                    <button type="button" class="close" data-dismissible="alert">&times;</button>
                    <strong>Éxito!</strong> Asignación creada correctamente.
                </asp:Panel>
                <asp:Panel ID="Panel6" runat="server" CssClass="alert alert-danger alert-dismissible" Visible="false">
                    <button type="button" class="close" data-dismissible="alert">&times;</button>
                    <strong>Fracaso!</strong> Asignación no creada.
                </asp:Panel>
                <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString='<%$ ConnectionStrings:DefaultConnection %>' InsertCommand="INSERT INTO AspNetUserRoles(UserId, RoleId) VALUES (@UserId, @RoleId)" SelectCommand="SELECT AspNetUserRoles.UserId, AspNetUserRoles.RoleId, AspNetUsers.UserName, AspNetRoles.Name FROM AspNetUserRoles INNER JOIN AspNetUsers ON AspNetUserRoles.UserId = AspNetUsers.Id INNER JOIN AspNetRoles ON AspNetUserRoles.RoleId = AspNetRoles.Id">
                    <InsertParameters>
                        <asp:Parameter Name="UserId"></asp:Parameter>
                        <asp:Parameter Name="RoleId"></asp:Parameter>
                    </InsertParameters>
                </asp:SqlDataSource>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12 form-group">
                <asp:Label ID="Label9" runat="server" Text="Asignaciones de Roles"></asp:Label>
                <asp:GridView ID="gvAsignaciones" runat="server" AutoGenerateColumns="False" DataKeyNames="UserId,RoleId" DataSourceID="SqlDataSource3" CellPadding="4" ForeColor="#333333" GridLines="None" CssClass="table table-striped">
                    <AlternatingRowStyle BackColor="White"></AlternatingRowStyle>
                    <Columns>
                        <asp:BoundField DataField="UserName" HeaderText="Nombre de Usuario" SortExpression="UserName"></asp:BoundField>
                        <asp:BoundField DataField="Name" HeaderText="Nombre del Rol" SortExpression="Name"></asp:BoundField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnEliminarAsignacion" runat="server" Text="Eliminar" CommandArgument='<%# Eval("UserId") + "," + Eval("RoleId") %>' OnClick="btnEliminarAsignacion_Click" CssClass="btn-custom" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#343a40" Font-Bold="True" ForeColor="White"></FooterStyle>
                    <HeaderStyle BackColor="#343a40" Font-Bold="True" ForeColor="White"></HeaderStyle>
                    <PagerStyle HorizontalAlign="Center" BackColor="#343a40" ForeColor="White"></PagerStyle>
                    <RowStyle BackColor="#EFF3FB"></RowStyle>
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>
                    <SortedAscendingCellStyle BackColor="#F5F7FB"></SortedAscendingCellStyle>
                    <SortedAscendingHeaderStyle BackColor="#6D95E1"></SortedAscendingHeaderStyle>
                    <SortedDescendingCellStyle BackColor="#E9EBEF"></SortedDescendingCellStyle>
                    <SortedDescendingHeaderStyle BackColor="#4870BE"></SortedDescendingHeaderStyle>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>

