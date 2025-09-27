<%@ Page Title="" Language="C#" MasterPageFile="~/MP/Bootstrap.Master" AutoEventWireup="true" CodeBehind="NewOpCC.aspx.cs" Inherits="Web.Secure.NewOpCC" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            position: relative;
            min-height: 1px;
            float: left;
            width: 25%;
            left: 0px;
            top: 0px;
            height: 84px;
            padding-left: 15px;
            padding-right: 15px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <script src="http://www.google.com/jsapi" type="text/jscript"></script>
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.4.4.min.js"></script>
    <script type="text/javascript" src="../App_Themes/Bootstrap/js/apprise-1.5.min.js"></script>
    <script type="text/javascript" src="../App_Themes/Bootstrap/js/appriseGreen-1.5.min.js"></script>
    <script type="text/javascript" src="../App_Themes/Bootstrap/js/appriseRed-1.5.min.js"></script>
    <link rel="stylesheet" href="../App_Themes/Bootstrap/css/apprise.min.css" type="text/css" />




    <div class="container" style="padding-top: 15px;">

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
                <!-- ////////////////////////////// PROVEEDOR //////////////////////////////////////////// -->
                <asp:UpdatePanel ID="uPanelProv" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-xs-3">
                                    <%--                            <div class="form-group">
                                <label for="fecha">Proveedor</label>
                                <asp:TextBox ID="txtIdProv" Text="67" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>--%>
                                    <div>
                                        <label for="fecha">Proveedor</label>
                                        <asp:TextBox ID="txtIdProv" CssClass="form-control" runat="server"
                                            placeholder="Ingrese codigo" AutoPostBack="True"
                                            OnTextChanged="txtIdProv_TextChanged1"></asp:TextBox>
                                        <asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="Debe Ingresar Proveedor" ControlToValidate="txtIdProv" Type="Integer" ValidationGroup="GroupDatos" Operator="DataTypeCheck" SetFocusOnError="True">*</asp:CompareValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtIdProv" ErrorMessage="Debe Seleccionar Proveedor" SetFocusOnError="True" ValidationGroup="GroupDatos">*</asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-xs-9">
                                    <label for="fecha">Razon Social</label>
                                    <div class="input-group">
                                        <input type="text" class="form-control" id="txtNameProv" runat="server" readonly="true" />
                                        <span class="input-group-btn">
                                            <div class="btn-group">
                                                <asp:LinkButton ID="lnkFindProv" CssClass="btn btn-default" runat="server"
                                                    OnClick="btnFindProv_click">
                                            <i class="fa fa-plus"></i> Buscar Proveedor
                                                </asp:LinkButton>
                                            </div>
                                        </span>
                                    </div>
                                    <%--                            <div class="form-group">
                                <label for="fecha">Razon Social</label>
                                <input type="text" class="form-control" id="txtNameProv" runat="server" disabled
                                    value="MUNICIPALIDAD V.ALLENDE -RENDICION" />
                            </div>--%>
                                </div>
                            </div>
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
                                    <asp:CompareValidator ID="CompareValidator5" runat="server" ErrorMessage="Debe ingresar Oficina Detino" ControlToValidate="txtIdDestino" Operator="DataTypeCheck" Type="Integer" ValidationGroup="GroupDatos" SetFocusOnError="True">*</asp:CompareValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtIdDestino" ErrorMessage="Debe Seleccionar Oficina Destino" SetFocusOnError="True" ValidationGroup="GroupDatos">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="col-xs-9">
                                    <label for="fecha">Nombre Oficina</label>
                                    <div class="input-group">
                                        <input type="text" class="form-control" id="txtNameDestino" readonly="true" runat="server" />
                                        <span class="input-group-btn">
                                            <div class="btn-group">
                                                <button class="btn btn-primary btn-sm" type="button"
                                                    id="btnFindDest" runat="server" onserverclick="btnFindDest_click">
                                                    <span class="fa fa-search"></span>Buscar
                                                </button>
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
                    <ContentTemplate>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-xs-6">
                                    <label for="ejemplo_email_1">SecretariaAutoriza</label>
                                    <asp:DropDownList ID="ddlSecretariaAutoriza" runat="server" CssClass="form-control"
                                        AutoPostBack="True" OnSelectedIndexChanged="ddlSecretariaAutoriza_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvSecretariaAutoriza" runat="server"
                                        ErrorMessage="Debe seleccionar Secretaría" ControlToValidate="ddlSecretariaAutoriza"
                                        ValidationGroup="GroupDatos" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                    &nbsp;
                                </div>
                                <div class="col-xs-6">
                                    <label for="ejemplo_email_1">Direccion Solicitante</label>
                                    <asp:DropDownList ID="ddlDireccionSolicitante" runat="server" CssClass="form-control"
                                        AutoPostBack="True" OnSelectedIndexChanged="ddlDireccionSolicitante_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvDireccionSolicitante" runat="server"
                                        ErrorMessage="Debe seleccionar Dirección" ControlToValidate="ddlDireccionSolicitante"
                                        ValidationGroup="GroupDatos" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-xs-6">
                                    <label for="ejemplo_email_1">Autorizado Por</label>
                                    <input type="text" class="form-control" id="txtAut"
                                        runat="server" placeholder="Introduzca el nombre de quien autoriza" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Debe Ingresar Campo Autorizado Por" ControlToValidate="txtAut" ValidationGroup="GroupDatos" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                    &nbsp;
                                </div>
                                <div class="col-xs-6">
                                    <label for="ejemplo_email_1">Solicitante</label>
                                    <input type="text" class="form-control" id="txtSolicitante"
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

                                <div class="col-xs-6">
                                    <label for="ejemplo_email_1">Nro Facturas 000/000/000/000</label>
                                    <input type="text" class="form-control" id="txtNroFactura"
                                        runat="server" placeholder="Introduzca el/los Nro. de Factura/s" />
                                </div>

                            </div>
                        </div>
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
                                <button type="button" class="btn btn-primary"
                                    runat="server" id="btnAddDetalle" onserverclick="btnAddDetalle_click">
                                    <span class="glyphicon glyphicon-plus-sign">Agregar
                                </button>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <!-- ///////////////////////////////////////////////////////////////////////////////////// -->
                <!-- ////////////////////////////// GRILLA DETALLE /////////////////////////////////////// -->
                <asp:UpdatePanel ID="uPanelDetalle" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="gvDetalle" runat="server" AutoGenerateColumns="False" Width="100%"
                            EmptyDataText="No hay detalle agrgado en la Orden de Pedido"
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
            </div>
        </div>
        <!-- ////////////////////////////////// BOTONES ////////////////////////////////////////////// -->
        <div class="form-group" style="text-align: right;">
            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                <ContentTemplate>
                    <div class="btn-group">
                        <button type="button" class="btn btn-primary"
                            runat="server" id="btnSave" onserverclick="btnSave_Click" validationgroup="GroupDatos">
                            <span class="glyphicon glyphicon-floppy-disk"></span>Guardar
                        </button>
                    </div>
                    <div class="btn-group">
                        <button type="button" class="btn btn-primary"
                            runat="server" id="btnExit" onclick="location.href='index.aspx';">
                            <span class="glyphicon glyphicon-log-out"></span>Salir
                        </button>
                    </div>
                    <div class="btn-group">
                        <button type="button" class="btn btn-primary"
                            runat="server" id="btnPrint" onserverclick="btnPrint_Click">
                            <span class="glyphicon glyphicon-print"></span>Imprimir
                        </button>
                    </div>
                    <div class="btn-group">
                        <button type="button" class="btn btn-primary"
                            runat="server" id="btnAddOrden" onserverclick="btnAddOrden_click">
                            <span class="glyphicon glyphicon-plus"></span>Agregar Orden
                        </button>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <!-- ///////////////////////////////////////////////////////////////////////////////////////// -->


        <!-- //POPUP DESTINO////////////////////////////////////////////////////////////////////// -->
        <asp:HiddenField ID="HiddenField1" runat="server" />
        <asp:ModalPopupExtender ID="popUpOfice" runat="server"
            PopupControlID="Div1" TargetControlID="HiddenField1"
            BackgroundCssClass="backgroundColor">
            <Animations>
            <OnShowing>
                <FadeIn Duration=".5" Fps="30" />
            </OnShowing>
            <OnShown>
                <FadeIn Duration=".5" Fps="30" />
            </OnShown>
            <%-- neither animation works from code-behind --%>
            <OnHiding>
                <FadeOut Duration=".5" Fps="30" />
            </OnHiding>
            <OnHidden>
                <FadeOut Duration=".5" Fps="30" />
            </OnHidden>
            </Animations>
        </asp:ModalPopupExtender>
        <div style="padding: 10px; width: 55%; background-color: White; box-shadow: 0px 0px 10px #000;"
            id="Div1" runat="server" class="panel panel-info">
            <br />
            <div class="panel-heading">Buscar Oficina de Destino</div>
            <div class="panel-body">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <br />
                        Destino
                        <div class="input-group">
                            <input type="text" class="form-control" id="txtFindDest" runat="server" />
                            <span class="input-group-btn">
                                <div class="btn-group">
                                    <button class="btn btn-primary btn-sm" type="button"
                                        runat="server" id="btnBuscarDest" onserverclick="btnBuscarDest_click">
                                        <span class="glyphicon glyphicon-search"></span>Buscar
                                    </button>
                                </div>
                            </span>
                        </div>
                        <br />
                        <div style="overflow: scroll; height: 150px;">
                            <asp:GridView ID="gvOficinas" runat="server" AutoGenerateColumns="False"
                                CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%"
                                DataKeyNames="idOficina,nombre"
                                OnRowCommand="gvOficinas_RowCommand" OnRowCreated="gvOficinas_RowCreated"
                                formnovalidate>
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:BoundField DataField="idOficina" HeaderText="Codigo" />
                                    <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                                    <asp:TemplateField HeaderText="Seleccionar">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgbSeleccionar" runat="server" CommandName="selected"
                                                ImageUrl="~/App_Themes/Tema1/Images/masGrilla.gif" CausesValidation="False" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EditRowStyle BackColor="#999999" />
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                            </asp:GridView>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-primary -sm" data-dismiss="modal"
                                runat="server" id="btnCancelDest" onserverclick="btnCancelDest_click">
                                Cancelar</button>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <!-- ///////////////////////////////////////////////////////////////////////////////////// -->
        <!-- ////////////////////////////// POPUP PROVEEDOR ///////////////////////////////////////// -->
        <div class="form-group">
            <asp:HiddenField ID="HiddenField2" runat="server" />
            <asp:ModalPopupExtender ID="Popup1" runat="server"
                PopupControlID="cedulon" TargetControlID="HiddenField2"
                BackgroundCssClass="backgroundColor">
                <Animations>
                <OnShowing>
                    <FadeIn Duration=".5" Fps="30" />
                </OnShowing>
                <OnShown>
                    <FadeIn Duration=".5" Fps="30" />
                </OnShown>
                <%-- neither animation works from code-behind --%>
                <OnHiding>
                    <FadeOut Duration=".5" Fps="30" />
                </OnHiding>
                <OnHidden>
                    <FadeOut Duration=".5" Fps="30" />
                </OnHidden>
                </Animations>
            </asp:ModalPopupExtender>
            <div style="padding: 20px; width: 55%; background-color: White; box-shadow: 0px 0px 10px #000;"
                id="cedulon" runat="server" class="panel panel-info">
                <div class="panel-heading">Buscar Proveedor</div>
                <div class="panel-body">
                    <br />
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <label for="fecha">Razon Social</label>
                            <div class="input-group">
                                <input type="text" class="form-control" id="txtBuscarProv" runat="server" />
                                <span class="input-group-btn">
                                    <div class="btn-group">
                                        <button class="btn btn-primary btn-sm" type="button"
                                            runat="server" id="btnBuscar" onserverclick="btnBuscar_click">
                                            <span class="glyphicon glyphicon-search"></span>Buscar
                                        </button>
                                    </div>
                                </span>
                            </div>
                            <div style="overflow: scroll; height: 150px;">
                                <br />
                                <asp:GridView ID="gvProv" runat="server" AutoGenerateColumns="False"
                                    CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%"
                                    DataKeyNames="codProveedor,nomProveedor"
                                    OnRowCommand="gvProv_RowCommand" OnRowCreated="gvProv_RowCreated"
                                    formnovalidate>
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="codProveedor" HeaderText="Codigo" />
                                        <asp:BoundField DataField="nomProveedor" HeaderText="Nombre" />
                                        <asp:BoundField DataField="nroCuit" HeaderText="CUIT" />
                                        <asp:BoundField DataField="nroBad" HeaderText="Nro BAD" />
                                        <asp:TemplateField HeaderText="Seleccionar">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgbSeleccionar" runat="server" CommandName="selected"
                                                    ImageUrl="~/App_Themes/Tema1/Images/masGrilla.gif" CausesValidation="False" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EditRowStyle BackColor="#999999" />
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-primary -sm" data-dismiss="modal"
                                    runat="server" id="btnCancelProv" onserverclick="btnCancelProv_click">
                                    Cancelar</button>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <!-- ///////////////////////////////////////////////////////////////////////////////////// -->
        <!-- ////////////////////////////// POPUP DETALLE //////////////////////////////////////// -->
        <asp:HiddenField ID="HiddenField3" runat="server" />
        <asp:ModalPopupExtender ID="popUpDetalle" runat="server"
            PopupControlID="Div2" TargetControlID="HiddenField3"
            BackgroundCssClass="backgroundColor">
            <Animations>
            <OnShowing>
                <FadeIn Duration=".5" Fps="30" />
            </OnShowing>
            <OnShown>
                <FadeIn Duration=".5" Fps="30" />
            </OnShown>
            <%-- neither animation works from code-behind --%>
            <OnHiding>
                <FadeOut Duration=".5" Fps="30" />
            </OnHiding>
            <OnHidden>
                <FadeOut Duration=".5" Fps="30" />
            </OnHidden>
            </Animations>
        </asp:ModalPopupExtender>
        <div style="padding: 10px; width: 36%; background-color: White; box-shadow: 0px 0px 10px #000;"
            id="Div2" runat="server" class="panel panel-info">

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
                                        runat="server" id="Button2" onserverclick="btnAceptar_Click" validationgroup="GroupItems">
                                        Aceptar</button>
                                    <button type="button" class="btn btn-sm"
                                        runat="server" id="Button1" onserverclick="btnCancelar_Click">
                                        Cancelar</button>
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

        <asp:HiddenField ID="HiddenField4" runat="server" />
        <asp:ModalPopupExtender ID="popUpdate" runat="server"
            PopupControlID="Div3" TargetControlID="HiddenField4"
            BackgroundCssClass="backgroundColor">
        </asp:ModalPopupExtender>
        <div style="padding: 10px; width: 43%; background-color: White; box-shadow: 0px 0px 10px #000;"
            id="Div3" runat="server" class="panel panel-info">
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

                        <asp:Button ID="btnAceptarPopUpUpdate" runat="server" Text="Aceptar"
                            CssClass="btn btn-sm"
                            OnClick="btnAceptarPopUpdate_Click" CausesValidation="False" />
                        <asp:Button ID="btnCancelarPopUpUdate" runat="server" Text="Cancelar"
                            CssClass="btn btn-sm"
                            CausesValidation="False" OnClick="btnCancelarPopUpUdate_Click" />
                        <br />
                        <br>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Debe Ingresar Descripcion en la Auditoria" ValidationGroup="GroupDatos" ControlToValidate="txtObservAuditoria">*</asp:RegularExpressionValidator>
                        <br />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>


    <asp:HiddenField ID="HiddenField5" runat="server" />
    <ajaxToolkit:ModalPopupExtender ID="popUpListado" runat="server" PopupControlID="Div10" TargetControlID="HiddenField5" BackgroundCssClass="modalBackground">
        <Animations>
                <OnShowing>
                    <FadeIn Duration=".5" Fps="30" />
                </OnShowing>
                <OnShown>
                    <FadeIn Duration=".5" Fps="30" />
                </OnShown>
                <%-- neither animation works from code-behind --%>
                <OnHiding>
                    <FadeOut Duration=".5" Fps="30" />
                </OnHiding>
                <OnHidden>
                    <FadeOut Duration=".5" Fps="30" />
                </OnHidden>
        </Animations>
    </ajaxToolkit:ModalPopupExtender>
    <div class="row" id="Div10" style="background-color: White; width: 70%; padding: 15px; border-radius: 12px; padding-top: 0px;">
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
    </div>

</asp:Content>
