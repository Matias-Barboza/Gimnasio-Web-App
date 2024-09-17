<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormularioSocio.aspx.cs" Inherits="Gimnasio_Web.Socios.FormularioSocio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="border rounded h-fill-available m-5 mt-4 p-3">
        <div>
            <h2 id="TituloFormulario" class="text-center" runat="server">Formulario de nuevo socio</h2>
            <hr class="mt-0" />
        </div>
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div class="overflow-y-scroll vh-65">
                    <div class="border rounded mx-2 mb-4 p-3">
                        <h4>Código de socio</h4>
                        <hr class="mt-0" />
                        <div class="mb-2">
                            <asp:TextBox ID="CodigoSocioTextBox" ReadOnly="true" PlaceHolder="Autogenerado" CssClass="form-control form-control-lg" runat="server" ></asp:TextBox>
                        </div>
                    </div>
                    <div class="border rounded my-4 mx-2 p-3">
                        <h4>Datos personales</h4>
                        <hr class="mt-0" />
                        <div>
                            <div class="form-floating mb-1">
                                <asp:TextBox ID="DniSocioTextBox" PlaceHolder="DNI" CssClass="form-control" runat="server" ></asp:TextBox>
                                <label for="DniSocioTextBox">DNI<span class="text-danger">*</span></label>
                            </div>
                            <div class="ms-2 mb-3">
                                <asp:RequiredFieldValidator ErrorMessage="El DNI no puede estar vacío." ControlToValidate="DniSocioTextBox"
                                    Display="Dynamic" CssClass="validator" runat="server" />
                            </div>
                        </div>
                        <div class="d-flex">
                            <div class="col">
                                <div class="form-floating me-2 mb-1">
                                    <asp:TextBox ID="NombreSocioTextBox" PlaceHolder="Nombre" CssClass="form-control" runat="server" ></asp:TextBox>
                                    <label for="NombreSocioTextBox">Nombre<span class="text-danger">*</span></label>
                                </div>
                                <div>
                                    <%-- Validators --%>
                                    <asp:RegularExpressionValidator ErrorMessage="El nombre solo puede contener letras." ValidationExpression="^[A-Za-zÁÉÍÓÚáéíóúÑñ\s]+$" 
                                        ControlToValidate="NombreSocioTextBox" Display="Dynamic" CssClass="validator" runat="server" />
                                    <asp:RegularExpressionValidator ErrorMessage="El nombre puede tener un mínimo de 2 y un máximo de 100 caracteres." ValidationExpression="^.{1,100}$"
                                        ControlToValidate="NombreSocioTextBox" Display="Dynamic" CssClass="validator" runat="server" />
                                </div>
                            </div>
                            <div class="col">
                                <div class="form-floating ms-2 mb-1">
                                    <asp:TextBox ID="ApellidoSocioTextBox" PlaceHolder="Apellido" CssClass="form-control" runat="server" ></asp:TextBox>
                                    <label for="ApellidoSocioTextBox">Apellido<span class="text-danger">*</span></label>
                                </div>
                                <div>
                                    <%-- Validators --%>
                                    <asp:RegularExpressionValidator ErrorMessage="El apellido solo puede contener letras." ValidationExpression="^[A-Za-zÁÉÍÓÚáéíóúÑñ\s]+$" 
                                        ControlToValidate="ApellidoSocioTextBox" Display="Dynamic" CssClass="validator" runat="server" />
                                    <asp:RegularExpressionValidator ErrorMessage="El apellido puede tener un mínimo de 2 y un máximo de 100 caracteres." ValidationExpression="^.{1,100}$"
                                        ControlToValidate="ApellidoSocioTextBox" Display="Dynamic" CssClass="validator" runat="server" />
                                </div>
                            </div>
                        </div>
                        <p class="form-text ms-1">Los datos marcados con (<span class="text-danger">*</span>) son obligatorios para registrar/editar un socio.</p>
                    </div>
                    <div class="border rounded my-4 mx-2 p-3">
                        <div class="d-flex justify-content-between align-items-baseline mx-1">
                            <h4>Registrar con primera cuota</h4>
                            <button id="ConPrimeraCuotaButton" onserverclick="ConPrimeraCuotaButton_ServerClick" class="btn btn-invisible" runat="server">
                                <input id="ConPrimeraCuotaCheckBox" type="checkbox" class="form-check-input checkbox-xl mt-0 me-1" runat="server" />
                            </button>
                        </div>
                        <%if (ConPrimeraCuota)
                          {%>
                        <hr class="mt-0" />
                        <div class="d-flex">
                            <div class="col">
                                <div class="me-2 mb-1">
                                    <label for="TipoCuotaDropDownList" class="form-label">Tipo de cuota<span class="text-danger">*</span></label>
                                    <asp:DropDownList ID="TipoCuotaDropDownList" CssClass="form-select" runat="server"></asp:DropDownList>
                                </div>
                                <div class="ms-2 mb-3">
                                    <%-- Validators --%>
                                </div>
                            </div>
                            <div class="col">
                                <div class="mx-2 mb-1">
                                    <label for="CantidadDropDownList" class="form-label">Cantidad<span class="text-danger">*</span></label>
                                    <asp:DropDownList ID="CantidadDropDownList" CssClass="form-select" runat="server"></asp:DropDownList>
                                </div>
                                <div class="ms-2 mb-3">
                                    <%-- Validators --%>
                                </div>
                            </div>
                            <div class="col">
                                <div class="mx-2 mb-1">
                                    <label for="MontoAbonarTextBox" class="form-label">Monto a abonar</label>
                                    <div class="input-group mb-3">
                                      <span class="input-group-text">$</span>
                                      <asp:TextBox ID="MontoAbonarTextBox" ReadOnly="true" PlaceHolder="El monto depende del tipo de cuota y la cantidad" CssClass="form-control" runat="server" ></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="d-flex">
                            <div class="col">
                                <div class="me-2 mb-1">
                                    <label for="FechaPagoTextBox" class="form-label">Fecha de pago<span class="text-danger">*</span></label>
                                    <asp:TextBox ID="FechaPagoTextBox" ReadOnly="true" TextMode="Date" CssClass="form-control" runat="server" ></asp:TextBox>
                                </div>
                                <div class="ms-2 mb-3">
                                    <%-- Validators --%>
                                </div>
                            </div>
                            <div class="col">
                                <div class="ms-2 mb-1">
                                    <label for="MesQueAbonaDropDownList" class="form-label">Mes que abona<span class="text-danger">*</span></label>
                                    <asp:TextBox ID="MesQueAbonaTextBox" ReadOnly="true" CssClass="form-control" runat="server" />
                                    <%--<asp:DropDownList ID="MesQueAbonaDropDownList" CssClass="form-select" runat="server"></asp:DropDownList>--%>
                                </div>
                                <div class="ms-2 mb-3">
                                    <%-- Validators --%>
                                </div>
                            </div>
                        </div>
                        <p class="form-text ms-1">Los datos marcados con (<span class="text-danger">*</span>) son obligatorios para registrar/editar un socio.</p>
                        <%}%>
                    </div>
                    <div class="d-flex justify-content-center align-items-center">
                        <asp:Button Text="Registrar" CssClass="btn btn-primary btn-lg" runat="server" />
                        <asp:Button Text="Editar" CssClass="btn btn-primary btn-lg" runat="server" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        
    </div>

</asp:Content>
