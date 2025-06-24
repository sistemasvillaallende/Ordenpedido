

using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;
using System.Reflection;


using System.Net;

namespace Web.Secure
{
    public partial class ExportPdf : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int idOrigen = 19;
            int nroOrden = 0;
            int idDestino = 0;
            int idProv = 0;
            string autorizado = string.Empty;
            string solicitante = string.Empty;
            string nroPresupuesto = string.Empty;
            DateTime? fecDesde = null;
            DateTime? fecHasta = null;
            string nombreUsuario = string.Empty;

            fillGrilla(BLL.OrdenPedidoBLL.getOrdenes(idOrigen, nroOrden, idDestino, idProv,
                    autorizado, solicitante, nroPresupuesto, fecDesde, fecHasta, nombreUsuario));
            //Filtros_Command(btnBuscar, null);
        }

        protected void fillGrilla(List<Entities.OrdenPedido> lstOp)
        {
            gvOrdenes.DataSource = lstOp;
            gvOrdenes.DataBind();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string apiKey = "abcde12345";
            string url = "http://www.google.com";
            using (var client = new WebClient())
            {
                client.QueryString.Add("apikey", apiKey);
                client.QueryString.Add("url", url);
                client.DownloadFile("http://api.htmlapdf.com/http://www.forosdelweb.com/f78/pasar-html-pdf-1016410/", @"c:\temp\mipdf.pdf");

            }
        }
        public void HTMLToPDF(string html, string fullDestinyFilePath)
        {
            //StringWriter sw = new StringWriter();
            //sw.WriteLine(html);
            //StringReader sr = new StringReader(sw.ToString());
            //Document pdfDoc = new Document();
            //HTMLWorker htmlparser = new HTMLWorker(pdfDoc);

            //PdfWriter.GetInstance(pdfDoc, new FileStream(Environment.GetFolderPath
            //(Environment.SpecialFolder.Desktop)
            //+ "\\Prueba.pdf", FileMode.Create));
            //pdfDoc.Open();
            //htmlparser.Parse(sr);

            

            //pdfDoc.Close();
        }

        protected void gvOrdenes_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void gvOrdenes_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

    }
}