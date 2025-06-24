using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DAL
{
    public class FACTURA_X_ORDEN_PEDIDO : DALBase
    {
        public int ID { get; set; }
        public int NRO_ORDEN_PEDIDO { get; set; }
        public string NRO_CUIT_PROVEEDOR { get; set; }
        public DateTime FECHA_EMISION { get; set; }
        public int PUNTO_VENTA { get; set; }
        public Int64 NRO_COMPROBANTE { get; set; }
        public Int64 NRO_CAE { get; set; }
        public decimal IMPORTE { get; set; }
        public int TIPO_COMPROBANTE { get; set; }
        public int USUARIO_CARGA { get; set; }
        public DateTime FECHA_CARGA { get; set; }
        public int ESTADO { get; set; }
        public string TIP_COMP { get; set; }
        public string COMP_COMPLETO { get; set; }

        public FACTURA_X_ORDEN_PEDIDO()
        {
            ID = 0;
            NRO_ORDEN_PEDIDO = 0;
            NRO_CUIT_PROVEEDOR = string.Empty;
            FECHA_EMISION = DateTime.Now;
            PUNTO_VENTA = 0;
            NRO_COMPROBANTE = 0;
            NRO_CAE = 0;
            IMPORTE = 0;
            TIPO_COMPROBANTE = 0;
            USUARIO_CARGA = 0;
            FECHA_CARGA = DateTime.Now;
            ESTADO = 0;
            TIP_COMP = string.Empty;
        }

        private static List<FACTURA_X_ORDEN_PEDIDO> mapeo(SqlDataReader dr)
        {
            List<FACTURA_X_ORDEN_PEDIDO> lst = new List<FACTURA_X_ORDEN_PEDIDO>();
            FACTURA_X_ORDEN_PEDIDO obj;
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    obj = new FACTURA_X_ORDEN_PEDIDO();
                    if (!dr.IsDBNull(0)) { obj.ID = dr.GetInt32(0); }
                    if (!dr.IsDBNull(1)) { obj.NRO_ORDEN_PEDIDO = dr.GetInt32(1); }
                    if (!dr.IsDBNull(2)) { obj.NRO_CUIT_PROVEEDOR = dr.GetString(2); }
                    if (!dr.IsDBNull(3)) { obj.FECHA_EMISION = dr.GetDateTime(3); }
                    if (!dr.IsDBNull(4)) { obj.PUNTO_VENTA = dr.GetInt32(4); }
                    if (!dr.IsDBNull(5)) { obj.NRO_COMPROBANTE = dr.GetInt64(5); }
                    if (!dr.IsDBNull(6)) { obj.NRO_CAE = dr.GetInt64(6); }
                    if (!dr.IsDBNull(7)) { obj.IMPORTE = dr.GetDecimal(7); }
                    if (!dr.IsDBNull(8)) { obj.TIPO_COMPROBANTE = dr.GetInt32(8); }
                    if (!dr.IsDBNull(9)) { obj.USUARIO_CARGA = dr.GetInt32(9); }
                    if (!dr.IsDBNull(10)) { obj.FECHA_CARGA = dr.GetDateTime(10); }
                    if (!dr.IsDBNull(11)) { obj.ESTADO = dr.GetInt32(11); }
                    switch (obj.TIPO_COMPROBANTE)
                    {
                        case 1:
                            obj.TIP_COMP = "Factura A";
                            break;
                        case 2:
                            obj.TIP_COMP = "Nota de Débito A";
                            break;
                        case 3:
                            obj.TIP_COMP = "Nota de Crédito A";
                            break;
                        case 4:
                            obj.TIP_COMP = "Recibo A";
                            break;
                        case 5:
                            obj.TIP_COMP = "Nota de Venta al Contado A";
                            break;
                        case 6:
                            obj.TIP_COMP = "Factura B";
                            break;
                        case 7:
                            obj.TIP_COMP = "Nota de Débito B";
                            break;
                        case 8:
                            obj.TIP_COMP = "Nota de Crédito B";
                            break;
                        case 9:
                            obj.TIP_COMP = "Recibo B";
                            break;
                        case 10:
                            obj.TIP_COMP = "Nota de Venta al Contado B";
                            break;
                        case 11:
                            obj.TIP_COMP = "Factura C";
                            break;
                        case 12:
                            obj.TIP_COMP = "Nota de Débito C";
                            break;
                        case 13:
                            obj.TIP_COMP = "Nota de Crédito C";
                            break;
                        case 15:
                            obj.TIP_COMP = "Recibo C";
                            break;
                        case 19:
                            obj.TIP_COMP = "Factura de Exportación";
                            break;
                        case 20:
                            obj.TIP_COMP = "Nota Déb. P/Operac. con el Exterior";
                            break;
                        case 21:
                            obj.TIP_COMP = "Nota Créd. P/Operac. con el Exterior";
                            break;
                        case 39:
                            obj.TIP_COMP = "Otros Comprobantes A que Cumplan con la R.G. Nro. 1415";
                            break;
                        case 40:
                            obj.TIP_COMP = "Otros Comprobantes B que Cumplan con la R.G. Nro. 1415";
                            break;
                        case 49:
                            obj.TIP_COMP = "Comprobante de Compra de Bienes Usados";
                            break;
                        case 51:
                            obj.TIP_COMP = "Factura M";
                            break;
                        case 52:
                            obj.TIP_COMP = "Nota de Débito M";
                            break;
                        case 53:
                            obj.TIP_COMP = "Nota de Crédito M";
                            break;
                        case 54:
                            obj.TIP_COMP = "Recibo M";
                            break;
                        case 60:
                            obj.TIP_COMP = "Cta. de Vta. y Líquido Prod. A";
                            break;
                        case 61:
                            obj.TIP_COMP = "Cta. de Vta. y Líquido Prod. B";
                            break;
                        case 63:
                            obj.TIP_COMP = "Liquidación A";
                            break;
                        case 64:
                            obj.TIP_COMP = "Liquidación B";
                            break;
                        default:
                            break;
                    }
                    lst.Add(obj);
                }
            }
            return lst;
        }

        public static List<FACTURA_X_ORDEN_PEDIDO> read(int nroOp)
        {
            try
            {
                List<FACTURA_X_ORDEN_PEDIDO> lst = new List<FACTURA_X_ORDEN_PEDIDO>();
                using (SqlConnection con = GetConnection())
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT *FROM FACTURA_X_ORDEN_PEDIDO WHERE NRO_ORDEN_PEDIDO=@NRO_ORDEN_PEDIDO";
                    cmd.Parameters.AddWithValue("@NRO_ORDEN_PEDIDO", nroOp);
                    cmd.Connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    lst = mapeo(dr);
                    return lst;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static FACTURA_X_ORDEN_PEDIDO getByPk(
        int ID)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("SELECT *FROM FACTURA_X_ORDEN_PEDIDO WHERE");
                sql.AppendLine("ID = @ID");
                FACTURA_X_ORDEN_PEDIDO obj = null;
                using (SqlConnection con = GetConnection())
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql.ToString();
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.Connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    List<FACTURA_X_ORDEN_PEDIDO> lst = mapeo(dr);
                    if (lst.Count != 0)
                        obj = lst[0];
                }
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<FACTURA_X_ORDEN_PEDIDO> ValidaExistencia(int PUNTO_VENTA, Int64 NRO_COMPROBANTE, string NRO_CUIT_PROVEEDOR)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("SELECT *FROM FACTURA_X_ORDEN_PEDIDO WHERE");
                sql.AppendLine("PUNTO_VENTA=@PUNTO_VENTA AND NRO_COMPROBANTE=@NRO_COMPROBANTE AND ");
                sql.AppendLine("NRO_CUIT_PROVEEDOR=@NRO_CUIT_PROVEEDOR AND ESTADO <> 5");
                using (SqlConnection con = GetConnection())
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql.ToString();
                    cmd.Parameters.AddWithValue("@PUNTO_VENTA", PUNTO_VENTA);
                    cmd.Parameters.AddWithValue("@NRO_COMPROBANTE", NRO_COMPROBANTE);
                    cmd.Parameters.AddWithValue("@NRO_CUIT_PROVEEDOR", NRO_CUIT_PROVEEDOR);
                    cmd.Connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    return mapeo(dr);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void insert(FACTURA_X_ORDEN_PEDIDO obj)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("INSERT INTO FACTURA_X_ORDEN_PEDIDO(");
                sql.AppendLine("NRO_ORDEN_PEDIDO");
                sql.AppendLine(", NRO_CUIT_PROVEEDOR");
                sql.AppendLine(", FECHA_EMISION");
                sql.AppendLine(", PUNTO_VENTA");
                sql.AppendLine(", NRO_COMPROBANTE");
                sql.AppendLine(", NRO_CAE");
                sql.AppendLine(", IMPORTE");
                sql.AppendLine(", TIPO_COMPROBANTE");
                sql.AppendLine(", USUARIO_CARGA");
                sql.AppendLine(", FECHA_CARGA");
                sql.AppendLine(", ESTADO");
                sql.AppendLine(")");
                sql.AppendLine("VALUES");
                sql.AppendLine("(");
                sql.AppendLine("@NRO_ORDEN_PEDIDO");
                sql.AppendLine(", @NRO_CUIT_PROVEEDOR");
                sql.AppendLine(", @FECHA_EMISION");
                sql.AppendLine(", @PUNTO_VENTA");
                sql.AppendLine(", @NRO_COMPROBANTE");
                sql.AppendLine(", @NRO_CAE");
                sql.AppendLine(", @IMPORTE");
                sql.AppendLine(", @TIPO_COMPROBANTE");
                sql.AppendLine(", @USUARIO_CARGA");
                sql.AppendLine(", @FECHA_CARGA");
                sql.AppendLine(", @ESTADO");
                sql.AppendLine(")");
                using (SqlConnection con = GetConnection())
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql.ToString();
                    cmd.Parameters.AddWithValue("@NRO_ORDEN_PEDIDO", obj.NRO_ORDEN_PEDIDO);
                    cmd.Parameters.AddWithValue("@NRO_CUIT_PROVEEDOR", obj.NRO_CUIT_PROVEEDOR);
                    cmd.Parameters.AddWithValue("@FECHA_EMISION", obj.FECHA_EMISION);
                    cmd.Parameters.AddWithValue("@PUNTO_VENTA", obj.PUNTO_VENTA);
                    cmd.Parameters.AddWithValue("@NRO_COMPROBANTE", obj.NRO_COMPROBANTE);
                    cmd.Parameters.AddWithValue("@NRO_CAE", obj.NRO_CAE);
                    cmd.Parameters.AddWithValue("@IMPORTE", obj.IMPORTE);
                    cmd.Parameters.AddWithValue("@TIPO_COMPROBANTE", obj.TIPO_COMPROBANTE);
                    cmd.Parameters.AddWithValue("@USUARIO_CARGA", obj.USUARIO_CARGA);
                    cmd.Parameters.AddWithValue("@FECHA_CARGA", obj.FECHA_CARGA);
                    cmd.Parameters.AddWithValue("@ESTADO", obj.ESTADO);
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void insert(FACTURA_X_ORDEN_PEDIDO obj, SqlConnection cn, SqlTransaction trx)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("INSERT INTO FACTURA_X_ORDEN_PEDIDO(");
                sql.AppendLine("NRO_ORDEN_PEDIDO");
                sql.AppendLine(", NRO_CUIT_PROVEEDOR");
                sql.AppendLine(", FECHA_EMISION");
                sql.AppendLine(", PUNTO_VENTA");
                sql.AppendLine(", NRO_COMPROBANTE");
                sql.AppendLine(", NRO_CAE");
                sql.AppendLine(", IMPORTE");
                sql.AppendLine(", TIPO_COMPROBANTE");
                sql.AppendLine(", USUARIO_CARGA");
                sql.AppendLine(", FECHA_CARGA");
                sql.AppendLine(", ESTADO");
                sql.AppendLine(")");
                sql.AppendLine("VALUES");
                sql.AppendLine("(");
                sql.AppendLine("@NRO_ORDEN_PEDIDO");
                sql.AppendLine(", @NRO_CUIT_PROVEEDOR");
                sql.AppendLine(", @FECHA_EMISION");
                sql.AppendLine(", @PUNTO_VENTA");
                sql.AppendLine(", @NRO_COMPROBANTE");
                sql.AppendLine(", @NRO_CAE");
                sql.AppendLine(", @IMPORTE");
                sql.AppendLine(", @TIPO_COMPROBANTE");
                sql.AppendLine(", @USUARIO_CARGA");
                sql.AppendLine(", @FECHA_CARGA");
                sql.AppendLine(", @ESTADO");
                sql.AppendLine(")");

                SqlCommand cmd = cn.CreateCommand();
                cmd.Transaction = trx;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql.ToString();
                cmd.Parameters.AddWithValue("@NRO_ORDEN_PEDIDO", obj.NRO_ORDEN_PEDIDO);
                cmd.Parameters.AddWithValue("@NRO_CUIT_PROVEEDOR", obj.NRO_CUIT_PROVEEDOR);
                cmd.Parameters.AddWithValue("@FECHA_EMISION", obj.FECHA_EMISION);
                cmd.Parameters.AddWithValue("@PUNTO_VENTA", obj.PUNTO_VENTA);
                cmd.Parameters.AddWithValue("@NRO_COMPROBANTE", obj.NRO_COMPROBANTE);
                cmd.Parameters.AddWithValue("@NRO_CAE", obj.NRO_CAE);
                cmd.Parameters.AddWithValue("@IMPORTE", obj.IMPORTE);
                cmd.Parameters.AddWithValue("@TIPO_COMPROBANTE", obj.TIPO_COMPROBANTE);
                cmd.Parameters.AddWithValue("@USUARIO_CARGA", obj.USUARIO_CARGA);
                cmd.Parameters.AddWithValue("@FECHA_CARGA", obj.FECHA_CARGA);
                cmd.Parameters.AddWithValue("@ESTADO", obj.ESTADO);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void update(FACTURA_X_ORDEN_PEDIDO obj)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("UPDATE  FACTURA_X_ORDEN_PEDIDO SET");
                sql.AppendLine("NRO_ORDEN_PEDIDO=@NRO_ORDEN_PEDIDO");
                sql.AppendLine(", NRO_CUIT_PROVEEDOR=@NRO_CUIT_PROVEEDOR");
                sql.AppendLine(", FECHA_EMISION=@FECHA_EMISION");
                sql.AppendLine(", PUNTO_VENTA=@PUNTO_VENTA");
                sql.AppendLine(", NRO_COMPROBANTE=@NRO_COMPROBANTE");
                sql.AppendLine(", NRO_CAE=@NRO_CAE");
                sql.AppendLine(", IMPORTE=@IMPORTE");
                sql.AppendLine(", TIPO_COMPROBANTE=@TIPO_COMPROBANTE");
                sql.AppendLine(", USUARIO_CARGA=@USUARIO_CARGA");
                sql.AppendLine(", FECHA_CARGA=@FECHA_CARGA");
                sql.AppendLine(", ESTADO=@ESTADO");
                sql.AppendLine("WHERE");
                sql.AppendLine("ID=@ID");
                using (SqlConnection con = GetConnection())
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql.ToString();
                    cmd.Parameters.AddWithValue("@NRO_ORDEN_PEDIDO", obj.NRO_ORDEN_PEDIDO);
                    cmd.Parameters.AddWithValue("@NRO_CUIT_PROVEEDOR", obj.NRO_CUIT_PROVEEDOR);
                    cmd.Parameters.AddWithValue("@FECHA_EMISION", obj.FECHA_EMISION);
                    cmd.Parameters.AddWithValue("@PUNTO_VENTA", obj.PUNTO_VENTA);
                    cmd.Parameters.AddWithValue("@NRO_COMPROBANTE", obj.NRO_COMPROBANTE);
                    cmd.Parameters.AddWithValue("@NRO_CAE", obj.NRO_CAE);
                    cmd.Parameters.AddWithValue("@IMPORTE", obj.IMPORTE);
                    cmd.Parameters.AddWithValue("@TIPO_COMPROBANTE", obj.TIPO_COMPROBANTE);
                    cmd.Parameters.AddWithValue("@USUARIO_CARGA", obj.USUARIO_CARGA);
                    cmd.Parameters.AddWithValue("@FECHA_CARGA", obj.FECHA_CARGA);
                    cmd.Parameters.AddWithValue("@ESTADO", obj.ESTADO);
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void delete(int nroOp)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("DELETE  FACTURA_X_ORDEN_PEDIDO ");
                sql.AppendLine("WHERE");
                sql.AppendLine("NRO_ORDEN_PEDIDO=@NRO_ORDEN_PEDIDO");
                using (SqlConnection con = GetConnection())
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql.ToString();
                    cmd.Parameters.AddWithValue("@NRO_ORDEN_PEDIDO", nroOp);
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static void delete(int nroOp, SqlConnection cn, SqlTransaction trx)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("DELETE  FACTURA_X_ORDEN_PEDIDO ");
                sql.AppendLine("WHERE");
                sql.AppendLine("NRO_ORDEN_PEDIDO=@NRO_ORDEN_PEDIDO");
                {
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.Transaction = trx;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql.ToString();
                    cmd.Parameters.AddWithValue("@NRO_ORDEN_PEDIDO", nroOp);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}

