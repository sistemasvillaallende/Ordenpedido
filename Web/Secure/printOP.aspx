<%@ Page Language=jscript debug=true AutoEventWireup="true" CodeBehind="printOP.aspx.cs" 
Inherits="Web.Secure.printOP"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../App_Themes/Tema1/grilla.css" rel="Stylesheet" type="text/css"/>

    <style type="text/css">
        .auto-style1 {
            width: 106px;
            height: 70px;
        }
    </style>

</head>
<body id="cuerpoPagina" style="margin: 0 0 0" onload="Imp()"; >
<object id="impr" viewastext style="display:none"
classid="clsid:1663ed61-23eb-11d2-b92f-008048fdd814"
codebase="ScriptX.cab#Version=6,1,429,14">
</object>
<script type="text/javascript">
    function Imp() {
        document.getElementById("div").style.width = '100%';
        document.getElementById("btnPrint").style.display = 'none';    
        document.getElementById('cuerpoPagina').style.marginRight = "0";
        document.getElementById('cuerpoPagina').style.marginTop = "0";
        document.getElementById('cuerpoPagina').style.marginLeft = "0";
        document.getElementById('cuerpoPagina').style.marginBottom = "0";
        window.print(this); //dialogo de impresion
        //window.close(this);  
        window.onfocus = function () {
            redireccionar();
        };
    }


    function redireccionar() {
        window.locationf = "findOp.aspx";
        }


    function Imprimir(){
        document.getElementById("div").style.width = '100%';        
        document.getElementById("btnPrint").style.display='none';        
        window.print(); window.close();  
    }
</script>
<script type="text/javascript">
    //impr.printing.header = ""
    //impr.printing.footer = ""
    //impr.printing.topMargin = 0
    //impr.printing.bottomMargin = 0
    //impr.printing.leftMargin = 0
    //impr.printing.rightMargin = 0
