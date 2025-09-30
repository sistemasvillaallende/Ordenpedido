using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entities;
using BLL;
namespace Web.Secure
{
    public partial class NewOpCC : System.Web.UI.Page
    {
        List<Entities.DetalleOrden> lstDetalle = new List<DetalleOrden>();
        Entities.OrdenPedido oOp = new Entities.OrdenPedido();



        protected void Page_Load(object sender, EventArgs e)
        {
            Entities.OrdenPedido ordenPedido = null;
            if (!IsPostBack)
            {

                fillDetalle(new List<DetalleOrden>());
                btnPrint.Visible = false;
                btnAddOrden.Visible = false;
                txtFechaOp.InnerHtml = BLL.OrdenPedidoBLL.FechaServer();
                if (Request.Cookies["UserOP"]["id_oficina_usuario"].ToString() != null)
                {
                    P1.InnerText = Request.Cookies["UserOP"]["id_oficina_usuario"].ToString().ToString() + " - " +
                    BLL.OficinasBLL.getOficinaByPk(Convert.ToInt32(Request.Cookies["UserOP"]["id_oficina_usuario"].ToString())).nombre;
                }
                Session.Add("Detalle", lstDetalle);
                Session.Add("Total", 0);
                Session.Add("opcion", 0);
                Session.Add("index", 0);
                Session.Add("ordenPedido", null);
                Session.Add("nroOrden", 0);
                CargarDesplegables();
                if (Request.QueryString["op"] != null)
                {
                    int op = int.Parse(Request.QueryString["op"]);
                    if (op != 0)
                    {
                        ordenPedido = BLL.OrdenPedidoBLL.getOrdenesByPk(op);
                        fillDatos(ordenPedido);
                    }
                }

                if (Request.QueryString["opcion"] != null)
                {
                    if (Request.QueryString["opcion"].ToString() == "cc")
                    {
                        txtIdProv.Enabled = false;
                        lnkFindProv.Enabled = false;
                        txtIdProv.Text = 67.ToString();
                        txtNameProv.Value = "MUNICIPALIDAD V.ALLENDE - RENDICION";
                    }
                    if (Request.QueryString["opcion"].ToString() == "sf")
                    {
                        txtIdProv.Enabled = true;
                        lnkFindProv.Enabled = true;
                    }
                }

                txtEstado_op.InnerHtml = ordenPedido != null
                ? GetEstadoOPDescripcion(ordenPedido.codEstadoOP)
                : "Sin estado";
            }
        }

        private static string GetEstadoOPDescripcion(int codEstadoOp)
        {
            switch (codEstadoOp)
            {
                case 1:
                    return "Recibida";
                case 2:
                    return "Devuelta";
                default:
                    return "Sin Estado";
            }
        }


        private void CargarDesplegables()
        {
            ddlSecretariaAutoriza.DataTextField = "descripcion";
            ddlSecretariaAutoriza.DataValueField = "id_secretaria";
            ddlSecretariaAutoriza.DataSource = DAL.DesplegablesDAL.GetSecretarias(0);
            ddlSecretariaAutoriza.DataBind();
            ddlSecretariaAutoriza.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

            //ddlDireccionSolicitante.DataTextField = "descripcion";
            //ddlDireccionSolicitante.DataValueField = "id_direccion";
            //ddlDireccionSolicitante.DataSource = DAL.DesplegablesDAL.GetDirecciones(0);
            //ddlDireccionSolicitante.DataBind();
            //ddlDireccionSolicitante.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
        }

        private void CargarDesplegables(Entities.OrdenPedido obj)
        {
            ddlDireccionSolicitante.Items.Clear();

            if (obj == null || obj.cod_secretaria_autoriza == null)
            {
                // 👇 Si no hay secretaria, muestro un ítem fijo
                ddlDireccionSolicitante.Items.Add(new ListItem("-- No disponible --", "0"));
                return;
            }

            int idsec = (int)obj.cod_secretaria_autoriza;

            ddlDireccionSolicitante.DataTextField = "descripcion";
            ddlDireccionSolicitante.DataValueField = "id_direccion";
            ddlDireccionSolicitante.DataSource = DAL.DesplegablesDAL.GetDirecciones(idsec);
            ddlDireccionSolicitante.DataBind();

            // Ítem inicial
            ddlDireccionSolicitante.Items.Insert(0, new ListItem("-- Seleccione una dirección --", "0"));
        }

