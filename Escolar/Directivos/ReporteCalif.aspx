<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReporteCalif.aspx.cs" Inherits="Escolar.Directivos.ReporteCalif" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Reporte de Calificaciones</h2>

    <div>
        <asp:Label ID="lblMateria" runat="server" Text="Materia:"></asp:Label>
        <asp:DropDownList ID="ddlMateria" runat="server" AutoPostBack="True"></asp:DropDownList>
        <asp:Button ID="btnGenerarReporte" runat="server" Text="Generar Reporte" OnClick="btnGenerarReporte_Click" />
    </div>

    <asp:GridView ID="gvCalificaciones" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered">
        <Columns>
            <asp:BoundField DataField="NombreCompleto" HeaderText="Nombre del Estudiante" />
            <asp:BoundField DataField="Calificacion1" HeaderText="Calificación 1er Parcial" />
            <asp:BoundField DataField="Calificacion2" HeaderText="Calificación 2do Parcial" />
            <asp:BoundField DataField="Calificacion3" HeaderText="Calificación 3er Parcial" />
            <asp:BoundField DataField="Promedio" HeaderText="Promedio General" />
        </Columns>
    </asp:GridView>

    <div class="actions" style="margin-top: 10px;">
        <asp:Button ID="btnGenerarExcel" runat="server" Text="Generar Reporte Excel" CssClass="btn btn-primary" OnClick="btnGenerarExcel_Click" />
        <asp:Button ID="btnGenerarPDF" runat="server" Text="Generar Reporte PDF" CssClass="btn btn-danger" OnClick="btnGenerarPDF_Click" />
    </div>
</asp:Content>

