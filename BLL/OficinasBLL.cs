using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BLL
{
    public class OficinasBLL
    {
        public static List<Entities.Oficinas> findOficinasByNombre(string nom)
        {
            return DAL.OficinasDAL.findOficinasByNombre(nom);
        }
        public static Entities.Oficinas getOficinaByPk(int cod)
        {
            return DAL.OficinasDAL.getOficinasByPk(cod);
        }

        public static List<Entities.Oficinas> OficinasByUsuario(string user)
        {
            return DAL.OficinasDAL.OficinasByUsuario(user);
        }

        public static DataSet ListOficinas(int id)
        {
            return DAL.OficinasDAL.ListOficinas(id);
        }
    }
}
