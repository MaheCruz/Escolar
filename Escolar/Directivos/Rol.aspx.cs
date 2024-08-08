using Escolar.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web;

namespace Escolar.Directivos
{
    public partial class Rol : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LimpiarMensajes();
            }
        }

        protected void btnCrearRol_Click(object sender, EventArgs e)
        {
            LimpiarMensajes();
            var roleName = txtRolName.Text.Trim();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));

            try
            {
                if (roleManager.RoleExists(roleName))
                {
                    MostrarMensajeError("El rol ya existe.", Panel2);
                }
                else
                {
                    roleManager.Create(new IdentityRole(roleName));
                    gvRolName.DataBind();
                    txtRolName.Text = string.Empty;
                    MostrarMensajeExito("Rol creado con éxito.", Panel1);
                }
            }
            catch (Exception ex)
            {
                MostrarMensajeError($"Error al crear el rol: {ex.Message}", Panel2);
            }
        }

        protected void btnEliminarRol_Click(object sender, EventArgs e)
        {
            LimpiarMensajes();
            var roleName = txtRolName.Text.Trim();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));

            try
            {
                if (roleManager.RoleExists(roleName))
                {
                    var role = roleManager.FindByName(roleName);
                    roleManager.Delete(role);
                    gvRolName.DataBind();
                    txtRolName.Text = string.Empty;
                    MostrarMensajeExito("Rol eliminado con éxito.", Panel1);
                }
                else
                {
                    MostrarMensajeError("Rol no encontrado.", Panel2);
                }
            }
            catch (Exception ex)
            {
                MostrarMensajeError($"Error al eliminar el rol: {ex.Message}", Panel2);
            }
        }

        protected void gvRolName_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtRolName.Text = gvRolName.SelectedRow.Cells[2].Text;
            lblNomRol.Text = gvRolName.SelectedRow.Cells[2].Text;
            lblIdRol.Text = gvRolName.SelectedRow.Cells[1].Text;
        }

        protected void gvUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblIdUser.Text = gvUsuarios.SelectedRow.Cells[1].Text;
            txtUserName.Text = gvUsuarios.SelectedRow.Cells[2].Text;
            lblNomUser.Text = gvUsuarios.SelectedRow.Cells[2].Text;
        }

        protected void txtCorreo_TextChanged(object sender, EventArgs e)
        {
            txtUserName.Text = txtCorreo.Text.Trim();
        }

        protected void btnCrearUsuario_Click(object sender, EventArgs e)
        {
            LimpiarMensajes();
            if (txtPass.Text == txtConfirmar.Text)
            {
                var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();
                var user = new ApplicationUser
                {
                    UserName = txtCorreo.Text.Trim(), // Asignar el nombre de usuario igual al correo
                    Email = txtCorreo.Text.Trim()
                };
                var result = manager.Create(user, txtPass.Text.Trim());
                if (result.Succeeded)
                {
                    gvUsuarios.DataBind();
                    txtUserName.Text = txtCorreo.Text; // Reflejar el nombre de usuario igual al correo
                    txtCorreo.Text = string.Empty;
                    txtPass.Text = string.Empty;
                    txtConfirmar.Text = string.Empty;
                    MostrarMensajeExito("Usuario creado correctamente.", Panel3);
                }
                else
                {
                    MostrarMensajeError("Error al crear el usuario: " + result.Errors.FirstOrDefault(), Panel4);
                }
            }
            else
            {
                MostrarMensajeError("Las contraseñas no coinciden.", Panel4);
            }
        }

        protected void btnBorrarUsuario_Click(object sender, EventArgs e)
        {
            LimpiarMensajes();
            var userName = txtCorreo.Text.Trim(); // Se toma el correo como identificador del usuario
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();

            try
            {
                var user = manager.FindByEmail(userName);
                if (user != null)
                {
                    var result = manager.Delete(user);
                    if (result.Succeeded)
                    {
                        gvUsuarios.DataBind();
                        txtUserName.Text = string.Empty;
                        txtCorreo.Text = string.Empty;
                        MostrarMensajeExito("Usuario eliminado con éxito.", Panel3);
                    }
                    else
                    {
                        MostrarMensajeError("Error al eliminar el usuario: " + result.Errors.FirstOrDefault(), Panel4);
                    }
                }
                else
                {
                    MostrarMensajeError("Usuario no encontrado.", Panel4);
                }
            }
            catch (Exception ex)
            {
                MostrarMensajeError($"Error al eliminar el usuario: {ex.Message}", Panel4);
            }
        }

        protected void btnRelacion_Click(object sender, EventArgs e)
        {
            LimpiarMensajes();
            try
            {
                if (!RolYaAsignado(lblIdUser.Text, lblIdRol.Text))
                {
                    SqlDataSource3.InsertParameters["RoleId"].DefaultValue = lblIdRol.Text;
                    SqlDataSource3.InsertParameters["UserId"].DefaultValue = lblIdUser.Text;
                    var result = SqlDataSource3.Insert();
                    if (result == 1)
                    {
                        lblIdRol.Text = string.Empty;
                        lblIdUser.Text = string.Empty;
                        lblNomRol.Text = string.Empty;
                        lblNomUser.Text = string.Empty;
                        MostrarMensajeExito("Asignación creada correctamente.", Panel5);
                    }
                    else
                    {
                        MostrarMensajeError("Error al crear la asignación.", Panel6);
                    }
                }
                else
                {
                    MostrarMensajeError("El usuario ya tiene asignado ese rol.", Panel6);
                }
            }
            catch (Exception ex)
            {
                MostrarMensajeError($"Error: {ex.Message}", Panel6);
            }
        }

        protected void btnEditarRol_Click(object sender, EventArgs e)
        {
            LimpiarMensajes();
            try
            {
                var roleName = txtRolName.Text.Trim();
                var roleId = lblIdRol.Text;
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));

                var role = roleManager.FindById(roleId);
                if (role != null)
                {
                    role.Name = roleName;
                    var result = roleManager.Update(role);
                    if (result.Succeeded)
                    {
                        gvRolName.DataBind();
                        MostrarMensajeExito("Rol editado con éxito.", Panel1);
                    }
                    else
                    {
                        MostrarMensajeError("Error al editar el rol.", Panel2);
                    }
                }
                else
                {
                    MostrarMensajeError("Rol no encontrado.", Panel2);
                }
            }
            catch (Exception ex)
            {
                MostrarMensajeError($"Error al editar el rol: {ex.Message}", Panel2);
            }
        }

        protected void btnEliminarAsignacion_Click(object sender, EventArgs e)
        {
            LimpiarMensajes();
            var args = ((Button)sender).CommandArgument.Split(',');
            var userId = args[0];
            var roleId = args[1];

            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var userRole = context.Set<IdentityUserRole>().FirstOrDefault(ur => ur.UserId == userId && ur.RoleId == roleId);
                    if (userRole != null)
                    {
                        context.Set<IdentityUserRole>().Remove(userRole);
                        context.SaveChanges();
                        gvAsignaciones.DataBind();
                        MostrarMensajeExito("Asignación eliminada correctamente.", Panel5);
                    }
                    else
                    {
                        MostrarMensajeError("Asignación no encontrada.", Panel6);
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarMensajeError($"Error al eliminar la asignación: {ex.Message}", Panel6);
            }
        }

        private bool RolYaAsignado(string userId, string roleId)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Set<IdentityUserRole>().Any(ur => ur.UserId == userId && ur.RoleId == roleId);
            }
        }

        private void MostrarMensajeExito(string mensaje, Panel panel)
        {
            panel.Visible = true;
        }

        private void MostrarMensajeError(string mensaje, Panel panel)
        {
            panel.Visible = true;
        }

        private void LimpiarMensajes()
        {
            Panel1.Visible = false;
            Panel2.Visible = false;
            Panel3.Visible = false;
            Panel4.Visible = false;
            Panel5.Visible = false;
            Panel6.Visible = false;
        }
    }
}
