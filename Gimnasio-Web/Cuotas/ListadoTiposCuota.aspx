<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListadoTiposCuota.aspx.cs" Inherits="Gimnasio_Web.Cuotas.ListadosTiposCuota" %>
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
                <p class="form-text form-text mt-1 mb-0 ms-2">Búsqueda por código de tipo, descripción o valor actual.</p>
            </div>

            <div class="border rounded m-5 mt-2 p-3 pb-5">
                <div>
                    <h2 id="TituloListado" class="text-center" runat="server">Listado de tipos de cuota</h2>
                    <hr class="mt-0" />
                </div>
                <div class="max-min-height-53-vh overflow-y-scroll table-responsive border rounded">
                    <asp:GridView ID="TiposCuotasGridView" DataKeyNames="Id" AutoGenerateColumns="false" OnRowCommand="TiposCuotasGridView_RowCommand"
                        CssClass="table table-bordered table-striped table-hover table-group-divider" HeaderStyle-CssClass="table-dark" runat="server">
                        <Columns>
                            <asp:TemplateField HeaderText="#" ItemStyle-Font-Bold="true">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Id" HeaderText="Código de tipo de cuota" ItemStyle-VerticalAlign="Middle" />
                            <asp:BoundField DataField="Descripcion" HeaderText="Descripción" ItemStyle-VerticalAlign="Middle"/>
                            <asp:BoundField DataField="Valor" HeaderText="Valor actual" ItemStyle-VerticalAlign="Middle" DataFormatString="{0:C2}" />
                            <asp:TemplateField HeaderText="Activo" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="center">
                                <ItemTemplate>
                                    <input type="checkbox" class="form-check-input" checked='<%#Eval("Visible") %>' disabled="disabled" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:ButtonField ButtonType="Button" Text="&#xF341;" HeaderText="Ver historial de actualizaciones" AccessibleHeaderText="Ver historial de actualizaciones" ItemStyle-HorizontalAlign="center"
                                CommandName="VerHistorial" ControlStyle-CssClass="btn btn-primary fs-5 font-family-bootstrap-icons py-0 px-1" />
                            <asp:ButtonField ButtonType="Button" Text="&#xF4CA;" HeaderText="Editar tipo de cuota" AccessibleHeaderText="Editar tipo de cuota" ItemStyle-HorizontalAlign="center"
                                CommandName="EditarTipoCuota" ControlStyle-CssClass="btn btn-warning text-white fs-5 font-family-bootstrap-icons py-0 px-1" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
