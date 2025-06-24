<%@ Page Title="" Language="C#" MasterPageFile="~/MP/Bootstrap.Master" AutoEventWireup="true" CodeBehind="AddFactura.aspx.cs" Inherits="Web.Secure.AddFactura" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .arrowAzul li, ul ul.arrowAzul li, ul li ul.arrowAzul li {
            list-style: disclosure-closed;
            color: #6e9d4d;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="container">
        <div class="row" style="margin-top: 50px;">
            <div class="col-md-10 col-md-offset-1">
                <div class="box box-info" style="margin-top: 20px;" runat="server" id="divConsulta">
                    <div class="box-header with-border">
                        <div class="row">
                            <div class="col-md-10">
                                <h2 style="color: #367fa9;"><span class="fa fa-info-circle"></span>&nbsp; Agregar Factura</h2>
                            </div>
                            <div class="col-md-2" style="padding-top: 20px;">
                                <a href="#modalBarCode" data-toggle="modal" style="font-size: 36px; width: 100%;">
                                    <span class="fa fa-barcode pull-right"></span>
                                </a>
                            </div>
                        </div>
                    </div>
                    <div class="box-body">
                        <div id="divConsultaRequest" runat="server">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Número de CUIT</label>
                                        <asp:TextBox ID="txtCUIT" CssClass="form-control"
                                            placeholder="Ingrese el Nro de CUIT del emisor del comprobante"
                                            runat="server"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator Display="Dynamic" ValidationGroup="consulta" ID="RV1" ForeColor="Red"
                                        runat="server"
                                        ErrorMessage="Ingrese el número de CUIT del emisor del comprobante"
                                        ControlToValidate="txtCUIT"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Fecha de Emisión del Comprobante</label>
                                        <asp:TextBox ID="txtFechaEmision" CssClass="form-control" TextMode="Date"
                                            placeholder="Ingrese la Fecha de emisión del comprobante"
                                            runat="server"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator Display="Dynamic" ValidationGroup="consulta" ID="rv2" ForeColor="Red"
                                        runat="server"
                                        ErrorMessage="Ingrese la Fecha de emisión del comprobante"
                                        ControlToValidate="txtFechaEmision"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Punto de Venta</label>
                                        <asp:TextBox ID="txtPuntoVenta" CssClass="form-control" placeholder="Ingrese el punto de venta"
                                            runat="server"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator Display="Dynamic" ValidationGroup="consulta" ID="rv3" ForeColor="Red"
                                        runat="server"
                                        ErrorMessage="Ingrese el punto de venta"
                                        ControlToValidate="txtPuntoVenta"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Número de Comprobante</label>
                                        <asp:TextBox ID="txtNumeroComprobante" CssClass="form-control"
                                            placeholder="Ingrese el número de comprobante"
                                            runat="server"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator Display="Dynamic" ValidationGroup="consulta" ID="rv4" ForeColor="Red"
                                        runat="server"
                                        ErrorMessage="Ingrese el número de comprobante"
                                        ControlToValidate="txtNumeroComprobante"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Número de CAE:</label>
                                        <asp:TextBox ID="txtCAE" CssClass="form-control" placeholder="Ingrese el Número de CAE"
                                            runat="server"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator Display="Dynamic" ValidationGroup="consulta" ID="rv5" ForeColor="Red"
                                        runat="server"
                                        ErrorMessage="Ingrese el Número de CAE"
                                        ControlToValidate="txtCAE"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Importe Total de la operación</label>
                                        <asp:TextBox ID="txtImporte" CssClass="form-control" placeholder="Importe Total de la operación"
                                            runat="server"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator Display="Dynamic" ValidationGroup="consulta" ID="rv6" ForeColor="Red"
                                        runat="server"
                                        ErrorMessage="Importe Total de la operación"
                                        ControlToValidate="txtImporte"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>Tipo de Comprobante:</label>
                                        <asp:DropDownList ID="DDLTipoComprobante" CssClass="form-control" runat="server">
                                            <asp:ListItem Value="1" Text="1 - Factura A"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="2 - Nota de Débito A"></asp:ListItem>
                                            <asp:ListItem Value="3" Text="3 - Nota de Crédito A"></asp:ListItem>
                                            <asp:ListItem Value="4" Text="4 - Recibo A"></asp:ListItem>
                                            <asp:ListItem Value="5" Text="5 - Nota de Venta al Contado A"></asp:ListItem>
                                            <asp:ListItem Value="6" Text="6 - Factura B"></asp:ListItem>
                                            <asp:ListItem Value="7" Text="7 - Nota de Débito B"></asp:ListItem>
                                            <asp:ListItem Value="8" Text="8 - Nota de Crédito B"></asp:ListItem>
                                            <asp:ListItem Value="9" Text="9 - Recibo B"></asp:ListItem>
                                            <asp:ListItem Value="10" Text="10 - Nota de Venta al Contado B"></asp:ListItem>
                                            <asp:ListItem Value="11" Text="11 - Factura C"></asp:ListItem>
                                            <asp:ListItem Value="12" Text="12 - Nota de Débito C"></asp:ListItem>
                                            <asp:ListItem Value="13" Text="13 - Nota de Crédito C"></asp:ListItem>
                                            <asp:ListItem Value="15" Text="15 - Recibo C"></asp:ListItem>
                                            <asp:ListItem Value="19" Text="19 - Factura de Exportación"></asp:ListItem>
                                            <asp:ListItem Value="20" Text="20 - Nota Déb. P/Operac. con el Exterior"></asp:ListItem>
                                            <asp:ListItem Value="21" Text="21 - Nota Créd. P/Operac. con el Exterior"></asp:ListItem>
                                            <asp:ListItem Value="39" Text="39 - Otros Comprobantes A que Cumplan con la R.G. Nro. 1415"></asp:ListItem>
                                            <asp:ListItem Value="40" Text="40 - Otros Comprobantes B que Cumplan con la R.G. Nro. 1415"></asp:ListItem>
                                            <asp:ListItem Value="49" Text="49 - Comprobante de Compra de Bienes Usados"></asp:ListItem>
                                            <asp:ListItem Value="51" Text="51 - Factura M"></asp:ListItem>
                                            <asp:ListItem Value="52" Text="52 - Nota de Débito M"></asp:ListItem>
                                            <asp:ListItem Value="53" Text="53 - Nota de Crédito M"></asp:ListItem>
                                            <asp:ListItem Value="54" Text="54 - Recibo M"></asp:ListItem>
                                            <asp:ListItem Value="60" Text="60 - Cta. de Vta. y Líquido Prod. A"></asp:ListItem>
                                            <asp:ListItem Value="61" Text="61 - Cta. de Vta. y Líquido Prod. B"></asp:ListItem>
                                            <asp:ListItem Value="63" Text="63 - Liquidación A"></asp:ListItem>
                                            <asp:ListItem Value="64" Text="64 - Liquidación B"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="divConsultaResponse" runat="server" visible="false">
                            <div class="row" style="border: 1px solid #6E9C4E; padding: 10px;">
                                <div class="col-md-2">
                                    <img src="../img/verify.png" class="icon48 padding-10" />
                                </div>
                                <div class="col-md-10">
                                    <h3>Los datos ingresados coinciden con una autorización otorgada por la AFIP.
                                                <span style="font-size: 16px;" id="lblactura" runat="server"></span>
                                    </h3>
                                </div>
                            </div>
                            <div class="row" style="margin-top: 20px;" id="divDetalle" runat="server">
                                <div class="col-md-12">
                                    <div style="border: 1px solid #6E9C4E; padding: 10px; text-align: right;">
                                        <!-- ////////////////////////////// DETALLE ////////////////////////////////////////////////// -->
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <h4>Detalle</h4>
                                            </div>
                                            <div class="panel-body">
                                                <!-- ////////////////////////////// BOTONES ////////////////////////////////////////////// -->
                                                <div class="form-group">
                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                        <ContentTemplate>
                                                            <div class="btn-group">
                                                                <%--<button type="button" class="btn btn-primary"
                                    runat="server" id="btnAddDetalle" onserverclick="btnAddDetalle_click">
                                    <span class="glyphicon glyphicon-plus-sign">Agregar
                                </button>--%>
                                                                <asp:LinkButton ID="lbtnAddDetalle" CssClass="btn btn-default" runat="server" OnClick="lbtnAddDetalle_Click">
                                            <i class="fa fa-plus"></i> Agregar Detalle
                                                                </asp:LinkButton>
                                                            </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <!-- ///////////////////////////////////////////////////////////////////////////////////// -->
                                                <!-- ////////////////////////////// GRILLA DETALLE /////////////////////////////////////// -->
                                                <asp:UpdatePanel ID="uPanelDetalle" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:GridView ID="gvDetalle" runat="server" AutoGenerateColumns="False" Width="100%"
                                                            EmptyDataText="No hay detalle agregado en la Orden de Pedido"
                                                            OnRowCommand="gvDetalle_RowCommand1" GridLines="None"
                                                            OnRowCreated="gvDetalle_RowCreated1">
                                                            <Columns>
                                                                <asp:BoundField HeaderText="Descripcion Pedido" DataField="descItems">
                                                                    <ControlStyle Width="400px" />
                                                                    <HeaderStyle HorizontalAlign="Left" BackColor="#d9edf7" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="cant" HeaderText=" Cantidad">
                                                                    <HeaderStyle BackColor="#d9edf7" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="precio" HeaderText=" Precio"
                                                                    DataFormatString="{0:C}">
                                                                    <HeaderStyle BackColor="#d9edf7" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="importe" HeaderText=" Importe"
                                                                    DataFormatString="{0:C}">
                                                                    <HeaderStyle BackColor="#d9edf7" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="Accion">
                                                                    <HeaderStyle BackColor="#d9edf7" />
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="imgbDelete" runat="server" CommandName="deleterow"
                                                                            ImageUrl="~/App_Themes/Tema1/Images/delete.gif"
                                                                            OnClientClick="return confirm('¿Está seguro de eliminar este registro?');"
                                                                            CausesValidation="False" />
                                                                        <asp:ImageButton ID="imgbEdit" runat="server" CommandName="editrow"
                                                                            ImageUrl="~/App_Themes/Tema1/Images/editar.gif"
                                                                            CausesValidation="False" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:GridView>
                                                        <br />
                                                        <label id="lblTotal" runat="server" class="form-control" style="text-align: right; background-color: #d9edf7;">
                                                            Total: $0.00</label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>


                                                <!-- ////////////////////////////// POPUP DETALLE //////////////////////////////////////// -->
                                                <asp:HiddenField ID="HiddenField3" runat="server" />
                                                <asp:Button ID="Button5" runat="server" Text="Button" Style="visibility: hidden;" />
                                                <ajaxToolkit:ModalPopupExtender runat="server"
                                                    BackgroundCssClass="modalBackground"
                                                    PopupControlID="modalDetalleCarga"
                                                    BehaviorID="popUpDetalle"
                                                    TargetControlID="Button5"
                                                    ID="popUpDetalle">
                                                </ajaxToolkit:ModalPopupExtender>
                                                <div style="padding: 10px; width: 36%; background-color: White; box-shadow: 0px 0px 10px #000;"
                                                    id="modalDetalleCarga" runat="server" class="panel panel-info">

                                                    <div class="panel-heading">
                                                        <h4>Agregar Items</h4>
                                                    </div>
                                                    <div class="panel-body">
                                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <div class="form-group">
                                                                    <label for="ejemplo_email_1">Descripcion</label>
                                                                    <br />
                                                                    <asp:TextBox ID="txtDescripcion" runat="server" autocomplete="false" Width="90%"
                                                                        placeholder="Ingrese descripcion del Articulo / Servicio" CssClass="form-control"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="GroupItems" ControlToValidate="txtDescripcion" ErrorMessage="Debe Seleccionar Descripcion del Item" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label for="ejemplo_email_1">Cantidad</label>
                                                                    <br />
                                                                    <asp:TextBox ID="txtCantidad" runat="server" Width="90%" autocomplete="false"
                                                                        placeholder="Cantidad" AutoPostBack="True" CssClass="form-control"
                                                                        OnTextChanged="txtCantidad_TextChanged"></asp:TextBox>
                                                                    <asp:CompareValidator ID="CompareValidator2" runat="server" ValidationGroup="GroupItems" ControlToValidate="txtCantidad" ErrorMessage="Debe Ingresar Cantidad" Type="Double" Operator="DataTypeCheck">*</asp:CompareValidator>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtCantidad" ErrorMessage="Debe ingresar Cantidad" SetFocusOnError="True" ValidationGroup="GroupItems">*</asp:RequiredFieldValidator>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label for="ejemplo_email_1">Precio Unitario</label>
                                                                    <br />
                                                                    <asp:TextBox ID="txtPU" runat="server" Width="90%" autocomplete="false" CssClass="form-control"
                                                                        placeholder="Precio Unitario" OnTextChanged="txtPU_TextChanged"
                                                                        AutoPostBack="True"></asp:TextBox>
                                                                    <asp:CompareValidator ID="CompareValidator3" runat="server" ValidationGroup="GroupItems" ControlToValidate="txtPU" ErrorMessage="Debe Ingresar Importe" Type="Double" Operator="DataTypeCheck">*</asp:CompareValidator>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtPU" ErrorMessage="Debe ingresar Precio Unitario" SetFocusOnError="True" ValidationGroup="GroupItems">*</asp:RequiredFieldValidator>
                                                                </div>
                                                                <br />
                                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" ValidationGroup="GroupItems" Width="409px" />
                                                                <br />
                                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                    <ContentTemplate>
                                                                        <div class="form-group">
                                                                            <button type="button" class="btn btn-sm"
                                                                                runat="server" id="Button2" onserverclick="Button2_ServerClick" validationgroup="GroupItems">
                                                                                Aceptar</button>
                                                                            <button type="button" class="btn btn-sm"
                                                                                runat="server" id="Button1" onserverclick="Button1_ServerClick">
                                                                                Salir</button>
                                                                        </div>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                                <br />
                                                                </fieldset>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>
                                                <!-- ///////////////////////////////////////////////////////////////////////////////////// -->
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <div class="row" style="margin-top: 20px;" id="divObsOk" runat="server">
                                <div class="col-md-12">
                                    <h4>Observaciones</h4>
                                    <hr />
                                </div>
                                <div class="col-md-12">
                                    <div style="border: 1px solid #6E9C4E; padding: 10px;">
                                        <ul class="arrowAzul" id="ulObsOk" runat="server">
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="units-row" id="divConsultaError" runat="server" visible="false">
                            <div class="unit-100">
                                <div id="divMensaje">
                                    <div class="row" style="margin-top: 20px;">
                                        <div class="col-md-12">
                                            <div style="border: 1px solid red; padding: 10px;">
                                                <h3>
                                                    <img src="../img/error.png" style="width: 30px; margin-right: 10px;" />Se ha detectado al menos uno de los siguientes errores:</h3>
                                                <ul class="checkNaranja2 marginLeft-20 marginTop-10 marginBottom-10"
                                                    id="ulErrores" runat="server">
                                                </ul>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="row" style="margin-top: 20px;" id="divObsError" runat="server">
                                        <div class="col-md-12">
                                            <h4>Observaciones</h4>
                                            <hr />
                                        </div>
                                        <div class="col-md-12">
                                            <div style="border: 1px solid red; padding: 10px;">
                                                <ul class="arrowAzul" id="ulObsError" runat="server">
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="box-footer" style="text-align: right;">
                    <asp:LinkButton ID="btnConsulta" OnClick="btnConsulta_Click" ValidationGroup="consulta"
                        CssClass="btn btn-primary" runat="server"><span class="fa fa-search"></span>&nbsp;CONSULTAR</asp:LinkButton>
                    <a href="ConsultaComprobante.aspx" class="btn btn-info"><span class="fa fa-sign-out">&nbsp;LIMPIAR DATOS</span></a>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade in" id="modalBarCode" style="padding-right: 17px;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span></button>
                    <h4 class="modal-title" id="lblContacto"></h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>Ingrese Codigo de Barra</label>
                                <asp:TextBox ID="txtBarsCode" placeholder="Ingrese el codigo de barra"
                                    AutoPostBack="true" OnTextChanged="txtBarsCode_TextChanged"
                                    CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Salir</button>
                </div>
            </div>
        </div>
        <!-- /.modal-content -->
    </div>





    <!-- jQuery 2.1.4 -->
    <script src="../App_Themes/plugins/jQuery/jQuery-2.1.4.min.js"></script>

    <!-- Bootstrap 3.3.2 JS -->
    <script src="../App_Themes/bootstrap/js/bootstrap.min.js"></script>
    <script type="text/javascript">

</script>
    <!-- AdminLTE App -->
    <script src="../App_Themes/dist/js/app.min.js" type="text/javascript"></script>
    <!-- AdminLTE for demo purposes -->
    <script src="../App_Themes/dist/js/demo.js" type="text/javascript"></script>
    <script>
        $(document).ready(function () {
            $("#modalBarCode").on('shown.bs.modal', function () {
                $(this).find('input[type="text"]').focus();
            });
        });
    </script>
</asp:Content>
