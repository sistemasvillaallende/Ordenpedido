using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Web.Secure
{
    public partial class AddFactura : System.Web.UI.Page
    {
        WSAFIP.WSAFIPSoapClient WSAfip = new WSAFIP.WSAFIPSoapClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void btnConsulta_Click(object sender, EventArgs e)
        {
            try
            {
                WSAFIP.CmpResponse resultado =
                WSAfip.consultaComprobante(Convert.ToDateTime(txtFechaEmision.Text), "CAE", long.Parse(txtNumeroComprobante.Text),
                   Convert.ToInt32(DDLTipoComprobante.SelectedItem.Value), txtCAE.Text, long.Parse(txtCUIT.Text),
                   double.Parse(txtImporte.Text), int.Parse(txtPuntoVenta.Text));
                if (resultado.Resultado == "A")
                {
                    lblactura.InnerHtml = string.Format("Factura: {0} {1}-{2} Monto: {3:C} CUIT: {4}",
                        resultado.CmpResp.CbteTipo, resultado.CmpResp.PtoVta.ToString().PadLeft(4,Convert.ToChar("0")), 
                        resultado.CmpResp.CbteNro.ToString().PadLeft(8, Convert.ToChar("0")),
                        resultado.CmpResp.ImpTotal, resultado.CmpResp.CuitEmisor);

                    if (resultado.Observaciones != null && resultado.Observaciones.Length > 0)
                    {
                        divObsOk.Visible = true;
                        foreach (var item in resultado.Observaciones)
                        {
                            HtmlGenericControl li = new HtmlGenericControl();
                            li.InnerText = item.Msg;
                            ulObsOk.Controls.Add(li);
                        }

                    }
                    else
                        divObsOk.Visible = false;

                    divConsultaRequest.Visible = false;
                    divConsultaResponse.Visible = true;
                    divConsultaError.Visible = false;
                }
                else
                {
                    divConsultaRequest.Visible = false;
                    divConsultaResponse.Visible = false;
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
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void txtBarsCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string cuit = txtBarsCode.Text.Substring(0, 11);
                int tipoComprobante = int.Parse(txtBarsCode.Text.Substring(11, 3));
                int puntoVenta = int.Parse(txtBarsCode.Text.Substring(14, 5));
                string codAutorizacion = txtBarsCode.Text.Substring(19, 14);

                txtCUIT.Text = cuit;
                txtCUIT.Enabled = false;
                DDLTipoComprobante.SelectedValue = tipoComprobante.ToString();
                DDLTipoComprobante.Enabled = false;
                txtPuntoVenta.Text = puntoVenta.ToString();
                txtPuntoVenta.Enabled = false;
                txtCAE.Text = codAutorizacion;
                txtCAE.Enabled = false;



            }
            catch (Exception ex)
            {

                throw;
            }
        }

        protected void gvDetalle_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void gvDetalle_RowCreated(object sender, GridViewRowEventArgs e)
        {

        }

        protected void lbtnAddDetalle_Click(object sender, EventArgs e)
        {

        }

        protected void gvDetalle_RowCommand1(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void gvDetalle_RowCreated1(object sender, GridViewRowEventArgs e)
        {

        }

        protected void txtCantidad_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtPU_TextChanged(object sender, EventArgs e)
        {

        }

        protected void Button2_ServerClick(object sender, EventArgs e)
        {

        }

        protected void Button1_ServerClick(object sender, EventArgs e)
        {

        }
    }
}