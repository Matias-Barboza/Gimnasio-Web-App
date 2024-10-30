using Gimnasio_Dominio;
using Gimnasio_Negocio;
using Gimnasio_Web.Utilidades;
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
        private const string TITULO_PAGINA_LISTADO_ACTIVAS = "Listado de tipos de cuota activas";
        public bool MostrarResultadoBusqueda;
        public bool SoloVisibles;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString.Count == 0) 
            {
                if (!Seguridad.UsuarioEsAdmin((Usuario)Session["UsuarioSessionActual"]))
                {
                    Response.Redirect("/Cuotas/ListadoTiposCuota.aspx?estado=activas");
                    return;
                }
            }

            if (Request.QueryString.Count != 0) 
            {
                if (Request["estado"] == "activas") 
                {
                    TituloListado.InnerText = TITULO_PAGINA_LISTADO_ACTIVAS;
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
                List<TipoCuota> listaTiposCuota = tipoCuotaNegocio.ObtenerTiposCuotaPor(campoBusqueda, soloVisibles);

                if (MostrarResultadoBusqueda) 
                {
                    ResultadoBusquedaLabel.InnerText = listaTiposCuota.Count.ToString();   
                }

                if (listaTiposCuota.Count == 0) 
                {
                    return;
                }


                TiposCuotasGridView.DataSource = listaTiposCuota;
                
                if (!Seguridad.UsuarioEsAdmin((Usuario)Session["UsuarioSessionActual"])) 
                {
                    TiposCuotasGridView.Columns[5].Visible = false;
                    TiposCuotasGridView.Columns[6].Visible = false;
                    TiposCuotasGridView.Columns[7].Visible = false;
                }

                TiposCuotasGridView.DataBind();
            }
            catch (Exception ex)
            {

            }
        }

        public void CambiarEstadoTipoDeCuota(int codigoTipoCuota) 
        {
            try
            {
                TipoCuotaNegocio tipoCuotaNegocio = new TipoCuotaNegocio();
                TipoCuota tipoCuotaACambiar = tipoCuotaNegocio.ObtenerTipoCuotaPorId(codigoTipoCuota);

                tipoCuotaNegocio.ActualizarEstadoTipoCuota(tipoCuotaACambiar);
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
                Response.Redirect($"/Cuotas/HistorialTipoCuota.aspx?id={idTipoCuota}");
            }
        }
    }
}