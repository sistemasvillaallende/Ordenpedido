using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Entities;
using DAL;
using System.Web.Services.Description;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Math;


namespace Web.Secure
{
    public partial class newOpBootstrap : System.Web.UI.Page
    {
        List<Entities.DetalleOrden> lstDetalle = new List<DetalleOrden>();
        Entities.OrdenPedido oOp = new Entities.OrdenPedido();
        WSAFIP.WSAFIPSoapClient WSAfip = new WSAFIP.WSAFIPSoapClient();
        // Agrega este campo protegido a la clase newOpBootstrap para que el control FileUpload esté disponible en el code-behind.
        // Debe coincidir con el ID del control FileUpload en el archivo .aspx (por ejemplo: <asp:FileUpload ID="fuExcel" ... />).

        protected FileUpload fuExcel;
        private List<DAL.FACTURA_X_ORDEN_PEDIDO> leerGrillaFacturas()
        {
            List<DAL.FACTURA_X_ORDEN_PEDIDO> lst = new List<DAL.FACTURA_X_ORDEN_PEDIDO>();
            for (int i = 0; i < gvFacturas.Rows.Count; i++)
            {
                GridViewRow row = gvFacturas.Rows[i];
                DAL.FACTURA_X_ORDEN_PEDIDO obj = new DAL.FACTURA_X_ORDEN_PEDIDO();
                obj.ID = int.Parse(gvFacturas.DataKeys[i].Values["ID"].ToString());
                obj.FECHA_EMISION = Convert.ToDateTime(gvFacturas.DataKeys[i].Values["FECHA_EMISION"].ToString());
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
            if (!ValidarCookieUsuario())
                return;
            Entities.OrdenPedido ordenPedido = null;
            if (!IsPostBack)
            {
                InicializarControles();
                CargarDatosUsuario();
                InicializarSesiones();
                CargarDesplegables();
                ordenPedido = CargarOrdenDesdeQuery();
                DDLTipoComprobante.SelectedIndex = 5;
                txtIdProv.Focus();
            }
            txtEstado_op.InnerHtml = ordenPedido != null
                ? GetEstadoOPDescripcion(ordenPedido.codEstadoOP)
                : "Sin estado";
        }

        private bool ValidarCookieUsuario()
        {
            var cookie = Request.Cookies["UserOP"];
            if (cookie == null)
            {
                Response.Redirect("../Login.aspx");
                return false;
            }
            return true;
        }

        private void InicializarControles()
        {
            fillFacturas(new List<DAL.FACTURA_X_ORDEN_PEDIDO>());
            fillDetalle(new List<DetalleOrden>());
            btnPrint.Visible = false;
            btnAddOrden.Visible = false;
            txtFechaOp.InnerHtml = BLL.OrdenPedidoBLL.FechaServer();
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

        private void InicializarSesiones()
        {
            Session["Detalle"] = lstDetalle;
            Session["Total"] = 0;
            Session["opcion"] = 0;
            Session["index"] = 0;
            Session["ordenPedido"] = null;
            Session["nroOrden"] = 0;
        }

        private Entities.OrdenPedido CargarOrdenDesdeQuery()
        {
            if (int.TryParse(Request.QueryString["op"], out int op) && op != 0)
            {
                var objOP = BLL.OrdenPedidoBLL.getOrdenesByPk(op);
                fillDatos(objOP);
                return objOP;
            }
            return null;
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
            ddlDireccionSolicitante.Items.Clear();
            ddlDireccionSolicitante.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

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
            ddlDireccionSolicitante.Items.Clear();
            ddlDireccionSolicitante.DataTextField = "descripcion";
            ddlDireccionSolicitante.DataValueField = "id_direccion";
            ddlDireccionSolicitante.DataSource = DAL.DesplegablesDAL.GetDirecciones(id);
            ddlDireccionSolicitante.DataBind();
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


        protected void fillDatos(Entities.OrdenPedido oOp)
        {
            CargarDesplegables(oOp);
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
            txtNroFactura.Value = oOp.nroFacturas;
            ddlSecretariaAutoriza.SelectedValue = (oOp.cod_secretaria_autoriza.HasValue) ? oOp.cod_secretaria_autoriza.Value.ToString() : "0";
            ddlDireccionSolicitante.SelectedValue = (oOp.cod_direccion_solicita.HasValue) ? oOp.cod_direccion_solicita.Value.ToString() : "0";
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
            if (e.CommandName == "selected")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                txtNameProv.Value = (string)(gvProv.DataKeys[index].Values["nomProveedor"]);
                txtIdProv.Text = gvProv.DataKeys[index].Values["codProveedor"].ToString();
                txtCUITProveedor.Value = (string)(gvProv.DataKeys[index].Values["nroCuit"]);
                txtCUIT.Text = (string)(gvProv.DataKeys[index].Values["nroCuit"]);

                // Limpiar grilla y campo de búsqueda
                gvProv.DataSource = null;
                gvProv.DataBind();
                txtBuscarProv.Value = string.Empty;

                // Actualizar paneles
                UpdatePanel4.Update();
                uPanelProv.Update();
                UPanelDatos.Update();

                // Cerrar modal Bootstrap
                ScriptManager.RegisterStartupScript(this, this.GetType(), "closeModal",
                    "$('#modalBuscarProveedor').modal('hide');", true);
            }
        }

        protected void gvProv_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btnSeleccionar = (LinkButton)e.Row.FindControl("btnSeleccionar");
                if (btnSeleccionar != null)
                {
                    btnSeleccionar.CommandArgument = e.Row.RowIndex.ToString();
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
            // Limpiar la grilla
            gvProv.DataSource = null;
            gvProv.DataBind();
            txtBuscarProv.Value = string.Empty;

            UpdatePanel4.Update();
            txtBuscarProv.Focus();

            // El modal se abre desde JavaScript (OnClientClick del LinkButton)
            // Este método solo prepara los campos
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
            // Cerrar modal Bootstrap
            ScriptManager.RegisterStartupScript(this, this.GetType(), "closeModal",
                "$('#modalBuscarProveedor').modal('hide');", true);
        }


        //protected void btnFindDest_click(object sender, EventArgs e)
        //{
        //    // Limpiar la grilla
        //    gvOficinas.DataSource = null;
        //    gvOficinas.DataBind();
        //    txtFindDest.Value = string.Empty;

        //    UpdatePanel3.Update();
        //    txtFindDest.Focus();

        //    // El modal se abre desde JavaScript (OnClientClick del LinkButton)
        //    // Este método solo prepara los campos
        //}

        protected void btnFindDest_click(object sender, EventArgs e)
        {
            UpdatePanel3.Update();
            txtFindDest.Focus();
            //popUpOficina.Show();
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
            if (e.CommandName == "selected")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                txtNameDestino.Value = gvOficinas.DataKeys[index].Values["nombre"].ToString();
                txtIdDestino.Text = gvOficinas.DataKeys[index].Values["idOficina"].ToString();

                // Limpiar grilla y campo de búsqueda
                gvOficinas.DataSource = null;
                gvOficinas.DataBind();
                txtFindDest.Value = string.Empty;

                // Actualizar paneles
                UpdatePanel3.Update();
                UPanelDestino.Update();

                // Cerrar modal Bootstrap
                ScriptManager.RegisterStartupScript(this, this.GetType(), "closeModal",
                    "$('#modalBuscarOficina').modal('hide');", true);
            }
        }

        protected void gvOficinas_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btnSeleccionar = (LinkButton)e.Row.FindControl("btnSeleccionar");
                if (btnSeleccionar != null)
                {
                    btnSeleccionar.CommandArgument = e.Row.RowIndex.ToString();
                }
            }
        }

