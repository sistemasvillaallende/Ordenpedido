using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Web.Reportes
{
    public partial class ReporteOrdenPedido : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GenerarPDF();
            }
        }

        private void GenerarPDF()
        {
            var oOrden = Request.QueryString["nroOrden"] != null
              ? BLL.OrdenPedidoBLL.getOrdenesByPk(int.Parse(Request.QueryString["nroOrden"]))
              : (Entities.OrdenPedido)Session["ordenPedido"];
            if (oOrden != null)
            {
                //OrdenPedidoPDF pdfGenerator = new OrdenPedidoPDF();
                //pdfGenerator.GenerarPDF(oOrden, Response);
                string logoPath = Server.MapPath("~/img/logogestion/escudomunicipalva.jpg");
                string usuario_imprime = Request.Cookies["UserOP"]["usuario"].ToString();
                ShowPdf(CreatePDF2(oOrden, logoPath, usuario_imprime));
            }
            else
            {
                Response.StatusCode = 404;
                Response.Write("Orden de Pedido no encontrada!!!");
            }
        }

        private void ShowPdf(byte[] strS)
        {
            //int anio = Convert.ToInt32(Request.QueryString["anio"]);
            //int nro_expediente = Convert.ToInt32(Request.QueryString["nro"]);
            string strExpediente = "prueba";
            Response.ClearContent();
            Response.ClearHeaders();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "Inline; filename=ordenpedido" + strExpediente + ".pdf");
            Response.BinaryWrite(strS);
            Response.End();
            Response.Flush();
            Response.Clear();
        }

        private byte[] CreatePDF2(Entities.OrdenPedido oOrden, string logoPath = null, string usuario_imprime = null)
        {
            OrdenPedidoPDF pdfGenerator = new OrdenPedidoPDF();
            var existe = BLL.OrdenPedidoBLL.readFactu_x_OP(oOrden.nroOrden);
            var constatada = false;
            if (existe != null && existe.Count > 0)
                constatada = true;
            else
                constatada = false;
            return pdfGenerator.GenerarPDF(oOrden, constatada, logoPath, usuario_imprime);//, Response);

        }
    }
}