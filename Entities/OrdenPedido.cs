using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public class OrdenPedido
    {
        public int nroOrden { get; set; }
        private DateTime atribFechaOrden;
        public DateTime fechaOrden
        {
            get { return atribFechaOrden; }
            set { atribFechaOrden = value; formatFec = atribFechaOrden.ToShortDateString(); }
        }
        public decimal total { get; set; }
        public decimal saldo { get; set; }
        public int codProveedor { get; set; }
        public int codOficinaOrigen { get; set; }
        public int codOficinaDestino { get; set; }
        public string solicitante { get; set; }
        public string aprobado { get; set; }
        public bool anulado { get; set; }
        public string usuario { get; set; }
        public bool finalizado { get; set; }
        public int asignado { get; set; }
        public int nroOrdenCompra { get; set; }
        public string formaPago { get; set; }
        public string nroPresupuesto { get; set; }
        public string nroFacturas { get; set; }
        public DateTime? fechaOrdenCompra { get; set; }
        public Int16 recibida { get; set; }
        public DateTime? fecha_recepcion { get; set; }
        public Int16 web { get; set; }
        public string entregadoPor { get; set; }
        public DateTime? fecha_aprobacion { get; set; }
        public Int16 codEstadoOP { get; set; }  

        public List<DetalleOrden> detalle { get; set; }
        public string obs { get; set; }
        public string proveedor { get; set; }
        public string destino { get; set; }
        public string obsAuditoria { get; set; }
        public string formatFec { get; set; }
        //public string observacion { get; set; }
        public string CUIT { get; set; }
    


        public OrdenPedido()
        {
            detalle = new List<DetalleOrden>();
            fechaOrdenCompra = null;
            obsAuditoria = string.Empty;
        }

    }
}
