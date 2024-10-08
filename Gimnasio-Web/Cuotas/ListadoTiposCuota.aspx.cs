using Gimnasio_Dominio;
using Gimnasio_Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gimnasio_Web.Cuotas
{
    public partial class ListadosTiposCuota : System.Web.UI.Page
    {
        public bool MostrarResultadoBusqueda;
        public bool SoloVisibles;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString.Count != 0) 
            {
                if (Request["estado"] == "activas") 
                {
                    TituloListado.InnerText += "activas";
                    SoloVisibles = true;
                }
            }

            if (!IsPostBack) 
            {
                CargarTiposDeCuota(SoloVisibles);
            }
        }

        //-------------------------------------------------- METÓDOS ------------------------------------------------------------------------------------------
        public void CargarTiposDeCuota(bool soloVisibles = false, string campoBusqueda = null)
        {
            try
            {
                TipoCuotaNegocio tipoCuotaNegocio = new TipoCuotaNegocio();
                List<TipoCuota> listaTiposCuota = tipoCuotaNegocio.ObtenerTiposCuotaPor(soloVisibles, campoBusqueda);

                if (MostrarResultadoBusqueda) 
                {
                    ResultadoBusquedaLabel.InnerText = listaTiposCuota.Count.ToString();   
                }

                if (listaTiposCuota.Count == 0) 
                {
                    return;
                }

                TiposCuotasGridView.DataSource = listaTiposCuota;
                TiposCuotasGridView.DataBind();
            }
            catch (Exception ex)
            {

            }
        }

        //-------------------------------------------------- EVENTOS ------------------------------------------------------------------------------------------
        protected void BuscarButton_ServerClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(CampoBusquedaTextBox.Text)) 
            {
                MostrarResultadoBusqueda = true;
                CargarTiposDeCuota(SoloVisibles, CampoBusquedaTextBox.Text);
                return;
            }

            CargarTiposDeCuota(SoloVisibles);
        }

        protected void TiposCuotasGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int indiceFila = Convert.ToInt32(e.CommandArgument);
            string idTipoCuota = TiposCuotasGridView.DataKeys[indiceFila].Value.ToString();

            if (e.CommandName == "EditarTipoCuota") 
            {
                Response.Redirect($"/Cuotas/FormularioTipoCuota.aspx?id={idTipoCuota}");
            }
            else if (e.CommandName == "VerHistorial") 
            {
                Response.Redirect($"/Cuotas/FormularioTipoCuota.aspx?id={idTipoCuota}&historial=true");
            }
        }
    }
}