<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormularioUsuario.aspx.cs" Inherits="Gimnasio_Web.Usuarios.FormularioUsuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="border rounded h-fill-available m-5 mt-4 p-3">
        <div>
            <h2 class="text-center">Formulario de registro de usuario</h2>
            <hr class="mt-0" />
        </div>
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div class="vh-65">
                    <div class="border rounded mx-2 mb-4 p-3">
                        <h4>Datos del usuario</h4>
                        <hr class="mt-0" />
                        <div class="d-flex mt-1 mb-2">
                            <div class="col mb-1">
                                <label for="NombreTextBox" class="ms-1 mb-1">Nombre<span class="text-danger">*</span></label>
                                <asp:TextBox ID="NombreTextBox" CssClass="form-control" runat="server"></asp:TextBox>
                                <div class="ms-2">
                                    <%-- Validators --%>
                                    <asp:RequiredFieldValidator ErrorMessage="El nombre es obligatorio." ControlToValidate="NombreTextBox"
                                        ValidationGroup="OperacionValidationGroup" Display="Dynamic" CssClass="validator" runat="server" />
                                    <asp:RegularExpressionValidator ErrorMessage="El nombre solo puede contener letras y un máximo de 50 caracteres."
                                        ValidationExpression="^[A-Za-zÁÉÍÓÚáéíóúÑñ\s]{1,50}$" ValidationGroup="OperacionValidationGroup"
                                        Display="Dynamic" ControlToValidate="NombreTextBox" CssClass="validator" runat="server" />
                                </div>
                            </div>
                            <div class="col ms-2 mb-1">
                                <label for="ApellidoTextBox" class="ms-1 mb-1">Apellido<span class="text-danger">*</span></label>
                                <asp:TextBox ID="ApellidoTextBox" CssClass="form-control" runat="server"></asp:TextBox>
                                <div class="ms-2">
                                    <%-- Validators --%>
                                    <asp:RequiredFieldValidator ErrorMessage="El apellido es obligatorio." ControlToValidate="ApellidoTextBox"
                                        ValidationGroup="OperacionValidationGroup" Display="Dynamic" CssClass="validator" runat="server" />
                                    <asp:RegularExpressionValidator ErrorMessage="El apellido solo puede contener letras y un máximo de 50 caracteres."
                                        ValidationExpression="^[A-Za-zÁÉÍÓÚáéíóúÑñ\s]{1,50}$" ValidationGroup="OperacionValidationGroup"
                                        Display="Dynamic" ControlToValidate="ApellidoTextBox" CssClass="validator" runat="server" />
                                </div>
                            </div>
                        </div>

                        <div class="mb-2">
                            <div class="d-flex">
                                <div class="col me-2">
                                    <label for="NombreUsuarioTextBox" class="ms-1 mb-1">Nombre de usuario<span class="text-danger">*</span></label>
                                    <asp:TextBox ID="NombreUsuarioTextBox" PlaceHolder="Ej: joseSchulz" CssClass="form-control" runat="server"></asp:TextBox>
                                    <div class="ms-2">
                                        <%-- Validators --%>
                                        <asp:RequiredFieldValidator ID="NombreUsuarioObligatorioValidator"
                                            ErrorMessage="El nombre de usuario es obligatorio." ControlToValidate="NombreUsuarioTextBox"
                                            ValidationGroup="OperacionValidationGroup" Display="Dynamic" CssClass="validator" runat="server" />
                                        <asp:RegularExpressionValidator ID="NombreUsuarioExpresionValidator" 
                                            ErrorMessage="El nombre de usuario no puede letras con tildes, espacios y debe tener un mínimo de 4 y un máximo de 50 caracteres."
                                            ValidationExpression="^[a-zA-Z0-9]{4,50}$" ValidationGroup="OperacionValidationGroup"
                                            Display="Dynamic" ControlToValidate="NombreUsuarioTextBox" CssClass="validator" runat="server" />
                                        <asp:CustomValidator ID="NombreUsuarioExistenteValidator" ErrorMessage="El nombre de usuario ya está usado."
                                            ControlToValidate="NombreUsuarioTextBox" OnServerValidate="NombreUsuarioExistenteValidator_ServerValidate" 
                                            ValidationGroup="OperacionValidationGroup" Display="Dynamic" CssClass="validator" runat="server" />
                                    </div>
                                </div>
                                <div class="col">
                                    <label for="PasswordTextBox" class="ms-1 mb-1">Contraseña<span class="text-danger">*</span></label>
                                    <div class="d-flex align-items-center">
                                        <div class="col">
                                            <asp:TextBox ID="PasswordTextBox" ReadOnly="true" PlaceHolder="Contraseña" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <div>
                                            <asp:Button ID="GenerarPasswordButton" Text="Generar contraseña aleatoria"
                                                OnClick="GenerarPasswordButton_Click" CssClass="btn btn-primary ms-2" runat="server" />
                                        </div>
                                    </div>
                                    <div class="ms-2">
                                        <%-- Validators --%>
                                        <asp:CustomValidator ID="ContrasenhaGeneradaValidator" ErrorMessage="La contraseña aleatoria debe ser generada."
                                            ControlToValidate="PasswordTextBox" OnServerValidate="ContrasenhaGeneradaValidator_ServerValidate"
                                            ValidationGroup="OperacionValidationGroup" ValidateEmptyText="true" Display="Dynamic" CssClass="validator" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="mb-2">
                            <div class="row">
                                <div class="col mb-3">
                                    <label for="EsAdminCheckBox" class="ms-1 mb-1">¿Usuario administrador?<span class="text-danger">*</span></label>
                                    <div class="d-flex justify-content-center">
                                        <input id="EsAdminCheckBox" type="checkbox" class="form-check-input checkbox-xl position-relative top-12-px mt-0" runat="server" />
                                    </div>
                                </div>
                                <div class="col mb-3">
                                    <label for="EsProfesorCheckBox" class="ms-1 mb-1">¿Usuario profesor?<span class="text-danger">*</span></label>
                                    <div class="d-flex justify-content-center">
                                        <input id="EsProfesorCheckBox" type="checkbox" checked="checked" class="form-check-input checkbox-xl position-relative top-12-px mt-0" runat="server" />
                                    </div>
                                </div>
                            </div>
                            <div class="mt-1 ms-2">
                                <%-- Validators --%>
                                <asp:CustomValidator ID="TipoUsuarioValidator" 
                                    ErrorMessage="Al menos uno de los tipos de usuario debe estar seleccionado." ValidationGroup="OperacionValidationGroup"
                                    OnServerValidate="TipoUsuarioValidator_ServerValidate" Display="Dynamic" CssClass="validator" runat="server" />
                            </div>
                        </div>

                        <p class="form-text ms-1 mb-0">Los datos marcados con (<span class="text-danger">*</span>) son obligatorios para registrar un profesor.</p>
                        <p class="form-text mt-0 ms-1 mb-0">Recuerde la contraseña para informar al nuevo usuario la misma.</p>
                    </div>

                    <div class="d-flex justify-content-center align-items-center">
                        <asp:Button ID="RegistrarProfesorButton" OnClick="RegistrarProfesorButton_Click" Text="Registrar" CausesValidation="true"
                            ValidationGroup="OperacionValidationGroup" CssClass="btn btn-primary btn-lg" runat="server" />
                        <%--<a href="/Profesores/ListadoProfesores.aspx" class="btn btn-danger btn-lg ms-1">Cancelar</a>--%>
                    </div>

                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

    </div>

</asp:Content>
