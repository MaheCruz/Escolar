<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Escolar.Estudiantes.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .dashboard-container {
            display: flex;
            flex-direction: column;
            align-items: center;
            margin-top: 20px;
        }

        .group-info {
            font-size: 18px;
            font-weight: bold;
            color: #337ab7;
            margin-bottom: 20px;
        }

        .icons-container {
            display: flex;
            justify-content: center;
            margin-bottom: 30px;
        }

        .icon-item {
            text-align: center;
            margin: 0 20px;
        }

        .icon-item img {
            width: 100px;
            height: 100px;
        }

        .icon-item a {
            text-decoration: none;
            color: #337ab7;
            font-size: 16px;
            font-weight: bold;
            margin-top: 10px;
            display: block;
        }

        .panel {
            background-color: #f9f9f9;
            padding: 20px;
            border-radius: 5px;
            width: 80%;
            max-width: 900px;
            margin-bottom: 20px;
        }

        .panel h3 {
            margin-top: 0;
            color: #333;
        }

        .panel p {
            font-size: 14px;
            color: #666;
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

    <div class="dashboard-container">
        <h1>Panel de Control - Estudiante</h1>
         <h2><asp:Label ID="lblNombreEstudiante" runat="server" Text=""></asp:Label></h2>
        <div class="group-info">
            Grupo: <asp:Label ID="lblGrupo" runat="server" Text=""></asp:Label> | Promedio General: <asp:Label ID="lblPromedioGeneral" runat="server" Text=""></asp:Label>
        </div>
            <div style="display: flex; justify-content: center; gap: 50px;">
                <div style="text-align: center;"> 
                    <asp:ImageButton ID="imgCalificaciones" runat="server" ImageUrl="~/Images/Calificaciones.png" Width="120px" Height="120px" PostBackUrl="Calificacion.aspx" />
                    <h4><a href="Calificacion.aspx" style="font-size: large; font-weight: bold;">Calificaciones</a></h4>
                </div>
                <div style="text-align: center;"> 
                    <asp:ImageButton ID="imgReportes" runat="server" ImageUrl="~/Images/Reportes.png" Width="120px" Height="120px" PostBackUrl="Reporte.aspx" />
                    <h4><a href="Reporte.aspx" style="font-size: large; font-weight: bold;">Reportes</a></h4>
                </div>
        </div>

        <div class="panel">
            <h3>Promedio General por Materia</h3>
            <asp:GridView ID="gvPromedios" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered">
                <Columns>
                    <asp:BoundField DataField="NombreMateria" HeaderText="Materia" />
                    <asp:BoundField DataField="Promedio" HeaderText="Promedio" />
                </Columns>
            </asp:GridView>
            <asp:Label ID="lblNoPromedios" runat="server" Text="No se encontraron promedios." CssClass="alert-warning" Visible="false" />
        </div>
    </div>
</asp:Content>
