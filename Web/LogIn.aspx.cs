using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;


namespace Web
{
    public partial class logonin : System.Web.UI.Page
    {


        BLL.SecurityBLL objSeguridad;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                txtPass.Attributes.Add("onKeyPress", "javascript:if (event.keyCode == 13) __doPostBack('" + btnIngresar.UniqueID + "','')");

                if (Request.QueryString["cerrar"] != null)
                {
                    Session.Abandon();
                }
                txtUser.Focus();
            }
        }



        protected void btnIngresar_ServerClick(object sender, EventArgs e)
        {
            bool ok = false;
            int id_oficina = 0;
            objSeguridad = new BLL.SecurityBLL();


            ok = objSeguridad.ValidUser(txtUser.Text, txtPass.Value);

            if (ok == true)
            {
                ok = false;
                ok = objSeguridad.ValidaPermiso(txtUser.Text, "CONTABILIDAD", out id_oficina);
                if (ok == true)
                {
                    id_oficina = Convert.ToInt32(ddlOficina.SelectedValue);
                    Session["id_oficina_usuario"] = id_oficina;
                    if (id_oficina == 0)
                    {
                        lblPregunta.Text = "Para Ingresar debe Seleccionar una Oficina...";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "$('#myModalMessage').modal('show');", true);
                    }
                    else
                    {
                        HttpCookie cookie = new HttpCookie("UserOP");
                        cookie["usuario"] = txtUser.Text.ToLower();
                        cookie["id_oficina_usuario"] = id_oficina.ToString();
                        cookie.Expires = DateTime.Now.AddDays(1);
                        Response.Cookies.Add(cookie);
                        FormsAuthentication.RedirectFromLoginPage(Request.Cookies["UserOP"]["usuario"].ToString(), false);
                        Response.Redirect("~\\secure\\index.aspx");
                    }
                }
                else
                {
                    // Response.Redirect("~\\secure\\accesodenegado.html");
                    divError.Visible = true;
                    lblError.InnerHtml = "Ud. no está autorizado a Visualizar esta Página,<br/>Por favor comuníquese con nuestra<br/>" +
                                          "Oficina de Sistemas al Interno 255/256";
                    divLogIn.Visible = false;
                    txtUser.Focus();
                }
            }
            else
            {
                //ImageOutput.Visible = true;
                //lblOutput.Text = "Usuario o Password Incorrecta...";
                if (ok == false)
                {
                    divError.Visible = true;
                    //lblError.InnerHtml = "No puede iniciar la Sesión. Usuario o Password Incorrecta!!! <br/>Por favor verifique los datos ingresados.";
                    lblError.InnerHtml = "<div class='alert alert-secondary' role='alert'><strong>¡Ups!</strong> No pudimos iniciar sesión.<br/>" +
                        "Verificá que el <strong>usuario</strong> y la <strong>contraseña</strong> sean correctos.</div>";
                    divLogIn.Visible = false;
                    ddlOficina.SelectedIndex = 0;
                }
                txtPass.Focus();
            }

            objSeguridad = null;
        }

        protected void txtUser_TextChanged(object sender, EventArgs e)
        {
            ListItem itemInicial = new ListItem();
            itemInicial.Text = "Seleccione Oficina";
            itemInicial.Value = "0";
            ddlOficina.Items.Clear();
            ddlOficina.Items.Add(itemInicial);
            ddlOficina.DataSource = BLL.OficinasBLL.OficinasByUsuario(txtUser.Text.Replace("%", "").TrimEnd());
            ddlOficina.DataTextField = "nombre";
            ddlOficina.DataValueField = "idOficina";
            ddlOficina.DataBind();
            txtPass.Focus();
            if (ddlOficina.Items.Count == 1)
            {
                divError.Visible = true;
                lblError.InnerHtml = "Usuario no Existe, <br/>Por favor Ingrese nuevamente el Nombre de Usuario.";
                divLogIn.Visible = false;
                ddlOficina.SelectedIndex = 0;

            }
        }

        protected void btnOkError_Click(object sender, EventArgs e)
        {
            divError.Visible = false;
            lblError.InnerHtml = string.Empty;
            divLogIn.Visible = true;
            txtUser.Focus();
        }

        protected void btnModalYes_ServerClick(object sender, EventArgs e)
        {

        }
    }
}