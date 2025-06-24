using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WS
{
    public class OP
    {
        public DateTime fecha { get; set; }
        public decimal total { get; set; }
        public int cod_proveedor { get; set; }
        public string observacion { get; set; }
        public List<DetOp> lst { get; set; }

        public OP()
        {
            fecha = DateTime.Now;
            total = 0;
            cod_proveedor = 0;
            observacion = string.Empty;
            lst = new List<DetOp>();
        }
    }
}
