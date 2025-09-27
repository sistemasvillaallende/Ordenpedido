<%@ Page Title="" Language="C#" MasterPageFile="~/MP/Bootstrap.Master" AutoEventWireup="true"
    CodeBehind="newOpBootstrap.aspx.cs" Inherits="Web.Secure.newOpBootstrap" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="container" style="padding-top: 10px;">

        <!-- ////////////////////////////// NRO ORDEN PEDIDO / FECHA ///////////////////////////////// -->
        <div class="box box-primary">
            <div class="box-header">
                <h4>Nueva Orden de Pedido</h4>
            </div>
            <div class="box-body">
                <div class="form-group">
                    <div class="col-sm-3">
                        <label for="nroOrden">Nro. Orden de Pedido</label>
                        <p class="form-control-static" id="txtOP" runat="server">0000</p>
                    </div>
                    <div class="col-sm-3">
                        <label for="fecha">Fecha</label>
                        <p class="form-control-static" id="txtFechaOp" runat="server"></p>
                    </div>
                    <div class="col-sm-3">
                        <label for="estado">Estado OP</label>
                        <p class="form-control-static" id="txtEstado_op" runat="server"></p>
                    </div>
                    <div class="col-sm-3">
                        <label for="fecha">Origen</label>
                        <p class="form-control-static" id="P1" runat="server"></p>
                    </div>
                </div>

            </div>
        </div>
        <!-- ///////////////////////////////////////////////////////////////////////////////////////// -->
        <!--  <div class="alert alert-danger">Error</div> -->
        <!-- ////////////////////////////// CABECERA ///////////////////////////////////////////////// -->
        <div class="box box-info">
            <div class="box-header">
                <h4>Cabecera</h4>
            </div>
            <div class="box-body">
                <!-- ////////////////////////////// PROVEEDOR //////////////////////////////////////////// -->
                <asp:UpdatePanel ID="uPanelProv" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-xs-3">
                                    <label for="fecha">Proveedor</label>
                                    <asp:TextBox ID="txtIdProv" CssClass="form-control" runat="server"
                                        placeholder="Ingrese codigo" AutoPostBack="True"
                                        OnTextChanged="txtIdProv_TextChanged1"></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="Debe Ingresar Proveedor" ControlToValidate="txtIdProv" Type="Integer" ValidationGroup="GroupDatos" Operator="DataTypeCheck" SetFocusOnError="True">*</asp:CompareValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtIdProv" ErrorMessage="Debe Seleccionar Proveedor" SetFocusOnError="True" ValidationGroup="GroupDatos">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="col-xs-3">
                                    <label for="fecha">C.U.I.T.</label>
                                    <input type="text" class="form-control" id="txtCUITProveedor" runat="server" readonly="true" />
                                </div>
                                <div class="col-xs-6">
                                    <label for="fecha">Razon Social</label>
                                    <div class="input-group">
                                        <input type="text" class="form-control" id="txtNameProv" runat="server" readonly="true" />
                                        <span class="input-group-btn">
                                            <div class="btn-group">
                                                <asp:LinkButton ID="lnkFindProv" CssClass="btn btn-default" runat="server"
                                                    OnClientClick="$('#modalBuscarProveedor').modal('show'); return false;">
                                                    <i class="fa fa-plus"></i> Buscar Proveedor
                                                </asp:LinkButton>
                                            </div>
                                        </span>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="modal fade in" id="actualizacuit">
                            <div class="modal-dialog">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">×</span></button>
                                        <h4 class="modal-title">Actualizar CUIT</h4>
                                    </div>
                                    <div class="modal-body">
                                        <div class="form-group">
                                            <label>CUIT Actual</label>
                                            <asp:TextBox CssClass="form-control" Enabled="false" ID="txtCuitActual" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label>CUIT Nuevo</label>
                                            <asp:TextBox CssClass="form-control" ID="txtCuitNuevo" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="modal-footer" style="text-align: right;">
                                        <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>
                                        <button type="button" class="btn btn-primary">Aceptar</button>
                                    </div>
                                </div>
                                <!-- /.modal-content -->
                            </div>
                            <!-- /.modal-dialog -->
                        </div>


                    </ContentTemplate>
                </asp:UpdatePanel>
                <!-- ////////////////////////////// DESTINO ////////////////////////////////////////////// -->
                <asp:UpdatePanel ID="UPanelDestino" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-xs-3">
                                    <label for="fecha">Destino</label>
                                    <asp:TextBox ID="txtIdDestino" runat="server" placeholder="Ingrese codigo"
                                        class="form-control" AutoPostBack="True"
                                        OnTextChanged="txtIdDestino_TextChanged1">
                                    </asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator5" runat="server" ErrorMessage="Debe ingresar Oficina Detino"
                                        ControlToValidate="txtIdDestino" Operator="DataTypeCheck" Type="Integer"
                                        ValidationGroup="GroupDatos" SetFocusOnError="True">*</asp:CompareValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtIdDestino"
                                        ErrorMessage="Debe Seleccionar Oficina Destino" SetFocusOnError="True"
                                        ValidationGroup="GroupDatos">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="col-xs-9">
                                    <label for="fecha">Nombre Oficina</label>
                                    <div class="input-group">
                                        <input type="text" class="form-control" id="txtNameDestino" readonly="true" runat="server" />
                                        <span class="input-group-btn">
                                            <div class="btn-group">
                                                <asp:LinkButton ID="lnkFinDest" CssClass="btn btn-default" runat="server"
                                                    OnClientClick="$('#modalBuscarOficina').modal('show'); return false;">
                                                    <i class="fa fa-desktop"></i> Buscar Destino
                                                </asp:LinkButton>
                                            </div>
                                        </span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <!-- ///////////////////////////////////////////////////////////////////////////////////// -->
                <!-- ////////////////////////////// AUTORIZA - SOLICITA/////////////////////////////////// -->
                <asp:UpdatePanel ID="UPanelDatos" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnConsulta" />
                        <asp:PostBackTrigger ControlID="btnProcesarExcel" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-xs-6">
                                    <label for="ejemplo_email_1">SecretariaAutoriza</label>
                                    <asp:DropDownList ID="ddlSecretariaAutoriza" runat="server" CssClass="form-control"
                                        AutoPostBack="True" OnSelectedIndexChanged="ddlSecretariaAutoriza_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvSecretariaAutoriza" runat="server"
                                        ErrorMessage="Debe seleccionar Secretaría" 
                                        ControlToValidate="ddlSecretariaAutoriza"
                                        InitialValue="0"
                                        ValidationGroup="GroupDatos" 
                                        SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                    &nbsp;
                                </div>
                                <div class="col-xs-6">
                                    <label for="ejemplo_email_1">Direccion Solicitante</label>
                                    <asp:DropDownList ID="ddlDireccionSolicitante" runat="server" CssClass="form-control"
                                        AutoPostBack="True" OnSelectedIndexChanged="ddlDireccionSolicitante_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvDireccionSolicitante" runat="server"
                                        ErrorMessage="Debe seleccionar Dirección" 
                                        ControlToValidate="ddlDireccionSolicitante"
                                        InitialValue="0"
                                        ValidationGroup="GroupDatos" 
                                        SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-xs-6">
                                    <label for="ejemplo_email_1">Autorizado Por</label>
                                    <input type="text" class="form-control" id="txtAut" disabled="disabled"
                                        runat="server" placeholder="Introduzca el nombre de quien autoriza" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Debe Ingresar Campo Autorizado Por" ControlToValidate="txtAut" ValidationGroup="GroupDatos" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                    &nbsp;
                                </div>
                                <div class="col-xs-6">
                                    <label for="ejemplo_email_1">Solicitante</label>
                                    <input type="text" class="form-control" id="txtSolicitante" disabled="disabled"
                                        runat="server" placeholder="Introduzca el nombre de quien solicita" />
                                </div>
                            </div>
                        </div>
                        <!-- ///////////////////////////////////////////////////////////////////////////////////// -->
                        <!-- ////////////////////////////// FORMA DE PAGO //////////////////////////////////////// -->
                        <div class="form-group">
                            <div class="row">
                                <div class="col-xs-6">
                                    <label for="ejemplo_email_1">Observaciones</label>
                                    <input type="text" class="form-control" id="txtObs"
                                        runat="server" placeholder="Introduzca tus observaciones" />
                                </div>
                                <div class="col-xs-6">
                                    <label for="ejemplo_email_1">Nro. Presupuesto 000/000/0000</label>
                                    <input type="text" class="form-control" id="txtNroPresup"
                                        runat="server" placeholder="Introduzca el Nro. de presupuesto" />
                                </div>
                            </div>
                        </div>
                        <!-- ///////////////////////////////////////////////////////////////////////////////////// -->
                        <!-- ////////////////////////////// OBSERVACIONES //////////////////////////////////////// -->
                        <div class="form-group">
                            <div class="row">
                                <div class="col-xs-6">
                                    <label for="ejemplo_email_1">Forma de Pago</label>
                                    <input type="text" class="form-control" id="txtFormaPago"
                                        runat="server" placeholder="Introduzca aqui la forma de pago convenida" />
                                </div>
                                <div class="col-xs-6" style="padding-top: 25px;">
                                    <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#modalAddFactura">
                                        Agregar Factura
                                    </button>
                                </div>
                                <div class="col-xs-6" style="display: none;">
                                    <label for="ejemplo_email_1">Facturas</label>
                                    <div class="input-group input-group-sm">
                                        <input type="text" class="form-control" id="txtNroFactura"
                                            runat="server" placeholder="Introduzca el/los Nro. de Factura/s" />
                                        <span class="input-group-btn"></span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-xs-12">
                                    <asp:GridView ID="gvFacturas"
                                        DataKeyNames="ID,FECHA_EMISION,PUNTO_VENTA,NRO_COMPROBANTE,NRO_CAE,IMPORTE,TIPO_COMPROBANTE,TIP_COMP,NRO_CUIT_PROVEEDOR"
                                        OnRowCommand="gvFacturas_RowCommand"
                                        OnRowDataBound="gvFacturas_RowDataBound"
                                        EmptyDataText="Aun no se han agregado facturas a la orden de pedido"
                                        CssClass="table"
                                        runat="server"
                                        CellPadding="4"
                                        AutoGenerateColumns="false"
                                        ForeColor="#333333" GridLines="None">
                                        <Columns>
                                            <asp:BoundField DataField="FECHA_EMISION" DataFormatString="{0:d}"
                                                HeaderText="Fecha"></asp:BoundField>
                                            <asp:BoundField DataField="TIP_COMP"
                                                HeaderText="Tipo Comprobante"></asp:BoundField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblComprobante" runat="server" Text=""></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="NRO_CAE"
                                                HeaderText="CAE"></asp:BoundField>
                                            <asp:BoundField DataField="IMPORTE" DataFormatString="{0:c}"
                                                HeaderText="Importe"></asp:BoundField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnEliminar"
                                                        OnClientClick="return confirm('¿Esta seguro de eliminar la factura?');"
                                                        CommandArgument='<%# Container.DataItemIndex %>'
                                                        CommandName="eliminar" runat="server">
                                                        <span class="fa fa-trash-o" style="color:red;"></span>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775"></AlternatingRowStyle>
                                        <EditRowStyle BackColor="#999999"></EditRowStyle>
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"></FooterStyle>
                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"></HeaderStyle>
                                        <PagerStyle HorizontalAlign="Center" BackColor="#284775" ForeColor="White"></PagerStyle>
                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333"></RowStyle>
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>
                                        <SortedAscendingCellStyle BackColor="#E9E7E2"></SortedAscendingCellStyle>
                                        <SortedAscendingHeaderStyle BackColor="#506C8C"></SortedAscendingHeaderStyle>
                                        <SortedDescendingCellStyle BackColor="#FFFDF8"></SortedDescendingCellStyle>
                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE"></SortedDescendingHeaderStyle>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <div class="units-row" id="divConsultaError" runat="server" visible="false">
                            <button type="button" style="padding-right: 10px; color: red; opacity: 1;"
                                runat="server" id="btnCloseError" onserverclick="btnCloseError_ServerClick" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span></button>
                            <div class="unit-100">
                                <div id="divMensaje" style="border: 1px solid red; padding: 10px;">
                                    <div class="row" style="margin-top: 20px;">
                                        <div class="col-md-12">
                                            <div>
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
                                            <div>
                                                <ul class="arrowAzul" id="ulObsError" runat="server">
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="modal fade in" id="modalAddFactura">
                            <div class="modal-dialog">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">×</span></button>
                                        <h4 class="modal-title">Agregar Factura</h4>
                                    </div>
                                    <div class="modal-body">
                                        <div id="divConsultaRequest" runat="server">
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label>Número de CUIT</label>
                                                        <asp:TextBox ID="txtCUIT" CssClass="form-control" Enabled="false"
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
                                    </div>
                                    <div class="modal-footer" style="text-align: right;">
                                        <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>
                                        <asp:LinkButton ID="btnConsulta" OnClick="btnConsulta_Click" ValidationGroup="consulta"
                                            CssClass="btn btn-primary" runat="server"><span class="fa fa-search"></span>&nbsp;CONSULTAR</asp:LinkButton>
                                    </div>
                                </div>
                                <!-- /.modal-content -->
                            </div>
                        </div>
                        <!-- /.modal-dialog -->
                    </ContentTemplate>
                </asp:UpdatePanel>

                <br />
                <asp:ValidationSummary ID="ValidationSummary3" runat="server" ForeColor="Red" ValidationGroup="GroupDatos" Width="409px" />
                <br />
                <!-- ///////////////////////////////////////////////////////////////////////////////////// -->

            </div>
        </div>
        <!-- ///////////////////////////////////////////////////////////////////////////////////////// -->
        <!-- ////////////////////////////// DETALLE ////////////////////////////////////////////////// -->
        <div class="box box-info">
            <div class="box-header">
                <h4>Detalle</h4>
            </div>
            <div class="box-body">
                <!-- ////////////////////////////// BOTONES ////////////////////////////////////////////// -->
                <div class="form-group">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="btn-group">

                                <%--<asp:LinkButton ID="lbtnAddDetalle" CssClass="btn btn-default" runat="server" OnClick="lbtnAddDetalle_Click">
                                            <i class="fa fa-plus"></i> Agregar Detalle
                                </asp:LinkButton>--%>
                                <asp:LinkButton ID="LinkButton1" CssClass="btn btn-default" runat="server"
                                    OnClientClick="$('#modalAgregarDetalle').modal('show'); return false;"
                                    OnClick="LinkButton1_Click">
                                    <i class="fa fa-plus"></i> Agregar Detalle
                                </asp:LinkButton>

                                <asp:LinkButton ID="lbtnAddExcel" CssClass="btn btn-default" runat="server"
                                    OnClientClick="$('#modalSubirExcel').modal('show'); return false;">
                                    <i class="fa fa-plus"></i> Subir Excel
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
                            OnRowCommand="gvDetalle_RowCommand" GridLines="None"
                            OnRowCreated="gvDetalle_RowCreated">
                            <Columns>
                                <asp:BoundField HeaderText="Descripcion Pedido" DataField="descItems">
                                    <ControlStyle Width="400px" />
                                    <HeaderStyle HorizontalAlign="Left" BackColor="#d9edf7" />
                                </asp:BoundField>
                                <asp:BoundField DataField="cant" HeaderText=" Cantidad">
                                    <HeaderStyle BackColor="#d9edf7" />
                                </asp:BoundField>
                                <asp:BoundField DataField="precio" HeaderText=" Precio"
                                    DataFormatString="{0:N2}">
                                    <HeaderStyle BackColor="#d9edf7" />
                                </asp:BoundField>
                                <asp:BoundField DataField="importe" HeaderText=" Importe"
                                    DataFormatString="{0:N2}">
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
            </div>
        </div>
        <!-- ////////////////////////////////// BOTONES ////////////////////////////////////////////// -->
        <div class="form-group">
            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                <ContentTemplate>
                    <div class="box-footer clearfix" style="text-align: right;">
                        <div class="btn-group">
                            <button type="button" class="btn btn-primary"
                                runat="server" id="btnSave" onserverclick="btnSave_Click" validationgroup="GroupDatos">
                                <span class="glyphicon glyphicon-floppy-disk"></span>Guardar
                            </button>

                            <button type="button" class="btn btn-primary"
                                runat="server" id="btnExit" onclick="location.href='index.aspx';">
                                <span class="glyphicon glyphicon-log-out"></span>Salir
                            </button>
                            <button type="button" class="btn btn-primary"
                                runat="server" id="btnPrint" onserverclick="btnPrint_Click">
                                <span class="glyphicon glyphicon-print"></span>Imprimir
                            </button>
                            <button type="button" class="btn btn-primary"
                                runat="server" id="btnAddOrden" onserverclick="btnAddOrden_click">
                                <span class="glyphicon glyphicon-plus"></span>Agregar Nueva Orden
                            </button>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <!-- ///////////////////////////////////////////////////////////////////////////////////////// -->
        <!-- //POPUP DESTINO////////////////////////////////////////////////////////////////////// -->
        <!-- MODAL BUSCAR OFICINA DE DESTINO (Bootstrap) -->
        <div class="modal fade" id="modalBuscarOficina" tabindex="-1" role="dialog" aria-labelledby="modalBuscarOficinaLabel">
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                        <h4 class="modal-title" id="modalBuscarOficinaLabel">
                            <i class="fa fa-building"></i>Buscar Oficina de Destino
                </h4>
                    </div>
                    <div class="modal-body">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="form-group">
                                    <label>Buscar por nombre de oficina</label>
                                    <div class="input-group">
                                        <input type="text" class="form-control" id="txtFindDest" runat="server"
                                            placeholder="Ingrese nombre de la oficina" />
                                        <span class="input-group-btn">
                                            <button class="btn btn-primary btn-sm" type="button"
                                                runat="server" id="btnBuscarDest" onserverclick="btnBuscarDest_click">
                                                <span class="glyphicon glyphicon-search"></span>Buscar
                                            </button>
                                        </span>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div style="max-height: 300px; overflow-y: auto;">
                                        <asp:GridView ID="gvOficinas" runat="server" AutoGenerateColumns="False"
                                            CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%"
                                            DataKeyNames="idOficina,nombre" CssClass="table table-hover table-striped"
                                            OnRowCommand="gvOficinas_RowCommand" OnRowCreated="gvOficinas_RowCreated"
                                            EmptyDataText="No se encontraron oficinas. Realice una búsqueda.">
                                            <Columns>
                                                <asp:BoundField DataField="idOficina" HeaderText="Código">
                                                    <HeaderStyle BackColor="#5D7B9D" ForeColor="White" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="nombre" HeaderText="Nombre de Oficina">
                                                    <HeaderStyle BackColor="#5D7B9D" ForeColor="White" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Seleccionar">
                                                    <HeaderStyle BackColor="#5D7B9D" ForeColor="White" Width="100px" />
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnSeleccionar" runat="server" CommandName="selected"
                                                            CssClass="btn btn-sm btn-success"
                                                            CommandArgument='<%# Container.DataItemIndex %>'
                                                            CausesValidation="False">
                                                    <i class="fa fa-check"></i> Seleccionar
                                                </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                            <EditRowStyle BackColor="#999999" />
                                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">
                            <i class="fa fa-times"></i>Cancelar
                       
                        </button>
                    </div>
                </div>
            </div>
        </div>

        <!-- ///////////////////////////////////////////////////////////////////////////////////// -->
        <!-- ////////////////////////////// POPUP PROVEEDOR ///////////////////////////////////////// -->
        <!-- MODAL BUSCAR PROVEEDOR (Bootstrap) -->
        <div class="modal fade" id="modalBuscarProveedor" tabindex="-1" role="dialog" aria-labelledby="modalBuscarProveedorLabel">
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                        <h4 class="modal-title" id="modalBuscarProveedorLabel">
                            <i class="fa fa-search"></i>Buscar Proveedor
                </h4>
                    </div>
                    <div class="modal-body">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="form-group">
                                    <label>Buscar por razón social</label>
                                    <div class="input-group">
                                        <input type="text" class="form-control" id="txtBuscarProv" runat="server"
                                            placeholder="Ingrese razón social del proveedor" />
                                        <span class="input-group-btn">
                                            <button class="btn btn-primary" type="button"
                                                runat="server" id="btnBuscar" onserverclick="btnBuscar_click">
                                                <i class="fa fa-search"></i>Buscar
                                            </button>
                                        </span>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div style="max-height: 350px; overflow-y: auto;">
                                        <asp:GridView ID="gvProv" runat="server" AutoGenerateColumns="False"
                                            CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%"
                                            DataKeyNames="codProveedor,nomProveedor,nroCuit"
                                            OnRowCommand="gvProv_RowCommand" OnRowCreated="gvProv_RowCreated"
                                            CssClass="table table-hover table-striped"
                                            EmptyDataText="No se encontraron proveedores. Realice una búsqueda.">
                                            <Columns>
                                                <asp:BoundField DataField="codProveedor" HeaderText="Código">
                                                    <HeaderStyle BackColor="#5D7B9D" ForeColor="White" Width="80px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="nomProveedor" HeaderText="Razón Social">
                                                    <HeaderStyle BackColor="#5D7B9D" ForeColor="White" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="nroCuit" HeaderText="CUIT">
                                                    <HeaderStyle BackColor="#5D7B9D" ForeColor="White" Width="120px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="nroBad" HeaderText="Nro BAD">
                                                    <HeaderStyle BackColor="#5D7B9D" ForeColor="White" Width="100px" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Seleccionar">
                                                    <HeaderStyle BackColor="#5D7B9D" ForeColor="White" Width="100px" />
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnSeleccionar" runat="server" CommandName="selected"
                                                            CssClass="btn btn-sm btn-success"
                                                            CommandArgument='<%# Container.DataItemIndex %>'
                                                            CausesValidation="False">
                                                    <i class="fa fa-check"></i> Seleccionar
                                                </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                            <EditRowStyle BackColor="#999999" />
                                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">
                            <i class="fa fa-times"></i>Cancelar
               
                        </button>
                    </div>
                </div>
            </div>
        </div>

        <!-- ///////////////////////////////////////////////////////////////////////////////////// -->
        <!-- ////////////////////////////// POPUP DETALLE //////////////////////////////////////// -->
        <%--//////////// aca va el codigo anterior ////////////--%>
        <!-- MODAL AGREGAR DETALLE (Bootstrap) -->
        <div class="modal fade" id="modalAgregarDetalle" tabindex="-1" role="dialog">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">
                            <span>&times;</span>
                        </button>
                        <h4 class="modal-title">
                            <i class="fa fa-plus-circle"></i>Agregar Items a la Lista
                </h4>
                    </div>
                    <div class="modal-body">
                        <!-- MANTENER el UpdatePanel para AJAX -->
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="form-group">
                                    <label>Descripción</label>
                                    <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control"
                                        autocomplete="false" placeholder="Ingrese descripcion del Articulo/Servicio" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                        ValidationGroup="GroupItems" ControlToValidate="txtDescripcion"
                                        ErrorMessage="Debe Seleccionar Descripcion del Item" SetFocusOnError="True"
                                        CssClass="text-danger">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group">
                                    <label>Cantidad</label>
                                    <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control"
                                        autocomplete="false" placeholder="Ingrese Cantidad" />
                                    <%-- AutoPostBack="True" OnTextChanged="txtCantidad_TextChanged"--%>
                                    <asp:CompareValidator ID="CompareValidator2" runat="server"
                                        ValidationGroup="GroupItems" ControlToValidate="txtCantidad"
                                        Type="Double" Operator="DataTypeCheck" CssClass="text-danger">*</asp:CompareValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                        ControlToValidate="txtCantidad" ValidationGroup="GroupItems"
                                        ErrorMessage="Debe ingresar Cantidad" SetFocusOnError="True"
                                        CssClass="text-danger">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group">
                                    <label>Precio Unitario</label>
                                    <asp:TextBox ID="txtPU" runat="server" CssClass="form-control"
                                        placeholder="Precio Unitario" />
                                    <%--AutoPostBack="True" OnTextChanged="txtPU_TextChanged" --%>
                                    <asp:CompareValidator ID="CompareValidator3" runat="server"
                                        ValidationGroup="GroupItems" ControlToValidate="txtPU"
                                        ErrorMessage="Debe Ingresar Importe"
                                        Type="Double" Operator="DataTypeCheck" CssClass="text-danger">*</asp:CompareValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                        ControlToValidate="txtPU" ValidationGroup="GroupItems"
                                        ErrorMessage="Debe ingresar Precio Unitario" SetFocusOnError="True"
                                        CssClass="text-danger">*</asp:RequiredFieldValidator>
                                </div>
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server"
                                    CssClass="alert alert-danger" ValidationGroup="GroupItems" />

                                <!-- MANTENER también el UpdatePanel2 para los botones -->
                                <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <div class="form-group text-right">
                                            <button type="button" class="btn btn-sm"
                                                runat="server" id="Button2" onserverclick="btnAceptar_Click" validationgroup="GroupItems">
                                                Aceptar</button>
                                            <button type="button" class="btn btn-sm"
                                                runat="server" id="Button1" onserverclick="btnCancelar_Click">
                                                Salir</button>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>--%>
                                <!-- Reemplazar todo el bloque de botones dentro del modal -->
                                <div class="form-group text-right">
                                    <asp:Button ID="btnItemAceptar" runat="server" Text="Aceptar"
                                        CssClass="btn btn-success btn-sm"
                                        ValidationGroup="GroupItems"
                                        UseSubmitBehavior="false"
                                        OnClick="btnAceptar_Click" />
                                    <asp:Button ID="btnItemCancelar" runat="server" Text="Cancelar"
                                        CssClass="btn btn-default btn-sm"
                                        CausesValidation="false"
                                        OnClick="btnCancelar_Click" />
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <script>
                            // Evita doble envío
                            $(function () {
                                $('#<%= btnItemAceptar.ClientID %>').on('click', function () {
                                    if (this.disabled) return false;
                                    this.disabled = true;
                                    setTimeout(() => { this.disabled = false; }, 1500);
                                });
                            });
                    </script>
                    </div>
                </div>
            </div>
        </div>


        <!-- ///////////////////////////////////////////////////////////////////////////////////// -->

        <%--<asp:HiddenField ID="HiddenField2" runat="server" />
        <asp:Button ID="Button6" runat="server" Text="Button" Style="visibility: hidden;" />
        <ajaxToolkit:ModalPopupExtender runat="server"
            BackgroundCssClass="modalBackground"
            PopupControlID="modalAuditoria"
            BehaviorID="popUpAuditoria"
            TargetControlID="Button6"
            ID="popUpAuditoria">
        </ajaxToolkit:ModalPopupExtender>
        <div style="padding: 10px; width: 43%; background-color: White; box-shadow: 0px 0px 10px #000;"
            id="modalAuditoria" runat="server" class="panel panel-info">
            <div class="panel-heading">
                <h4>Auditoria Orden de Pedido</h4>
            </div>
            <div class="panel-body">
                <asp:UpdatePanel ID="uPanelUpdate" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:TextBox ID="txtObservAuditoria" runat="server" Width="357px"
                            autocomplete="false" placeholder="Ingrese el motivo de la modificacion"
                            Height="106px" TextMode="MultiLine"></asp:TextBox>
                        <br />
                        <br />
                        <asp:Button ID="btnAceptarAuditoria" runat="server" Text="Aceptar"
                            CssClass="btn btn-sm"
                            OnClick="btnAceptarAuditoria_Click" ValidationGroup="GroupDatos" />
                        <asp:Button ID="btnCancelarAuditoria" runat="server" Text="Cancelar"
                            CssClass="btn btn-sm"
                            CausesValidation="False" OnClick="btnCancelarAuditoria_Click" />
                        <br />
                        <br>
                        <br />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>--%>

        <!-- MODAL AUDITORIA ORDEN DE PEDIDO (Bootstrap) -->
        <div class="modal fade" id="modalAuditoria" tabindex="-1" role="dialog" aria-labelledby="modalAuditoriaLabel">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header" style="background-color: #5bc0de; color: white;">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="color: white;">
                            <span aria-hidden="true">&times;</span>
                        </button>
                        <h4 class="modal-title" id="modalAuditoriaLabel">
                            <i class="fa fa-clipboard"></i>Auditoria Orden de Pedido
                </h4>
                    </div>
                    <div class="modal-body">
                        <asp:UpdatePanel ID="uPanelUpdate" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="form-group">
                                    <label for="txtObservAuditoria">Motivo de la modificación *</label>
                                    <asp:TextBox ID="txtObservAuditoria" runat="server"
                                        CssClass="form-control"
                                        autocomplete="false"
                                        placeholder="Ingrese el motivo de la modificacion"
                                        TextMode="MultiLine"
                                        Rows="5"
                                        MaxLength="500"></asp:TextBox>
                                    <small class="text-muted">Máximo 500 caracteres</small>
                                </div>
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server"
                                    CssClass="alert alert-danger" ValidationGroup="GroupAudita"
                                    HeaderText="Por favor Ingese datos:"
                                    DisplayMode="BulletList" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="modal-footer text-right">
                        <%--<asp:Button ID="btnCancelarAuditoria" runat="server" Text="Cancelar"
                            CssClass="btn btn-sm"
                            CausesValidation="False" OnClick="btnCancelarAuditoria_Click" />
                        <asp:Button ID="btnAceptarAuditoria" runat="server" Text="Aceptar"
                            CssClass="btn btn-sm"
                            OnClick="btnAceptarAuditoria_Click"
                            OnClientClick="return validarObservacionAuditoria();"
                            <i class="fa fa-check"></i>Aceptar
                        </asp:Button>--%>

                        <asp:LinkButton ID="lnkCancelarAuditoria" runat="server"
                            CssClass="btn btn-sm"
                            CausesValidation="False"
                            OnClick="lnkCancelarAuditoria_Click">
                            <i class="fa fa-times"></i> Cancelar
                        </asp:LinkButton>
                        <asp:LinkButton ID="lnkAceptarAuditoria" runat="server"
                            CssClass="btn btn-sm"
                            OnClick="lnkAceptarAuditoria_Click"
                            OnClientClick="return validarObservacionAuditoria();">
                            <i class="fa fa-check"></i> Aceptar
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>

        <%-- <asp:HiddenField ID="HiddenField4" runat="server" />
        <asp:Button ID="Button7" runat="server" Text="Button" Style="visibility: hidden;" />
        <ajaxToolkit:ModalPopupExtender runat="server"
        BackgroundCssClass="modalBackground"
        PopupControlID="modalReporte"
        BehaviorID="popUpListado"
        TargetControlID="Button7"
        ID="popUpListado">
        </ajaxToolkit:ModalPopupExtender>
        <div class="row" id="modalReporte" style="background-color: White; width: 70%; padding: 15px; border-radius: 12px; padding-top: 0px;">
        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="modal-header" style="background-color: #3587B2;">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true" runat="server"
                            id="btnCloseListado" onserverclick="btnCloseListado_Click">
                            ×</button>
                        <h2 style="color: white">Reporte Orden de Pedido</h2>
                    </div>
                </div>
                <div runat="server" id="divReporte">
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        </div>--%>
        <!-- ///////////////////////////////////////////////////////////////////////////////////// -->
        <!-- MODAL REPORTE ORDEN DE PEDIDO (Bootstrap) -->
        <div class="modal fade" id="modalReporteOP" tabindex="-1" role="dialog" aria-labelledby="modalReporteOPLabel">
            <div class="modal-dialog modal-lg" role="document" style="width: 90%; max-width: 1200px;">
                <div class="modal-content">
                    <div class="modal-header" style="background-color: #3587B2; color: white;">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="color: white;">
                            <span aria-hidden="true">&times;</span>
                        </button>
                        <h4 class="modal-title" id="modalReporteOPLabel">
                            <i class="fa fa-file-pdf-o"></i>Reporte Orden de Pedido
                </h4>
                    </div>
                    <div class="modal-body" style="padding: 0; height: 70vh;">
                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                            <ContentTemplate>
                                <div runat="server" id="divReporte"
                                    style="width: 100%; height: 100%; min-height: 600px;">
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">
                            <i class="fa fa-times"></i>Cerrar
               
                        </button>
                        <%--<button type="button" class="btn btn-primary" onclick="window.print();">
                            <i class="fa fa-print"></i>Imprimir
               
                        </button>--%>
                    </div>
                </div>
            </div>
        </div>

        <!-- MODAL SUBIR EXCEL -->
        <div class="modal fade" id="modalSubirExcel" tabindex="-1" role="dialog" aria-labelledby="modalSubirExcelLabel">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                        <h4 class="modal-title" id="modalSubirExcelLabel">
                            <i class="fa fa-file-excel-o"></i>Subir Archivo Excel
                </h4>
                    </div>
                    <div class="modal-body">
                        <div class="form-group">
                            <label>Seleccione el archivo Excel (.xlsx, .xls)</label>
                            <asp:FileUpload ID="fuExcel" runat="server" CssClass="form-control"
                                accept=".xlsx,.xls" />
                            <small class="text-muted">Formatos permitidos: .xlsx, .xls (Máximo 5MB)</small>
                        </div>
                        <div class="form-group">
                            <div class="alert alert-info">
                                <strong>Por favor, Leer !!!</strong>
                                <strong>Formato esperado del Excel:</strong>
                                <ul>
                                    <li>Columna A: Descripción del artículo/servicio</li>
                                    <li>Columna B: Cantidad</li>
                                    <li>Columna C: Precio Unitario</li>
                                </ul>
                                <p><small>La primera fila debe contener los encabezados</small></p>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>
                        <asp:Button ID="btnProcesarExcel" runat="server" Text="Subir y Procesar"
                            CssClass="btn btn-primary" OnClick="btnProcesarExcel_Click" />
                    </div>
                </div>
            </div>
        </div>


        <script type="text/javascript">

            function validarObservacionAuditoria() {
                var txtObs = document.getElementById('<%= txtObservAuditoria.ClientID %>');
                var observacion = txtObs.value.trim();

                if (observacion === '') {
                    alert('Debe ingresar el motivo de la modificación');
                    txtObs.focus();
                    return false; // Cancela el postback
                }

                if (observacion.length < 10) {
                    alert('El motivo debe tener al menos 10 caracteres');
                    txtObs.focus();
                    return false;
                }

                return true; // Permite el postback
            }
        </script>
</asp:Content>
