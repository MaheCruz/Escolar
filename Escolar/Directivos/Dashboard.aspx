<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Escolar.Directivos.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" />

    <style>
        .dashboard-container {
            padding: 20px;
        }
        .card-custom {
            border-radius: 8px;
            box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
            margin-bottom: 20px;
            cursor: pointer;
        }
        .card-header-custom {
            background-color: #343a40;
            color: white;
            border-radius: 8px 8px 0 0;
            padding: 15px;
            display: flex;
            align-items: center;
        }
        .card-body-custom {
            padding: 20px;
        }
        .icon-container {
            font-size: 50px;
            color: #343a40;
            margin-right: 15px;
        }
        .chart-container {
            position: relative;
            height: 40vh;
            width: 80vw;
        }
        .shortcut-container {
            margin-top: 30px;
            text-align: center;
        }
        .shortcut {
            display: inline-block;
            margin: 10px;
            text-align: center;
            cursor: pointer;
        }
        .shortcut-icon {
            font-size: 40px;
            color: #007bff;
        }
        .shortcut-label {
            margin-top: 5px;
            font-size: 16px;
            color: #343a40;
        }
    </style>

    <div class="dashboard-container">
        <div class="row">
            <div class="col-md-4" onclick="window.location.href='CRUDestudiante.aspx'">
                <div class="card card-custom">
                    <div class="card-header-custom">
                        <i class="fas fa-user-graduate icon-container"></i>
                        <h5 class="card-title">Estudiantes</h5>
                    </div>
                    <div class="card-body-custom">
                        <h3><asp:Label ID="lblTotalEstudiantes" runat="server" Text="0"></asp:Label></h3>
                        <p>Total de estudiantes registrados</p>
                    </div>
                </div>
            </div>
            <div class="col-md-4" onclick="window.location.href='CRUDdocente.aspx'">
                <div class="card card-custom">
                    <div class="card-header-custom">
                        <i class="fas fa-chalkboard-teacher icon-container"></i>
                        <h5 class="card-title">Docentes</h5>
                    </div>
                    <div class="card-body-custom">
                        <h3><asp:Label ID="lblTotalDocentes" runat="server" Text="0"></asp:Label></h3>
                        <p>Total de docentes registrados</p>
                    </div>
                </div>
            </div>
            <div class="col-md-4" onclick="window.location.href='AsigGrupoMateria.aspx'">
                <div class="card card-custom">
                    <div class="card-header-custom">
                        <i class="fas fa-book icon-container"></i>
                        <h5 class="card-title">Materias</h5>
                    </div>
                    <div class="card-body-custom">
                        <h3><asp:Label ID="lblTotalMaterias" runat="server" Text="0"></asp:Label></h3>
                        <p>Total de materias registradas</p>
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-6">
                <div class="card card-custom">
                    <div class="card-header-custom">
                        <h5 class="card-title">Distribución de Estudiantes por Grupos</h5>
                    </div>
                    <div class="card-body-custom">
                        <canvas id="chartEstudiantesGrupos"></canvas>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="card card-custom">
                    <div class="card-header-custom">
                        <h5 class="card-title">Distribución de Docentes por Materias</h5>
                    </div>
                    <div class="card-body-custom">
                        <canvas id="chartDocentesMaterias"></canvas>
                    </div>
                </div>
            </div>
        </div>

        <div class="shortcut-container">
            <div class="shortcut" onclick="window.location.href='AsigEstudianteGrupo.aspx'">
                <i class="fas fa-users shortcut-icon"></i>
                <div class="shortcut-label">Asignar Estudiantes a Grupo</div>
            </div>
            <div class="shortcut" onclick="window.location.href='AsigGrupoMateria.aspx'">
                <i class="fas fa-book shortcut-icon"></i>
                <div class="shortcut-label">Asignar Grupo a Materia</div>
            </div>
            <div class="shortcut" onclick="window.location.href='AsigTutorEstudiante.aspx'">
                <i class="fas fa-user-tie shortcut-icon"></i>
                <div class="shortcut-label">Asignar Tutor a Estudiante</div>
            </div>
            <div class="shortcut" onclick="window.location.href='CRUDdocente.aspx'">
                <i class="fas fa-chalkboard-teacher shortcut-icon"></i>
                <div class="shortcut-label">CRUD Docente</div>
            </div>
            <div class="shortcut" onclick="window.location.href='CRUDestudiante.aspx'">
                <i class="fas fa-user-graduate shortcut-icon"></i>
                <div class="shortcut-label">CRUD Estudiante</div>
            </div>
            <div class="shortcut" onclick="window.location.href='CRUDtutor.aspx'">
                <i class="fas fa-user-tie shortcut-icon"></i>
                <div class="shortcut-label">CRUD Tutor</div>
            </div>
            <div class="shortcut" onclick="window.location.href='Reporte.aspx'">
                <i class="fas fa-chart-bar shortcut-icon"></i>
                <div class="shortcut-label">Reporte</div>
            </div>
            <div class="shortcut" onclick="window.location.href='Rol.aspx'">
                <i class="fas fa-user-cog shortcut-icon"></i>
                <div class="shortcut-label">Roles</div>
            </div>
        </div>
    </div>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/3.7.1/chart.min.js"></script>
    <script>
        document.addEventListener('DOMContentLoaded', function () {
            // Cargar datos para el gráfico de estudiantes por grupo
            fetch('Dashboard.aspx/GetEstudiantesPorGrupo', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                }
            })
                .then(response => response.json())
                .then(data => {
                    var ctxEstudiantesGrupos = document.getElementById('chartEstudiantesGrupos').getContext('2d');
                    var chartEstudiantesGrupos = new Chart(ctxEstudiantesGrupos, {
                        type: 'bar',
                        data: {
                            labels: data.labels,
                            datasets: [{
                                label: 'Estudiantes',
                                data: data.values,
                                backgroundColor: 'rgba(54, 162, 235, 0.2)',
                                borderColor: 'rgba(54, 162, 235, 1)',
                                borderWidth: 1
                            }]
                        },
                        options: {
                            scales: {
                                y: {
                                    beginAtZero: true
                                }
                            }
                        }
                    });
                });

            // Cargar datos para el gráfico de docentes por materia
            fetch('Dashboard.aspx/GetDocentesPorMateria', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                }
            })
                .then(response => response.json())
                .then(data => {
                    var ctxDocentesMaterias = document.getElementById('chartDocentesMaterias').getContext('2d');
                    var chartDocentesMaterias = new Chart(ctxDocentesMaterias, {
                        type: 'pie',
                        data: {
                            labels: data.labels,
                            datasets: [{
                                label: 'Docentes',
                                data: data.values,
                                backgroundColor: [
                                    'rgba(255, 99, 132, 0.2)',
                                    'rgba(54, 162, 235, 0.2)',
                                    'rgba(255, 206, 86, 0.2)',
                                    'rgba(75, 192, 192, 0.2)',
                                    'rgba(153, 102, 255, 0.2)'
                                ],
                                borderColor: [
                                    'rgba(255, 99, 132, 1)',
                                    'rgba(54, 162, 235, 1)',
                                    'rgba(255, 206, 86, 1)',
                                    'rgba(75, 192, 192, 1)',
                                    'rgba(153, 102, 255, 1)'
                                ],
                                borderWidth: 1
                            }]
                        },
                        options: {
                            responsive: true
                        }
                    });
                });
        });
    </script>
</asp:Content>
