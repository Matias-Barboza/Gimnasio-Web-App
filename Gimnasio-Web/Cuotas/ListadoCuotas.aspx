<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListadoCuotas.aspx.cs" Inherits="Gimnasio_Web.Cuotas.ListadoCuotas" %>
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
                <p class="form-text form-text mt-1 mb-0 ms-2">Búsqueda por código de cuota, tipo de cuota, fecha de pago, fecha de vencimiento, mes abonado, monto abonado o código de socio.</p>
                <p class="form-text form-text mt-0 mb-0 ms-2">Formato de fecha: (dd/MM/aaaa), (dd-MM-aaaa).</p>
            </div>

            <div class="border rounded mt-2 mx-5 mb-3 p-3">
                <div>
                    <h2 id="TituloListado" class="text-center" runat="server">Listado de cuotas</h2>
                    <hr class="mt-0" />
                </div>
                <div class="max-min-height-40-vh overflow-y-scroll table-responsive border rounded">
                    <asp:GridView ID="CuotasGridView" DataKeyNames="Id" AutoGenerateColumns="false" OnRowCommand="CuotasGridView_RowCommand"
                        CssClass="table table-bordered table-striped table-hover table-group-divider" HeaderStyle-CssClass="table-dark" runat="server">
                        <Columns>
                            <asp:TemplateField HeaderText="#" ItemStyle-Font-Bold="true">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Id" HeaderText="Código de cuota" ItemStyle-VerticalAlign="Middle" />
                            <asp:BoundField DataField="FechaPago" HeaderText="Fecha de pago" ItemStyle-VerticalAlign="Middle" DataFormatString="{0:d}" />
                            <asp:BoundField DataField="FechaVencimiento" HeaderText="Fecha de vencimiento" ItemStyle-VerticalAlign="Middle" DataFormatString="{0:d}" />
                            <asp:BoundField DataField="MesQueAbona" HeaderText="Mes abonado" ItemStyle-VerticalAlign="Middle" />
                            <asp:BoundField DataField="TipoCuota.Descripcion" HeaderText="Tipo de cuota" ItemStyle-VerticalAlign="Middle" />
                            <asp:BoundField DataField="MontoAbonado" HeaderText="Monto abonado" ItemStyle-VerticalAlign="Middle" DataFormatString="{0:C2}" />
                            <asp:BoundField DataField="Socio.Id" HeaderText="Código de socio" ItemStyle-VerticalAlign="Middle" />
                            <asp:ButtonField ButtonType="Button" Text="&#xF341;" HeaderText="Ver socio" AccessibleHeaderText="Ver socio" ItemStyle-HorizontalAlign="center"
                                CommandName="VerSocio" ControlStyle-CssClass="btn btn-primary fs-5 font-family-bootstrap-icons py-0 px-1" />
                            <asp:ButtonField ButtonType="Button" Text="&#xF4CA;" HeaderText="Editar cuota" AccessibleHeaderText="Editar cuota" ItemStyle-HorizontalAlign="center"
                                CommandName="EditarCuota" ControlStyle-CssClass="btn btn-warning text-white fs-5 font-family-bootstrap-icons py-0 px-1" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="border rounded m-5 mt-2 p-3">
                <div>
                    <h4 class="text-start">Socio relacionado a la cuota</h4>
                    <hr class="mt-0" />
                </div>
                <div class="d-flex">
                    <div class="col form-floating mx-2 mb-2">
                        <asp:TextBox ID="CodigoSocioTextBox" PlaceHolder="Código de socio" CssClass="form-control" runat="server" ></asp:TextBox>
                        <label for="ApellidoSocioTextBox">Código de socio</label>
                    </div>
                    <div class="col form-floating mx-2 mb-2">
                        <asp:TextBox ID="NombreSocioTextBox" PlaceHolder="Nombre" CssClass="form-control" runat="server" ></asp:TextBox>
                        <label for="NombreSocioTextBox">Nombre</label>
                    </div>
                    <div class="col form-floating ms-2 mb-2">
                        <asp:TextBox ID="ApellidoSocioTextBox" PlaceHolder="Apellido" CssClass="form-control" runat="server" ></asp:TextBox>
                        <label for="ApellidoSocioTextBox">Apellido</label>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
