using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Gimnasio_Dominio;
using Gimnasio_Web.Cuotas;
using Gimnasio_Web.Socios;

namespace Gimnasio_Web
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CargarMensajeHeader();
        }

        //-------------------------------------------------- MÉTODOS ------------------------------------------------------------------------------------------
        public void CargarMensajeHeader() 
        {
            if (Page is Default) 
            {
                Usuario usuarioActual = (Usuario)Session["UsuarioSessionActual"];

                if (!string.IsNullOrEmpty(usuarioActual.Nombre)) 
                {
                    MensajeHeader.InnerText = $"¡Hola {usuarioActual.Nombre}!".ToUpper();
                    return;
                }

                MensajeHeader.InnerText = $"¡Hola!".ToUpper();
            }
        }

        //-------------------------------------------------- EVENTOD ------------------------------------------------------------------------------------------
        protected void CambiarEstadoSocioButton_Click(object sender, EventArgs e)
        {
            if (Page is ListadoSocios listadoSociosPagina)
            {
                int codigoSocio = Convert.ToInt32(CodigoSocioHiddenField.Value);

                listadoSociosPagina.CambiarEstadoSocio(codigoSocio);
                listadoSociosPagina.CargarSocios(listadoSociosPagina.SoloActivos);

                CodigoSocioHiddenField.Value = string.Empty;
            }
        }

        protected void CambiarEstadoTipoButton_Click(object sender, EventArgs e)
        {
            if (Page is ListadosTiposCuota listadoTiposCuotaPagina)
            {
                int codigoTipoCuota = Convert.ToInt32(CodigoTipoCuotaHiddenField.Value);

                listadoTiposCuotaPagina.CambiarEstadoTipoDeCuota(codigoTipoCuota);
                listadoTiposCuotaPagina.CargarTiposDeCuota(listadoTiposCuotaPagina.SoloVisibles);

                CodigoSocioHiddenField.Value = string.Empty;
            }
        }
    }
}