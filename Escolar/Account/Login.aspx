<%@ Page Title="INICIO SESIÓN" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Escolar.Account.Login" Async="true" %>

<%@ Register Src="~/Account/OpenAuthProviders.ascx" TagPrefix="uc" TagName="OpenAuthProviders" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <main aria-labelledby="title">
        <h2 id="title"><%: Title %>.</h2>
        <div class="col-md-8">
            <section id="loginForm">
                <div class="row">
                    <h4>Usa una cuenta local para ingresar.</h4>
                    <hr />
                    <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                        <p class="text-danger">
                            <asp:Literal runat="server" ID="FailureText" />
                        </p>
                    </asp:PlaceHolder>
                    <div class="row">
                        <asp:Label runat="server" AssociatedControlID="Email" CssClass="col-md-2 col-form-label">Correo electrónico:</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="Email" CssClass="form-control" TextMode="Email" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Email"
                                CssClass="text-danger" ErrorMessage="Es necesario rellenar el campo del correo." />
                        </div>
                    </div>
                    <div class="row">
                        <asp:Label runat="server" AssociatedControlID="Password" CssClass="col-md-2 col-form-label">Contraseña:</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Password" CssClass="text-danger" ErrorMessage="Es necesario rellenar el campo de la contraseña." />
                        </div>
                    </div>
                    <div class="row">
                        <div class="offset-md-2 col-md-10">
                            <div class="checkbox">
                                <asp:CheckBox runat="server" ID="RememberMe" />
                                <asp:Label runat="server" AssociatedControlID="RememberMe">¿Recordarme?</asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-offset-md-2 col-md-10">
                            <asp:Button runat="server" OnClick="LogIn" Text="INICIAR SESIÓN" CssClass="btn btn-outline-dark" />
                        </div>
                    </div>
                </div>
                <p>
                    <asp:HyperLink runat="server" ID="RegisterHyperLink" ViewStateMode="Disabled">Registrarme</asp:HyperLink>
                </p>
                <p>
                    <%-- Habilita esto una vez que tengas habilitada la confirmación de cuenta para la funcionalidad de restablecimiento de contraseña
                    <asp:HyperLink runat="server" ID="ForgotPasswordHyperLink" ViewStateMode="Disabled">¿Olvidaste tu contraseña?</asp:HyperLink>
                    --%>
                </p>
            </section>
        </div>

        <div class="col-md-4">
            <section id="socialLoginForm">
                <uc:OpenAuthProviders runat="server" ID="OpenAuthLogin" />
            </section>
        </div>
    </main>
</asp:Content>

