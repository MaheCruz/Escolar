<%@ Page Title="Reportes de Estudiantes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reportes.aspx.cs" Inherits="Escolar.Tutor.Reportes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        h2 {
            color: #337ab7;
        }

        .btn-primary {
            background-color: #007bff;
            border-color: #007bff;
            color: white;
        }

        .table {
            background-color: #f9f9f9;
            border-collapse: collapse;
            width: 100%;
        }

        .table th, .table td {
            border: 1px solid #ddd;
            padding: 8px;
        }
        .table th {
            background-color: #5BC0DE;
            color: black;
            text-align: center;
        }

        .table-striped > tbody > tr:nth-of-type(odd) {
            background-color: #f2f2f2;
        }

        .table-bordered {
            border: 1px solid #ddd;
        }

        .no-data {
            text-align: center;
            color: #d9534f;
        }

        #lblError {
            color: #d9534f;
        }

    </style>

    <h2>Reportes de Tutorado(s)</h2>

    <asp:DropDownList ID="ddlEstudiantes" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlEstudiantes_SelectedIndexChanged">
        <asp:ListItem Value="" Text="Selecciona un estudiante" />
    </asp:DropDownList>

    <br /><br />

    <asp:GridView ID="gvReportes" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered">
        <Columns>
            <asp:BoundField DataField="tipo" HeaderText="Tipo de Reporte" />
            <asp:BoundField DataField="detalle" HeaderText="Detalle" />
            <asp:BoundField DataField="fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
        </Columns>
    </asp:GridView>

    <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
</asp:Content>
