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
        private const string TITULO_PAGINA_EDICION = "Formulario de edición de tipo de cuota";
        private const string PLACEHOLDER_CODIGO_TIPO_CUOTA_EDICION = "Ej: 1";
        public bool EsEdicion { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            bool convertido = int.TryParse(Request.QueryString["id"], out int idTipoCuota);

            if (Request.QueryString.Count == 0) 
            {
                ConfigurarTextBoxsReadOnly(false);
                return;
            }

            if (Request.QueryString.Count > 1) 
            {
                return;
            }

            if (!convertido) 
            {
                return;
            }

            if (!ExisteTipoCuotaConId(idTipoCuota)) 
            {
                return;
            }
            
            EsEdicion = true;
            TituloPagina.InnerText = TITULO_PAGINA_EDICION;
            CodigoTipoCuotaTextBox.Attributes["Placeholder"] = PLACEHOLDER_CODIGO_TIPO_CUOTA_EDICION;
            ConfigurarTextBoxsReadOnly(true);

            if (!IsPostBack) 
            {
                CargarDatosTipoCuota(idTipoCuota);
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
                CantidadDiasTextBox.Text = tipoCuota.CantidadEnDias.ToString();
                MontoTextBox.Text = tipoCuota.Valor.ToString("C2").Substring(2);
            }
            catch (Exception ex)
            {

            }
        }

        public void ConfigurarTextBoxsReadOnly(bool readOnly) 
        {
            DescripcionTextBox.ReadOnly = readOnly;
            CantidadDiasTextBox.ReadOnly = readOnly;
            MontoTextBox.ReadOnly = EsEdicion ? !EsEdicion : readOnly;
        }

        public void VincularDatosATipoCuota(TipoCuota tipoCuota) 
        {
            tipoCuota.Descripcion = DescripcionTextBox.Text;
            tipoCuota.Valor = decimal.Parse(MontoTextBox.Text);
            tipoCuota.CantidadEnDias = int.Parse(CantidadDiasTextBox.Text);
        }

        //-------------------------------------------------- FUNCIONES ----------------------------------------------------------------------------------------
        public bool ExisteTipoCuotaConId(int idTipoCuota)
        {
            try
            {
                TipoCuotaNegocio tipoCuotaNegocio = new TipoCuotaNegocio();

                return tipoCuotaNegocio.ExisteTipoCuotaConId(idTipoCuota);
            }
            catch (Exception)
            {
                return false;
            }
        }

        //-------------------------------------------------- EVENTOS ------------------------------------------------------------------------------------------
        protected void EditarTipoCuotaButton_Click(object sender, EventArgs e)
        {
            try
            {
                TipoCuotaNegocio tipoCuotaNegocio = new TipoCuotaNegocio();
                TipoCuota tipoCuota = new TipoCuota();

                if (!Page.IsValid)
                {
                    return;
                }

                tipoCuota.Id = int.Parse(Request.QueryString["id"]);

                VincularDatosATipoCuota(tipoCuota);

                tipoCuotaNegocio.ActualizarMontoTipoCuota(tipoCuota);

                Response.Redirect("/Cuotas/ListadoTiposCuota.aspx", false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void AñadirTipoCuotaButton_Click(object sender, EventArgs e)
        {
            try
            {
                TipoCuotaNegocio tipoCuotaNegocio = new TipoCuotaNegocio();
                TipoCuota tipoCuota = new TipoCuota();

                if (!Page.IsValid) 
                {
                    return;
                }

                VincularDatosATipoCuota(tipoCuota);

                tipoCuotaNegocio.AñadirTipoCuota(tipoCuota);

                Response.Redirect("/Cuotas/ListadoTiposCuota.aspx", false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        //-------------------------------------------------- VALIDATORS ---------------------------------------------------------------------------------------
        protected void MayorACeroValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                if (!CantidadRequiredValidator.IsValid || !CantidadSoloNumerosValidator.IsValid)
                {
                    args.IsValid = true;
                    return;
                }

                args.IsValid = int.TryParse(CantidadDiasTextBox.Text, out int cantidad) && cantidad > 0;
            }
            catch (Exception)
            {
                args.IsValid = false;
            }
        }

        protected void MontoCustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                args.IsValid = decimal.TryParse(MontoTextBox.Text, out decimal monto) && monto > 1;
            }
            catch (Exception)
            {
                args.IsValid = false;
            }
        }
    }
}