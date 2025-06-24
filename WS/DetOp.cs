using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WS
{
    public class DetOp
    {
        public int cod_mat { get; set; }
        public string descripcion { get; set; }
        public decimal cantidad { get; set; }
        public decimal precio { get; set; }

        public DetOp()
        {
            cod_mat = 0;
            descripcion = string.Empty;
            cantidad = 0;
            precio = 0;
        }
    }
}
