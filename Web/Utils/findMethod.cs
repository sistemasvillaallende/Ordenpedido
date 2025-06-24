using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace Web.Utils
{
    public class findMethod
    {
        public static string findProv()
        {
            List<Entities.Proveedores> lstProv = BLL.ProveedoreBLL.findProveedorByNombre("");
            StringBuilder lista = new StringBuilder();
            foreach (Entities.Proveedores prov in lstProv)
            {
                lista.Append(String.Format("{0}:", prov.nomProveedor));
                //Se elimina el ultimo ":"
                lista = lista.Remove(lista.Length - 1, 1);
            }
            return lista.ToString();
        }
    }
}