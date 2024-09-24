using Gimnasio_Dominio;
using Gimnasio_Negocio;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gimnasio_Web.Cuotas
{
    public partial class FormularioCuota : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                CargarDropDownListTiposCuotas();
            }
        }

        //-------------------------------------------------- MÉTODOS ------------------------------------------------------------------------------------------
        public void CargarDropDownListTiposCuotas() 
        {
            try
            {
                TipoCuotaNegocio tipoCuotaNegocio = new TipoCuotaNegocio();
                List<TipoCuota> listaTiposCuotas = tipoCuotaNegocio.ObtenerTiposCuotaVisibles();

                if (listaTiposCuotas.Count == 0)
                {
                    TiposCuotasDropDownList.Items.Insert(0, "No se encontraron tipos de cuotas disponibles");
                    return;
                }

                TiposCuotasDropDownList.DataSource = listaTiposCuotas;
                TiposCuotasDropDownList.DataTextField = "Descripcion";
                TiposCuotasDropDownList.DataValueField = "Id";
                TiposCuotasDropDownList.DataBind();
                TiposCuotasDropDownList.Items.Insert(0, new ListItem("Seleccione una opción", "0"));
            }
            catch (Exception ex)
            {
                TiposCuotasDropDownList.Items.Insert(0, "No se cargaron correctamente los tipos de cuotas disponibles. Intente nuevamente recargando la página.");
            }
        }

        public void ConfigurarFechaYMesActual() 
        {
            FechaPagoTextBox.Text = DateTime.Today.ToString("yyyy-MM-dd");
            MesQueAbonaTextBox.Text = DateTime.Today.ToString("MMMM", new CultureInfo("es-ES"));
        }

        public void VincularDatosSocioAFormulario(Socio socio) 
        {
            CodigoSocioTextBox.Text = socio.Id.ToString();
            DniSocioTextBox.Text = socio.Dni;
            NombreSocioTextBox.Text = socio.Nombre;
            ApellidoSocioTextBox.Text = socio.Apellido;
            EstaActivoCheckBox.Checked = socio.EstaActivo;
        }

        public void VincularDatosUltimaCuotaAFormulario(Cuota cuota)
        {
            FechaPagoTextBox.Text = cuota.FechaVencimiento.ToString("yyyy-MM-dd");
            MesQueAbonaTextBox.Text = cuota.FechaVencimiento.ToString("MMMM", new CultureInfo("es-ES"));
        }

        //-------------------------------------------------- FUNCIONES ----------------------------------------------------------------------------------------
        public bool TipoCuotaYCantidadValidos()
        {
            TipoCuotaValidator.Validate();
            CantidadRequiredValidator.Validate();
            CantidadSoloNumerosValidator.Validate();
            MayorACeroValidator.Validate();

            return TipoCuotaValidator.IsValid && CantidadRequiredValidator.IsValid &&
                   CantidadSoloNumerosValidator.IsValid && MayorACeroValidator.IsValid;
        }

        //-------------------------------------------------- EVENTOS ------------------------------------------------------------------------------------------
        protected void BuscarButton_ServerClick(object sender, EventArgs e)
        {
            try
            {
                int.TryParse(CodigoSocioTextBox.Text, out int idSocio);
                SocioNegocio socioNegocio = new SocioNegocio();
                Socio socio = socioNegocio.ObtenerSocioPorId(idSocio);
                CuotaNegocio cuotaNegocio = new CuotaNegocio();
                Cuota cuota = cuotaNegocio.ObtenerUltimaCuotaPorIdSocio(idSocio);

                VincularDatosSocioAFormulario(socio);

                //Si no está activo es como si volviera a empezar de 0, o si no tiene una ultima cuota encontrada, la fecha y mes, van a ser la actual
                if (!socio.EstaActivo || cuota.Id == 0) 
                {
                    ConfigurarFechaYMesActual();
                    return;
                }

                VincularDatosUltimaCuotaAFormulario(cuota);
            }
            catch (Exception ex)
            {
                
            }
        }

        protected void CalcularMontoButton_Click(object sender, EventArgs e)
        {
            try
            {
                TipoCuotaNegocio tipoCuotaNegocio;
                List<string> valoresCalculados;
                string idTipoCuotaStr = TiposCuotasDropDownList.SelectedItem.Value;
                string cantidadStr = CantidadTextBox.Text;
                int idTipoCuota;
                int cantidad;

                if (!TipoCuotaYCantidadValidos())
                {
                    return;
                }

                tipoCuotaNegocio = new TipoCuotaNegocio();
                idTipoCuota = int.Parse(idTipoCuotaStr);
                cantidad = int.Parse(cantidadStr);


                MontoAbonarTextBox.Text = tipoCuotaNegocio.CalcularMontoAbonar(idTipoCuota, cantidad);

                valoresCalculados = new List<string>() { idTipoCuotaStr, cantidadStr, MontoAbonarTextBox.Text };

                Session.Add("ValoresCalculados", valoresCalculados);
            }
            catch (Exception ex)
            {

            }
        }

        protected void RegistrarCuotaButton_Click(object sender, EventArgs e)
        {

        }

        protected void EditarCuotaButton_Click(object sender, EventArgs e)
        {

        }

        //-------------------------------------------------- VALIDATORS ---------------------------------------------------------------------------------------
        protected void TipoCuotaValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                int.TryParse(TiposCuotasDropDownList.SelectedItem.Value, out int idTipoCuota);

                args.IsValid = idTipoCuota > 0;
            }
            catch (Exception)
            {
                args.IsValid = false;
            }
        }


        protected void MayorACeroValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                if (!CantidadRequiredValidator.IsValid || !CantidadSoloNumerosValidator.IsValid)
                {
                    args.IsValid = true;
                    return;
                }

                args.IsValid = int.TryParse(CantidadTextBox.Text, out int cantidad) && cantidad > 0;
            }
            catch (Exception)
            {
                args.IsValid = false;
            }
        }

        protected void ValoresCalculadosValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                if (Session["ValoresCalculados"] == null)
                {
                    args.IsValid = true;
                    return;
                }

                List<string> valoresCalculados = (List<string>)Session["ValoresCalculados"];

                args.IsValid = TiposCuotasDropDownList.SelectedItem.Value == valoresCalculados[0] &&
                               CantidadTextBox.Text == valoresCalculados[1] &&
                               MontoAbonarTextBox.Text == valoresCalculados[2];
            }
            catch (Exception)
            {
                args.IsValid = false;
            }
        }

        protected void FechaValidaValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                args.IsValid = DateTime.TryParse(FechaPagoTextBox.Text, out _);
            }
            catch (Exception)
            {
                args.IsValid = false;
            }
        }
    }
}