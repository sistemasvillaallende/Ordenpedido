using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;
using System.Transactions;
using DAL;
using System.Data.SqlClient;

namespace BLL
{
    public class OrdenPedidoBLL
    {
        public static string FechaServer()
        {
            return DAL.OrdenPedidoDAL.FechaServer();
        }
        public static Int64 Insert(OrdenPedido op, List<DAL.FACTURA_X_ORDEN_PEDIDO> lstFacturas)
        {
            Int64 nroOrden = 0;
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    nroOrden = DAL.OrdenPedidoDAL.getNroOP();
                    op.nroOrden = Convert.ToInt32(nroOrden);
                    nroOrden = OrdenPedidoDAL.insert(op, lstFacturas);
                    int i = 1;
                    foreach (var itemDet in op.detalle)
                    {
                        itemDet.nroItems = i;
                        itemDet.nroOrden = op.nroOrden;
                        OrdenPedidoDAL.insertDetalle(itemDet);
                        i++;
                    }
                    OrdenPedidoDAL.insertAuditoria(op, 0);
                    foreach (var item in lstFacturas)
                    {
                        item.NRO_ORDEN_PEDIDO = op.nroOrden;
                        FACTURA_X_ORDEN_PEDIDO.insert(item);
                    }
                    OrdenPedidoDAL.updateNroClave(op.nroOrden);
                    scope.Complete();
                }
                catch (Exception ex) { throw ex; }
            }
            return nroOrden;
        }

        //public static void Update(OrdenPedido op, List<DAL.FACTURA_X_ORDEN_PEDIDO> lstFacturas)
        //{
        //    using (TransactionScope scope = new TransactionScope( ))
        //    {
        //        try
        //        {
        //            OrdenPedidoDAL.updateOP(op);
        //            OrdenPedidoDAL.updateDetalle(op);
        //            int i = 1;
        //            foreach (var itemDet in op.detalle)
        //            {
        //                itemDet.nroItems = i;
        //                OrdenPedidoDAL.insertDetalle(itemDet);
        //                i++;
        //            }
        //            OrdenPedidoDAL.insertAuditoria(op, 1);
        //            DAL.FACTURA_X_ORDEN_PEDIDO.delete(op.nroOrden);
        //            foreach (var item in lstFacturas)
        //            {
        //                item.NRO_ORDEN_PEDIDO = op.nroOrden;
        //                FACTURA_X_ORDEN_PEDIDO.insert(item);
        //            }
        //            scope.Complete();
        //        }
        //        catch (Exception ex) { throw ex; }
        //    }
        //}


        public static void Update(OrdenPedido op, List<DAL.FACTURA_X_ORDEN_PEDIDO> lstFacturas)
        {
            using (SqlConnection cn = DAL.DALBase.GetConnection())
            {
                cn.Open();
                using (SqlTransaction trx = cn.BeginTransaction())
                {
                    try
                    {
                        OrdenPedidoDAL.updateOP(op, cn, trx);
                        OrdenPedidoDAL.updateDetalle(op, cn, trx);

                        int i = 1;
                        foreach (var itemDet in op.detalle)
                        {
                            itemDet.nroItems = i;
                            itemDet.nroOrden = op.nroOrden;
                            OrdenPedidoDAL.insertDetalle(itemDet, cn, trx);
                            i++;
                        }

                        OrdenPedidoDAL.insertAuditoria(op, 1, cn, trx);
                        DAL.FACTURA_X_ORDEN_PEDIDO.delete(op.nroOrden, cn, trx);

                        foreach (var item in lstFacturas)
                        {
                            item.NRO_ORDEN_PEDIDO = op.nroOrden;
                            FACTURA_X_ORDEN_PEDIDO.insert(item, cn, trx);
                        }

                        trx.Commit();
                    }
                    catch
                    {
                        trx.Rollback();
                        throw;
                    }
                }
            }
        }

        public static Entities.OrdenPedido getOrdenesByPk(int cod)
        {
            return DAL.OrdenPedidoDAL.getsOpByPk(cod);
        }

        public static List<Entities.OrdenPedido> getOrdenes(int idOrigen, int nroOrden,
            int idDestino, int idProv, string aut, string solic,
            string nroPresup, DateTime? fecDesde, DateTime? fecHasta, string nombreUsuario)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.AppendLine(" Cod_oficina_origen = " + idOrigen);

            if (nroOrden != 0)
                strSQL.AppendLine(" AND Nro_orden_pedido = " + nroOrden);

            if (idDestino != 0)
                strSQL.AppendLine(" AND OP.Cod_oficina_destino = " + idDestino);

            if (idProv != 0)
                strSQL.AppendLine(" AND OP.Cod_proveedor = " + idProv);

            if (aut.Trim() != string.Empty)
                strSQL.AppendLine(" AND Aprobado LIKE '%" + aut.Trim().ToUpper() + "%'");

            if (solic.Trim() != string.Empty)
                strSQL.AppendLine(" AND Solicitante LIKE '%" + solic.ToUpper().Trim() + "%'");

            if (nroPresup.Trim() != string.Empty)
                strSQL.AppendLine(" AND Nro_presupuesto LIKE '%" +
                    nroPresup.Trim().ToUpper() + "%'");

            if (fecDesde != null && fecHasta == null)
                strSQL.AppendLine(" AND Fecha_orden_pedido > '" +
                    fecDesde.Value.ToShortDateString() + "'");

            if (fecDesde == null && fecHasta != null)
                strSQL.AppendLine(" AND Fecha_orden_pedido < '" +
                    fecHasta.Value.ToShortDateString() + "'");

            if (fecDesde != null && fecHasta != null)
                strSQL.AppendLine(" AND Fecha_orden_pedido BETWEEN '" +
                    fecDesde.Value.ToShortDateString() + "' AND '" +
                    fecHasta.Value.ToShortDateString() + "'");

            if (nombreUsuario != string.Empty)
                strSQL.AppendLine(" AND OP.Usuario = '" + nombreUsuario + "'");

            return DAL.OrdenPedidoDAL.getsOP(strSQL.ToString());
        }

        public static List<FACTURA_X_ORDEN_PEDIDO> readFactu_x_OP(int nroOp)
        {
            return DAL.FACTURA_X_ORDEN_PEDIDO.read(nroOp);
        }

        public static List<ConsultaEstadoOP> GetEstadoOP(int nroNotaPedido)
        {
            return DAL.ConsultaEstadoOP.GetByOP(nroNotaPedido);
        }
    }
}
