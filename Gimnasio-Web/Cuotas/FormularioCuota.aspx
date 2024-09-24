<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormularioCuota.aspx.cs" Inherits="Gimnasio_Web.Cuotas.FormularioCuota" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="border rounded h-fill-available m-5 mt-4 p-3">
    <div>
        <h2 id="TituloFormulario" class="text-center" runat="server">Formulario de nueva cuota</h2>
        <hr class="mt-0" />
    </div>
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div class="overflow-y-scroll vh-65">

                    <div class="border rounded mx-2 mb-4 p-3">
                        <h4>Datos del socio</h4>
                        <hr class="mt-0" />
                        <div class="mb-2">
                            <label for="CodigoSocioTextBox" class="ms-1 mb-1">Código de socio<span class="text-danger">*</span></label>
                            <div class="d-flex align-items-baseline">
                                <asp:TextBox ID="CodigoSocioTextBox" PlaceHolder="Código de socio" CssClass="form-control" runat="server" ></asp:TextBox>
                                <button id="BuscarButton" type="button" onserverclick="BuscarButton_ServerClick" class="d-flex align-items-center btn btn-primary py-1 ms-1" runat="server">
                                    <i class="bi bi-search me-2"></i>
                                    Buscar socio
                                </button>
                            </div>
                            <div>
                                <%-- Validators --%>
                                <asp:RequiredFieldValidator ErrorMessage="El código de socio no puede estar vacío." ControlToValidate="CodigoSocioTextBox"
                                        ValidationGroup="OperacionValidationGroup" Display="Dynamic" CssClass="validator" runat="server" />
                                <asp:RegularExpressionValidator ErrorMessage="El código de socio solo debe contener números (sin puntos u otros caracteres)." ValidationExpression="^\d+$" 
                                        ControlToValidate="CodigoSocioTextBox" ValidationGroup="OperacionValidationGroup" Display="Dynamic" CssClass="validator" runat="server" />
                            </div>
                        </div>
                        <div class="d-flex mt-1 mb-2">
                            <div class="col me-2 mb-1">
                                <label for="DniSocioTextBox" class="ms-1 mb-1">DNI</label>
                                <asp:TextBox ID="DniSocioTextBox" ReadOnly="true" CssClass="form-control" runat="server" ></asp:TextBox>
                            </div>
                            <div class="col mb-1">
                                <label for="NombreSocioTextBox" class="ms-1 mb-1">Nombre</label>
                                <asp:TextBox ID="NombreSocioTextBox" ReadOnly="true" CssClass="form-control" runat="server" ></asp:TextBox>
                            </div>
                            <div class="col ms-2 mb-1">
                                <label for="ApellidoSocioTextBox" class="ms-1 mb-1">Apellido</label>
                                <asp:TextBox ID="ApellidoSocioTextBox" CssClass="form-control" runat="server" ></asp:TextBox>
                            </div>
                            <div class="col ms-2 mb-1">
                                <label for="ApellidoSocioTextBox" class="ms-1 mb-1">Activo</label>
                                <div class="d-flex justify-content-center">
                                    <input id="EstaActivoCheckBox" type="checkbox" disabled="disabled" class="form-check-input checkbox-xl position-relative top-12-px mt-0" runat="server" />
                                </div>
                            </div>
                        </div>
                        <p class="form-text ms-1">Los datos marcados con (<span class="text-danger">*</span>) son obligatorios para registrar/editar una cuota.</p>
                    
                        <h4>Datos de la cuota</h4>
                        <hr class="mt-0" />
                        <%-- TIPO DE CUOTA, CANTIDAD Y MONTO A ABONAR --%>
                        <div class="d-flex">
                            <div class="col">
                                <div class="me-2 mb-2">
                                    <label for="TiposCuotasDropDownList" class="form-label">Tipo de cuota<span class="text-danger">*</span></label>
                                    <asp:DropDownList ID="TiposCuotasDropDownList" CssClass="form-select" runat="server"></asp:DropDownList>
                                </div>
                                <div>
                                    <%-- Validators --%>
                                    <asp:CustomValidator ID="TipoCuotaValidator" ErrorMessage="Debe seleccionarse un tipo de cuota válido." OnServerValidate="TipoCuotaValidator_ServerValidate"
                                        ControlToValidate="TiposCuotasDropDownList" ValidationGroup="OperacionValidationGroup" Display="Dynamic" CssClass="validator ms-2 mb-3" runat="server" />
                                </div>
                            </div>
                            <div class="col">
                                <div class="d-flex">
                                    <div class="w-100 mx-2 mb-2">
                                        <label for="CantidadTextBox" class="form-label">Cantidad<span class="text-danger">*</span></label>
                                        <asp:TextBox ID="CantidadTextBox" PlaceHolder="Escriba la cantidad de días, semanas o meses (solo números)" CssClass="form-control" runat="server" ></asp:TextBox>
                                    </div>
                                    <div class="d-flex align-items-end me-2 mb-2">
                                        <asp:Button ID="CalcularMontoButton" OnClick="CalcularMontoButton_Click" Text="Calcular" CausesValidation="false"
                                            CssClass="btn btn-primary" runat="server" />
                                    </div>
                                </div>
                                <div>
                                    <%-- Validators --%>
                                    <asp:RequiredFieldValidator ID="CantidadRequiredValidator" ErrorMessage="La cantidad no puede estar vacía."
                                        ControlToValidate="CantidadTextBox" ValidationGroup="OperacionValidationGroup" Display="Dynamic" CssClass="validator ms-3 mb-3" runat="server" />
                                    <asp:RegularExpressionValidator ID="CantidadSoloNumerosValidator" ErrorMessage="La cantidad debe contener unicamente números."
                                        ValidationExpression="^\d+$" ControlToValidate="CantidadTextBox" ValidationGroup="OperacionValidationGroup" Display="Dynamic" CssClass="validator ms-3 mb-3" runat="server" />
                                    <asp:CustomValidator ID="MayorACeroValidator" ErrorMessage="La cantidad debe ser mayor a cero." OnServerValidate="MayorACeroValidator_ServerValidate"
                                        ControlToValidate="CantidadTextBox" ValidationGroup="OperacionValidationGroup" Display="Dynamic" CssClass="validator ms-3 mb-3" runat="server" />
                                </div>
                            </div>
                            <div class="col">
                                <div class="mx-2 mb-2">
                                    <label for="MontoAbonarTextBox" class="form-label">Monto a abonar<span class="text-danger">*</span></label>
                                    <div class="input-group">
                                        <span class="input-group-text">$</span>
                                        <asp:TextBox ID="MontoAbonarTextBox" ReadOnly="true" PlaceHolder="El monto depende del tipo de cuota y la cantidad" CssClass="form-control" runat="server" ></asp:TextBox>
                                    </div>
                                </div>
                                <div>
                                    <%-- Validators --%>
                                    <asp:RequiredFieldValidator ErrorMessage="El monto debe ser calculado para poder registrar el socio." ControlToValidate="MontoAbonarTextBox"
                                        ValidationGroup="OperacionValidationGroup" Display="Dynamic" CssClass="validator ms-3 mb-3" runat="server" />
                                </div>
                            </div>
                        </div>
                        <div class="d-flex">
                            <%-- Validators --%>
                            <asp:CustomValidator ID="ValoresCalculadosValidator" ErrorMessage="Hubo cambios en los valores calculados y ya no coindicen, calcule nuevamente."
                                    OnServerValidate="ValoresCalculadosValidator_ServerValidate" ValidationGroup="OperacionValidationGroup"
                                    Display="Dynamic" CssClass="validator ms-2 mb-3" runat="server" />
                        </div>
                        <%-- FECHA DE PAGO Y MES --%>
                        <div class="d-flex">
                            <div class="col">
                                <div class="mt-2 me-2 mb-1">
                                    <label for="FechaPagoTextBox" class="form-label">Fecha de pago</label>
                                    <asp:TextBox ID="FechaPagoTextBox" ReadOnly="true" TextMode="Date" CssClass="form-control" runat="server" ></asp:TextBox>
                                </div>
                                <div>
                                    <%-- Validators --%>
                                    <asp:CustomValidator ID="FechaValidaValidator" ErrorMessage="La fecha de pago es obligatoria." OnServerValidate="FechaValidaValidator_ServerValidate"
                                            ControlToValidate="FechaPagoTextBox" ValidationGroup="OperacionValidationGroup" ValidateEmptyText="true" Display="Dynamic" CssClass="validator" runat="server" />
                                </div>
                            </div>
                            <div class="col">
                                <div class="mt-2 ms-2 mb-1">
                                    <label for="MesQueAbonaTextBox" class="form-label">Mes que abona</label>
                                    <asp:TextBox ID="MesQueAbonaTextBox" ReadOnly="true" CssClass="form-control" runat="server" />
                                </div>
                                <div>
                                    <%-- Validators --%>
                                    <asp:RequiredFieldValidator ErrorMessage="El mes que abona no puede estar vacío."
                                        ControlToValidate="MesQueAbonaTextBox" ValidationGroup="OperacionValidationGroup" Display="Dynamic" CssClass="validator ms-3" runat="server" />
                                </div>
                            </div>
                        </div>
                        <p class="form-text ms-1 mb-0">Los datos marcados con (<span class="text-danger">*</span>) son obligatorios para registrar una cuota.</p>
                        <p class="form-text ms-1 mb-0">Luego de ingresar la cantidad que corresponda, presione el botón calcular para obtener el monto a abonar.</p>
                        <p class="form-text ms-1">El sistema pondrá como fecha de pago la fecha de vencimiento de la última cuota encontrada. De lo contrario será la fecha actual.</p>
                    </div>

                    <div class="d-flex justify-content-center align-items-center">
                        <asp:Button ID="RegistrarCuotaButton" OnClick="RegistrarCuotaButton_Click" Text="Registrar" CausesValidation="true"
                            ValidationGroup="OperacionValidationGroup" CssClass="btn btn-primary btn-lg" runat="server" />
                        <asp:Button ID="EditarCuotaButton" OnClick="EditarCuotaButton_Click" Text="Editar" CausesValidation="true" ValidationGroup="OperacionValidationGroup"
                            CssClass="btn btn-primary btn-lg px-4 me-1" runat="server" />
                        <a href="/Socios/ListadoSocios.aspx" class="btn btn-danger btn-lg ms-1">Cancelar</a>
                    </div>

                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    
    </div>

</asp:Content>
