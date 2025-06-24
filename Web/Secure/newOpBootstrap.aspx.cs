using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Entities;
namespace Web.Secure
{
    public partial class newOpBootstrap : System.Web.UI.Page
    {
        List<Entities.DetalleOrden> lstDetalle = new List<DetalleOrden>();
        Entities.OrdenPedido oOp = new Entities.OrdenPedido();

        WSAFIP.WSAFIPSoapClient WSAfip = new WSAFIP.WSAFIPSoapClient();

        private List<DAL.FACTURA_X_ORDEN_PEDIDO> leerGrillaFacturas()
        {
            List<DAL.FACTURA_X_ORDEN_PEDIDO> lst = new List<DAL.FACTURA_X_ORDEN_PEDIDO>();
            for (int i = 0; i < gvFacturas.Rows.Count; i++)
            {
                GridViewRow row = gvFacturas.Rows[i];
                DAL.FACTURA_X_ORDEN_PEDIDO obj = new DAL.FACTURA_X_ORDEN_PEDIDO();
                obj.ID = int.Parse(gvFacturas.DataKeys[i].Values["ID"].ToString());
                obj.FECHA_EMISION = Convert.ToDateTime(
                    gvFacturas.DataKeys[i].Values["FECHA_EMISION"].ToString());
                obj.PUNTO_VENTA = Convert.ToInt32(gvFacturas.DataKeys[i].Values["PUNTO_VENTA"].ToString());
                obj.NRO_COMPROBANTE = Convert.ToInt64(gvFacturas.DataKeys[i].Values["NRO_COMPROBANTE"]);
                obj.NRO_CAE = Int64.Parse(gvFacturas.DataKeys[i].Values["NRO_CAE"].ToString());
                obj.IMPORTE = decimal.Parse(gvFacturas.DataKeys[i].Values["IMPORTE"].ToString());
                obj.TIPO_COMPROBANTE = int.Parse(gvFacturas.DataKeys[i].Values["TIPO_COMPROBANTE"].ToString());
                obj.TIP_COMP = gvFacturas.DataKeys[i].Values["TIP_COMP"].ToString();
                obj.COMP_COMPLETO = string.Format("{0}-{1}", obj.PUNTO_VENTA.ToString().PadLeft(4, Convert.ToChar("0")),
                    obj.NRO_COMPROBANTE.ToString().PadLeft(8, Convert.ToChar("0")));
                obj.TIP_COMP = gvFacturas.DataKeys[i].Values["TIP_COMP"].ToString();
                obj.NRO_CUIT_PROVEEDOR = gvFacturas.DataKeys[i].Values["NRO_CUIT_PROVEEDOR"].ToString();
                lst.Add(obj);
            }
            //DDLFactura.DataValueField = "COMP_COMPLETO";
            //DDLFactura.DataTextField = "COMP_COMPLETO";
            //DDLFactura.DataSource = lst;
            //DDLFactura.DataBind();
            return lst;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["UserOP"] == null)
            {   // La cookie no existe
                Response.Redirect("../Login.aspx");
            }
            //
            if (!IsPostBack)
            {
                fillFacturas(new List<DAL.FACTURA_X_ORDEN_PEDIDO>());
                fillDetalle(new List<DetalleOrden>());
                btnPrint.Visible = false;
                btnAddOrden.Visible = false;
                txtFechaOp.InnerHtml = BLL.OrdenPedidoBLL.FechaServer();
                //DateTime.Now.ToString();
                if (Request.Cookies["UserOP"]["id_oficina_usuario"] != null)
                {
                    P1.InnerText = Request.Cookies["UserOP"]["id_oficina_usuario"].ToString() + " - " +
                    BLL.OficinasBLL.getOficinaByPk(Convert.ToInt32(
                        Request.Cookies["UserOP"]["id_oficina_usuario"])).nombre;
                }
                Session.Add("Detalle", lstDetalle);
                Session.Add("Total", 0);
                Session.Add("opcion", 0);
                Session.Add("index", 0);
                Session.Add("ordenPedido", null);
                Session.Add("nroOrden", 0);
                if (Request.QueryString["op"] != null)
                {
                    int op = int.Parse(Request.QueryString["op"]);
                    if (op != 0)
                    {
                        fillDatos(BLL.OrdenPedidoBLL.getOrdenesByPk(op));
                    }
                }
                txtIdProv.Focus();
                DDLTipoComprobante.SelectedIndex = 5;


            }
        }


