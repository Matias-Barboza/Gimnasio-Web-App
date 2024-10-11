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
    public partial class FormularioTipoCuota : System.Web.UI.Page
    {
        public bool EsEdicion { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString.Count == 1) 
            {
                if (int.TryParse(Request.QueryString["id"], out int idTipoCuota)) 
                {
                    EsEdicion = true;
                    CargarDatosTipoCuota(idTipoCuota);
                }
                return;
            }

            if (Request.QueryString.Count == 2) 
            {
                int.TryParse(Request.QueryString["id"], out int idTipoCuota);
                bool.TryParse(Request.QueryString["historial"], out bool mostrarHistorial);

                if (idTipoCuota > 0 && mostrarHistorial) 
                {
                    CargarHistorialTipoCuota(idTipoCuota);
                }
            }
        }

        //-------------------------------------------------- METÓDOS ------------------------------------------------------------------------------------------
        public void CargarDatosTipoCuota(int idTipoCuota) 
        {
            try
            {
                TipoCuotaNegocio tipoCuotaNegocio = new TipoCuotaNegocio();
                TipoCuota tipoCuota = tipoCuotaNegocio.ObtenerTipoCuotaPorId(idTipoCuota);

                if (tipoCuota.Id == 0) 
                {
                    return;
                }

                CodigoTipoCuotaTextBox.Text = tipoCuota.Id.ToString();
                DescripcionTextBox.Text = tipoCuota.Descripcion;
                MontoActualTextBox.Text = tipoCuota.Valor.ToString("C2").Substring(2);
            }
            catch (Exception ex)
            {

            }
        }

        public void CargarHistorialTipoCuota(int idTipoCuota) 
        {
            try
            {

            }
            catch (Exception ex)
            {

            }
        }
    }
}