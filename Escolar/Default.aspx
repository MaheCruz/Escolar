<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Escolar._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <section class="col-md-3 text-center">
            <a href="Directivos/Dashboard.aspx">
                <img src="Images/directivo.png" alt="Directivos" style="width: 100px; height: 100px;" />
                <h4 class="text-primary">Directivos</h4>
            </a>
        </section>
        <section class="col-md-3 text-center">
            <a href="Docentes/Dashboard.aspx">
                <img src="Images/docente.png" alt="Docentes" style="width: 100px; height: 100px;" />
                <h4 class="text-primary">Docentes</h4>
            </a>
        </section>
        <section class="col-md-3 text-center">
            <a href="Estudiantes/Dashboard.aspx">
                <img src="Images/estudiante.png" alt="Estudiantes" style="width: 100px; height: 100px;" />
                <h4 class="text-primary">Estudiantes</h4>
            </a>
        </section>
        <section class="col-md-3 text-center">
            <a href="Tutor/Dashboard.aspx">
                <img src="Images/tutor.png" alt="Tutores" style="width: 100px; height: 100px;" />
                <h4 class="text-primary">Tutores</h4>
            </a>
        </section>
    </div>
</asp:Content>