        protected void fillDatos(Entities.OrdenPedido oOp)
        {
            txtOP.InnerText = oOp.nroOrden.ToString();
            txtFechaOp.InnerText = oOp.fechaOrden.ToShortDateString();
            txtIdProv.Text = oOp.codProveedor.ToString();
            txtNameProv.Value = oOp.proveedor;
            P1.InnerText = oOp.codOficinaOrigen.ToString();
            txtCUITProveedor.Value = oOp.CUIT;
            txtCUIT.Text = oOp.CUIT;
            txtAut.Value = oOp.aprobado;
            txtSolicitante.Value = oOp.solicitante;
            txtIdDestino.Text = oOp.codOficinaDestino.ToString();
            txtNameDestino.Value = oOp.destino;
            txtObs.Value = oOp.obs;
            txtFormaPago.Value = oOp.formaPago;
            txtNroPresup.Value = oOp.nroPresupuesto;
            txtNroFactura.Value = oOp.nroFacturas;
            fillDetalle(oOp.detalle);
            decimal total = 0;
            for (int i = 0; i < oOp.detalle.Count; i++)
            {
                total += decimal.Round(oOp.detalle[i].importe, 2);
            }

            List<DAL.FACTURA_X_ORDEN_PEDIDO> lstFacturas = DAL.FACTURA_X_ORDEN_PEDIDO.read(oOp.nroOrden);
            fillFacturas(lstFacturas);
            lblTotal.InnerText = "$" + total.ToString();
            Session["Detalle"] = oOp.detalle;
            Session["Total"] = total;

            txtIdProv.Enabled = false;
            txtNameProv.Disabled = true;
            lnkFindProv.Enabled = false;
        }


