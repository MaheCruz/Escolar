<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReporteAsist.aspx.cs" Inherits="Escolar.Directivos.ReporteAsist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Reporte de Asistencias - Directivo</h2>

    <div>
        <asp:Label ID="lblMateria" runat="server" Text="Materia:"></asp:Label>
        <asp:DropDownList ID="ddlMateria" runat="server" AutoPostBack="True"></asp:DropDownList>
        <asp:Label ID="lblFechaInicio" runat="server" Text="Fecha Inicio:"></asp:Label>
        <asp:TextBox ID="txtFechaInicio" runat="server" TextMode="Date"></asp:TextBox>
        <asp:Label ID="lblFechaFin" runat="server" Text="Fecha Fin:"></asp:Label>
        <asp:TextBox ID="txtFechaFin" runat="server" TextMode="Date"></asp:TextBox>
        <asp:Button ID="btnGenerarReporte" runat="server" Text="Generar Reporte" OnClick="btnGenerarReporte_Click" />
    </div>

    <asp:GridView ID="gvAsistencias" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered">
        <Columns>
            <asp:BoundField DataField="NombreCompleto" HeaderText="Nombre del Estudiante" />
            <%-- Las columnas dinámicas para las fechas se agregarán desde el code-behind --%>
            <asp:BoundField DataField="% Asistencia" HeaderText="Porcentaje de Asistencia (%)" />
        </Columns>
    </asp:GridView>

    <div class="actions" style="margin-top: 10px;">
        <asp:Button ID="btnGenerarExcel" runat="server" Text="Generar Reporte Excel" CssClass="btn btn-primary" OnClick="btnGenerarExcel_Click" />
        <asp:Button ID="btnGenerarPDF" runat="server" Text="Generar Reporte PDF" CssClass="btn btn-danger" OnClick="btnGenerarPDF_Click" />
    </div>
</asp:Content>

