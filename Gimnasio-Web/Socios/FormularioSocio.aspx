﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormularioSocio.aspx.cs" Inherits="Gimnasio_Web.Socios.FormularioSocio" %>
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
                                <asp:RequiredFieldValidator ID="DniRequiredValidator" ErrorMessage="El DNI no puede estar vacío." ControlToValidate="DniSocioTextBox"
                                    Display="Dynamic" CssClass="validator" runat="server" />
                                <asp:RegularExpressionValidator ID="DniLongitudValidator" ErrorMessage="El DNI solo debe contener de 7 a 8 caracteres." ValidationExpression="^\d{7,8}$"
                                    ControlToValidate="DniSocioTextBox" Display="Dynamic" CssClass="validator" runat="server" />
                                <asp:RegularExpressionValidator ID="DniSoloNumerosValidator" ErrorMessage="El DNI solo debe contener números (sin puntos u otros caracteres)." ValidationExpression="^\d+$" 
                                    ControlToValidate="DniSocioTextBox" Display="Dynamic" CssClass="validator" runat="server" />
                                <asp:CustomValidator ID="DniUnicoValidator" ErrorMessage="El DNI ya se encuentra registrado." OnServerValidate="DniUnicoValidator_ServerValidate"
                                    ControlToValidate="DniSocioTextBox" Display="Dynamic" CssClass="validator" runat="server" />
                            </div>
                        </div>
                        <div class="d-flex">
                            <div class="col">
                                <div class="form-floating me-2 mb-1">
                                    <asp:TextBox ID="NombreSocioTextBox" PlaceHolder="Nombre" CssClass="form-control" runat="server" ></asp:TextBox>
                                    <label for="NombreSocioTextBox">Nombre<span class="text-danger">*</span></label>
                                </div>
                                <div class="ms-2 mb-3">
                                    <%-- Validators --%>
                                    <asp:RegularExpressionValidator ErrorMessage="El nombre solo puede contener letras." ValidationExpression="^[A-Za-zÁÉÍÓÚáéíóúÑñ\s]+$"
                                        ControlToValidate="NombreSocioTextBox" Display="Dynamic" CssClass="validator" runat="server" />
                                    <asp:RegularExpressionValidator ErrorMessage="El nombre puede tener un mínimo de 2 y un máximo de 100 caracteres." ValidationExpression="^.{2,100}$"
                                        ControlToValidate="NombreSocioTextBox" Display="Dynamic" CssClass="validator" runat="server" />
                                </div>
                            </div>
                            <div class="col">
                                <div class="form-floating ms-2 mb-1">
                                    <asp:TextBox ID="ApellidoSocioTextBox" PlaceHolder="Apellido" CssClass="form-control" runat="server" ></asp:TextBox>
                                    <label for="ApellidoSocioTextBox">Apellido<span class="text-danger">*</span></label>
                                </div>
                                <div class="ms-3 mb-3">
                                    <%-- Validators --%>
                                    <asp:RegularExpressionValidator ErrorMessage="El apellido solo puede contener letras." ValidationExpression="^[A-Za-zÁÉÍÓÚáéíóúÑñ\s]+$"
                                        ControlToValidate="ApellidoSocioTextBox" Display="Dynamic" CssClass="validator" runat="server" />
                                    <asp:RegularExpressionValidator ErrorMessage="El apellido puede tener un mínimo de 2 y un máximo de 100 caracteres." ValidationExpression="^.{2,100}$"
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
                                    <label for="TiposCuotasDropDownList" class="form-label">Tipo de cuota<span class="text-danger">*</span></label>
                                    <asp:DropDownList ID="TiposCuotasDropDownList" CssClass="form-select" runat="server"></asp:DropDownList>
                                </div>
                                <div class="ms-2 mb-3">
                                    <%-- Validators --%>
                                </div>
                            </div>
                            <div class="col">
                                <div class="d-flex">
                                    <div class="w-100 mx-2 mb-1">
                                        <label for="CantidadTextBox" class="form-label">Cantidad<span class="text-danger">*</span></label>
                                        <asp:TextBox ID="CantidadTextBox" PlaceHolder="Escriba la cantidad de días, semanas o meses (solo números)" CssClass="form-control" runat="server" ></asp:TextBox>
                                    </div>
                                    <div class="d-flex align-items-end mx-2 mb-1">
                                        <asp:Button ID="CalcularButton" Text="Calcular" CssClass="btn btn-primary" runat="server" />
                                    </div>
                                </div>
                                <div class="ms-3 mb-3">
                                    <%-- Validators --%>
                                    <asp:RequiredFieldValidator ID="CantidadRequiredValidator" ErrorMessage="La cantidad no puede estar vacía."
                                        ControlToValidate="CantidadTextBox" Display="Dynamic" CssClass="validator" runat="server" />
                                    <asp:RegularExpressionValidator ID="CantidadSoloNumerosValidator" ErrorMessage="La cantidad debe contener unicamente números."
                                        ValidationExpression="^\d+$" ControlToValidate="CantidadTextBox" Display="Dynamic" CssClass="validator" runat="server" />
                                    <asp:CustomValidator ID="MayorACeroValidator" ErrorMessage="La cantidad debe ser mayor a cero." OnServerValidate="MayorACeroValidator_ServerValidate"
                                        ControlToValidate="CantidadTextBox" Display="Dynamic" CssClass="validator" runat="server" />
                                </div>
                            </div>
                            <div class="col">
                                <div class="mx-2 mb-1">
                                    <label for="MontoAbonarTextBox" class="form-label">Monto a abonar</label>
                                    <div class="input-group">
                                      <span class="input-group-text">$</span>
                                      <asp:TextBox ID="MontoAbonarTextBox" ReadOnly="true" PlaceHolder="El monto depende del tipo de cuota y la cantidad" CssClass="form-control" runat="server" ></asp:TextBox>
                                    </div>
                                </div>
                                <div class="ms-3 mb-3">
                                    <asp:RequiredFieldValidator ErrorMessage="El monto debe ser calculado para poder registrar el socio." ControlToValidate="MontoAbonarTextBox"
                                        Display="Dynamic" CssClass="validator" runat="server" />
                                </div>
                            </div>
                        </div>
                        <div class="d-flex">
                            <div class="col">
                                <div class="me-2 mb-1">
                                    <label for="FechaPagoTextBox" class="form-label">Fecha de pago</label>
                                    <asp:TextBox ID="FechaPagoTextBox" ReadOnly="true" TextMode="Date" CssClass="form-control" runat="server" ></asp:TextBox>
                                </div>
                            </div>
                            <div class="col">
                                <div class="ms-2 mb-1">
                                    <label for="MesQueAbonaTextBox" class="form-label">Mes que abona</label>
                                    <asp:TextBox ID="MesQueAbonaTextBox" ReadOnly="true" CssClass="form-control" runat="server" />
                                </div>
                            </div>
                        </div>
                        <p class="form-text ms-1 mb-0">Los datos marcados con (<span class="text-danger">*</span>) son obligatorios para registrar un socio.</p>
                        <p class="form-text ms-1">Luego de ingresar la cantidad que corresponda, presione el botón calcular para obtener el monto a abonar.</p>
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