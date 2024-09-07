using Gimnasio_Dominio;
using Gimnasio_Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gimnasio_Web
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //-------------------------------------------------- EVENTOS ------------------------------------------------------------------------------------------
        protected void IngresarButton_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
            {
                return;
            }

            try
            {
                UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
                Usuario usuario = usuarioNegocio.ObtenerUsuarioPor(UsuarioTextBox.Text, PasswordTextBox.Text);

                Session.Add("UsuarioSessionActual", usuario);
                Response.Redirect("Default.aspx");
            }
            catch (Exception)
            {

            }
        }

        //-------------------------------------------------- VALIDATORS ---------------------------------------------------------------------------------------
        protected void ExisteUsuarioValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                if (!UsuarioRequeridoValidator.IsValid || !PasswordRequiredValidator.IsValid)
                {
                    args.IsValid = true;
                    return;
                }

                UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
                Usuario usuario = new Usuario()
                {
                    NombreUsuario = UsuarioTextBox.Text,
                    Password = PasswordTextBox.Text
                };

                args.IsValid = usuarioNegocio.ExisteUsuario(usuario);
            }
            catch (Exception)
            {
                args.IsValid = false;
            }
        }

        protected void DatosUsuarioValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                if (!UsuarioRequeridoValidator.IsValid || !PasswordRequiredValidator.IsValid || !ExisteUsuarioValidator.IsValid) 
                {
                    args.IsValid = true;
                    return;
                }

                UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
                Usuario usuario = new Usuario() { 
                    NombreUsuario = UsuarioTextBox.Text,
                    Password = PasswordTextBox.Text
                };

                args.IsValid = usuarioNegocio.InformacionCorrectaUsuario(usuario);
            }
            catch (Exception)
            {
                args.IsValid = false;
            }
        }
    }
}