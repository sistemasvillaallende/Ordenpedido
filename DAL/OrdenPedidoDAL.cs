using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class OrdenPedidoDAL : DALBase
    {

        public static string FechaServer()
        {

            string fecha = "";
            SqlCommand cmd;
            SqlDataReader dr;
            SqlConnection cn = null;

            cmd = new SqlCommand();

            string strSQL = "SELECT CONVERT(VARCHAR(10), GETDATE(),103) as fecha";
            try
            {
                cn = DALBase.GetConnection();

                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSQL.ToString();
                cmd.Connection.Open();

                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    fecha = dr.GetString((dr.GetOrdinal("fecha")));
                }
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                cn.Close();
                cmd = null;
            }
            return fecha;
        }

        public static int getNroOP()
        {
            try
            {
                using (SqlConnection con = GetConnection())
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT nro_orden_pedido FROM NUMEROS_CLAVES";
                    cmd.Connection.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar()) + 1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void updateNroClave(int nroOP)
        {
            try
            {
                using (SqlConnection con = GetConnection())
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "UPDATE NUMEROS_CLAVES SET nro_orden_pedido=@nro_orden_pedido";
                    cmd.Parameters.AddWithValue("@nro_orden_pedido", nroOP);
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static Int64 insert(OrdenPedido op, List<FACTURA_X_ORDEN_PEDIDO> lstFacturas)
        {
            StringBuilder strSQL = new StringBuilder();
            try
            {
                strSQL.AppendLine("INSERT INTO ORDENES_PEDIDO");
                strSQL.AppendLine("(Nro_orden_pedido,");
                strSQL.AppendLine("Fecha_orden_pedido,");
                strSQL.AppendLine("Total,");
                strSQL.AppendLine("Saldo,");
                strSQL.AppendLine("Cod_proveedor,");
                strSQL.AppendLine("Cod_oficina_origen,");
                strSQL.AppendLine("Cod_oficina_destino,");
                strSQL.AppendLine("Solicitante,");
                strSQL.AppendLine("Aprobado,");
                strSQL.AppendLine("Anulado,");
                strSQL.AppendLine("Usuario,");
                strSQL.AppendLine("Observacion,");
                strSQL.AppendLine("Finalizado,");
                strSQL.AppendLine("Asignado,");
                //Nro_orden_compra
                strSQL.AppendLine("Forma_pago,");
                strSQL.AppendLine("Nro_presupuesto,");
                strSQL.AppendLine("Nro_facturas,");
                //Asigno Valores por defecto
                strSQL.AppendLine("Web,");
                strSQL.AppendLine("Recibida,");
                strSQL.AppendLine("Cod_estado_op)");
                //Fecha_orden_compra
                strSQL.AppendLine("VALUES");
                strSQL.AppendLine("(@Nro_orden_pedido,");
                strSQL.AppendLine("@Fecha_orden_pedido,");
                strSQL.AppendLine("@Total,");
                strSQL.AppendLine("@Saldo,");
                strSQL.AppendLine("@Cod_proveedor,");
                strSQL.AppendLine("@Cod_oficina_origen,");
                strSQL.AppendLine("@Cod_oficina_destino,");
                strSQL.AppendLine("@Solicitante,");
                strSQL.AppendLine("@Aprobado,");
                strSQL.AppendLine("0,");
                strSQL.AppendLine("@Usuario,");
                strSQL.AppendLine("@Observacion,");
                strSQL.AppendLine("0,0,");
                strSQL.AppendLine("@Forma_pago,");
                strSQL.AppendLine("@Nro_presupuesto,");
                strSQL.AppendLine("@Nro_facturas,");
                strSQL.AppendLine("@web,0,0)");

                using (SqlConnection con = GetConnection())
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();

                    cmd.Parameters.Add(new SqlParameter("@Nro_orden_pedido", op.nroOrden));
                    cmd.Parameters.Add(new SqlParameter("@Fecha_orden_pedido", op.fechaOrden));
                    cmd.Parameters.Add(new SqlParameter("@Total", op.total));
                    cmd.Parameters.Add(new SqlParameter("@Saldo", op.saldo));
                    cmd.Parameters.Add(new SqlParameter("@Cod_proveedor", op.codProveedor));
                    cmd.Parameters.Add(new SqlParameter("@Cod_oficina_origen", op.codOficinaOrigen));
                    cmd.Parameters.Add(new SqlParameter("@Cod_oficina_destino", op.codOficinaDestino));

                    if (op.solicitante != null)
                        cmd.Parameters.Add(new SqlParameter("@Solicitante", op.solicitante));
                    else
                        cmd.Parameters.Add(new SqlParameter("@Solicitante", System.Data.SqlTypes.SqlString.Null));

                    if (op.aprobado != null)
                        cmd.Parameters.Add(new SqlParameter("@Aprobado", op.aprobado));
                    else
                        cmd.Parameters.Add(new SqlParameter("@Aprobado", System.Data.SqlTypes.SqlString.Null));

                    cmd.Parameters.Add(new SqlParameter("@Usuario", op.usuario));

                    if (op.obs != null)
                        cmd.Parameters.Add(new SqlParameter("@Observacion", op.obs));
                    else
                        cmd.Parameters.Add(new SqlParameter("@Observacion", System.Data.SqlTypes.SqlString.Null));
                    if (op.formaPago != null)
                        cmd.Parameters.Add(new SqlParameter("@Forma_pago", op.formaPago));
                    else
                        cmd.Parameters.Add(new SqlParameter("@Forma_pago", System.Data.SqlTypes.SqlString.Null));

                    if (op.nroPresupuesto != null)
                        cmd.Parameters.Add(new SqlParameter("@Nro_presupuesto", op.nroPresupuesto));
                    else
                        cmd.Parameters.Add(new SqlParameter("@Nro_presupuesto", System.Data.SqlTypes.SqlString.Null));


                    if (op.nroFacturas != null)
                        cmd.Parameters.Add(new SqlParameter("@Nro_facturas", op.nroFacturas));
                    else
                        cmd.Parameters.Add(new SqlParameter("@Nro_facturas", System.Data.SqlTypes.SqlString.Null));


                    cmd.Parameters.Add(new SqlParameter("@web", op.web));
                    cmd.Parameters.Add(new SqlParameter("@recibida", op.recibida));
                    cmd.Parameters.Add(new SqlParameter("@cod_estado_op", op.codEstadoOP));

                    cmd.Connection.Open();

                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ex) { throw ex; }

            return op.nroOrden;
        }

        public static void insertDetalle(DetalleOrden det)
        {
            try
            {
                StringBuilder strSQL = new StringBuilder();

                strSQL.AppendLine("INSERT INTO DETALLE_ORDEN_PEDIDO");
                strSQL.AppendLine("(Nro_orden_pedido,");
                strSQL.AppendLine("Nro_item,");
                strSQL.AppendLine("Desc_item,");
                strSQL.AppendLine("Cantidad,");
                strSQL.AppendLine("Precio,");
                strSQL.AppendLine("Importe)");
                strSQL.AppendLine("VALUES");
                strSQL.AppendLine("(@Nro_orden_pedido,");
                strSQL.AppendLine("@Nro_item,");
                strSQL.AppendLine("@Desc_item,");
                strSQL.AppendLine("@Cantidad,");
                strSQL.AppendLine("@Precio,");
                strSQL.AppendLine("@Importe)");

                using (SqlConnection con = GetConnection())
                {

                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();
                    cmd.Parameters.Add(new SqlParameter("@Nro_orden_pedido", det.nroOrden));
                    cmd.Parameters.Add(new SqlParameter("@Nro_item", det.nroItems));
                    cmd.Parameters.Add(new SqlParameter("@Desc_item", det.descItems));
                    cmd.Parameters.Add(new SqlParameter("@Cantidad", det.cant));
                    cmd.Parameters.Add(new SqlParameter("@Precio", det.precio));
                    cmd.Parameters.Add(new SqlParameter("@Importe", det.importe));

                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { throw ex; }
        }

        public static void insertDetalle(DetalleOrden det, SqlConnection cn, SqlTransaction trx)
        {
            try
            {
                StringBuilder strSQL = new StringBuilder();

                strSQL.AppendLine("INSERT INTO DETALLE_ORDEN_PEDIDO");
                strSQL.AppendLine("(Nro_orden_pedido,");
                strSQL.AppendLine("Nro_item,");
                strSQL.AppendLine("Desc_item,");
                strSQL.AppendLine("Cantidad,");
                strSQL.AppendLine("Precio,");
                strSQL.AppendLine("Importe)");
                strSQL.AppendLine("VALUES");
                strSQL.AppendLine("(@Nro_orden_pedido,");
                strSQL.AppendLine("@Nro_item,");
                strSQL.AppendLine("@Desc_item,");
                strSQL.AppendLine("@Cantidad,");
                strSQL.AppendLine("@Precio,");
                strSQL.AppendLine("@Importe)");
                //
                SqlCommand cmd = cn.CreateCommand();
                cmd.Transaction = trx;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSQL.ToString();
                cmd.Parameters.Add(new SqlParameter("@Nro_orden_pedido", det.nroOrden));
                cmd.Parameters.Add(new SqlParameter("@Nro_item", det.nroItems));
                cmd.Parameters.Add(new SqlParameter("@Desc_item", det.descItems));
                cmd.Parameters.Add(new SqlParameter("@Cantidad", det.cant));
                cmd.Parameters.Add(new SqlParameter("@Precio", det.precio));
                cmd.Parameters.Add(new SqlParameter("@Importe", det.importe));

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { throw ex; }
        }

        public static void insertAuditoria(OrdenPedido op, int opcion)
        {
            try
            {
                using (SqlConnection con = GetConnection())
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "AUDITOR_V2";
                    cmd.Parameters.Add(new SqlParameter("@Usuario", op.usuario));
                    cmd.Parameters.Add(new SqlParameter("@autorizacion", string.Empty));
                    if (opcion == 0)
                        cmd.Parameters.Add(new SqlParameter("@proceso", "NUEVA ORDEN DE PEDIDO WEB"));
                    else
                        cmd.Parameters.Add(new SqlParameter("@proceso", "MODIFICA ORDEN DE PEDIDO WEB"));
                    cmd.Parameters.Add(new SqlParameter("@identificacion", op.nroOrden));
                    cmd.Parameters.Add(new SqlParameter("@observaciones", op.obsAuditoria));
                    cmd.Parameters.Add(new SqlParameter("@detalle", "Nº orden de pedido: " +
                        op.nroOrden + " Fecha de orden de pedido: " + op.fechaOrden));
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { throw ex; }
        }

        public static void insertAuditoria(OrdenPedido op, int opcion, SqlConnection cn, SqlTransaction trx)
        {
            try
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.Transaction = trx;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "AUDITOR_V2";
                cmd.Parameters.Add(new SqlParameter("@Usuario", op.usuario));
                cmd.Parameters.Add(new SqlParameter("@autorizacion", string.Empty));
                if (opcion == 0)
                    cmd.Parameters.Add(new SqlParameter("@proceso", "NUEVA ORDEN DE PEDIDO WEB"));
                else
                    cmd.Parameters.Add(new SqlParameter("@proceso", "MODIFICA ORDEN DE PEDIDO WEB"));
                cmd.Parameters.Add(new SqlParameter("@identificacion", op.nroOrden));
                cmd.Parameters.Add(new SqlParameter("@observaciones", op.obsAuditoria));
                cmd.Parameters.Add(new SqlParameter("@detalle", "Nº orden de pedido: " +
                    op.nroOrden + " Fecha de orden de pedido: " + op.fechaOrden));
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { throw ex; }
        }

        public static List<Entities.OrdenPedido> getsOP(string filtros)
        {
            List<Entities.OrdenPedido> lstOp = new List<OrdenPedido>();
            Entities.OrdenPedido oOp = new OrdenPedido();
            SqlCommand cmd;
            SqlDataReader dr;
            SqlConnection cn = null;
            StringBuilder strSQL = new StringBuilder();

            strSQL.AppendLine("SELECT OP.Nro_orden_pedido, OP.Fecha_orden_pedido,");
            strSQL.AppendLine("OP.Total, OP.Saldo, OP.Cod_proveedor, OP.Cod_oficina_origen,");
            strSQL.AppendLine("OP.Cod_oficina_destino, OP.Solicitante, OP.Aprobado, OP.Anulado,");
            strSQL.AppendLine("OP.Usuario, OP.Observacion, OP.Finalizado, OP.Asignado, OP.Nro_orden_compra,");
            strSQL.AppendLine("OP.Forma_pago, OP.Nro_presupuesto, OP.Nro_facturas, OP.Fecha_orden_compra,");
            strSQL.AppendLine("Ofic.nombre_oficina, PR.nom_proveedor");
            strSQL.AppendLine("FROM ORDENES_PEDIDO OP");
            strSQL.AppendLine("INNER JOIN OFICINAS Ofic ON OP.Cod_oficina_destino = Ofic.codigo_oficina");
            strSQL.AppendLine("INNER JOIN Proveedores PR ON OP.Cod_proveedor = PR.cod_proveedor");
            strSQL.AppendLine("WHERE " + filtros);
            strSQL.AppendLine("ORDER BY OP.Nro_orden_pedido desc ");

            cmd = new SqlCommand();
            //cmd.Parameters.Add(new SqlParameter("@filtros", filtros));
            try
            {
                cn = DALBase.GetConnection();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSQL.ToString();
                cmd.Connection.Open();
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    oOp = new OrdenPedido();

                    if (!dr.IsDBNull(dr.GetOrdinal("Nro_orden_pedido")))
                        oOp.nroOrden = dr.GetInt32((dr.GetOrdinal("Nro_orden_pedido")));

                    if (!dr.IsDBNull(dr.GetOrdinal("Fecha_orden_pedido")))
                        oOp.fechaOrden = dr.GetDateTime((dr.GetOrdinal("Fecha_orden_pedido")));

                    if (!dr.IsDBNull(dr.GetOrdinal("Total")))
                        oOp.total = dr.GetDecimal(dr.GetOrdinal("Total"));

                    if (!dr.IsDBNull(dr.GetOrdinal("Saldo")))
                        oOp.saldo = dr.GetDecimal(dr.GetOrdinal("Saldo"));

                    if (!dr.IsDBNull(dr.GetOrdinal("Cod_proveedor")))
                        oOp.codProveedor = dr.GetInt32(dr.GetOrdinal("Cod_proveedor"));

                    if (!dr.IsDBNull(dr.GetOrdinal("Cod_oficina_origen")))
                        oOp.codOficinaOrigen = dr.GetInt32(dr.GetOrdinal("Cod_oficina_origen"));

                    if (!dr.IsDBNull(dr.GetOrdinal("Cod_oficina_destino")))
                        oOp.codOficinaDestino = dr.GetInt32(dr.GetOrdinal("Cod_oficina_destino"));

                    if (!dr.IsDBNull(dr.GetOrdinal("Solicitante")))
                        oOp.solicitante = dr.GetString(dr.GetOrdinal("Solicitante"));

                    if (!dr.IsDBNull(dr.GetOrdinal("Aprobado")))
                        oOp.aprobado = dr.GetString(dr.GetOrdinal("Aprobado"));

                    if (!dr.IsDBNull(dr.GetOrdinal("Anulado")))
                        oOp.anulado = dr.GetBoolean(dr.GetOrdinal("Anulado"));

                    if (!dr.IsDBNull(dr.GetOrdinal("Usuario")))
                        oOp.usuario = dr.GetString(dr.GetOrdinal("Usuario"));

                    if (!dr.IsDBNull(dr.GetOrdinal("Observacion")))
                        oOp.obs = dr.GetString(dr.GetOrdinal("Observacion"));

                    if (!dr.IsDBNull(dr.GetOrdinal("Finalizado")))
                        oOp.finalizado = dr.GetBoolean(dr.GetOrdinal("Finalizado"));

                    if (!dr.IsDBNull(dr.GetOrdinal("Asignado")))
                        oOp.asignado = (int)dr.GetInt16(dr.GetOrdinal("Asignado"));

                    if (!dr.IsDBNull(dr.GetOrdinal("Nro_orden_compra")))
                        oOp.nroOrdenCompra = dr.GetInt32(dr.GetOrdinal("Nro_orden_compra"));

                    if (!dr.IsDBNull(dr.GetOrdinal("Forma_pago")))
                        oOp.formaPago = dr.GetString(dr.GetOrdinal("Forma_pago"));

                    if (!dr.IsDBNull(dr.GetOrdinal("Nro_presupuesto")))
                        oOp.nroPresupuesto = dr.GetString(dr.GetOrdinal("Nro_presupuesto"));

                    if (!dr.IsDBNull(dr.GetOrdinal("Nro_facturas")))
                        oOp.nroFacturas = dr.GetString(dr.GetOrdinal("Nro_facturas"));

                    if (!dr.IsDBNull(dr.GetOrdinal("Fecha_orden_compra")))
                        oOp.fechaOrdenCompra = dr.GetDateTime(dr.GetOrdinal("Fecha_orden_compra"));

                    if (!dr.IsDBNull(dr.GetOrdinal("nombre_oficina")))
                        oOp.destino = dr.GetString(dr.GetOrdinal("nombre_oficina"));

                    if (!dr.IsDBNull(dr.GetOrdinal("nom_proveedor")))
                        oOp.proveedor = dr.GetString(dr.GetOrdinal("nom_proveedor"));

                    lstOp.Add(oOp);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error in query!" + e.ToString());
                throw e;
            }
            finally
            { cn.Close(); }
            return lstOp;
        }
        public static List<Entities.OrdenPedido> getOpByOficina(int oficina)
        {
            List<Entities.OrdenPedido> lstOp = new List<OrdenPedido>();
            Entities.OrdenPedido oOp = new OrdenPedido();
            SqlCommand cmd;
            SqlDataReader dr;
            SqlConnection cn = null;
            StringBuilder strSQL = new StringBuilder();

            strSQL.AppendLine("SELECT OP.Nro_orden_pedido, OP.Fecha_orden_pedido,");
            strSQL.AppendLine("OP.Total, OP.Saldo, OP.Cod_proveedor, OP.Cod_oficina_origen,");
            strSQL.AppendLine("OP.Cod_oficina_destino, OP.Solicitante, OP.Aprobado, OP.Anulado,");
            strSQL.AppendLine("OP.Usuario, OP.Observacion, OP.Finalizado, OP.Asignado, OP.Nro_orden_compra,");
            strSQL.AppendLine("OP.Forma_pago, OP.Nro_presupuesto, OP.Nro_facturas, OP.Fecha_orden_compra,");
            strSQL.AppendLine("Ofic.nombre_oficina, PR.nom_proveedor");
            strSQL.AppendLine("FROM ORDENES_PEDIDO OP");
            strSQL.AppendLine("INNER JOIN OFICINAS Ofic ON OP.Cod_oficina_destino = Ofic.codigo_oficina");
            strSQL.AppendLine("INNER JOIN Proveedores PR ON OP.Cod_proveedor = PR.cod_proveedor");
            strSQL.AppendLine("WHERE Cod_oficina_origen = @Cod_oficina_origen");
            strSQL.AppendLine("ORDER BY OP.Nro_orden_pedido desc ");

            cmd = new SqlCommand();
            //cmd.Parameters.Add(new SqlParameter("@filtros", filtros));
            try
            {
                cn = DALBase.GetConnection();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSQL.ToString();
                cmd.Parameters.AddWithValue("Cod_oficina_origen", oficina);
                cmd.Connection.Open();
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    oOp = new OrdenPedido();

                    if (!dr.IsDBNull(dr.GetOrdinal("Nro_orden_pedido")))
                        oOp.nroOrden = dr.GetInt32((dr.GetOrdinal("Nro_orden_pedido")));

                    if (!dr.IsDBNull(dr.GetOrdinal("Fecha_orden_pedido")))
                        oOp.fechaOrden = dr.GetDateTime((dr.GetOrdinal("Fecha_orden_pedido")));

                    if (!dr.IsDBNull(dr.GetOrdinal("Total")))
                        oOp.total = dr.GetDecimal(dr.GetOrdinal("Total"));

                    if (!dr.IsDBNull(dr.GetOrdinal("Saldo")))
                        oOp.saldo = dr.GetDecimal(dr.GetOrdinal("Saldo"));

                    if (!dr.IsDBNull(dr.GetOrdinal("Cod_proveedor")))
                        oOp.codProveedor = dr.GetInt32(dr.GetOrdinal("Cod_proveedor"));

                    if (!dr.IsDBNull(dr.GetOrdinal("Cod_oficina_origen")))
                        oOp.codOficinaOrigen = dr.GetInt32(dr.GetOrdinal("Cod_oficina_origen"));

                    if (!dr.IsDBNull(dr.GetOrdinal("Cod_oficina_destino")))
                        oOp.codOficinaDestino = dr.GetInt32(dr.GetOrdinal("Cod_oficina_destino"));

                    if (!dr.IsDBNull(dr.GetOrdinal("Solicitante")))
                        oOp.solicitante = dr.GetString(dr.GetOrdinal("Solicitante"));

                    if (!dr.IsDBNull(dr.GetOrdinal("Aprobado")))
                        oOp.aprobado = dr.GetString(dr.GetOrdinal("Aprobado"));

                    if (!dr.IsDBNull(dr.GetOrdinal("Anulado")))
                        oOp.anulado = dr.GetBoolean(dr.GetOrdinal("Anulado"));

                    if (!dr.IsDBNull(dr.GetOrdinal("Usuario")))
                        oOp.usuario = dr.GetString(dr.GetOrdinal("Usuario"));

                    if (!dr.IsDBNull(dr.GetOrdinal("Observacion")))
                        oOp.obs = dr.GetString(dr.GetOrdinal("Observacion"));

                    if (!dr.IsDBNull(dr.GetOrdinal("Finalizado")))
                        oOp.finalizado = dr.GetBoolean(dr.GetOrdinal("Finalizado"));

                    if (!dr.IsDBNull(dr.GetOrdinal("Asignado")))
                        oOp.asignado = (int)dr.GetInt16(dr.GetOrdinal("Asignado"));

                    if (!dr.IsDBNull(dr.GetOrdinal("Nro_orden_compra")))
                        oOp.nroOrdenCompra = dr.GetInt32(dr.GetOrdinal("Nro_orden_compra"));

                    if (!dr.IsDBNull(dr.GetOrdinal("Forma_pago")))
                        oOp.formaPago = dr.GetString(dr.GetOrdinal("Forma_pago"));

                    if (!dr.IsDBNull(dr.GetOrdinal("Nro_presupuesto")))
                        oOp.nroPresupuesto = dr.GetString(dr.GetOrdinal("Nro_presupuesto"));

                    if (!dr.IsDBNull(dr.GetOrdinal("Nro_facturas")))
                        oOp.nroFacturas = dr.GetString(dr.GetOrdinal("Nro_facturas"));

                    if (!dr.IsDBNull(dr.GetOrdinal("Fecha_orden_compra")))
                        oOp.fechaOrdenCompra = dr.GetDateTime(dr.GetOrdinal("Fecha_orden_compra"));

                    if (!dr.IsDBNull(dr.GetOrdinal("nombre_oficina")))
                        oOp.destino = dr.GetString(dr.GetOrdinal("nombre_oficina"));

                    if (!dr.IsDBNull(dr.GetOrdinal("nom_proveedor")))
                        oOp.proveedor = dr.GetString(dr.GetOrdinal("nom_proveedor"));

                    lstOp.Add(oOp);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error in query!" + e.ToString());
                throw e;
            }
            finally
            { cn.Close(); }
            return lstOp;
        }
        public static Entities.OrdenPedido getsOpByPk(int cod)
        {
            Entities.OrdenPedido oOp = new OrdenPedido();
            SqlCommand cmd;
            SqlDataReader dr;
            SqlConnection cn = null;
            StringBuilder strSQL = new StringBuilder();

            strSQL.AppendLine("SELECT OP.Nro_orden_pedido, OP.Fecha_orden_pedido,");
            strSQL.AppendLine("OP.Total, OP.Saldo, OP.Cod_proveedor, OP.Cod_oficina_origen,");
            strSQL.AppendLine("OP.Cod_oficina_destino, OP.Solicitante, OP.Aprobado, OP.Anulado,");
            strSQL.AppendLine("OP.Usuario, OP.Observacion, OP.Finalizado, OP.Asignado, OP.Nro_orden_compra,");
            strSQL.AppendLine("OP.Forma_pago, OP.Nro_presupuesto, OP.Nro_facturas, OP.Fecha_orden_compra,");
            strSQL.AppendLine("Ofic.nombre_oficina, PR.nom_proveedor, OP.web, PR.nro_cuit");
            strSQL.AppendLine("FROM ORDENES_PEDIDO OP");
            strSQL.AppendLine("INNER JOIN OFICINAS Ofic ON OP.Cod_oficina_destino = Ofic.codigo_oficina");
            strSQL.AppendLine("INNER JOIN Proveedores PR ON OP.Cod_proveedor = PR.cod_proveedor");
            strSQL.AppendLine("WHERE OP.Nro_orden_pedido = @op");

            cmd = new SqlCommand();

            cmd.Parameters.Add(new SqlParameter("@op", cod));


            try
            {
                cn = DALBase.GetConnection();

                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSQL.ToString();
                cmd.Connection.Open();

                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    oOp = new OrdenPedido();

                    if (!dr.IsDBNull(dr.GetOrdinal("Nro_orden_pedido")))
                        oOp.nroOrden = dr.GetInt32((dr.GetOrdinal("Nro_orden_pedido")));

                    if (!dr.IsDBNull(dr.GetOrdinal("Fecha_orden_pedido")))
                        oOp.fechaOrden = dr.GetDateTime((dr.GetOrdinal("Fecha_orden_pedido")));

                    if (!dr.IsDBNull(dr.GetOrdinal("Total")))
                        oOp.total = dr.GetDecimal(dr.GetOrdinal("Total"));

                    if (!dr.IsDBNull(dr.GetOrdinal("Saldo")))
                        oOp.saldo = dr.GetDecimal(dr.GetOrdinal("Saldo"));

                    if (!dr.IsDBNull(dr.GetOrdinal("Cod_proveedor")))
                        oOp.codProveedor = dr.GetInt32(dr.GetOrdinal("Cod_proveedor"));

                    if (!dr.IsDBNull(dr.GetOrdinal("Cod_oficina_origen")))
                        oOp.codOficinaOrigen = dr.GetInt32(dr.GetOrdinal("Cod_oficina_origen"));

                    if (!dr.IsDBNull(dr.GetOrdinal("Cod_oficina_destino")))
                        oOp.codOficinaDestino = dr.GetInt32(dr.GetOrdinal("Cod_oficina_destino"));

                    if (!dr.IsDBNull(dr.GetOrdinal("Solicitante")))
                        oOp.solicitante = dr.GetString(dr.GetOrdinal("Solicitante"));

                    if (!dr.IsDBNull(dr.GetOrdinal("Aprobado")))
                        oOp.aprobado = dr.GetString(dr.GetOrdinal("Aprobado"));

                    if (!dr.IsDBNull(dr.GetOrdinal("Anulado")))
                        oOp.anulado = dr.GetBoolean(dr.GetOrdinal("Anulado"));

                    if (!dr.IsDBNull(dr.GetOrdinal("Usuario")))
                        oOp.usuario = dr.GetString(dr.GetOrdinal("Usuario"));

                    if (!dr.IsDBNull(dr.GetOrdinal("Observacion")))
                        oOp.obs = dr.GetString(dr.GetOrdinal("Observacion"));

                    if (!dr.IsDBNull(dr.GetOrdinal("Finalizado")))
                        oOp.finalizado = dr.GetBoolean(dr.GetOrdinal("Finalizado"));

                    if (!dr.IsDBNull(dr.GetOrdinal("Asignado")))
                        oOp.asignado = (int)dr.GetInt16(dr.GetOrdinal("Asignado"));

                    if (!dr.IsDBNull(dr.GetOrdinal("Nro_orden_compra")))
                        oOp.nroOrdenCompra = dr.GetInt32(dr.GetOrdinal("Nro_orden_compra"));

                    if (!dr.IsDBNull(dr.GetOrdinal("Forma_pago")))
                        oOp.formaPago = dr.GetString(dr.GetOrdinal("Forma_pago"));

                    if (!dr.IsDBNull(dr.GetOrdinal("Nro_presupuesto")))
                        oOp.nroPresupuesto = dr.GetString(dr.GetOrdinal("Nro_presupuesto"));

                    if (!dr.IsDBNull(dr.GetOrdinal("Nro_facturas")))
                        oOp.nroFacturas = dr.GetString(dr.GetOrdinal("Nro_facturas"));

                    if (!dr.IsDBNull(dr.GetOrdinal("Fecha_orden_compra")))
                        oOp.fechaOrdenCompra = dr.GetDateTime(dr.GetOrdinal("Fecha_orden_compra"));

                    if (!dr.IsDBNull(dr.GetOrdinal("nombre_oficina")))
                        oOp.destino = dr.GetString(dr.GetOrdinal("nombre_oficina"));

                    if (!dr.IsDBNull(dr.GetOrdinal("nom_proveedor")))
                        oOp.proveedor = dr.GetString(dr.GetOrdinal("nom_proveedor"));

                    if (!dr.IsDBNull(dr.GetOrdinal("web")))
                        oOp.web = dr.GetInt16(dr.GetOrdinal("web"));

                    if (!dr.IsDBNull(dr.GetOrdinal("nro_cuit")))
                        oOp.CUIT = dr.GetString(dr.GetOrdinal("nro_cuit"));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error in query!" + e.ToString());
                throw e;
            }
            finally
            { cn.Close(); }
            oOp.detalle = getDetalleByPk(cod);
            return oOp;
        }

        private static List<Entities.DetalleOrden> getDetalleByPk(int Cod)
        {
            List<Entities.DetalleOrden> lstDet = new List<DetalleOrden>();
            Entities.DetalleOrden oDet = new DetalleOrden();
            SqlCommand cmd;
            SqlDataReader dr;
            SqlConnection cn = null;
            StringBuilder strSQL = new StringBuilder();

            strSQL.AppendLine("SELECT Nro_orden_pedido, Nro_item, Desc_item, Cantidad,");
            strSQL.AppendLine("Precio, Importe");
            strSQL.AppendLine("FROM DETALLE_ORDEN_PEDIDO");
            strSQL.AppendLine("WHERE Nro_orden_pedido = @NroOP");

            cmd = new SqlCommand();

            cmd.Parameters.Add(new SqlParameter("@NroOP", Cod));


            try
            {
                cn = DALBase.GetConnection();

                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSQL.ToString();
                cmd.Connection.Open();

                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    oDet = new DetalleOrden();

                    if (!dr.IsDBNull(dr.GetOrdinal("Nro_orden_pedido")))
                        oDet.nroOrden = dr.GetInt32((dr.GetOrdinal("Nro_orden_pedido")));

                    if (!dr.IsDBNull(dr.GetOrdinal("Nro_item")))
                        oDet.nroItems = dr.GetInt32((dr.GetOrdinal("Nro_item")));

                    if (!dr.IsDBNull(dr.GetOrdinal("Desc_item")))
                        oDet.descItems = dr.GetString(dr.GetOrdinal("Desc_item"));

                    if (!dr.IsDBNull(dr.GetOrdinal("Cantidad")))
                        oDet.cant = dr.GetDecimal(dr.GetOrdinal("Cantidad"));

                    if (!dr.IsDBNull(dr.GetOrdinal("Precio")))
                        oDet.precio = dr.GetDecimal(dr.GetOrdinal("Precio"));

                    if (!dr.IsDBNull(dr.GetOrdinal("Importe")))
                        oDet.importe = dr.GetDecimal(dr.GetOrdinal("Importe"));

                    lstDet.Add(oDet);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error in query!" + e.ToString());
                throw e;
            }
            finally
            { cn.Close(); }
            return lstDet;
        }

        public static void updateOP(OrdenPedido op)
        {
            SqlCommand objCommand = null;
            try
            {
                StringBuilder strSQL = new StringBuilder();
                SqlConnection objConn = DALBase.GetConnection();

                objCommand = new SqlCommand();
                objCommand.Connection = objConn;

                strSQL.AppendLine("UPDATE ORDENES_PEDIDO");
                strSQL.AppendLine("SET");
                strSQL.AppendLine("Fecha_orden_pedido = @Fecha_orden_pedido,");
                strSQL.AppendLine("Total = @Total,");
                strSQL.AppendLine("Saldo = @Saldo,");
                strSQL.AppendLine("Cod_proveedor = @Cod_proveedor,");
                strSQL.AppendLine("Cod_oficina_origen = @Cod_oficina_origen,");
                strSQL.AppendLine("Cod_oficina_destino = @Cod_oficina_destino,");
                strSQL.AppendLine("Solicitante = @Solicitante,");
                strSQL.AppendLine("Aprobado = @Aprobado,");
                strSQL.AppendLine("Usuario = @Usuario,");
                strSQL.AppendLine("Observacion = @Observacion,");
                strSQL.AppendLine("Forma_pago = @Forma_pago,");
                strSQL.AppendLine("Nro_presupuesto = @Nro_presupuesto,");
                strSQL.AppendLine("Nro_facturas = @Nro_facturas,");
                strSQL.AppendLine("web = @web");
                strSQL.AppendLine("WHERE Nro_orden_pedido = @Nro_orden_pedido");



                objCommand.Parameters.Add(new SqlParameter("@Nro_orden_pedido", op.nroOrden));
                objCommand.Parameters.Add(new SqlParameter("@Fecha_orden_pedido", op.fechaOrden));
                objCommand.Parameters.Add(new SqlParameter("@Total", op.total));
                objCommand.Parameters.Add(new SqlParameter("@Saldo", op.saldo));
                objCommand.Parameters.Add(new SqlParameter("@Cod_proveedor", op.codProveedor));
                objCommand.Parameters.Add(new SqlParameter("@Cod_oficina_origen", op.codOficinaOrigen));
                objCommand.Parameters.Add(new SqlParameter("@Cod_oficina_destino", op.codOficinaDestino));

                if (op.solicitante != null)
                    objCommand.Parameters.Add(new SqlParameter("@Solicitante", op.solicitante));
                else
                    objCommand.Parameters.Add(new SqlParameter("@Solicitante", System.Data.SqlTypes.SqlString.Null));

                if (op.aprobado != null)
                    objCommand.Parameters.Add(new SqlParameter("@Aprobado", op.aprobado));
                else
                    objCommand.Parameters.Add(new SqlParameter("@Aprobado", System.Data.SqlTypes.SqlString.Null));

                objCommand.Parameters.Add(new SqlParameter("@Usuario", op.usuario));

                if (op.obs != null)
                    objCommand.Parameters.Add(new SqlParameter("@Observacion", op.obs));
                else
                    objCommand.Parameters.Add(new SqlParameter("@Observacion", System.Data.SqlTypes.SqlString.Null));

                if (op.formaPago != null)
                    objCommand.Parameters.Add(new SqlParameter("@Forma_pago", op.formaPago));
                else
                    objCommand.Parameters.Add(new SqlParameter("@Forma_pago", System.Data.SqlTypes.SqlString.Null));

                if (op.nroPresupuesto != null)
                    objCommand.Parameters.Add(new SqlParameter("@Nro_presupuesto", op.nroPresupuesto));
                else
                    objCommand.Parameters.Add(new SqlParameter("@Nro_presupuesto", System.Data.SqlTypes.SqlString.Null));

                if (op.nroFacturas != null)
                    objCommand.Parameters.Add(new SqlParameter("@Nro_facturas", op.nroFacturas));
                else
                    objCommand.Parameters.Add(new SqlParameter("@Nro_facturas", System.Data.SqlTypes.SqlString.Null));


                objCommand.Parameters.Add(new SqlParameter("@web", op.web));
                objCommand.Parameters.Add(new SqlParameter("@recibida", op.recibida));
                objCommand.Parameters.Add(new SqlParameter("@cod_estado_op", op.codEstadoOP));


                objCommand.CommandType = CommandType.Text;
                objCommand.CommandText = strSQL.ToString();

                objCommand.Connection.Open();

                objCommand.ExecuteNonQuery();


                // objCommand.Connection.Open();

            }
            catch (Exception ex) { throw ex; }
            finally
            {
                objCommand.Connection.Close();
            }
        }

        public static void updateOP(OrdenPedido op, SqlConnection cn, SqlTransaction trx)
        {
            SqlCommand objCommand = null;
            try
            {
                StringBuilder strSQL = new StringBuilder();

                objCommand = new SqlCommand();
                objCommand.Connection = cn;
                objCommand.Transaction = trx;

                strSQL.AppendLine("UPDATE ORDENES_PEDIDO");
                strSQL.AppendLine("SET");
                strSQL.AppendLine("Fecha_orden_pedido = @Fecha_orden_pedido,");
                strSQL.AppendLine("Total = @Total,");
                strSQL.AppendLine("Saldo = @Saldo,");
                strSQL.AppendLine("Cod_proveedor = @Cod_proveedor,");
                strSQL.AppendLine("Cod_oficina_origen = @Cod_oficina_origen,");
                strSQL.AppendLine("Cod_oficina_destino = @Cod_oficina_destino,");
                strSQL.AppendLine("Solicitante = @Solicitante,");
                strSQL.AppendLine("Aprobado = @Aprobado,");
                strSQL.AppendLine("Usuario = @Usuario,");
                strSQL.AppendLine("Observacion = @Observacion,");
                strSQL.AppendLine("Forma_pago = @Forma_pago,");
                strSQL.AppendLine("Nro_presupuesto = @Nro_presupuesto,");
                strSQL.AppendLine("Nro_facturas = @Nro_facturas,");
                strSQL.AppendLine("web = @web");
                strSQL.AppendLine("WHERE Nro_orden_pedido = @Nro_orden_pedido");



                objCommand.Parameters.Add(new SqlParameter("@Nro_orden_pedido", op.nroOrden));
                objCommand.Parameters.Add(new SqlParameter("@Fecha_orden_pedido", op.fechaOrden));
                objCommand.Parameters.Add(new SqlParameter("@Total", op.total));
                objCommand.Parameters.Add(new SqlParameter("@Saldo", op.saldo));
                objCommand.Parameters.Add(new SqlParameter("@Cod_proveedor", op.codProveedor));
                objCommand.Parameters.Add(new SqlParameter("@Cod_oficina_origen", op.codOficinaOrigen));
                objCommand.Parameters.Add(new SqlParameter("@Cod_oficina_destino", op.codOficinaDestino));

                if (op.solicitante != null)
                    objCommand.Parameters.Add(new SqlParameter("@Solicitante", op.solicitante));
                else
                    objCommand.Parameters.Add(new SqlParameter("@Solicitante", System.Data.SqlTypes.SqlString.Null));

                if (op.aprobado != null)
                    objCommand.Parameters.Add(new SqlParameter("@Aprobado", op.aprobado));
                else
                    objCommand.Parameters.Add(new SqlParameter("@Aprobado", System.Data.SqlTypes.SqlString.Null));

                objCommand.Parameters.Add(new SqlParameter("@Usuario", op.usuario));

                if (op.obs != null)
                    objCommand.Parameters.Add(new SqlParameter("@Observacion", op.obs));
                else
                    objCommand.Parameters.Add(new SqlParameter("@Observacion", System.Data.SqlTypes.SqlString.Null));

                if (op.formaPago != null)
                    objCommand.Parameters.Add(new SqlParameter("@Forma_pago", op.formaPago));
                else
                    objCommand.Parameters.Add(new SqlParameter("@Forma_pago", System.Data.SqlTypes.SqlString.Null));

                if (op.nroPresupuesto != null)
                    objCommand.Parameters.Add(new SqlParameter("@Nro_presupuesto", op.nroPresupuesto));
                else
                    objCommand.Parameters.Add(new SqlParameter("@Nro_presupuesto", System.Data.SqlTypes.SqlString.Null));

                if (op.nroFacturas != null)
                    objCommand.Parameters.Add(new SqlParameter("@Nro_facturas", op.nroFacturas));
                else
                    objCommand.Parameters.Add(new SqlParameter("@Nro_facturas", System.Data.SqlTypes.SqlString.Null));


                objCommand.Parameters.Add(new SqlParameter("@web", op.web));
                objCommand.Parameters.Add(new SqlParameter("@recibida", op.recibida));
                objCommand.Parameters.Add(new SqlParameter("@cod_estado_op", op.codEstadoOP));


                objCommand.CommandType = CommandType.Text;
                objCommand.CommandText = strSQL.ToString();

                //objCommand.Connection.Open();

                objCommand.ExecuteNonQuery();


                // objCommand.Connection.Open();

            }
            catch (Exception ex) { throw ex; }
            finally
            {
                objCommand = null;
            }
        }

        public static void updateDetalle(OrdenPedido op)
        {
            try
            {
                StringBuilder strSQL = new StringBuilder();

                strSQL.AppendLine("DELETE DETALLE_ORDEN_PEDIDO");
                strSQL.AppendLine("WHERE Nro_orden_pedido = @Nro_orden_pedido");

                using (SqlConnection con = GetConnection())
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSQL.ToString();
                    cmd.Parameters.AddWithValue("@Nro_orden_pedido", op.nroOrden);
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { throw ex; }
        }

        public static void updateDetalle(OrdenPedido op, SqlConnection cn, SqlTransaction trx)
        {
            try
            {
                StringBuilder strSQL = new StringBuilder();

                strSQL.AppendLine("DELETE DETALLE_ORDEN_PEDIDO");
                strSQL.AppendLine("WHERE Nro_orden_pedido = @Nro_orden_pedido");

                SqlCommand cmd = cn.CreateCommand();
                cmd.Transaction = trx;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSQL.ToString();
                cmd.Parameters.AddWithValue("@Nro_orden_pedido", op.nroOrden);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { throw ex; }
        }

    }
}
