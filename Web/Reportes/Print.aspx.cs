using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Configuration;
using System.Web.UI;

namespace Web.Secure
{
    public partial class Print : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var oOrden = Request.QueryString["nroOrden"] != null
                ? BLL.OrdenPedidoBLL.getOrdenesByPk(int.Parse(Request.QueryString["nroOrden"]))
                : (Entities.OrdenPedido)Session["ordenPedido"];

            var reporte = new ReportDocument();
            reporte.Load(Server.MapPath("~/Reportes/rptOp.rpt"));
            reporte.SetDataSource(oOrden.detalle);

            reporte.SetParameterValue("Fecha", oOrden.formatFec ?? string.Empty);
            reporte.SetParameterValue("Obs", oOrden.obs ?? string.Empty);
            reporte.SetParameterValue("nroPresupuesto", oOrden.nroPresupuesto ?? string.Empty);
            reporte.SetParameterValue("nroFactura", oOrden.nroFacturas ?? string.Empty);
            reporte.SetParameterValue("formaPago", oOrden.formaPago ?? string.Empty);
            reporte.SetParameterValue("proveedor", oOrden.proveedor ?? string.Empty);
            reporte.SetParameterValue("destino", oOrden.destino ?? string.Empty);

            var origen = BLL.OficinasBLL.getOficinaByPk(oOrden.codOficinaOrigen);
            reporte.SetParameterValue("origen", origen?.nombre ?? string.Empty);
            reporte.SetParameterValue("solicito", oOrden.solicitante ?? string.Empty);
            reporte.SetParameterValue("aprobo", oOrden.aprobado ?? string.Empty);
            reporte.SetParameterValue("usuario", Convert.ToString(Session["usuario"]) ?? string.Empty);

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