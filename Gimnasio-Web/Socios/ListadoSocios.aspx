<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListadoSocios.aspx.cs" Inherits="Gimnasio_Web.Socios.ListadoSocios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager runat="server" />

    <asp:UpdatePanel class="position-fixed w-fill-available container-principal" runat="server">
        <ContentTemplate>
            <div class="border rounded mt-4 mx-5 mb-3 p-3">
                <div class="d-flex">
                    <asp:TextBox ID="CampoBusquedaTextBox" CssClass="form-control" runat="server" />
                    <button id="BuscarButton" type="button" onserverclick="BuscarButton_ServerClick" class="d-flex btn btn-primary py-1 ms-2" runat="server">
                        <i class="bi bi-search me-2"></i>
                        Buscar
                    </button>
                </div>
                <p class="form-text form-text mt-1 mb-0 ms-2">Búsqueda por Código de socio, DNI, nombre y/o apellido.</p>
            </div>

            <div class="border rounded m-5 mt-2 p-3 pb-5">
                <div>
                    <h2 class="text-center">Listado de socios</h2>
                    <hr class="mt-0" />
                </div>
                <div class="max-height-53-vh overflow-y-scroll table-responsive">
                    <asp:GridView ID="SociosGridView" DataKeyNames="Id" AutoGenerateColumns="false" CssClass="table table-bordered table-striped table-hover table-group-divider"
                        OnRowCommand="SociosGridView_RowCommand" runat="server">
                        <Columns>
                            <asp:TemplateField HeaderText="#" ItemStyle-Font-Bold="true" HeaderStyle-CssClass="table-dark">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Id" HeaderText="Código Socio" HeaderStyle-CssClass="table-dark" ItemStyle-VerticalAlign="Middle" />
                            <asp:BoundField DataField="Dni" HeaderText="DNI" HeaderStyle-CssClass="table-dark" ItemStyle-VerticalAlign="Middle" />
                            <asp:BoundField DataField="Nombre" HeaderText="Nombre" HeaderStyle-CssClass="table-dark" ItemStyle-VerticalAlign="Middle" />
                            <asp:BoundField DataField="Apellido" HeaderText="Apellido" HeaderStyle-CssClass="table-dark" ItemStyle-VerticalAlign="Middle"/>
                            <asp:CheckBoxField DataField="EstaActivo" HeaderText="Activo" HeaderStyle-CssClass="table-dark" ItemStyle-VerticalAlign="Middle"/>
                            <asp:ButtonField ButtonType="Link" HeaderText="Acción" HeaderStyle-CssClass="table-dark" AccessibleHeaderText="Acción"
                                            ItemStyle-CssClass="has-icon" Text="&#xF4CA;" ItemStyle-HorizontalAlign="center" CommandName="EditarSocio" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
