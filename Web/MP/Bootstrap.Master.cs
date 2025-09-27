using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.MP
{
    public partial class Bootstrap : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                HttpCookie userCookie = Request.Cookies["UserOP"];
                string usuario = (userCookie != null && userCookie["usuario"] != null)
                                 ? userCookie["usuario"]
                                 : "Invitado";

                lblUsuario.Text = usuario;
                lblUsuario2.Text = usuario;
            }
        }
    }
}