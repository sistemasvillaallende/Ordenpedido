<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExportPdf.aspx.cs" 
Inherits="Web.Secure.ExportPdf" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align:center; margin-left 100px;">
        <div id="head" style="width:80%; height:50px; background-color:#CCFFCC"CCFFCC";">
            <h1>Exportar a PDF</h1>
        </div>
    </div>
    <br />
                <asp:GridView ID="gvOrdenes" runat="server" CellPadding="4" ForeColor="#333333" 
                    GridLines="None" Width="100%" AutoGenerateColumns="False" 
                    onrowdatabound="gvOrdenes_RowDataBound" onrowcommand="gvOrdenes_RowCommand"
                    DataKeyNames="nroOrden" EnableViewState="False">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="nroOrden" HeaderText="Nº Orden" />
                        <asp:BoundField DataField="formatFec" HeaderText="Fecha" />
                        <asp:BoundField DataField="proveedor" HeaderText="Proveedor" />
                        <asp:BoundField DataField="destino" HeaderText="Destino" />
                        <asp:BoundField DataField="usuario" HeaderText="Usuario" />
                        <asp:BoundField DataField="anulado" HeaderText="Anolado" />
                        <asp:BoundField DataField="finalizado" HeaderText="Finalizado" />
                        <asp:BoundField DataField="obs" HeaderText="Observaciones" />
                        <asp:BoundField DataField="total" HeaderText="Total" />
                        <asp:TemplateField HeaderText="Edicion">
                            <ItemTemplate>
                                <asp:ImageButton ID="ImageButton2" runat="server" 
                                ImageUrl="~/App_Themes/Tema1/Images/editar.gif"
                                CommandName="edit"
                                CommandArgument='<%# Eval("nroOrden") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>
    <br />
    <br />
                <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
        Text="Button" />
    <asp:Button ID="Button2" runat="server" onclick="Button2_Click" Text="Button" />
    </form>
</body>
</html>
