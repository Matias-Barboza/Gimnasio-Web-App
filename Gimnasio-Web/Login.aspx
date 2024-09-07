<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Gimnasio_Web.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
        <title>Gimnasio Web App</title>

        <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous"/>
        <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.min.css"/>
        <link href="Css/styles.css" rel="stylesheet" />
        <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" crossorigin="anonymous"></script>

    </head>
    <body>
        <form id="form1" runat="server">
            <div class="d-flex align-items-center vh-100">
                <div class="border shadow rounded w-25 mx-auto p-4">
                    <h2 class="text-center">INICIO DE SESIÓN</h2>
                    <hr />
                    <div class="form-floating mb-1">
                        <asp:TextBox ID="UsuarioTextBox" PlaceHolder="Usuario" CssClass="form-control" runat="server" ></asp:TextBox>
                        <label for="UsuarioTextBox">Usuario</label>
                    </div>
                    <div class="ms-2 mb-3">
                        <%-- Validator --%>
                        <asp:RequiredFieldValidator ID="UsuarioRequeridoValidator" ErrorMessage="El usuario no puede estar vacío." ControlToValidate="UsuarioTextBox"
                            Display="Dynamic" CssClass="validator" runat="server" />
                    </div>
                    <div class="form-floating mb-1">
                        <asp:TextBox ID="PasswordTextBox" TextMode="Password" PlaceHolder="Contraseña" CssClass="form-control" runat="server" ></asp:TextBox>
                        <label for="PasswordTextBox">Contraseña</label>
                    </div>
                    <div class="ms-2 mb-3">
                        <%-- Validators --%>
                        <asp:RequiredFieldValidator ID="PasswordRequiredValidator" ErrorMessage="La contraseña no puede estar vacía." ControlToValidate="PasswordTextBox"
                            Display="Dynamic" CssClass="validator" runat="server" />
                        <asp:CustomValidator ID="ExisteUsuarioValidator" ErrorMessage="El usuario no existe." OnServerValidate="ExisteUsuarioValidator_ServerValidate"
                            Display="Dynamic" CssClass="validator" runat="server" />
                        <asp:CustomValidator ID="DatosUsuarioValidator" ErrorMessage="El usuario o contraseña son incorrectos." OnServerValidate="DatosUsuarioValidator_ServerValidate" 
                            Display="Dynamic" CssClass="validator" runat="server" />
                    </div>
                    <hr />
                    <div class="d-flex flex-column justify-content-center mb-0">
                        <asp:Button ID="IngresarButton" Text="INGRESAR" OnClick="IngresarButton_Click" CssClass="btn btn-primary btn-lg rounded-pill mb-2" runat="server" />
                        <p class="text-center mb-2">¿Problemas con las credenciales? Consulte con su administrador.</p>
                    </div>
                </div>
            </div>
        </form>
    </body>
</html>