        //ACCION DE LA GRILLA PROVEEDORES QUE ASIGNA LOS VALORES DE LA FILA SELECCIONADA A LOS//////////// 
        //TEXTOS ID Y NOMBRE PROVEEDOR                                                                   
        protected void gvProv_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int i = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "selected")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                txtNameProv.Value = (string)(gvProv.DataKeys[index].Values["nomProveedor"]);
                txtIdProv.Text = gvProv.DataKeys[index].Values["codProveedor"].ToString();
                txtCUITProveedor.Value = (string)(gvProv.DataKeys[index].Values["nroCuit"]);
                txtCUIT.Text = (string)(gvProv.DataKeys[index].Values["nroCuit"]);
                popUpProveedor.Hide();
                gvProv.DataSource = null;
                DataBind();
                txtBuscarProv.Value = string.Empty;
                uPanelProv.Update();
                UPanelDatos.Update();
            }
        }

        protected void gvProv_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton imgbtn;

                imgbtn = (ImageButton)e.Row.FindControl("imgbSeleccionar");
                if (imgbtn != null)
                {
                    imgbtn.CommandArgument = e.Row.RowIndex.ToString();
                }
            }
        }

        protected void btnBuscar_click(object sender, EventArgs e)
        {
            fillProveedores(txtBuscarProv.Value);
        }
        //BOTON QUE ABRE EL POPUP BUSCAR PROVEEDOR////////////////////////
        protected void btnFindProv_click(object sender, EventArgs e)
        {
            UpdatePanel4.Update();
            txtBuscarProv.Focus();
            popUpProveedor.Show();
        }


        //METODO QUE CARGA LA GRILLA DE PROVEEDORES///////////////////////////////////////////////////
        protected void fillProveedores(string nombre)
        {
            List<Entities.Proveedores> lstProv = BLL.ProveedoreBLL.findProveedorByNombre(nombre);
            gvProv.DataSource = lstProv;
            gvProv.DataBind();
        }


        protected void btnCancelProv_click(object sender, EventArgs e)
        {
            popUpProveedor.Dispose();
            popUpProveedor.Hide();
        }


        protected void btnFindDest_click(object sender, EventArgs e)
        {
            UpdatePanel3.Update();
            txtFindDest.Focus();
            popUpOficina.Show();
        }


        protected void btnBuscarDest_click(object sender, EventArgs e)
        {
            fillOficinas(txtFindDest.Value);
        }

        protected void fillOficinas(string nombre)
        {
            List<Entities.Oficinas> lstOfice = BLL.OficinasBLL.findOficinasByNombre(nombre);
            gvOficinas.DataSource = lstOfice;
            gvOficinas.DataBind();
        }

        protected void gvOficinas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int i = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "selected")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                txtNameDestino.Value = gvOficinas.DataKeys[index].Values["nombre"].ToString();
                txtIdDestino.Text = gvOficinas.DataKeys[index].Values["idOficina"].ToString();
                popUpOficina.Dispose();
                popUpOficina.Hide();
                gvOficinas.DataSource = null;
                gvOficinas.DataBind();
                txtFindDest.Value = string.Empty;
                UpdatePanel3.Update();
                UPanelDestino.Update();
            }
        }

        protected void gvOficinas_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton imgbtn;

                imgbtn = (ImageButton)e.Row.FindControl("imgbSeleccionar");
                if (imgbtn != null)
                {
                    imgbtn.CommandArgument = e.Row.RowIndex.ToString();
                }
            }
        }

        protected void btnCancelDest_click(object sender, EventArgs e)
        {
            popUpOficina.Dispose();
            popUpOficina.Hide();
        }

        protected void btnAddDetalle_click(object sender, EventArgs e)
        {
            CleanCamposDetalle();
            popUpDetalle.Show();
        }

        protected void CleanCamposDetalle()
        {
            txtDescripcion.Text = string.Empty;
            txtCantidad.Text = string.Empty;
            txtPU.Text = string.Empty;
            UpdatePanel5.Update();
            txtDescripcion.Focus();
        }

        protected void txtIdProv_TextChanged1(object sender, EventArgs e)
        {
            Proveedores oProveedor = new Proveedores();
            int cod;
            try
            {
                cod = int.Parse(txtIdProv.Text);
                oProveedor = BLL.ProveedoreBLL.getProveedorByPk(cod);
                if (oProveedor != null)
                {
                    txtIdProv.Text = oProveedor.codProveedor.ToString();
                    txtNameProv.Value = oProveedor.nomProveedor;
                    txtCUITProveedor.Value = oProveedor.nroCuit;
                    txtCUIT.Text = oProveedor.nroCuit;
                    //lblError.Text = string.Empty;
                    //UpdateError.Update();
                    UPanelDatos.Update();
                }
                /*else
                {
                  string script =  @"<script type='text/javascript'> 
                    apprise('El numero ingresado no corresponde a un proveedor',{'animate':true}); </script>";
                  ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
                  txtIdProv.Text = string.Empty;
                  txtNameProv.Value = string.Empty;
                  txtIdProv.Focus();
                }*/
            }
            catch (Exception)
            {
                //lblError.Text = "Verifique que el dato ingresado sea numerico";
                //UpdateError.Update();
                txtIdProv.Text = string.Empty;
                txtNameProv.Value = string.Empty;
                txtIdProv.Focus();
            }
        }

        protected void txtIdDestino_TextChanged1(object sender, EventArgs e)
        {
            Oficinas oOfice = new Oficinas();
            int cod;
            try
            {
                cod = int.Parse(txtIdDestino.Text);
                oOfice = BLL.OficinasBLL.getOficinaByPk(cod);
                if (oOfice != null)
                {
                    txtIdDestino.Text = oOfice.idOficina.ToString();
                    txtNameDestino.Value = oOfice.nombre;
                    //lblError.Text = string.Empty;
                    //UpdateError.Update();
                }
                else
                {
                    string script = @"<script type='text/javascript'> 
            apprise('El numero ingresado no corresponde a una Oficina',{'animate':true}); </script>";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
                    txtIdDestino.Text = string.Empty;
                    txtNameDestino.Value = string.Empty;
                    txtIdDestino.Focus();
                }
            }
            catch (Exception)
            {
                //lblError.Text = "Verifique que el dato ingresado sea numerico";
                //UpdateError.Update();
                txtIdDestino.Text = string.Empty;
                txtNameDestino.Value = string.Empty;
                txtIdDestino.Focus();
            }
        }

        protected void gvDetalle_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "deleterow")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                List<Entities.DetalleOrden> lstDetalle = (List<Entities.DetalleOrden>)Session["Detalle"];
                decimal total = Convert.ToDecimal(Session["Total"]);
                total -= lstDetalle[index].importe;
                lblTotal.InnerText = "TOTAL: $" + total.ToString();
                lstDetalle.RemoveAt(index);
                Session["Detalle"] = lstDetalle;
                Session["Total"] = total;
                fillDetalle(lstDetalle);
            }
            if (e.CommandName == "editrow")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                List<Entities.DetalleOrden> lstDetalle = (List<Entities.DetalleOrden>)Session["Detalle"];
                txtDescripcion.Text = lstDetalle[index].descItems;
                txtCantidad.Text = lstDetalle[index].cant.ToString();
                txtPU.Text = lstDetalle[index].precio.ToString();
                Session["opcion"] = 1;
                Session["index"] = index;
                UpdatePanel5.Update();
                popUpDetalle.Show();
            }
        }

        protected void gvDetalle_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.BackColor = System.Drawing.Color.FromArgb(0, 205, 248, 241);
                e.Row.Height = Unit.Pixel(34);
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton imgbtn;
                imgbtn = (ImageButton)e.Row.FindControl("imgbDelete");
                if (imgbtn != null)
                    imgbtn.CommandArgument = e.Row.RowIndex.ToString();
                ImageButton imgbtnEdit;
                imgbtnEdit = (ImageButton)e.Row.FindControl("imgbEdit");
                if (imgbtnEdit != null)
                    imgbtnEdit.CommandArgument = e.Row.RowIndex.ToString();
            }
        }

        protected void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            /* try
            {
              string pu = txtCantidad.Text;
              txtCantidad.Text = pu.Replace(".", ",");
              decimal can = decimal.Parse(txtCantidad.Text);
            }
            catch
            {
              string script = @"<script type='text/javascript'> 
              apprise('Tipo de dato Incorrecto',{'animate':true}); </script>";
              ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
              txtCantidad.Text = string.Empty;
              txtCantidad.Focus();
            }*/
            txtPU.Focus();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            popUpDetalle.Hide();
            Button1.Focus();
        }

        protected void txtPU_TextChanged(object sender, EventArgs e)
        {
            /*try
            {
              string pu = txtPU.Text;
              txtPU.Text = pu.Replace(".", ",");
              decimal can = decimal.Parse(txtPU.Text);
            }
            catch
            {
              string script =@"<script type='text/javascript'> apprise('Tipo de dato Incorrecto',{'animate':true}); </script>";
              ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
              txtPU.Text = string.Empty;
              txtPU.Focus();
            }*/
            Button2.Focus();
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            if (txtCantidad.Text.Trim() == string.Empty ||
                txtDescripcion.Text.Trim() == string.Empty || txtPU.Text == string.Empty)
            {
                string script =
                @"<script type='text/javascript'> apprise('Complete los datos Solicitados',{'animate':true}); </script>";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
            }
            else
            {
                lstDetalle = (List<Entities.DetalleOrden>)Session["Detalle"];
                Entities.DetalleOrden detalle = new DetalleOrden();
                detalle.descItems = txtDescripcion.Text;
                detalle.cant = Convert.ToDecimal(txtCantidad.Text);
                detalle.precio = Convert.ToDecimal(txtPU.Text);
                detalle.importe = decimal.Round((detalle.precio * detalle.cant), 2);

                if ((int)Session["opcion"] == 0)
                    lstDetalle.Add(detalle);
                else
                {
                    lstDetalle[(int)Session["index"]].descItems = detalle.descItems;
                    lstDetalle[(int)Session["index"]].cant = detalle.cant;
                    lstDetalle[(int)Session["index"]].precio = detalle.precio;
                    lstDetalle[(int)Session["index"]].importe = detalle.importe;
                }
                decimal total = 0;
                foreach (DetalleOrden det in lstDetalle)
                {
                    total += det.importe;
                }
                Session["Detalle"] = lstDetalle;
                Session["Total"] = total;
                Session["opcion"] = 0;

                lblTotal.InnerText = "TOTAL: $" + total.ToString();
                fillDetalle(lstDetalle);
                CleanCamposDetalle();
                txtDescripcion.Focus();
                popUpDetalle.Show();

            }
        }

        protected void fillDetalle(List<Entities.DetalleOrden> lstDetalle)
        {
            gvDetalle.DataSource = lstDetalle;
            gvDetalle.DataBind();
            uPanelDetalle.Update();
        }

        protected void fillFacturas(List<DAL.FACTURA_X_ORDEN_PEDIDO> lstFactura)
        {
            gvFacturas.DataSource = lstFactura;
            gvFacturas.DataBind();
            UPanelDatos.Update();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int op = 0;
            decimal totaldetalle = 0;
            //
            Entities.OrdenPedido oOrden = new Entities.OrdenPedido();
            //
            oOrden.anulado = false;
            //
            if (txtAut.Value.Trim().ToUpper() != string.Empty)
                oOrden.aprobado = txtAut.Value.Trim().ToUpper();

            if (txtIdDestino.Text != string.Empty)
                oOrden.codOficinaDestino = int.Parse(txtIdDestino.Text);


            oOrden.codOficinaOrigen = int.Parse(Request.Cookies["UserOP"]["id_oficina_usuario"].ToString());
            if (txtIdProv.Text != string.Empty)
                oOrden.codProveedor = int.Parse(txtIdProv.Text);
            /*else
            {
              string script = @"<script type='text/javascript'> apprise('Debe Ingresar el Proveedor',{'animate':true});
                </script>"; ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
              txtIdProv.Focus();
              return;
            }*/
            //
            oOrden.fechaOrden = Convert.ToDateTime(txtFechaOp.InnerText); //DateTime.Now;
                                                                          //
            if (txtFormaPago.Value.Trim().ToUpper() != string.Empty)
                oOrden.formaPago = txtFormaPago.Value.Trim().ToUpper();


            if (txtNroPresup.Value.Trim().ToUpper() != string.Empty)
                oOrden.nroPresupuesto = txtNroPresup.Value.Trim().ToUpper();

            //NRO FACTURA                                                                                               
            if (txtNroFactura.Value.Trim().ToUpper() != string.Empty)
                oOrden.nroFacturas = txtNroFactura.Value.Trim().ToUpper();

            //SOLICITANTE                                                                                               
            if (txtSolicitante.Value.Trim().ToUpper() != string.Empty)
                oOrden.solicitante = txtSolicitante.Value.Trim().ToUpper();

            if (txtObs.Value.Trim().ToUpper() != string.Empty)
                oOrden.obs = txtObs.Value.Trim().ToUpper();
            else
                oOp.obs = string.Empty;

            List<Entities.DetalleOrden> lstDetalle;
            lstDetalle = (List<Entities.DetalleOrden>)Session["Detalle"];

            if (lstDetalle.Count == 0)
            {
                divConsultaError.Visible = true;
                HtmlGenericControl li = new HtmlGenericControl();
                li.InnerText = "Debe agregar al menos un Items al detalle";
                ulErrores.Controls.Add(li);
                UPanelDatos.Update();
                return;
            }
            //oOrden.total = Convert.ToDecimal(Session["Total"]);
            //oOrden.usuario = Request.Cookies["UserOP"]["usuario"].ToString();
            for (int i = 0; i < lstDetalle.Count; i++)
            {
                lstDetalle[i].nroItems = i + 1;
                oOrden.detalle.Add(lstDetalle[i]);
            }
            //asigno a la ordendepedido el importe total
            //para que ambas tanto la OP, como el Detalle sumen el mismo importe
            totaldetalle = lstDetalle.Sum(ent => ent.importe);
            oOrden.total = totaldetalle;
            oOrden.usuario = Request.Cookies["UserOP"]["usuario"].ToString();
            //


            if (Request.QueryString["op"] != null)
                op = (Request.QueryString["op"].Length == 0) ? 0 : int.Parse(Request.QueryString["op"]);

            if (op == 0)                                                                                                //
            {
                List<DAL.FACTURA_X_ORDEN_PEDIDO> lstFacturas = leerGrillaFacturas();
                if (lstFacturas.Count == 0)
                {
                    divConsultaError.Visible = true;
                    HtmlGenericControl li = new HtmlGenericControl();
                    li.InnerText = "Debe agregar al menos una Factura a la orden";
                    ulErrores.Controls.Add(li);
                    UPanelDatos.Update();
                    return;
                }
                //COMPROBAR MONTOS
                if (lstDetalle.Sum(det => det.importe) != lstFacturas.Sum(fact => fact.IMPORTE))
                {
                    divConsultaError.Visible = true;
                    HtmlGenericControl li = new HtmlGenericControl();
                    li.InnerText = "El importe total de facturas no coinside con el importe total cargado en el detalle";
                    ulErrores.Controls.Add(li);
                    UPanelDatos.Update();
                    return;
                }
                oOrden.web = 1;

                Int64 nroOrden = BLL.OrdenPedidoBLL.Insert(oOrden, lstFacturas);
                oOrden.nroOrden = Convert.ToInt32(nroOrden);                                                            //
                txtOP.InnerText = oOrden.nroOrden.ToString();                                                           //
                string script =
                @"<script type='text/javascript'> 
                appriseGreen('La orden a sido ingresada de forma correcta',{'animate':true});</script>";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
                Session["ordenPedido"] = oOrden;
                //Response.Redirect("Home.aspx");
            }
            else
            {
                Session["ordenPedido"] = oOrden;
                txtObservAuditoria.Focus();
                popUpAuditoria.Show();
            }
            btnPrint.Visible = true;
            btnAddOrden.Visible = true;
            btnSave.Visible = false;
            txtOP.InnerText = oOrden.nroOrden.ToString();
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            Entities.OrdenPedido objOrden = (Entities.OrdenPedido)Session["ordenPedido"];
            int cod = objOrden.nroOrden;
            Session["ordenPedido"] = BLL.OrdenPedidoBLL.getOrdenesByPk(cod);
            //string script = "window.open('printOp.aspx','Orden de Pedido', 'width=700,height=600,scrollbars=NO');";
            //ScriptManager.RegisterStartupScript(this, typeof(Page), "popup", script, true);

            divReporte.InnerHtml = "<iframe src=\" " +
             string.Format("../reportes/print.aspx?nroOrden_pedido={0}", cod) + "\"  width=\"100%\" height=\"600\"></iframe>";
            popUpListado.Show();
        }
        //string.Format("../impresiones/consultaexpedientes.aspx?fecha_desde={0}&fecha_hasta={1}&cod_desde={2}&cod_hasta={3}&tipo={4}&agrupado={5}",
        //   txtFecha_desde.Text, txtFecha_hasta.Text, cod_desde, cod_hasta, "estado", agrupado) + "\"  width=\"100%\" height=\"600\"></iframe>";
        //  popUpListado.Show();

        protected void btnCloseListado_Click(object sender, EventArgs e)
        {
            popUpListado.Hide();
        }


        protected void btnAddOrden_click(object sender, EventArgs e)
        {
            //BOTONES/////////////////////////////////////////////////////////////////////////
            btnSave.Visible = true;                                                         //
            btnExit.Visible = true;                                                         //
            btnPrint.Visible = false;                                                       //
            btnAddOrden.Visible = false;                                                    //
                                                                                            //txtFechaOp.InnerText = DateTime.Now.ToShortDateString();                      
            txtFechaOp.InnerText = BLL.OrdenPedidoBLL.FechaServer();
            if (Request.Cookies["UserOP"]["id_oficina_usuario"] != null)
            {
                P1.InnerText = Request.Cookies["UserOP"]["id_oficina_usuario"].ToString() + " - " +
                BLL.OficinasBLL.getOficinaByPk(Convert.ToInt32(
                    Request.Cookies["UserOP"]["id_oficina_usuario"])).nombre;
                txtOP.InnerText = "0000";
            }
            //////////////////////////////////////////////////////////////////////////////////
            //DATOS///////////////////////////////////////////////////////////////////////////
            txtIdProv.Text = string.Empty;                                                  //
            txtNameProv.Value = string.Empty;                                               //
            txtIdDestino.Text = string.Empty;                                               //
            txtNameDestino.Value = string.Empty;                                            //
            txtAut.Value = string.Empty;                                                    //
            txtSolicitante.Value = "";                                                      //
            txtFormaPago.Value = string.Empty;                                              //
            txtNroPresup.Value = string.Empty;                                              //
            txtNroFactura.Value = string.Empty;                                             //
            txtObs.Value = string.Empty;
            Session["Detalle"] = new List<DetalleOrden>();
            fillDetalle(new List<DetalleOrden>());
            UPanelDestino.Update();                                                         //
            uPanelDetalle.Update();                                                         //
            uPanelProv.Update();
            UPanelDatos.Update();
        }                                                                                  //

        protected void lbtnAddDetalle_Click(object sender, EventArgs e)
        {
            txtDescripcion.Text = string.Empty;
            txtCantidad.Text = string.Empty;
            txtPU.Text = string.Empty;
            UpdatePanel5.Update();
            txtDescripcion.Focus();
            popUpDetalle.Show();
        }

        protected void btnAceptarAuditoria_Click(object sender, EventArgs e)
        {
            Entities.OrdenPedido oOrden = (Entities.OrdenPedido)Session["ordenPedido"];
            oOrden.nroOrden = int.Parse(Request.QueryString["op"]);
            if (txtObservAuditoria.Text.Trim() != string.Empty)
            {
                oOrden.obsAuditoria = txtObservAuditoria.Text.Trim().ToUpper();

                List<DAL.FACTURA_X_ORDEN_PEDIDO> lstFacturas = leerGrillaFacturas();
                if (lstFacturas.Count == 0)
                {
                    divConsultaError.Visible = true;
                    HtmlGenericControl li = new HtmlGenericControl();
                    li.InnerText = "Debe agregar al menos una Factura a la orden";
                    ulErrores.Controls.Add(li);
                    UPanelDatos.Update();
                    return;
                }
                List<Entities.DetalleOrden> lstDetalle;
                lstDetalle = (List<Entities.DetalleOrden>)Session["Detalle"];
                //COMPROBAR MONTOS
                if (lstDetalle.Sum(det => det.importe) != lstFacturas.Sum(fact => fact.IMPORTE))
                {
                    divConsultaError.Visible = true;
                    HtmlGenericControl li = new HtmlGenericControl();
                    li.InnerText = "El importe total de facturas no coinside con el importe total cargado en el detalle";
                    ulErrores.Controls.Add(li);
                    UPanelDatos.Update();
                    return;
                }

                oOrden.web = 1;
                BLL.OrdenPedidoBLL.Update(oOrden, lstFacturas);
                popUpAuditoria.Hide();
                Session["ordenPedido"] = oOrden;
            }
        }

        protected void btnCancelarAuditoria_Click(object sender, EventArgs e)
        {
            popUpAuditoria.Hide();
            btnPrint.Visible = false;
            btnAddOrden.Visible = false;
            btnSave.Visible = true;
        }

        protected void btnConsulta_Click(object sender, EventArgs e)
        {
            try
            {
                List<DAL.FACTURA_X_ORDEN_PEDIDO> lst = leerGrillaFacturas();
                DAL.FACTURA_X_ORDEN_PEDIDO objFact = new DAL.FACTURA_X_ORDEN_PEDIDO();
                if (lst.Exists(fact => fact.PUNTO_VENTA == int.Parse(txtPuntoVenta.Text) &&
                fact.NRO_COMPROBANTE == int.Parse(txtNumeroComprobante.Text)))
                {
                    divConsultaError.Visible = true;
                    HtmlGenericControl li = new HtmlGenericControl();
                    li.InnerText = "No puede agregarse mas de una vez la misma factura a la order de pedido";
                    ulErrores.Controls.Add(li);
                    UPanelDatos.Update();
                    return;
                }
                List<DAL.FACTURA_X_ORDEN_PEDIDO> lstOrdenes = DAL.FACTURA_X_ORDEN_PEDIDO.ValidaExistencia(int.Parse(txtPuntoVenta.Text),
                    Int64.Parse(txtNumeroComprobante.Text), txtCUIT.Text);

                if (lstOrdenes.Count > 0)
                {
                    foreach (var item in lstOrdenes)
                    {
                        divConsultaError.Visible = true;
                        HtmlGenericControl li = new HtmlGenericControl();
                        li.InnerHtml = string.Format(
                    "No puede agregarse la factura porque ya se encuentra en la order de pedido Nro.:<a href=\"newOpBootstrap.aspx?op={0}\" target=\"_balnk\">{0}</a>",
                            item.NRO_ORDEN_PEDIDO);
                        ulErrores.Controls.Add(li);
                        UPanelDatos.Update();
                        return;
                    }
                }
                WSAFIP.CmpResponse resultado =
                WSAfip.consultaComprobante(Convert.ToDateTime(txtFechaEmision.Text), "CAE", long.Parse(txtNumeroComprobante.Text),
                   Convert.ToInt32(DDLTipoComprobante.SelectedItem.Value), txtCAE.Text, long.Parse(txtCUIT.Text),
                   double.Parse(txtImporte.Text), int.Parse(txtPuntoVenta.Text));
                if (resultado.Resultado == "A")
                {
                    objFact.NRO_CUIT_PROVEEDOR = resultado.CmpResp.CuitEmisor.ToString();
                    objFact.ESTADO = 0;
                    objFact.FECHA_CARGA = DateTime.Now;
                    objFact.FECHA_EMISION = Convert.ToDateTime(txtFechaEmision.Text);
                    objFact.IMPORTE = Convert.ToDecimal(resultado.CmpResp.ImpTotal);
                    objFact.NRO_CAE = Int64.Parse(resultado.CmpResp.CodAutorizacion);
                    objFact.NRO_COMPROBANTE = resultado.CmpResp.CbteNro;
                    objFact.PUNTO_VENTA = resultado.CmpResp.PtoVta;
                    objFact.TIPO_COMPROBANTE = resultado.CmpResp.CbteTipo;
                    objFact.TIP_COMP = Utils.Utils.getTipoComprobante(objFact.TIPO_COMPROBANTE);

                    lst.Add(objFact);
                    if (txtNroFactura.Value == string.Empty)
                        txtNroFactura.Value = string.Format("{0}-{1}", resultado.CmpResp.PtoVta.ToString().PadLeft(4, Convert.ToChar("0")),
    resultado.CmpResp.CbteNro.ToString().PadLeft(8, Convert.ToChar("0")));
                    else
                        txtNroFactura.Value += string.Format(" / {0}-{1}", resultado.CmpResp.PtoVta.ToString().PadLeft(4, Convert.ToChar("0")),
    resultado.CmpResp.CbteNro.ToString().PadLeft(8, Convert.ToChar("0")));

                    fillFacturas(lst);

                    divConsultaError.Visible = false;
                }
                else
                {
                    divConsultaError.Visible = true;
                    if (resultado.Errors != null && resultado.Errors.Length > 0)
                    {
                        foreach (var item in resultado.Errors)
                        {
                            HtmlGenericControl li = new HtmlGenericControl();
                            li.InnerText = item.Msg;
                            ulErrores.Controls.Add(li);
                        }
                    }
                    if (resultado.Observaciones != null && resultado.Observaciones.Length > 0)
                    {
                        divObsError.Visible = true;
                        foreach (var item in resultado.Observaciones)
                        {
                            HtmlGenericControl li = new HtmlGenericControl();
                            li.InnerText = item.Msg;
                            ulObsError.Controls.Add(li);
                        }
                    }
                    else
                        divObsError.Visible = false;
                }
                clearFactura();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void clearFactura()
        {
            //txtCUIT.Text = string.Empty;
            txtFechaEmision.Text = string.Empty;
            txtPuntoVenta.Text = string.Empty;
            txtNumeroComprobante.Text = string.Empty;
            txtCAE.Text = string.Empty;
            txtImporte.Text = string.Empty;
            DDLTipoComprobante.SelectedIndex = 5;
        }

        protected void btnCloseError_ServerClick(object sender, EventArgs e)
        {
            divConsultaError.Visible = false;
        }

        protected void gvFacturas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                List<DAL.FACTURA_X_ORDEN_PEDIDO> lst = leerGrillaFacturas();
                lst.RemoveAt(Convert.ToInt32(e.CommandArgument));
                fillFacturas(lst);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvFacturas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DAL.FACTURA_X_ORDEN_PEDIDO obj = (DAL.FACTURA_X_ORDEN_PEDIDO)e.Row.DataItem;
                    Label lblComprobante = (Label)e.Row.FindControl("lblComprobante");
                    lblComprobante.Text = string.Format("{0}-{1}", obj.PUNTO_VENTA.ToString().PadLeft(4, Convert.ToChar("0")),
                    obj.NRO_COMPROBANTE.ToString().PadLeft(8, Convert.ToChar("0")));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////

    }
}