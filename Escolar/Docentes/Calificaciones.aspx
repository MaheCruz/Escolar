
<%@ Page Title="CALIFICACIONES" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Calificaciones.aspx.cs" Inherits="Escolar.Docentes.Calificaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<style>
    .table-bordered {
    border: 1px solid #dee2e6;
    border-collapse: collapse;
}

.table-bordered th,
.table-bordered td {
    border: 1px solid #dee2e6;
    padding: 8px;
}

.materia-section {
    margin-bottom: 20px;
}

.materia-section h3 {
    background-color: #007bff;
    color: white;
    padding: 10px;
    border-radius: 5px;
}

.btn {
    padding: 10px 15px;
    border-radius: 5px;
    color: white;
    text-align: center;
    cursor: pointer;
    margin-right: 10px;
}

.btn-success {
    background-color: #28a745;
}

.btn-primary {
    background-color: #007bff;
}

.btn-danger {
    background-color: #dc3545;
}

.btn:hover {
    opacity: 0.9;
}

</style>
    <h2>Gestión de Calificaciones por Asignatura</h2>

    <asp:Repeater ID="rptMaterias" runat="server" OnItemDataBound="rptMaterias_ItemDataBound">
    <ItemTemplate>
        <div class="materia-section">
            <h3><%# Eval("nombreMateria") %></h3>
            <asp:HiddenField ID="hfMateriaId" runat="server" Value='<%# Eval("idMateria") %>' />
            <asp:GridView ID="gvCalificaciones" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered" DataKeyNames="idCalificacion"
                OnRowEditing="gvCalificaciones_RowEditing" 
                OnRowUpdating="gvCalificaciones_RowUpdating" 
                OnRowCancelingEdit="gvCalificaciones_RowCancelingEdit">
                <Columns>
                    <asp:BoundField DataField="nombreCompleto" HeaderText="Nombre del Estudiante" ReadOnly="True" />
                    <asp:TemplateField HeaderText="Calificación 1er Parcial">
                        <ItemTemplate>
                            <asp:Label ID="lblCalificacion1" runat="server" Text='<%# Eval("calificacion1") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCalificacion1" runat="server" Text='<%# Bind("calificacion1") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Calificación 2do Parcial">
                        <ItemTemplate>
                            <asp:Label ID="lblCalificacion2" runat="server" Text='<%# Eval("calificacion2") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCalificacion2" runat="server" Text='<%# Bind("calificacion2") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Calificación 3er Parcial">
                        <ItemTemplate>
                            <asp:Label ID="lblCalificacion3" runat="server" Text='<%# Eval("calificacion3") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCalificacion3" runat="server" Text='<%# Bind("calificacion3") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="promedioMateria" HeaderText="Promedio General" ReadOnly="True" />
                    <asp:CommandField ShowEditButton="True" HeaderText="Acciones" />
                </Columns>
            </asp:GridView>
            <div class="actions" style="margin-top: 10px;">
               <asp:Button ID="btnGenerarExcel" runat="server" Text="Generar Reporte Excel" CssClass="btn btn-primary" CommandName="GenerateExcel" CommandArgument='<%# Eval("idMateria") %>' OnClick="btnGenerarExcel_Click" />
               <asp:Button ID="btnGenerarPDF" runat="server" Text="Generar Reporte PDF" CssClass="btn btn-danger" CommandName="GeneratePDF" CommandArgument='<%# Eval("idMateria") %>' OnClick="btnGenerarPDF_Click" />    
                </div>
            <hr />
        </div>
    </ItemTemplate>
</asp:Repeater>

</asp:Content>

