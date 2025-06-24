using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public class DetalleOrden
    {
        public int nroOrden { get; set; }
        public int nroItems { get; set; }
        public string descItems { get; set; }
        public decimal cant { get; set; }
        public decimal precio { get; set; }
        public decimal importe { get; set; }

        public DetalleOrden()
        { 
        
        }
    }
    
}
