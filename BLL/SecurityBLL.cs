using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
namespace BLL
{
  public class SecurityBLL
  {
    SecurityDAL objSeguridad = null;

    bool? estaAutenticado = null;
    string nombre;
    int id_oficina;

    public SecurityBLL()
    {
      nombre = "";
      objSeguridad = new DAL.SecurityDAL();
    }

    public string Nombre
    {
      get { return nombre; }
    }

    public bool ValidUser(string user, string password)
    {
      return objSeguridad.ValidUser(user, password, nombre);
    }

    public bool ValidaPermiso(string user, string Proceso, out int id_oficina)
    {
      return objSeguridad.ValidaPermiso(user, Proceso, out id_oficina);
    }

    public bool ValidaPermiso(string user, string Proceso)
    {
      return objSeguridad.ValidaPermiso(user, Proceso);
    }


  }
}