</script>
    <form id="form1" runat="server" style="text-align:center; margin:50px;" >
    <br />
        <div id="div" style="width:100%; text-align:center; height: 90%;" >
            <div style="float:left; width:45%; min-height:80px; 
                text-align:left; height: 65px;">
                <img src="../App_Themes/Tema1/Images/GESTION%20vertical.png" alt="Logo" class="auto-style1"/>
            </div>
            <div style="float:right; width:45%; min-height:100px; 
                height: 65px; text-align:right;">
                Nro: <asp:Label ID="lblNroOrden" runat="server" Text="1744" Width="100px"></asp:Label>
                <br />
                <br />
                Fecha OP:  <asp:Label ID="lblFecha" runat="server" Text="14/04/2014" Width="100px"></asp:Label>
            </div>
            <div style="text-align:center; clear:both;" >
                <h1>ORDEN DE PEDIDO</h1>
            </div>
            <div style="text-align:center; clear:both;" >
                <asp:GridView ID="gvDetalle" runat="server" 
                    Width="100%" AutoGenerateColumns="False"
                    CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                    <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                    <Columns>
                        <asp:BoundField HeaderText="Anexo" NullDisplayText="Anexo" ReadOnly="True" >
                        <HeaderStyle/>
                        <ItemStyle Font-Size="Smaller" Width="10px" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Inciso" >
                        <ItemStyle Width="10px" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="P.Ppal." >
                        <ItemStyle Width="10px" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Item" >
                        <ItemStyle Width="10px" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="S.Item" >
                        <ItemStyle Width="10px" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Part" >
                        <ItemStyle Width="10px" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="S.Part" >
                        <ItemStyle Width="10px" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Descripcion" DataField="descItems">
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Cant" DataField="cant"/>
                        <asp:BoundField HeaderText="P.Unitario" DataField="precio" 
                            DataFormatString="{0:C}">
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Subtotal" DataField="importe" 
                            DataFormatString="{0:C}">
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                    </Columns>
                    <HeaderStyle 
                        Font-Names="Calibri" Font-Size="Smaller" />
                        <PagerStyle CssClass="pgr"></PagerStyle>
                        <RowStyle Font-Names="Calibri" Font-Size="Smaller" />
                </asp:GridView>

                
            </div>
           
            <!--Total -->    
            <div style="width:100%; 
                text-align:right;">
                <asp:Label ID="Label6" runat="server" Text="Total: " Font-Size="10pt"></asp:Label>
                <asp:Label ID="lblTotal" runat="server" Font-Size="10pt"></asp:Label>
            </div>
             <br /><br />
            <div style="width:100%; border-top-style:solid; 
                text-align:left;">
                <br />
                <asp:Label ID="Label3" runat="server" Text="Observaciones:" Font-Size="10pt" 
                    Font-Bold="True" Font-Overline="False" Font-Underline="True"></asp:Label>
                <br />
                <asp:Label ID="lblObs" runat="server" Width="100%" Font-Size="10pt" 
                    Height="25px"></asp:Label>
            <!--Observaciones -->
                <br /><br />
                <asp:Label ID="Label4" runat="server" Text="Nº Presupuesto:" Width="300px" 
                    Font-Size="10pt" Font-Bold="True" Font-Strikeout="False" 
                    Font-Underline="True"></asp:Label>
                <asp:Label ID="Label5" runat="server" Text="Nº Facturas:" Font-Size="10pt" 
                    Font-Bold="True" Font-Strikeout="False" Font-Underline="True"></asp:Label>
                <br />
                <asp:Label ID="lblPresupuesto" runat="server" Width="300px" Font-Size="10pt"></asp:Label>
                <asp:Label ID="lblFacturas" runat="server" Font-Size="10pt"></asp:Label>
                <br /><br />
                <asp:Label ID="Label7" runat="server" Text="Forma de Pago:" Width="100%" 
                    Font-Size="10pt" Font-Bold="True" Font-Strikeout="False" 
                    Font-Underline="True"></asp:Label>
                <br />
                <asp:Label ID="lblFormaPago" runat="server" Text="Label" Width="100%" 
                    Font-Size="10pt"></asp:Label>
                <br /><br />
            </div>
            <div style="float:left; width:50%; min-height:80px; 
                text-align:left; height: 100px;">
                <asp:Label ID="Label8" runat="server" Text="Proveedor:" Width="100px" 
                    Font-Size="10pt" Font-Bold="True"></asp:Label>
                <asp:Label ID="lblProv" runat="server" Font-Size="10pt"></asp:Label>
                <br />
                <asp:Label ID="Label10" runat="server" Text="Destino:" Width="100px" 
                    Font-Size="10pt" Font-Bold="True"></asp:Label>
                <asp:Label ID="lblDestino" runat="server" Font-Size="10pt"></asp:Label>
                <br />
                <asp:Label ID="Label12" runat="server" Text="Origen:" Width="100px" 
                    Font-Size="10pt" Font-Bold="True"></asp:Label>
                <asp:Label ID="lblOrigen" runat="server" Font-Size="10pt"></asp:Label>
                <br />
                <asp:Label ID="Label14" runat="server" Text="Solicito:" Width="100px" 
                    Font-Size="10pt" Font-Bold="True"></asp:Label>
                <asp:Label ID="lblSolicito" runat="server" Font-Size="10pt"></asp:Label>
                <br />
                <asp:Label ID="Label16" runat="server" Text="Aprobo:" Width="100px" 
                    Font-Size="10pt" Font-Bold="True"></asp:Label>
                <asp:Label ID="lblAprobo" runat="server" Font-Size="10pt"></asp:Label>
            </div>
            <div style="float:right; width:45%; min-height:100px; 
                height: 100px; text-align:right;">
                <asp:Image ID="Image1" runat="server" ImageUrl="~/App_Themes/Tema1/Images/Visado.png"/>
            </div>

    </div>
    </form>
    <p>
        <input id="btnPrint" type="button" value="Imprimir" onclick="Imp()"/></p> 
</body>
</html>
