<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Calificacion.aspx.cs" Inherits="Escolar.Estudiantes.Calificacion" %>
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
            color: black;
            text-align: center;
        }

        .table-striped > tbody > tr:nth-of-type(odd) {
            background-color: #f2f2f2;
        }

        .table-bordered {
            border: 1px solid #ddd;
        }
    </style>
    <h2>Calificaciones</h2>
    <asp:Label ID="lblPromedioGeneral" runat="server" Text="Promedio General: N/A" />
    <asp:GridView ID="gvCalificaciones" runat="server" AutoGenerateColumns="False" CssClass="table table-striped">
        <Columns>
            <asp:BoundField DataField="NombreMateria" HeaderText="Materia" />
            <asp:BoundField DataField="calificacion1" HeaderText="1er Parcial" />
            <asp:BoundField DataField="calificacion2" HeaderText="2do Parcial" />
            <asp:BoundField DataField="calificacion3" HeaderText="3er Parcial" />
            <asp:BoundField DataField="promedioMateria" HeaderText="Promedio" />
        </Columns>
    </asp:GridView>
</asp:Content>
