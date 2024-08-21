<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Calificaciones.aspx.cs" Inherits="Escolar.Tutor.Calificaciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        h2.text-primary {
            color: #B6E19F;
        }

        h4.text-info {
            color:       #5bc0de;
        }

        .btn-success {
            background-color: #5cb85c;
            border-color: #4cae4c;
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

        .table-striped > tbody > tr:first-child {
            background-color: #cce5ff;
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
    <h2>Calificaciones de Tutorado(s)</h2>
    <asp:DropDownList ID="ddlEstudiantes" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlEstudiantes_SelectedIndexChanged">
        <asp:ListItem Value="" Text="Selecciona un estudiante" />
    </asp:DropDownList><br /><br />
    <asp:Label ID="lblPromedioGeneral" runat="server" Text="Promedio General : N/A" Font-Bold="True" />
    <br /><br/>
    <asp:Button ID="btnRecargar" runat="server" Text="Recargar" CssClass="btn btn-primary" OnClick="btnRecargar_Click" />
    <br /><br />
    <asp:GridView ID="gvCalificaciones" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered">
        <Columns>
            <asp:BoundField DataField="NombreMateria" HeaderText="Materia" />
            <asp:BoundField DataField="calificacion1" HeaderText="1er Parcial" />
            <asp:BoundField DataField="calificacion2" HeaderText="2do Parcial" />
            <asp:BoundField DataField="calificacion3" HeaderText="3er Parcial" />
            <asp:BoundField DataField="promedioMateria" HeaderText="Promedio" />
        </Columns>
    </asp:GridView>
    <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
</asp:Content>
