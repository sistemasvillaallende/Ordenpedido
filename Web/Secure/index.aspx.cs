using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entities;


namespace Web.Secure
{
    public partial class findOpBootstrap : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["UserOP"] != null)
            {
                //La cookie existe
                //string valor = Request.Cookies["UserOP"].Value;
                //Podés usar el valor
                if (!IsPostBack)
                {
                    lblOffice.InnerHtml = "<strong>" +
                        BLL.OficinasBLL.getOficinaByPk(Convert.ToInt32(Request.Cookies["UserOP"]["id_oficina_usuario"])).nombre + "</strong>";
                    lblUsuario.InnerHtml = "<strong>" +
                        Request.Cookies["UserOP"]["usuario"].ToString() + "</strong>";
                    fillGrilla(DAL.OrdenPedidoDAL.getOpByOficina(Convert.ToInt32(Request.Cookies["UserOP"]["id_oficina_usuario"])));
                }
            }
            else
            {
                // La cookie no existe
                Response.Redirect("../Login.aspx");
            }
        }

        protected void fillGrilla(List<Entities.OrdenPedido> lstOp)
        {
            gvOrden.DataSource = lstOp;
            gvOrden.DataBind();

            if (lstOp.Count > 0)
            {
                gvOrden.UseAccessibleHeader = true;
                gvOrden.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        protected void gvOrden_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "edit")
            {
                string cod = "0";
                int index = Convert.ToInt32(e.CommandArgument);
                int nro_oc = 0;
                nro_oc = Convert.ToInt32(gvOrden.DataKeys[index].Values["nroOrdenCompra"]);
                cod = Convert.ToString(gvOrden.DataKeys[index].Values["nroOrden"]);
                int codProveedor = Convert.ToInt32(gvOrden.DataKeys[index].Values["codProveedor"]);
                Entities.OrdenPedido objOp = BLL.OrdenPedidoBLL.getOrdenesByPk(int.Parse(cod));
                if (objOp.web == 1)
                    Response.Redirect("newOpBootstrap.aspx?op=" + cod);
                else
                    Response.Redirect("NewOpCC.aspx?op=" + cod);

            }
            //if (e.CommandName == "ver")
            //{
            //    int index = Convert.ToInt32(e.CommandArgument);
            //    string cod = Convert.ToString(gvOrden.DataKeys[index].Values["nroOrden"]);
            //    var estados = BLL.OrdenPedidoBLL.GetEstadoOP(int.Parse(cod)); // List<DAL.ConsultaEstadoOP>

            //    gvEstados.DataSource = estados;
            //    gvEstados.DataBind();

            //    string script = "$('#modalVerOP').modal('show');";
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "verOPModal", script, true);
            //}

        }

        protected void gvOrden_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    LinkButton btnEdit = (LinkButton)e.Row.FindControl("btnEdit");
                    Entities.OrdenPedido op = (Entities.OrdenPedido)e.Row.DataItem;
                    if (op.nroOrdenCompra == 0)
                        btnEdit.Visible = true;
                    else
                        btnEdit.Visible = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}