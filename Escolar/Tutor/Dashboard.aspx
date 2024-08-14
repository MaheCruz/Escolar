<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Escolar.Tutor.Dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Panel de control - Tutor</h1>
    <h2><asp:Label ID="lblNombreTutor" runat="server" Text=""></asp:Label></h2>
    <div style="display: flex; justify-content: center; gap: 50px;">
    <div style="text-align: center;"> 
        <asp:ImageButton ID="imgCalificaciones" runat="server" ImageUrl="~/Images/Calificaciones.png" Width="120px" Height="120px" PostBackUrl="Calificaciones.aspx" />
        <h4><a href="Calificaciones.aspx" style="font-size: large; font-weight: bold;">Calificaciones</a></h4>
    </div>
    <div style="text-align: center;"> 
        <asp:ImageButton ID="imgReportes" runat="server" ImageUrl="~/Images/Reportes.png" Width="120px" Height="120px" PostBackUrl="Reportes.aspx" />
        <h4><a href="Reportes.aspx" style="font-size: large; font-weight: bold;">Reportes</a></h4>
    </div>
</div>

    <div style="display: flex; justify-content: space-between; margin-bottom: 20px;">
        <div>
            <h3>Promedio General de Tutorado(s)</h3>
            <asp:Label ID="lblPromedioGeneral" runat="server" Text="Cargando..." ForeColor="Black"></asp:Label>
        </div>
        <div>
            <h3>Reporte Más Reciente</h3>
            <p><strong>Tipo:</strong> <asp:Label ID="lblTipo" runat="server" Text="Cargando..."></asp:Label></p>
            <p><strong>Fecha:</strong> <asp:Label ID="lblFecha" runat="server" Text="Cargando..."></asp:Label></p>
            <p><strong>Detalle:</strong> <asp:Label ID="lblDetalle" runat="server" Text="Cargando..."></asp:Label></p>
            <p><strong>Nombre Completo:</strong> <asp:Label ID="lblNombreCompleto" runat="server" Text="Cargando..."></asp:Label></p>
        </div>
    </div>

   
</asp:Content>
