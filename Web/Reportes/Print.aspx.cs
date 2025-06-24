using CrystalDecisions.CrystalReports.Engine;
using Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Secure
{
    public partial class Print : System.Web.UI.Page
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

            var reporte = new ReportDocument();
            reporte.Load(Server.MapPath("~/Reportes/rptOp.rpt"));
            reporte.SetDataSource(oOrden.detalle);
            if (oOrden.formatFec != null)
                reporte.SetParameterValue("Fecha", oOrden.formatFec);
            else
                reporte.SetParameterValue("Fecha", string.Empty);
            if (oOrden.obs != null)
                reporte.SetParameterValue("Obs", oOrden.obs);
            else
                reporte.SetParameterValue("Obs", string.Empty);
            if (oOrden.nroPresupuesto != null)
                reporte.SetParameterValue("nroPresupuesto", oOrden.nroPresupuesto);
            else
                reporte.SetParameterValue("nroPresupuesto", string.Empty);
            if (oOrden.nroFacturas != null)
                reporte.SetParameterValue("nroFactura", oOrden.nroFacturas);
            else
                reporte.SetParameterValue("nroFactura", string.Empty);
            if (oOrden.formaPago != null)
                reporte.SetParameterValue("formaPago", oOrden.formaPago);
            else
                reporte.SetParameterValue("formaPago", string.Empty);
            if (oOrden.proveedor != null)
                reporte.SetParameterValue("proveedor", oOrden.proveedor);
            else
                reporte.SetParameterValue("proveedor", string.Empty);
            if (oOrden.destino != null)
                reporte.SetParameterValue("destino", oOrden.destino);
            else
                reporte.SetParameterValue("destino", oOrden.destino);
            Oficinas origen = BLL.OficinasBLL.getOficinaByPk(oOrden.codOficinaOrigen);
            if (origen.nombre != null)
                reporte.SetParameterValue("origen", origen.nombre);
            else
                reporte.SetParameterValue("origen", string.Empty);
            if (oOrden.solicitante != null)
                reporte.SetParameterValue("solicito", oOrden.solicitante);
            else
                reporte.SetParameterValue("solicito", string.Empty);
            if (oOrden.aprobado != null)
                reporte.SetParameterValue("aprobo", oOrden.aprobado);
            else
                reporte.SetParameterValue("aprobo", string.Empty);



            if (Session["usuario"] != null)
                reporte.SetParameterValue("usuario", Convert.ToString(Session["usuario"]));
            else
                reporte.SetParameterValue("usuario", string.Empty);

            switch (oOrden.web)
            {
                case 0:
                    reporte.SetParameterValue("msjAfip", ConfigurationManager.AppSettings["msjAfipNoVerificada"]);
                    reporte.SetParameterValue("picturePath", "0");
                    break;
                case 1:
                    reporte.SetParameterValue("msjAfip", ConfigurationManager.AppSettings["msjAfipVerificada"]);
                    reporte.SetParameterValue("picturePath", "1");
                    break;
                default:
                    reporte.SetParameterValue("msjAfip", ConfigurationManager.AppSettings["msjAfipNoVerificada"]);
                    reporte.SetParameterValue("picturePath", "0");
                    break;
            }

            VisorCR.ReportSource = reporte;

            reporte.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "");
        }
    }
}