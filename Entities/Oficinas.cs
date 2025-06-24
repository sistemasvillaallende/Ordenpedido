using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
  public class Oficinas
  {
    public int idOficina { get; set; }
    public string nombre { get; set; }
    public int secretaria { get; set; }
    public int activo { get; set; }

    public Oficinas()
    {
      idOficina = 0;
      nombre = string.Empty;
      secretaria = 0;
      activo = 0;
    }
  }
}
