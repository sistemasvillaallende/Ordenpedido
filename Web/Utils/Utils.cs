using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Utils
{
    public class Utils
    {
        public static string getTipoComprobante(int cod)
        {
            try
            {
                string tipoComp = string.Empty;
                switch (cod)
                {
                    case 1:
                        tipoComp = "Factura A";
                        break;
                    case 2:
                        tipoComp = "Nota de Débito A";
                        break;
                    case 3:
                        tipoComp = "Nota de Crédito A";
                        break;
                    case 4:
                        tipoComp = "Recibo A";
                        break;
                    case 5:
                        tipoComp = "Nota de Venta al Contado A";
                        break;
                    case 6:
                        tipoComp = "Factura B";
                        break;
                    case 7:
                        tipoComp = "Nota de Débito B";
                        break;
                    case 8:
                        tipoComp = "Nota de Crédito B";
                        break;
                    case 9:
                        tipoComp = "Recibo B";
                        break;
                    case 10:
                        tipoComp = "Nota de Venta al Contado B";
                        break;
                    case 11:
                        tipoComp = "Factura C";
                        break;
                    case 12:
                        tipoComp = "Nota de Débito C";
                        break;
                    case 13:
                        tipoComp = "Nota de Crédito C";
                        break;
                    case 15:
                        tipoComp = "Recibo C";
                        break;
                    case 19:
                        tipoComp = "Factura de Exportación";
                        break;
                    case 20:
                        tipoComp = "Nota Déb. P/Operac. con el Exterior";
                        break;
                    case 21:
                        tipoComp = "Nota Créd. P/Operac. con el Exterior";
                        break;
                    case 39:
                        tipoComp = "Otros Comprobantes A que Cumplan con la R.G. Nro. 1415";
                        break;
                    case 40:
                        tipoComp = "Otros Comprobantes B que Cumplan con la R.G. Nro. 1415";
                        break;
                    case 49:
                        tipoComp = "Comprobante de Compra de Bienes Usados";
                        break;
                    case 51:
                        tipoComp = "Factura M";
                        break;
                    case 52:
                        tipoComp = "Nota de Débito M";
                        break;
                    case 53:
                        tipoComp = "Nota de Crédito M";
                        break;
                    case 54:
                        tipoComp = "Recibo M";
                        break;
                    case 60:
                        tipoComp = "Cta. de Vta. y Líquido Prod. A";
                        break;
                    case 61:
                        tipoComp = "Cta. de Vta. y Líquido Prod. B";
                        break;
                    case 63:
                        tipoComp = "Liquidación A";
                        break;
                    case 64:
                        tipoComp = "Liquidación B";
                        break;
                    default:
                        break;
                }
                return tipoComp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}