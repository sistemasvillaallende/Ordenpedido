using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class ProveedoreBLL
    {
        public static Entities.Proveedores getProveedorByPk(int cod)
        {
            return DAL.ProveedoresDAL.getProveedorByPk(cod);
        }
        public static List<Entities.Proveedores> findProveedorByNombre(string nom)
        {
            return DAL.ProveedoresDAL.findProveedorByNombre(nom);
        }
        public static List<Entities.Proveedores> read()
        {
            return DAL.ProveedoresDAL.read();
        }
    }
}
