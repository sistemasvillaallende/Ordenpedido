using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;
namespace BLL
{
    public class UsuarioBLL
    {
        public static List<Usuarios> getUserByOfice(int codOffice)
        {
            return DAL.UsuariosDAL.getUserByOffice(codOffice);
        }
    }
}
