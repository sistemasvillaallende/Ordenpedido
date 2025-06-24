using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Web;
using System.Web.Services;

namespace WS
{
    /// <summary>
    /// Summary description for OPInsert
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class OPInsert : System.Web.Services.WebService
    {

        [WebMethod]
        public Int64 insertOP(OP obj)
        {
            Entities.OrdenPedido op = new Entities.OrdenPedido();
            StringBuilder strSQL = new StringBuilder();
            op.fechaOrden = obj.fecha;
            op.total = obj.total;
            op.saldo = obj.total;
            op.codProveedor = obj.cod_proveedor;

            return BLL.OrdenPedidoBLL.Insert(op);
        }

    }
}
