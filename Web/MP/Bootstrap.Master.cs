using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.MP
{
    public partial class Boottrap : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblUsuario.InnerHtml = Request.Cookies["UserOP"]["usuario"].ToString();
            lblUsuario2.InnerHtml = Request.Cookies["UserOP"]["usuario"].ToString();
        }
    }
}