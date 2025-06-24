using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Utils
{
    public class SessionHandler
    {
        private static string proveedor = "PROVEEDOR";
        private static string proveedores = "PROVEEDORES";
        private static string lstDetalle = "DETALE";

        public static Entities.Proveedores Proveedor
        {
            get
            {
                if (HttpContext.Current.Session[proveedor] == null)
                {
                    return new Entities.Proveedores();
                }
                else
                {
                    return (Entities.Proveedores)HttpContext.Current.Session[proveedor];
                }
            }
            set
            {
                HttpContext.Current.Session[proveedor] = value;
            }
        }

        public static List<Entities.Proveedores> Proveedores
        {
            get
            {
                if (HttpContext.Current.Session[proveedores] == null)
                {
                    return new List<Entities.Proveedores>();
                }
                else
                {
                    return (List<Entities.Proveedores>)HttpContext.Current.Session[proveedores];
                }
            }
            set
            {
                HttpContext.Current.Session[proveedores] = value;
            }
        }

        public static List<Entities.DetalleOrden> detalleOrden
        {
            get
            {
                if (HttpContext.Current.Session[lstDetalle] == null)
                {
                    return new List<Entities.DetalleOrden>();
                }
                else
                {
                    return (List<Entities.DetalleOrden>)HttpContext.Current.Session[lstDetalle];
                }
            }
            set
            {
                HttpContext.Current.Session[lstDetalle] = value;
            }
        }
    }
}