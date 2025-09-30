using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace Web
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();

            if (ex is HttpException httpEx)
            {
                // El mensaje que devuelve cuando se excede el tamaño
                if (httpEx.Message.Contains("Se excedió la longitud de solicitud máxima"))
                {
                    // Evita que salga la página amarilla de error
                    Server.ClearError();

                    // Redirige a página personalizada
                    Response.Redirect("~/ErrorArchivos.aspx?tipo=limite");
                    return;
                }
            }

            // Si no es ese error, va al error general
            Server.ClearError();
            Response.Redirect("~/ErrorGeneral.aspx");
        }
    }
}