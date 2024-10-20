<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HistorialTipoCuota.aspx.cs" Inherits="Gimnasio_Web.Cuotas.HistorialTipoCuota" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="border rounded h-fill-available m-5 mt-4 p-3">
    <div>
        <h2 class="text-center">Historial de tipo de cuota</h2>
        <hr class="mt-0" />
    </div>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="overflow-y-scroll vh-65">
                <div class="border rounded mx-2 mb-4 p-3">
                    <h4>Datos del tipo de cuota</h4>
                    <hr class="mt-0" />
                    <div class="mb-2">
                        <label for="CodigoTipoCuotaTextBox" class="form-label">Código tipo de cuota</label>
                        <asp:TextBox ID="CodigoTipoCuotaTextBox" ReadOnly="true" PlaceHolder="Ej: 1" CssClass="form-control form-control-lg" runat="server"></asp:TextBox>
                    </div>
                    <div class="d-flex">
                        <div class="col mb-1">
                            <label for="DescripcionTextBox" class="form-label">Descripción</label>
                            <asp:TextBox ID="DescripcionTextBox" ReadOnly="true" PlaceHolder="Ej: Diaria" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col mx-2 mb-1">
                            <label for="MontoActualTextBox" class="form-label">Monto actual</label>
                            <div class="input-group">
                                <span class="input-group-text">$</span>
                                <asp:TextBox ID="MontoActualTextBox" ReadOnly="true" PlaceHolder="Ej: 2000.00" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="border rounded my-4 mx-2 p-3 pb-5">
                    <div>
                        <h2 class="text-center">Listado de movimientos sobre valor de cuota</h2>
                        <hr class="mt-0" />
                    </div>
                    <%if (NoHayAuditorias)
                        {%>
                    <div class="bg-body-secondary border rounded p-3">
                        <p class="fs-5 fw-semibold mb-0">No se encontraron auditorias.</p>
                    </div>
                    <%}
                    else
                    {%>
                    <%-- Historial tipo de cuota --%>
                    <div class="max-min-height-53-vh overflow-y-scroll table-responsive border rounded">
                        <asp:GridView ID="HistorialTipoCuotaGridView" AutoGenerateColumns="false"
                            CssClass="table table-bordered table-striped table-hover table-group-divider" HeaderStyle-CssClass="table-dark" runat="server">
                            <Columns>
                                <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Id" Visible="false"/>
                                <asp:BoundField DataField="TipoCuota.Descripcion" HeaderText="Tipo de cuota" ItemStyle-VerticalAlign="Middle"/>
                                <asp:BoundField DataField="TipoCuota.Valor" HeaderText="Valor viejo" ItemStyle-VerticalAlign="Middle" DataFormatString="{0:C2}"/>
                                <asp:BoundField DataField="NuevoValor" HeaderText="Valor nuevo" ItemStyle-VerticalAlign="Middle" DataFormatString="{0:C2}"/>
                                <asp:TemplateField HeaderText="Descripción">
                                    <ItemTemplate>
                                        <%#$"El cambio fué realizado por  el usuario '{Eval("Usuario.NombreUsuario")}' el {Eval("FechaCambio")}." %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <%}%>
                </div>
                
                <div class="d-flex justify-content-center align-items-center">
                    <a href="/Cuotas/ListadoTiposCuota.aspx" class="btn btn-primary btn-lg ms-1">Volver al listado</a>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</div>

</asp:Content>
