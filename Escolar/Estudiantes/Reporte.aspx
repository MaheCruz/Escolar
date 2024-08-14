<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reporte.aspx.cs" Inherits="Escolar.Estudiantes.Reporte" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        h2.text-primary {
            color: #337ab7;
        }

        h4.text-info {
            color: #5bc0de;
        }

        .btn-success {
            background-color: #5cb85c;
            border-color: #4cae4c;
        }

        .table {
            background-color: #f9f9f9;
        }

        .table th {
            background-color: #ACE8A0;
            color: white;
        }

        .table-striped > tbody > tr:nth-of-type(odd) {
            background-color: #f2f2f2;
        }

        .table-bordered {
            border: 1px solid #ddd;
        }

        .alert-warning {
            color: #856404;
            background-color: #fff3cd;
            border-color: #ffeeba;
            padding: 15px;
            margin-bottom: 20px;
            border: 1px solid transparent;
            border-radius: 4px;
        }
    </style>

    <h2>Reportes</h2>
    <asp:GridView ID="gvReportes" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered">
        <Columns>
            <asp:BoundField DataField="tipo" HeaderText="Tipo" />
            <asp:BoundField DataField="fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
            <asp:BoundField DataField="detalle" HeaderText="Detalle" />
        </Columns>
    </asp:GridView>

    <asp:Label ID="lblNoReportes" runat="server" Text="No se encontraron reportes." CssClass="alert-warning" Visible="false" />
</asp:Content>
