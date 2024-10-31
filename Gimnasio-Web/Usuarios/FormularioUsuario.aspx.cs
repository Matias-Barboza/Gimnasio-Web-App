using Gimnasio_Dominio;
using Gimnasio_Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gimnasio_Web.Usuarios
{
    public partial class FormularioUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //-------------------------------------------------- EVENTOS ------------------------------------------------------------------------------------------
        protected void GenerarPasswordButton_Click(object sender, EventArgs e)
        {
            string nuevaContrasenha = Membership.GeneratePassword(10, 0);

            PasswordTextBox.Text = nuevaContrasenha;

            Session.Add("ContrasenhaGenerada", nuevaContrasenha);
        }

        protected void RegistrarProfesorButton_Click(object sender, EventArgs e)
        {
            try
            {
                UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
                Usuario usuario = new Usuario() 
                {
                    Nombre = NombreTextBox.Text,
                    Apellido = ApellidoTextBox.Text,
                    NombreUsuario = NombreUsuarioTextBox.Text,
                    Password = Seguridad.GetSHA256(PasswordTextBox.Text),
                    EsAdmin = EsAdminCheckBox.Checked,
                    EsProfesor = EsProfesorCheckBox.Checked
                };

                usuarioNegocio.AñadirUsuario(usuario);

                //Response.Redirect("/Usuarios/ListadoUsuarios.aspx");
            }
            catch (Exception)
            {

            }
        }

        //-------------------------------------------------- VALIDATORS ---------------------------------------------------------------------------------------
        protected void NombreUsuarioExistenteValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                UsuarioNegocio usuarioNegocio;
                Usuario usuario;
                
                if (!NombreUsuarioObligatorioValidator.IsValid || !NombreUsuarioExpresionValidator.IsValid) 
                {
                    args.IsValid = true;
                    return;
                }

                usuarioNegocio = new UsuarioNegocio();
                usuario = new Usuario()
                {
                    NombreUsuario = NombreUsuarioTextBox.Text
                };

                args.IsValid = !usuarioNegocio.ExisteNombreUsuario(usuario);
            }
            catch (Exception)
            {
                args.IsValid = false;
            }
        }

        protected void ContrasenhaGeneradaValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                string generada = Session["ContrasenhaGenerada"]?.ToString() ?? string.Empty;

                args.IsValid = !string.IsNullOrEmpty(generada);
            }
            catch (Exception)
            {
                args.IsValid = false;
            }
        }

        protected void TipoUsuarioValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                args.IsValid = EsAdminCheckBox.Checked || EsProfesorCheckBox.Checked;
            }
            catch (Exception)
            {
                args.IsValid = false;
            }
        }
    }
}