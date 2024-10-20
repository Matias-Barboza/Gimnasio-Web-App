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
        public bool EsEdicion;

        protected void Page_Load(object sender, EventArgs e)
        {
            bool convertido = int.TryParse(Request.QueryString["id"], out int idCuota);

            if (Request.QueryString.Count != 0)
            {
                if (!convertido) 
                {
                    return;
                }

                if (!ExisteCuotaConId(idCuota))
                {
                    return;
                }

                EsEdicion = true;
            }

            if (!IsPostBack) 
            {
                CargarDropDownListTiposCuotas();
                EliminarDatosSession();

                if (EsEdicion) 
                {
                    CargarDatosCuotaAEditar(idCuota);
                    CargarValoresCalculadosYSocioBuscado();
                }
            }
        }

        //-------------------------------------------------- MÉTODOS ------------------------------------------------------------------------------------------
        public void CargarDatosCuotaAEditar(int idCuota) 
        {
            try
            {
                CuotaNegocio cuotaNegocio = new CuotaNegocio();
                SocioNegocio socioNegocio = new SocioNegocio();
                Cuota cuota = cuotaNegocio.ObtenerCuotaConSocioYTipoPorIdCuota(idCuota);
                Socio socio = socioNegocio.ObtenerSocioPorId(cuota.Socio.Id);

                VincularDatosCuotaAFormulario(cuota);
                VincularDatosSocioAFormulario(socio);
            }
            catch (Exception)
            {

                throw;
            }
        }

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

        public void VincularDatosCuotaAFormulario(Cuota cuota) 
        {
            TiposCuotasDropDownList.Items.FindByValue(cuota.TipoCuota.Id.ToString()).Selected = true;
            CantidadTextBox.Text = ((int)(cuota.MontoAbonado / cuota.ValorTipoCuota)).ToString();
            MontoAbonarTextBox.Text = cuota.MontoAbonado.ToString("C2").Substring(2);
            FechaPagoTextBox.Text = cuota.FechaPago.ToString("yyyy-MM-dd");
            MesQueAbonaTextBox.Text = cuota.MesQueAbona;
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

        public void VincularDatosACuota(Cuota cuota, bool conSocioSession = false) 
        {
            TipoCuotaNegocio tipoCuotaNegocio = new TipoCuotaNegocio();
            int cantidad = Convert.ToInt32(CantidadTextBox.Text);

            if (conSocioSession) 
            {
                cuota.Socio = (Socio)Session["SocioBuscado"];
            }

            cuota.TipoCuota.Id = Convert.ToInt32(TiposCuotasDropDownList.SelectedItem.Value);
            cuota.FechaPago = DateTime.Parse(FechaPagoTextBox.Text);
            cuota.FechaVencimiento = CuotaNegocio.FechaVencimientoCalculada(cuota.FechaPago, cuota.TipoCuota.Id, cantidad);
            cuota.MesQueAbona = MesQueAbonaTextBox.Text;
            cuota.MontoAbonado = decimal.Parse(MontoAbonarTextBox.Text);
            cuota.ValorTipoCuota = tipoCuotaNegocio.ObtenerMontoActualTipoCuotaPorId(cuota.TipoCuota.Id);
        }

        public void EliminarDatosSession() 
        {
            Session.Remove("SocioBuscado");
        }

        public void CargarValoresCalculadosYSocioBuscado() 
        {
            string idTipoCuotaStr = TiposCuotasDropDownList.SelectedItem.Value;
            string cantidadStr = CantidadTextBox.Text;
            List<string> valoresCalculados = new List<string>() { idTipoCuotaStr, cantidadStr, MontoAbonarTextBox.Text };
            int.TryParse(CodigoSocioTextBox.Text, out int idSocio);
            SocioNegocio socioNegocio = new SocioNegocio();
            Socio socio = socioNegocio.ObtenerSocioPorId(idSocio);

            Session.Add("ValoresCalculados", valoresCalculados);
            Session.Add("SocioBuscado", socio);
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

        public bool CodigoSocioValido() 
        {
            CodigoSocioNoVacioValidator.Validate();
            CodigoSocioSoloNumerosValidator.Validate();
            CodigoSocioExistenteValidator.Validate();

            return CodigoSocioNoVacioValidator.IsValid && CodigoSocioSoloNumerosValidator.IsValid && CodigoSocioExistenteValidator.IsValid;
        }

        private bool ExisteCuotaConId(int idCuota)
        {
            try
            {
                CuotaNegocio cuotaNegocio = new CuotaNegocio();

                return cuotaNegocio.ExisteCuotaConId(idCuota);
            }
            catch (Exception)
            {
                return false;
            }
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

                CodigoSocioValido();

                if (!Page.IsValid) 
                {
                    return;
                }

                VincularDatosSocioAFormulario(socio);
                Session.Add("SocioBuscado", socio);

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
                CuotaNegocio cuotaNegocio;
                List<string> valoresCalculados;
                string idTipoCuotaStr = TiposCuotasDropDownList.SelectedItem.Value;
                string cantidadStr = CantidadTextBox.Text;
                int idTipoCuota;
                int cantidad;
                decimal valorTipoCuota = 0;
                bool mismoTipoCuota = false;

                if (!TipoCuotaYCantidadValidos())
                {
                    return;
                }

                tipoCuotaNegocio = new TipoCuotaNegocio();
                idTipoCuota = int.Parse(idTipoCuotaStr);
                cantidad = int.Parse(cantidadStr);

                if (EsEdicion) 
                {
                    cuotaNegocio = new CuotaNegocio();
                    int idCuota = int.Parse(Request.QueryString["id"]);
                    Cuota cuota = cuotaNegocio.ObtenerCuotaConSocioYTipoPorIdCuota(idCuota);
                    valorTipoCuota = cuota.ValorTipoCuota;
                    mismoTipoCuota = cuota.TipoCuota.Id == idTipoCuota;
                }

                MontoAbonarTextBox.Text = mismoTipoCuota ? tipoCuotaNegocio.CalcularMontoAbonar(valorTipoCuota, cantidad) :
                                                           tipoCuotaNegocio.CalcularMontoAbonar(idTipoCuota, cantidad);

                valoresCalculados = new List<string>() { idTipoCuotaStr, cantidadStr, MontoAbonarTextBox.Text };

                Session.Add("ValoresCalculados", valoresCalculados);
            }
            catch (Exception ex)
            {

            }
        }

        protected void RegistrarCuotaButton_Click(object sender, EventArgs e)
        {
            try
            {
                CuotaNegocio cuotaNegocio = new CuotaNegocio();
                Cuota cuota = new Cuota();

                if (!Page.IsValid) 
                {
                    return;
                }

                VincularDatosACuota(cuota,true);

                int idCuotaNueva = cuotaNegocio.AñadirCuota(cuota);

                EliminarDatosSession();

                Response.Redirect("/Cuotas/ListadoCuotas.aspx", false);
            }
            catch (Exception ex)
            {
                
            }
        }

        protected void EditarCuotaButton_Click(object sender, EventArgs e)
        {

        }

        //-------------------------------------------------- VALIDATORS ---------------------------------------------------------------------------------------
        protected void CodigoSocioExistenteValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                if (!CodigoSocioNoVacioValidator.IsValid || !CodigoSocioSoloNumerosValidator.IsValid) 
                {
                    args.IsValid = true;
                    return;
                }

                int idSocio = Convert.ToInt32(CodigoSocioTextBox.Text);
                SocioNegocio socioNegocio = new SocioNegocio();
                Socio socio = socioNegocio.ObtenerSocioPorId(idSocio);

                args.IsValid = socio.Id != 0;
            }
            catch (Exception)
            {
                args.IsValid = false;
            }
        }

        protected void SocioBuscadoValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                if (!CodigoSocioNoVacioValidator.IsValid || !CodigoSocioSoloNumerosValidator.IsValid || !CodigoSocioExistenteValidator.IsValid) 
                {
                    args.IsValid = true;
                    return;
                }

                args.IsValid = Session["SocioBuscado"] != null; 
            }
            catch (Exception)
            {
                args.IsValid = false;
            }
        }

        protected void DatosSocioBuscadoValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                if (Session["SocioBuscado"] is Socio socio) 
                {
                    args.IsValid = CodigoSocioTextBox.Text == socio.Id.ToString() &&
                                   DniSocioTextBox.Text == socio.Dni &&
                                   NombreSocioTextBox.Text == socio.Nombre &&
                                   ApellidoSocioTextBox.Text == socio.Apellido;
                }
            }
            catch (Exception)
            {
                args.IsValid = false;
            }
        }

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