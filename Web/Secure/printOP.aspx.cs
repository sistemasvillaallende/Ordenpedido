using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entities;
namespace Web.Secure
{
    public partial class printOP : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            Entities.OrdenPedido oOrden = new Entities.OrdenPedido();

            if (Request.QueryString["nroOrden"] != null)
            {
                int nro = int.Parse(Request.QueryString["nroOrden"]);
                oOrden = BLL.OrdenPedidoBLL.getOrdenesByPk(nro);
            }
            else
                oOrden = (Entities.OrdenPedido)Session["ordenPedido"];

            gvDetalle.DataSource = oOrden.detalle;
            gvDetalle.DataBind();
            lblNroOrden.Text = oOrden.nroOrden.ToString();
            lblFecha.Text = oOrden.formatFec;
            lblObs.Text = oOrden.obs;
            lblTotal.Text = oOrden.total.ToString();
            lblPresupuesto.Text = oOrden.nroPresupuesto;
            lblFacturas.Text = oOrden.nroFacturas;
            lblFormaPago.Text = oOrden.formaPago;
            lblProv.Text = oOrden.proveedor;
            lblDestino.Text = oOrden.destino;
            Oficinas origen = BLL.OficinasBLL.getOficinaByPk(oOrden.codOficinaOrigen);
            lblOrigen.Text = origen.nombre;
            lblSolicito.Text = oOrden.solicitante;
            lblAprobo.Text = oOrden.aprobado;
        }
    }
}