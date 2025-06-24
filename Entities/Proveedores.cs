using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public class Proveedores
    {
        public int codProveedor { get; set; }
        public string nomProveedor { get; set; }
        public int nroBad { get; set; }
        public int codTipoProveedor { get; set; }
        public int codCondAnteIva { get; set; }
        public int codCalle { get; set; }
        public string nomCAlle { get; set; }
        public int nroDom { get; set; }
        public int codBarrio { get; set; }
        public string nomBarrio { get; set; }
        public string ciudad { get; set; }
        public string codPostal { get; set; }
        public string provincia { get; set; }
        public string pais { get; set; }
        public string nroCuit { get; set; }
        public string nroIngBruto { get; set; }
        public string nroCajaJub { get; set; }
        public string telefono { get; set; }
        public string pisoDpto { get; set; }
        public DateTime? fechaAlta { get; set; }
        public string eMail { get; set; }
        public int codSubtipo { get; set; }
    
        public Proveedores()
        {
            codProveedor = 0;
            nomProveedor = string.Empty;
            nroBad = 0;
            codTipoProveedor = 0;
            codCondAnteIva = 0;
            codCalle = 0;
            nomCAlle = string.Empty;
            nroDom = 0;
            codBarrio = 0;
            nomBarrio = string.Empty;
            ciudad = string.Empty;
            codPostal = string.Empty;
            provincia = string.Empty;
            pais = string.Empty;
            nroCuit = string.Empty;
            nroIngBruto = string.Empty;
            nroCajaJub = string.Empty;
            telefono = string.Empty;
            pisoDpto = string.Empty;
            fechaAlta = null;
            eMail = string.Empty;
            codSubtipo = 0;
        }
    }
    
}