        protected void btnCancelDest_click(object sender, EventArgs e)
        {
            // Cerrar modal Bootstrap
            ScriptManager.RegisterStartupScript(this, this.GetType(), "closeModal",
                "$('#modalBuscarOficina').modal('hide');", true);
            //popUpOficina.Dispose();
            //popUpOficina.Hide();

        }

        protected void btnAddDetalle_click(object sender, EventArgs e)
        {
            CleanCamposDetalle();
            //popUpDetalle.Show();
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
                //popUpDetalle.Show();
                // Abrir el modal Bootstrap para editar
                ScriptManager.RegisterStartupScript(this, this.GetType(), "openModal",
                    "$('#modalAgregarDetalle').modal('show');", true);
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

        //protected void txtCantidad_TextChanged(object sender, EventArgs e)
        //{
        //    /* try
        //    {
        //      string pu = txtCantidad.Text;
        //      txtCantidad.Text = pu.Replace(".", ",");
        //      decimal can = decimal.Parse(txtCantidad.Text);
        //    }
        //    catch
        //    {
        //      string script = @"<script type='text/javascript'> 
        //      apprise('Tipo de dato Incorrecto',{'animate':true}); </script>";
        //      ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
        //      txtCantidad.Text = string.Empty;
        //      txtCantidad.Focus();
        //    }*/
        //    txtPU.Focus();
        //}

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            //popUpDetalle.Hide();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "closeModal",
                "$('#modalAgregarDetalle').modal('hide');", true);
            //Button1.Focus();
        }

        //protected void txtPU_TextChanged(object sender, EventArgs e)
        //{
        //    /*try
        //    {
        //      string pu = txtPU.Text;
        //      txtPU.Text = pu.Replace(".", ",");
        //      decimal can = decimal.Parse(txtPU.Text);
        //    }
        //    catch
        //    {
        //      string script =@"<script type='text/javascript'> apprise('Tipo de dato Incorrecto',{'animate':true}); </script>";
        //      ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
        //      txtPU.Text = string.Empty;
        //      txtPU.Focus();
        //    }*/
        //    //Button2.Focus();
        //}

        //protected void btnAceptar_Click(object sender, EventArgs e)
        //{
        //    if (txtCantidad.Text.Trim() == string.Empty ||
        //        txtDescripcion.Text.Trim() == string.Empty || txtPU.Text == string.Empty)
        //    {
        //        string script =
        //        @"<script type='text/javascript'> apprise('Complete los datos Solicitados',{'animate':true}); </script>";
        //        ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
        //    }
        //    else
        //    {
        //        lstDetalle = (List<Entities.DetalleOrden>)Session["Detalle"];

        //        // Validar que la lista no sea nula
        //        if (lstDetalle == null)
        //        {
        //            lstDetalle = new List<Entities.DetalleOrden>();
        //        }


        //        Entities.DetalleOrden detalle = new DetalleOrden();
        //        detalle.descItems = txtDescripcion.Text;
        //        detalle.cant = Convert.ToDecimal(txtCantidad.Text);
        //        detalle.precio = Convert.ToDecimal(txtPU.Text);
        //        detalle.importe = decimal.Round((detalle.precio * detalle.cant), 2);

        //        if ((int)Session["opcion"] == 0)
        //            lstDetalle.Add(detalle);
        //        else
        //        {
        //            lstDetalle[(int)Session["index"]].descItems = detalle.descItems;
        //            lstDetalle[(int)Session["index"]].cant = detalle.cant;
        //            lstDetalle[(int)Session["index"]].precio = detalle.precio;
        //            lstDetalle[(int)Session["index"]].importe = detalle.importe;
        //        }
        //        decimal total = 0;
        //        foreach (DetalleOrden det in lstDetalle)
        //        {
        //            total += det.importe;
        //        }
        //        Session["Detalle"] = lstDetalle;
        //        Session["Total"] = total;
        //        Session["opcion"] = 0;

        //        lblTotal.InnerText = "TOTAL: $" + total.ToString();
        //        fillDetalle(lstDetalle);
        //        CleanCamposDetalle();
        //        //txtDescripcion.Focus();
        //        //popUpDetalle.Show();
        //        // Cerrar el modal Bootstrap
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "closeModal",
        //            "$('#modalAgregarDetalle').modal('hide');", true);

        //        // Mostrar mensaje de éxito
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "success",
        //            "alert('Item agregado correctamente');", true);
        //    }
        //}

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            // Forzar validación explícita
            if (!Page.IsValid)
            {
                return;
            }

            // Verificar campos manualmente también
            if (string.IsNullOrWhiteSpace(txtDescripcion.Text) ||
                string.IsNullOrWhiteSpace(txtCantidad.Text) ||
                string.IsNullOrWhiteSpace(txtPU.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                    "alert('Complete todos los campos obligatorios');", true);
                return;
            }

            // Validar tipos de datos
            if (!decimal.TryParse(txtCantidad.Text, out decimal cantidad) || cantidad <= 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                    "alert('Ingrese una cantidad válida mayor a 0');", true);
                return;
            }

            if (!decimal.TryParse(txtPU.Text, out decimal precio))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                    "alert('Ingrese un valor numérico válido');", true);
                return;
            }

            try
            {
                lstDetalle = (List<Entities.DetalleOrden>)Session["Detalle"] ?? new List<Entities.DetalleOrden>();

                Entities.DetalleOrden detalle = new DetalleOrden();
                detalle.descItems = txtDescripcion.Text.Trim();
                detalle.cant = cantidad;
                detalle.precio = precio;
                detalle.importe = decimal.Round((precio * cantidad), 2);

                if ((int?)Session["opcion"] == 1)
                {
                    int index = (int)Session["index"];
                    lstDetalle[index] = detalle;
                }
                else
                {
                    lstDetalle.Add(detalle);
                }

                decimal total = lstDetalle.Sum(det => det.importe);

                Session["Detalle"] = lstDetalle;
                Session["Total"] = total;
                Session["opcion"] = 0;
                Session["index"] = -1;

                lblTotal.InnerText = "TOTAL: $" + total.ToString("N2");
                fillDetalle(lstDetalle);
                CleanCamposDetalle();

                ////  // Cerrar modal y mostrar mensaje
                ////  ScriptManager.RegisterStartupScript(this, this.GetType(), "closeModalSuccess",
                ////      @"$('#modalAgregarDetalle').modal('hide'); 
                ////           setTimeout(function() { 
                ////    alert('Item agregado correctamente'); 
                ////}, 300);", true);
                ///

                // SIEMPRE mantener modal abierto y enfocar
                string script = $@"
                        $('#{txtDescripcion.ClientID}').focus(); 
                         // Opcional: highlight temporal del modal
                        $('.modal-content').addClass('border-success');
                        setTimeout(function() {{
                            $('.modal-content').removeClass('border-success');
                        }}, 1000);";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "keepModalOpen", script, true);

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                    $"alert('Error al agregar item: {ex.Message}');", true);
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
            //AUTORIZANTE Aprobado
            if (txtAut.Value.Trim().ToUpper() != string.Empty)
                oOrden.aprobado = txtAut.Value.Trim().ToUpper();
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
                    li.InnerText = "El importe total de facturas no coincide con el importe total cargado en el detalle";
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
                //Session["ordenPedido"] = oOrden;
                //txtObservAuditoria.Focus();
                //popUpAuditoria.Show();
                Session["ordenPedido"] = oOrden;
                txtObservAuditoria.Focus();

                // Cambiar popUpAuditoria.Show() por:
                uPanelUpdate.Update();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showAuditModal",
                    "$('#modalAuditoria').modal('show');", true);
            }
            btnPrint.Visible = true;
            btnAddOrden.Visible = true;
            btnSave.Visible = false;
            txtOP.InnerText = oOrden.nroOrden.ToString();
        }




        //<a target = "_blank" href="../Reportes/ReporteOrdenPedido.aspx?nroOrden=<%#Eval("nroOrden")%>">
        //                                <span class="fa fa-print" style="font-size: 20px;"></span>
        //                            </a>


        //protected void btnPrint_Click(object sender, EventArgs e)
        //{
        //    Entities.OrdenPedido objOrden = (Entities.OrdenPedido)Session["ordenPedido"];
        //    int cod = objOrden.nroOrden;
        //    Session["ordenPedido"] = BLL.OrdenPedidoBLL.getOrdenesByPk(cod);
        //    divReporte.InnerHtml = "<iframe src=\" " +
        //        string.Format("../Reportes/ReporteOrdenPedido.aspx?nroOrden_pedido={0}", cod) + "\"  width=\"100%\" height=\"600\"></iframe>";
        //    popUpListado.Show();
        //}
        ////string.Format("../impresiones/consultaexpedientes.aspx?fecha_desde={0}&fecha_hasta={1}&cod_desde={2}&cod_hasta={3}&tipo={4}&agrupado={5}",
        ////   txtFecha_desde.Text, txtFecha_hasta.Text, cod_desde, cod_hasta, "estado", agrupado) + "\"  width=\"100%\" height=\"600\"></iframe>";
        ////  popUpListado.Show();

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            Entities.OrdenPedido objOrden = (Entities.OrdenPedido)Session["ordenPedido"];
            int cod = objOrden.nroOrden;
            Session["ordenPedido"] = BLL.OrdenPedidoBLL.getOrdenesByPk(cod);

            divReporte.InnerHtml = "<iframe src=\"" +
                string.Format("../Reportes/ReporteOrdenPedido.aspx?nroOrden_pedido={0}", cod) +
                "\" width=\"100%\" height=\"600\" style=\"border: none;\"></iframe>";

            // Actualizar el UpdatePanel
            //UpdatePanel7.Update();

            // Abrir modal Bootstrap
            ScriptManager.RegisterStartupScript(this, this.GetType(), "openReportModal",
                "$('#modalReporteOP').modal('show');", true);
        }

        protected void btnCloseListado_Click(object sender, EventArgs e)
        {
            // Cerrar modal Bootstrap
            ScriptManager.RegisterStartupScript(this, this.GetType(), "closeReportModal",
                "$('#modalReporteOP').modal('hide');", true);
        }
        //protected void btnCloseListado_Click(object sender, EventArgs e)
        //{
        //    popUpListado.Hide();
        //}


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
            //txtDescripcion.Text = string.Empty;
            //txtCantidad.Text = string.Empty;
            //txtPU.Text = string.Empty;
            //UpdatePanel5.Update();
            //txtDescripcion.Focus();
            // Limpiar campos para nuevo item
            CleanCamposDetalle();

            // Resetear la sesión para modo "agregar"
            Session["opcion"] = 0;
            Session["index"] = -1;

            // El modal se abre desde JavaScript (OnClientClick del LinkButton)
            // Este método solo prepara los campos
            //popUpDetalle.Show();
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

        protected void lbtnAddExcel_Click(object sender, EventArgs e)
        {
            // Este método ahora abre el modal desde JavaScript
            // La lógica se maneja en btnProcesarExcel_Click
        }

        protected void btnProcesarExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (fuExcel.HasFile)
                {
                    // Validar extensión del archivo
                    string extension = System.IO.Path.GetExtension(fuExcel.FileName).ToLower();
                    if (extension != ".xlsx" && extension != ".xls")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                            "alert('Por favor seleccione un archivo Excel válido (.xlsx o .xls)');", true);
                        return;
                    }

                    // Validar tamaño del archivo (máximo 5MB)
                    if (fuExcel.PostedFile.ContentLength > 5 * 1024 * 1024)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                            "alert('El archivo no puede ser mayor a 5MB');", true);
                        return;
                    }

                    // Guardar archivo temporalmente
                    string fileName = Guid.NewGuid().ToString() + extension;
                    string tempPath = Server.MapPath("~/App_Data/Temp/");

                    // Crear directorio si no existe
                    if (!System.IO.Directory.Exists(tempPath))
                    {
                        System.IO.Directory.CreateDirectory(tempPath);
                    }

                    string filePath = tempPath + fileName;
                    fuExcel.SaveAs(filePath);

                    // Procesar el archivo Excel
                    var itemsExcel = ProcesarArchivoExcel(filePath);

                    if (itemsExcel.Count > 0)
                    {
                        // Agregar items al detalle existente
                        AgregarItemsDesdeExcel(itemsExcel);

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "success",
                            $"alert('Se agregaron {itemsExcel.Count} items desde el Excel');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                            "alert('No se encontraron datos válidos en el archivo Excel');", true);
                    }

                    // Eliminar archivo temporal
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }

                    // Cerrar modal
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "closeModal",
                        "$('#modalSubirExcel').modal('hide');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                        "alert('Por favor seleccione un archivo');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                    $"alert('Error al procesar archivo: {ex.Message}');", true);
            }
        }

        private List<Entities.DetalleOrden> ProcesarArchivoExcel(string filePath)
        {
            var items = new List<Entities.DetalleOrden>();

            try
            {
                // Elegir el método según la librería instalada:

                // Opción 1: EPPlus (recomendado)
                // items = LeerExcelConEPPlus(filePath);

                // Opción 2: ClosedXML (solo .xlsx)
                items = LeerExcelConClosedXML(filePath);

                // Opción 3: ExcelDataReader
                // items = LeerExcelConDataReader(filePath);

                // Opción 4: OleDb (método actual)
                // items = LeerExcelConOleDb(filePath);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al leer el archivo Excel: {ex.Message}");
            }

            return items;
        }

        private List<Entities.DetalleOrden> LeerExcelConClosedXML(string filePath)
        {
            var items = new List<Entities.DetalleOrden>();

            using (var workbook = new XLWorkbook(filePath))
            {
                var worksheet = workbook.Worksheet(1); // Primera hoja

                // Empezar desde la fila 2
                for (int row = 2; row <= worksheet.LastRowUsed().RowNumber(); row++)
                {
                    try
                    {
                        string descripcion = worksheet.Cell(row, 1).Value.ToString()?.Trim();
                        string cantidadStr = worksheet.Cell(row, 2).Value.ToString()?.Trim();
                        string precioStr = worksheet.Cell(row, 3).Value.ToString()?.Trim();

                        if (string.IsNullOrEmpty(descripcion) ||
                            string.IsNullOrEmpty(cantidadStr) ||
                            string.IsNullOrEmpty(precioStr))
                            continue;

                        if (decimal.TryParse(cantidadStr, out decimal cantidad) &&
                            decimal.TryParse(precioStr, out decimal precio))
                        {
                            var detalle = new Entities.DetalleOrden
                            {
                                descItems = descripcion,
                                cant = cantidad,
                                precio = precio,
                                importe = decimal.Round(cantidad * precio, 2)
                            };

                            items.Add(detalle);
                        }
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
            }

            return items;
        }

        private void AgregarItemsDesdeExcel(List<Entities.DetalleOrden> itemsExcel)
        {
            // Obtener el detalle actual de la sesión
            var lstDetalle = (List<Entities.DetalleOrden>)Session["Detalle"] ?? new List<Entities.DetalleOrden>();

            // Agregar los nuevos items
            lstDetalle.AddRange(itemsExcel);

            // Calcular el nuevo total
            decimal total = lstDetalle.Sum(det => det.importe);

            // Actualizar sesión
            Session["Detalle"] = lstDetalle;
            Session["Total"] = total;

            // Actualizar la grilla y el total en pantalla
            fillDetalle(lstDetalle);
            lblTotal.InnerText = "TOTAL: $" + total.ToString();
        }

        protected void lnkAceptarAuditoria_Click(object sender, EventArgs e)
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

                //oOrden.web = 1;
                //BLL.OrdenPedidoBLL.Update(oOrden, lstFacturas);
                //popUpAuditoria.Hide();
                //Session["ordenPedido"] = oOrden;


                oOrden.web = 1;
                BLL.OrdenPedidoBLL.Update(oOrden, lstFacturas);

                // Cambiar popUpAuditoria.Hide() por:
                ScriptManager.RegisterStartupScript(this, this.GetType(), "hideAuditModal",
                    "$('#modalAuditoria').modal('hide');", true);

                Session["ordenPedido"] = oOrden;
            }
            else
            {
                // Agregar validación para campo obligatorio
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                    "alert('Debe ingresar el motivo de la modificación');", true);
                txtObservAuditoria.Focus();
            }
        }


        protected void lnkCancelarAuditoria_Click(object sender, EventArgs e)
        {
            // Cambiar popUpAuditoria.Hide() por:
            ScriptManager.RegisterStartupScript(this, this.GetType(), "hideAuditModal",
                "$('#modalAuditoria').modal('hide');", true);
            btnPrint.Visible = false;
            btnAddOrden.Visible = false;
            btnSave.Visible = true;
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            CleanCamposDetalle();
        }
    }
}





