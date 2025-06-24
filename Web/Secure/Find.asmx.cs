using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Web.Secure
{
    /// <summary>
    /// Descripción breve de Find
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio Web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    public class Find : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod]
        public string[] findProv(string nombre)
        {
            List<string> nombres = new List<string>();
            List<Entities.Proveedores> lstProv = new List<Entities.Proveedores>();
            lstProv = BLL.ProveedoreBLL.findProveedorByNombre(nombre);
            for(int i = 0; i < lstProv.Count; i++)
                nombres.Add(lstProv[i].nomProveedor);

            return nombres.ToArray();
        }

    }
}
