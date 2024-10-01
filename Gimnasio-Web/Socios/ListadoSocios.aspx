<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListadoSocios.aspx.cs" Inherits="Gimnasio_Web.Socios.ListadoSocios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:UpdatePanel class="position-fixed w-fill-available container-principal" runat="server">
        <ContentTemplate>
            <div class="border rounded mt-4 mx-5 mb-3 p-3">
                <div class="d-flex">
                    <asp:TextBox ID="CampoBusquedaTextBox" CssClass="form-control me-1" runat="server" />
                    <%if (MostrarResultadoBusqueda)
                      {%>
                    <label id="ResultadoBusquedaLabel" class="badge text-bg-secondary d-flex align-items-center" runat="server"></label>
                    <%}%>
                    <button id="BuscarButton" type="button" onserverclick="BuscarButton_ServerClick" class="d-flex btn btn-primary py-1 ms-1" runat="server">
                        <i class="bi bi-search me-2"></i>
                        Buscar
                    </button>
                </div>
                <p class="form-text form-text mt-1 mb-0 ms-2">Búsqueda por código de socio, DNI o nombre y/o apellido.</p>
            </div>

            <div class="border rounded m-5 mt-2 p-3 pb-5">
                <div>
                    <h2 id="TituloListado" class="text-center" runat="server">Listado de socios</h2>
                    <hr class="mt-0" />
                </div>
                <div class="max-min-height-53-vh overflow-y-scroll table-responsive border rounded">
                    <asp:GridView ID="SociosGridView" DataKeyNames="Id" AutoGenerateColumns="false" OnRowCommand="SociosGridView_RowCommand"
                        CssClass="table table-bordered table-striped table-hover table-group-divider" HeaderStyle-CssClass="table-dark" runat="server">
                        <Columns>
                            <asp:TemplateField HeaderText="#" ItemStyle-Font-Bold="true">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Id" HeaderText="Código Socio" ItemStyle-VerticalAlign="Middle" />
                            <asp:BoundField DataField="Dni" HeaderText="DNI" ItemStyle-VerticalAlign="Middle" />
                            <asp:BoundField DataField="Nombre" HeaderText="Nombre" ItemStyle-VerticalAlign="Middle" />
                            <asp:BoundField DataField="Apellido" HeaderText="Apellido" ItemStyle-VerticalAlign="Middle"/>
                            <asp:TemplateField HeaderText="Activo" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="center">
                                <ItemTemplate>
                                    <input type="checkbox" class="form-check-input" checked='<%#Eval("EstaActivo") %>' disabled="disabled" runat="server"/>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Pasar a activo/inactivo" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="center">
                                <ItemTemplate>
                                    <button type="button" class="btn btn-primary fs-5 py-0 px-1" data-bs-toggle="modal" data-bs-target="#cambiarEstadoSocioModal"
                                        onclick="VincularDatosSocioAModal('<%#Eval("Apellido") %>', '<%#Eval("Nombre") %>', <%#Eval("Id") %>, '<%#Eval("EstaActivo") %>')">
                                        <i class="bi bi-eye"></i>
                                    </button>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:ButtonField ButtonType="Button" Text="&#xF4CA;" HeaderText="Editar socio" AccessibleHeaderText="Editar socio" ItemStyle-HorizontalAlign="center"
                                CommandName="EditarSocio" ControlStyle-CssClass="btn btn-warning text-white fs-5 font-family-bootstrap-icons py-0 px-1" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