//protected void btnAceptarAuditoria_Click(object sender, EventArgs e)
//{
//    Entities.OrdenPedido oOrden = (Entities.OrdenPedido)Session["ordenPedido"];
//    oOrden.nroOrden = int.Parse(Request.QueryString["op"]);
//    if (txtObservAuditoria.Text.Trim() != string.Empty)
//    {
//        oOrden.obsAuditoria = txtObservAuditoria.Text.Trim().ToUpper();

//        List<DAL.FACTURA_X_ORDEN_PEDIDO> lstFacturas = leerGrillaFacturas();
//        if (lstFacturas.Count == 0)
//        {
//            divConsultaError.Visible = true;
//            HtmlGenericControl li = new HtmlGenericControl();
//            li.InnerText = "Debe agregar al menos una Factura a la orden";
//            ulErrores.Controls.Add(li);
//            UPanelDatos.Update();
//            return;
//        }
//        List<Entities.DetalleOrden> lstDetalle;
//        lstDetalle = (List<Entities.DetalleOrden>)Session["Detalle"];
//        //COMPROBAR MONTOS
//        if (lstDetalle.Sum(det => det.importe) != lstFacturas.Sum(fact => fact.IMPORTE))
//        {
//            divConsultaError.Visible = true;
//            HtmlGenericControl li = new HtmlGenericControl();
//            li.InnerText = "El importe total de facturas no coinside con el importe total cargado en el detalle";
//            ulErrores.Controls.Add(li);
//            UPanelDatos.Update();
//            return;
//        }

