<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormularioTipoCuota.aspx.cs" Inherits="Gimnasio_Web.Cuotas.FormularioTipoCuota" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="border rounded h-fill-available m-5 mt-4 p-3">
        <div>
            <h2 id="TituloPagina" class="text-center" runat="server">Formulario de tipo de cuota</h2>
            <hr class="mt-0" />
        </div>
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div class="vh-65">
                    <div class="border rounded mx-2 mb-4 p-3 pb-5">
                        <h4>Código de tipo de cuota</h4>
                        <hr class="mt-0" />
                        <div class="mb-3">
                            <asp:TextBox ID="CodigoTipoCuotaTextBox" ReadOnly="true" PlaceHolder="Autogenerado" CssClass="form-control form-control-lg" runat="server"></asp:TextBox>
                        </div>
                        <h4>Datos del tipo de cuota</h4>
                        <hr class="mt-0" />
                        <div class="d-flex">
                            <div class="col mb-1">
                                <label for="DescripcionTextBox" class="form-label">Descripción</label>
                                <asp:TextBox ID="DescripcionTextBox" PlaceHolder="Ej: Diaria" CssClass="form-control" runat="server"></asp:TextBox>
                                <div>
                                    <%-- Validators --%>
                                    <asp:RequiredFieldValidator ErrorMessage="La descripción no puede estar vacía." ControlToValidate="DescripcionTextBox"
                                        ValidationGroup="OperacionValidationGroup" Display="Dynamic" CssClass="validator ms-3 mb-3" runat="server" />
                                </div>
                            </div>
                            <div class="col mx-2 mb-1">
                                <label for="CantidadDiasTextBox" class="form-label">Cantidad de días</label>
                                <asp:TextBox ID="CantidadDiasTextBox" PlaceHolder="Días que dura la membresía" CssClass="form-control" runat="server"></asp:TextBox>
                                <div>
                                    <%-- Validators --%>
                                    <asp:RequiredFieldValidator ID="CantidadRequiredValidator" ErrorMessage="La cantidad no puede estar vacía."
                                        ControlToValidate="CantidadDiasTextBox" ValidationGroup="OperacionValidationGroup" Display="Dynamic" CssClass="validator ms-3 mb-3" runat="server" />
                                    <asp:RegularExpressionValidator ID="CantidadSoloNumerosValidator" ErrorMessage="La cantidad debe contener unicamente números."
                                        ValidationExpression="^\d+$" ControlToValidate="CantidadDiasTextBox" ValidationGroup="OperacionValidationGroup" Display="Dynamic" CssClass="validator ms-3 mb-3" runat="server" />
                                    <asp:CustomValidator ID="MayorACeroValidator" ErrorMessage="La cantidad debe ser mayor a cero." OnServerValidate="MayorACeroValidator_ServerValidate"
                                        ControlToValidate="CantidadDiasTextBox" ValidationGroup="OperacionValidationGroup" Display="Dynamic" CssClass="validator ms-3 mb-3" runat="server" />
                                </div>
                            </div>
                            <div class="col mx-2 mb-1">
                                <label for="MontoTextBox" class="form-label">Monto actual</label>
                                <div class="input-group">
                                    <span class="input-group-text">$</span>
                                    <asp:TextBox ID="MontoTextBox" PlaceHolder="Ej: 2000.00" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div>
                                    <%-- Validators --%>
                                    <asp:RequiredFieldValidator ID="MontoRequiredFieldValidator" ErrorMessage="El monto no puede estar vacío."
                                        ControlToValidate="MontoTextBox" ValidationGroup="OperacionValidationGroup" Display="Dynamic" CssClass="validator ms-3 mb-3" runat="server" />
                                    <asp:CustomValidator ID="MontoCustomValidator" ErrorMessage="El monto debe ser un número válido mayor a 0." OnServerValidate="MontoCustomValidator_ServerValidate"
                                        ControlToValidate="MontoTextBox" ValidationGroup="OperacionValidationGroup" Display="Dynamic" CssClass="validator ms-3 mb-3" runat="server" />
                                </div>
                            </div>
                        </div>
                        <%if (EsEdicion) 
                            {%>
                        <p class="form-text form-text my-0 ms-1">Únicamente se puede editar el valor del tipo de cuota.</p>
                        <%}%>
                    </div>

                    <div class="d-flex justify-content-center align-items-center">
                        <%if (EsEdicion)
                            {%>
                        <asp:Button ID="EditarTipoCuotaButton" Text="Editar" OnClick="EditarTipoCuotaButton_Click" CausesValidation="true"
                            ValidationGroup="OperacionValidationGroup" CssClass="btn btn-primary btn-lg" runat="server" />
                        <%}
                          else
                            {%>
                        <asp:Button ID="AñadirTipoCuotaButton" Text="Añadir" OnClick="AñadirTipoCuotaButton_Click" CausesValidation="true"
                            ValidationGroup="OperacionValidationGroup" CssClass="btn btn-primary btn-lg" runat="server" />
                        <%}%>
                        <a href="/Cuotas/ListadoTiposCuota.aspx" class="btn btn-danger btn-lg ms-1">Cancelar</a>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

    </div>

</asp:Content>