        protected void ddlSecretariaAutoriza_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(ddlSecretariaAutoriza.SelectedValue);
            ddlDireccionSolicitante.DataTextField = "descripcion";
            ddlDireccionSolicitante.DataValueField = "id_direccion";
            ddlDireccionSolicitante.DataSource = DAL.DesplegablesDAL.GetDirecciones(id);
            ddlDireccionSolicitante.DataBind();
            ddlDireccionSolicitante.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

            // Copiar el texto seleccionado en ddlSecretariaAutoriza al campo solicitante
            txtAut.Value = ddlSecretariaAutoriza.SelectedItem.Text;
            txtSolicitante.Value = string.Empty;
            UPanelDatos.Update();
        }

        protected void ddlDireccionSolicitante_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Copiar el texto seleccionado en ddlDireccionSolicitante al campo aprobado
            txtSolicitante.Value = ddlDireccionSolicitante.SelectedItem.Text;
            UPanelDatos.Update();
        }

        private void CargarDatosUsuario()
        {
            var cookie = Request.Cookies["UserOP"];
            if (cookie != null && !string.IsNullOrEmpty(cookie["id_oficina_usuario"]))
            {
                int idOficina = Convert.ToInt32(cookie["id_oficina_usuario"]);
                var oficina = BLL.OficinasBLL.getOficinaByPk(idOficina);
                P1.InnerText = $"{idOficina} - {oficina.nombre}";
            }
        }

        protected void fillDatos(Entities.OrdenPedido oOp)
        {
            CargarDesplegables(oOp);
            txtOP.InnerText = oOp.nroOrden.ToString();
            txtFechaOp.InnerText = oOp.fechaOrden.ToShortDateString();
            txtIdProv.Text = oOp.codProveedor.ToString();
            txtNameProv.Value = oOp.proveedor;
            //P1.InnerText = oOp.codOficinaOrigen.ToString();
            int idOficina = oOp.codOficinaOrigen;
            var oficina = BLL.OficinasBLL.getOficinaByPk(idOficina);
            P1.InnerText = $"{idOficina} - {oficina.nombre}";
            txtAut.Value = oOp.aprobado;
            txtSolicitante.Value = oOp.solicitante;
            txtIdDestino.Text = oOp.codOficinaDestino.ToString();
            txtNameDestino.Value = oOp.destino;
            txtObs.Value = oOp.obs;
            txtFormaPago.Value = oOp.formaPago;
            txtNroPresup.Value = oOp.nroPresupuesto;
            txtNroFactura.Value = oOp.nroFacturas;
            ddlSecretariaAutoriza.SelectedValue = (oOp.cod_secretaria_autoriza.HasValue) ? oOp.cod_secretaria_autoriza.Value.ToString() : "0";
            ddlDireccionSolicitante.SelectedValue = (oOp.cod_direccion_solicita.HasValue) ? oOp.cod_direccion_solicita.Value.ToString() : "0";
            fillDetalle(oOp.detalle);
            decimal total = 0;
            for (int i = 0; i < oOp.detalle.Count; i++)
            {
                total += oOp.detalle[i].importe;
            }
            lblTotal.InnerText = "$" + total.ToString();
            Session["Detalle"] = oOp.detalle;
            Session["Total"] = total;
        }


        //ACCION DE LA GRILLA PROVEEDORES QUE ASIGNA LOS VALORES DE LA FILA SELECCIONADA A LOS//////////// 
        //TEXTOS ID Y NOMBRE PROVEEDOR                                                                   
        protected void gvProv_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int i = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "selected")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                //txtNameProv.Value = (string)(gvProv.DataKeys[index].Values["nomProveedor"]);
                //txtIdProv.Text = gvProv.DataKeys[index].Values["codProveedor"].ToString();
                //Popup1.Hide();
                //gvProv.DataSource = null;
                //DataBind();
                //txtBuscarProv.Value = string.Empty;

                txtNameProv.Value = (string)(gvProv.DataKeys[index].Values["nomProveedor"]);
                txtIdProv.Text = gvProv.DataKeys[index].Values["codProveedor"].ToString();

                Popup1.Hide();
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
            Popup1.Show();
            UpdatePanel4.Update();
            txtIdProv.Focus();
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
            Popup1.Dispose();
            Popup1.Hide();
        }

        protected void btnFindDest_click(object sender, EventArgs e)
        {
            popUpOfice.Show();
            UpdatePanel3.Update();
            txtIdDestino.Focus();
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
                popUpOfice.Dispose();
                popUpOfice.Hide();
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
            popUpOfice.Dispose();
            popUpOfice.Hide();
        }

        protected void btnAddDetalle_click(object sender, EventArgs e)
        {
            txtDescripcion.Text = string.Empty;
            txtCantidad.Text = string.Empty;
            txtPU.Text = string.Empty;
            UpdatePanel5.Update();
            txtDescripcion.Focus();
            popUpDetalle.Show();
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
                    //lblError.Text = string.Empty;
                    //UpdateError.Update();
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
            catch (Exception ex)
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
            catch (Exception ex)
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
                detalle.importe = (detalle.precio * detalle.cant);

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
                popUpDetalle.Hide();

            }
        }

        protected void fillDetalle(List<Entities.DetalleOrden> lstDetalle)
        {
            gvDetalle.DataSource = lstDetalle;
            gvDetalle.DataBind();
            uPanelDetalle.Update();
        }



        protected void btnSave_Click(object sender, EventArgs e)
        {
            decimal totaldetalle = 0;
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
            //
            oOrden.cod_secretaria_autoriza = Convert.ToInt32(ddlSecretariaAutoriza.SelectedValue);
            oOrden.cod_direccion_solicita = Convert.ToInt32(ddlDireccionSolicitante.SelectedValue);
            //
            List<Entities.DetalleOrden> lstDetalle;
            lstDetalle = (List<Entities.DetalleOrden>)Session["Detalle"];

            if (lstDetalle.Count == 0)
            {
                string script = @"<script type='text/javascript'> apprise('Debe agregar al menos un Items al detalle',{'animate':true});                
          </script>"; ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
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
            //para que ambas tanto la OP, como el Detalle sumen el 
            totaldetalle = lstDetalle.Sum(ent => ent.importe);
            oOrden.total = totaldetalle;
            oOrden.usuario = Request.Cookies["UserOP"]["usuario"].ToString();

            //if (Request.QueryString["op"] != null)
            //{
            //  int op = (Request.QueryString["op"].Length == 0) ? 0 : int.Parse(Request.QueryString["op"]);

            //  if (op == 0)
            //  {
            //    Int64 nroOrden = BLL.OrdenPedidoBLL.Insert(oOrden);
            //    oOrden.nroOrden = Convert.ToInt32(nroOrden);
            //    txtOP.InnerText = oOrden.nroOrden.ToString();
            //    string script =
            //    @"<script type='text/javascript'> 
            //          appriseGreen('La orden a sido ingresada de forma correcta',{'animate':true});</script>";
            //    ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
            //    Session["ordenPedido"] = oOrden;
            //    //Response.Redirect("Home.aspx");
            //  }
            //}
            //else
            //{
            //  Session["ordenPedido"] = oOrden;
            //  popUpdate.Show();
            //}
            //btnPrint.Visible = true;
            //btnAddOrden.Visible = true;
            //btnSave.Visible = false;
            //txtOP.InnerText = oOrden.nroOrden.ToString();


            int op = 0;

            if (Request.QueryString["op"] != null)
                op = (Request.QueryString["op"].Length == 0) ? 0 : int.Parse(Request.QueryString["op"]);


            if (op == 0)                                                                                                //
            {
                if (Request.QueryString["opcion"] != null)
                {
                    if (Request.QueryString["opcion"].ToString() == "cc")
                    {
                        oOrden.web = 2;
                    }
                    if (Request.QueryString["opcion"].ToString() == "sf")
                    {
                        oOrden.web = 3;
                    }
                }
                Int64 nroOrden = OrdenPedidoBLL.Insert(oOrden, new List<DAL.FACTURA_X_ORDEN_PEDIDO>());
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
                popUpdate.Show();
            }
            btnPrint.Visible = true;
            btnAddOrden.Visible = true;
            btnSave.Visible = false;
            txtOP.InnerText = oOrden.nroOrden.ToString();
        }




        protected void btnCancelarPopUpUdate_Click(object sender, EventArgs e)  //
        {                                                                       //
            popUpdate.Hide();                                                   //
            btnPrint.Visible = false;                                           //
            btnAddOrden.Visible = false;                                        //
            btnSave.Visible = true;                                             //
        }                                                                       //


        protected void btnAceptarPopUpdate_Click(object sender, EventArgs e)                                        //
        {                                                                                                           //
            Entities.OrdenPedido oOrden = (Entities.OrdenPedido)Session["ordenPedido"];                             //
            oOrden.nroOrden = int.Parse(Request.QueryString["op"]);
            if (Request.QueryString["opcion"] != null)
            {
                if (Request.QueryString["opcion"].ToString() == "cc")
                {
                    oOrden.web = 2;
                }
                if (Request.QueryString["opcion"].ToString() == "sf")
                {
                    oOrden.web = 3;
                }
            }//
            if (txtObservAuditoria.Text.Trim() != string.Empty)                                                     //
            {                                                                                                       //
                oOrden.obsAuditoria = txtObservAuditoria.Text.Trim().ToUpper();                                     //
                BLL.OrdenPedidoBLL.Update(oOrden, new List<DAL.FACTURA_X_ORDEN_PEDIDO>());                                                                  //
                popUpdate.Hide();                                                                                   //
                Session["ordenPedido"] = oOrden;                                                                    //
            }                                                                                                       //
        }


        protected void btnPrint_Click(object sender, EventArgs e)
        {
            Entities.OrdenPedido objOrden = (Entities.OrdenPedido)Session["ordenPedido"];
            int cod = objOrden.nroOrden;
            Session["ordenPedido"] = BLL.OrdenPedidoBLL.getOrdenesByPk(cod);
            //string script = "window.open('printOp.aspx','Orden de Pedido', 'width=700,height=600,scrollbars=NO');";
            //ScriptManager.RegisterStartupScript(this, typeof(Page), "popup", script, true);

            divReporte.InnerHtml = "<iframe src=\" " +
             string.Format("../Reportes/ReporteOrdenPedido.aspx?nroOrden_pedido={0}", cod) + "\"  width=\"100%\" height=\"600\"></iframe>";

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
                                                                                            //////////////////////////////////////////////////////////////////////////////////
                                                                                            //ENCABEZADO//////////////////////////////////////////////////////////////////////
                                                                                            //txtFechaOp.InnerText = DateTime.Now.ToShortDateString();                      
            txtFechaOp.InnerText = BLL.OrdenPedidoBLL.FechaServer();
            if (Request.Cookies["UserOP"]["id_oficina_usuario"].ToString() != null)
            {
                P1.InnerText = Request.Cookies["UserOP"]["id_oficina_usuario"].ToString() + " - " +
                BLL.OficinasBLL.getOficinaByPk(Convert.ToInt32(
                    Request.Cookies["UserOP"]["id_oficina_usuario"].ToString())).nombre;
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
            UPanelDatos.Update();
        }                                                                                  //
                                                                                           /////////////////////////////////////////////////////////////////////////////////////

    }
}