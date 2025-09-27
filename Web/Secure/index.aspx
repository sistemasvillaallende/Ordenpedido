<%@ Page Title="" Language="C#" MasterPageFile="~/MP/Bootstrap.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Web.Secure.findOpBootstrap" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container" style="padding-top: 10px;">
        <div class="box box-primary">
            <div class="box-header" style="height: 80px; padding: 10px;">
                <div class="col-md-6">
                    <h3>Buscar Orden de Pedido</h3>
                </div>
                <div class="col-md-6">
                    <div class="row">
                        <p class="pull-right">Oficina Origen:&nbsp; <span id="lblOffice" runat="server"></span></p>
                    </div>
                    <div class="row">
                        <p class="pull-right">Usuario:&nbsp; <span id="lblUsuario" runat="server"></span></p>
                    </div>


                </div>
            </div>
            <div class="box-body">
                <div style="padding: 10px;">
                    <a class="btn btn-primary" href="newOpBootstrap.aspx">Nueva O.P.</a>
                    <a class="btn btn-info" href="NewOpCC.aspx?opcion=sf">Nueva O.P. s/Fact.</a>
                    <a class="btn btn-warning" href="NewOpCC.aspx?opcion=cc">Nueva O.P. Caja chica</a>
                </div>
                <div style="padding: 10px;">
                    <br />
                    <asp:GridView ID="gvOrden" runat="server"
                        CssClass="table table-hover"
                        Style="margin-top: 1px" Width="100%"
                        DataKeyNames="nroOrden,nroOrdenCompra,codProveedor" GridLines="None"
                        AutoGenerateColumns="False"
                        OnRowDataBound="gvOrden_RowDataBound"
                        OnRowCommand="gvOrden_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="nroOrden" HeaderText="Nº Orden P."></asp:BoundField>
                            <asp:BoundField DataField="nroOrdenCompra" HeaderText="Nº Orden Cpra."></asp:BoundField>
                            <asp:BoundField DataField="formatFec" HeaderText="Fecha"></asp:BoundField>
                            <asp:BoundField DataField="proveedor" HeaderText="Proveedor"></asp:BoundField>
                            <asp:BoundField DataField="destino" HeaderText="Destino"></asp:BoundField>
                            <asp:BoundField DataField="usuario" HeaderText="Usuario"></asp:BoundField>
                            <asp:BoundField DataField="estado_op" HeaderText="Estado"></asp:BoundField>
                            <asp:BoundField DataField="total" HeaderText="Total"></asp:BoundField>
                           <%-- <asp:TemplateField HeaderText="Ver Estado" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnVer"
                                        CommandName="ver"
                                        CommandArgument="<%# Container.DataItemIndex %>"
                                        CausesValidation="false"
                                        runat="server">
                                        <span class="fa fa-cubes" style="font-size: 20px;"></span>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Edicion" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnEdit"
                                        CommandName="edit"
                                        CommandArgument="<%# Container.DataItemIndex %>"
                                        CausesValidation="false"
                                        runat="server">
                                        <span class="fa fa-edit" style="font-size: 20px;"></span>
                                    </asp:LinkButton>
                                    <a target="_blank" href="../Reportes/ReporteOrdenPedido.aspx?nroOrden=<%#Eval("nroOrden")%>">
                                        <span class="fa fa-print" style="font-size: 20px;"></span>
                                    </a>
                                    <%--<a target="_blank" href="../Reportes/Print.aspx?nroOrden=<%#Eval("nroOrden")%>">
                                        <span class="fa fa-print" style="font-size: 20px;"></span>
                                    </a>--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle BackColor="#428bca" ForeColor="White" />
                    </asp:GridView>
                </div>

            </div>
        </div>


        <!-- Modal Bootstrap -->
        <%--<div class="modal fade" id="modalVerOP" tabindex="-1" role="dialog" aria-labelledby="modalVerOPLabel">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title" id="modalVerOPLabel">Estados de la Orden</h4>
                    </div>
                    <div class="modal-body">
                        <asp:GridView ID="gvEstados" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="False">
                            <Columns>
                                <asp:BoundField DataField="nro_nota_pedido" HeaderText="Nro Orden" />
                                <asp:BoundField DataField="nro_paso" HeaderText="Paso" />
                                <asp:BoundField DataField="estado" HeaderText="Estado" />
                                <asp:BoundField DataField="fecha" HeaderText="Fecha" />
                                <asp:BoundField DataField="observaciones" HeaderText="Observaciones" />
                                <asp:BoundField DataField="responsable_op" HeaderText="Responsable" />
                                <asp:BoundField DataField="usuario" HeaderText="Usuario" />
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                    </div>
                </div>
            </div>
        </div>--%>
    </div>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ScriptsContent" runat="server">



    <link rel="stylesheet" href="https://cdn.datatables.net/1.13.7/css/jquery.dataTables.css" />
    <script src="https://cdn.datatables.net/1.13.7/js/jquery.dataTables.js"></script>
    <!-- DataTables -->
    <script src="../App_Themes/bower_components/datatables.net/js/jquery.dataTables.min.js"></script>
    <!-- iCheck -->
    <%--<script src="../App_Themes/plugins/iCheck/icheck.min.js"></script>
    <link rel="stylesheet" href="../App_Themes/plugins/iCheck/square/blue.css" />--%>

    <script type="text/javascript">
        $.noConflict();
        jQuery(document).ready(function ($) {
            var tabla = $('#' + '<%=gvOrden.ClientID %>').DataTable({
                "order": [[0, "desc"]],
                "language": {
                    "url": "//cdn.datatables.net/plug-ins/1.10.16/i18n/Spanish.json"
                }
            });


        });
    </script>


</asp:Content>
