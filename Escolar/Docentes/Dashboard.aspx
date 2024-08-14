<%@ Page Title="Panel de Control - Docentes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Escolar.Docentes.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <h1>Panel de Control - Docentes</h1>
        <h2><asp:Label ID="lblNombreDocente" runat="server" Text=""></asp:Label></h2>
        <div class="row text-center">
            <div class="col-md-3">
                <asp:ImageButton ID="imgAsistencia" runat="server" ImageUrl="~/Images/Asistencia.png" Width="80px" Height="80px" PostBackUrl="~/Docentes/Asistencia.aspx" />
                <h4><a href="Asistencia.aspx" style="font-size: large; font-weight: bold;">Asistencia</a></h4>
            </div>
            <div class="col-md-3">
                <asp:ImageButton ID="imgCalificaciones" runat="server" ImageUrl="~/Images/Calificaciones.png" Width="80px" Height="80px" PostBackUrl="~/Docentes/Calificaciones.aspx" />
                <h4><a href="Calificaciones.aspx" style="font-size: large; font-weight: bold;">Calificaciones</a></h4>
            </div>
            <div class="col-md-3">
                <asp:ImageButton ID="imgReporteGeneral" runat="server" ImageUrl="~/Images/Reportes.png" Width="80px" Height="80px" PostBackUrl="~/Docentes/Reporte.aspx" />
                <h4><a href="Reporte.aspx" style="font-size: large; font-weight: bold;">Reporte General</a></h4>
            </div>
            <div class="col-md-3">
                <asp:ImageButton ID="imgReporteAsistencia" runat="server" ImageUrl="~/Images/ReporteAsistencia.png" Width="80px" Height="80px" PostBackUrl="~/Docentes/ReporteAsist.aspx" />
                <h4><a href="ReporteAsist.aspx" style="font-size: large; font-weight: bold;">Reporte de Asistencia</a></h4>
            </div>
        </div>
    </div>
</asp:Content>