//        //oOrden.web = 1;
//        //BLL.OrdenPedidoBLL.Update(oOrden, lstFacturas);
//        //popUpAuditoria.Hide();
//        //Session["ordenPedido"] = oOrden;
//        oOrden.web = 1;
//        BLL.OrdenPedidoBLL.Update(oOrden, lstFacturas);

//        // Cambiar popUpAuditoria.Hide() por:
//        ScriptManager.RegisterStartupScript(this, this.GetType(), "hideAuditModal",
//            "$('#modalAuditoria').modal('hide');", true);

//        Session["ordenPedido"] = oOrden;
//    }
//    else
//    {
//        // Agregar validación para campo obligatorio
//        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
//            "alert('Debe ingresar el motivo de la modificación');", true);
//        txtObservAuditoria.Focus();
//    }
//}

//protected void btnCancelarAuditoria_Click(object sender, EventArgs e)
//{
//    //popUpAuditoria.Hide();
//    //btnPrint.Visible = false;
//    //btnAddOrden.Visible = false;
//    //btnSave.Visible = true;


//    // Cambiar popUpAuditoria.Hide() por:
//    ScriptManager.RegisterStartupScript(this, this.GetType(), "hideAuditModal",
//        "$('#modalAuditoria').modal('hide');", true);
//    btnPrint.Visible = false;
//    btnAddOrden.Visible = false;
//    btnSave.Visible = true;
//}









