using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DAL
{
    public class FACTURA_X_ORDEN_PEDIDO_DETALLE : DALBase
    {
        public int NRO_ORDEN_PEDIDO { get; set; }
        public int NRO_ITEM { get; set; }
        public int NRO_FACTURA { get; set; }

        public FACTURA_X_ORDEN_PEDIDO_DETALLE()
        {
            NRO_ORDEN_PEDIDO = 0;
            NRO_ITEM = 0;
            NRO_FACTURA = 0;
        }

        private static List<FACTURA_X_ORDEN_PEDIDO_DETALLE> mapeo(SqlDataReader dr)
        {
            List<FACTURA_X_ORDEN_PEDIDO_DETALLE> lst = new List<FACTURA_X_ORDEN_PEDIDO_DETALLE>();
            FACTURA_X_ORDEN_PEDIDO_DETALLE obj;
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    obj = new FACTURA_X_ORDEN_PEDIDO_DETALLE();
                    if (!dr.IsDBNull(0)) { obj.NRO_ORDEN_PEDIDO = dr.GetInt32(0); }
                    if (!dr.IsDBNull(1)) { obj.NRO_ITEM = dr.GetInt32(1); }
                    if (!dr.IsDBNull(2)) { obj.NRO_FACTURA = dr.GetInt32(2); }
                    lst.Add(obj);
                }
            }
            return lst;
        }

        public static List<FACTURA_X_ORDEN_PEDIDO_DETALLE> read()
        {
            try
            {
                List<FACTURA_X_ORDEN_PEDIDO_DETALLE> lst = new List<FACTURA_X_ORDEN_PEDIDO_DETALLE>();
                using (SqlConnection con = GetConnection())
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT *FROM FACTURA_X_ORDEN_PEDIDO_DETALLE";
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

        public static FACTURA_X_ORDEN_PEDIDO_DETALLE getByPk(
        )
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("SELECT *FROM FACTURA_X_ORDEN_PEDIDO_DETALLE WHERE");
                FACTURA_X_ORDEN_PEDIDO_DETALLE obj = null;
                using (SqlConnection con = GetConnection())
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql.ToString();
                    cmd.Connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    List<FACTURA_X_ORDEN_PEDIDO_DETALLE> lst = mapeo(dr);
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

        public static int insert(FACTURA_X_ORDEN_PEDIDO_DETALLE obj)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("INSERT INTO FACTURA_X_ORDEN_PEDIDO_DETALLE(");
                sql.AppendLine("NRO_ORDEN_PEDIDO");
                sql.AppendLine(", NRO_ITEM");
                sql.AppendLine(", NRO_FACTURA");
                sql.AppendLine(")");
                sql.AppendLine("VALUES");
                sql.AppendLine("(");
                sql.AppendLine("@NRO_ORDEN_PEDIDO");
                sql.AppendLine(", @NRO_ITEM");
                sql.AppendLine(", @NRO_FACTURA");
                sql.AppendLine(")");
                sql.AppendLine("SELECT SCOPE_IDENTITY()");
                using (SqlConnection con = GetConnection())
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql.ToString();
                    cmd.Parameters.AddWithValue("@NRO_ORDEN_PEDIDO", obj.NRO_ORDEN_PEDIDO);
                    cmd.Parameters.AddWithValue("@NRO_ITEM", obj.NRO_ITEM);
                    cmd.Parameters.AddWithValue("@NRO_FACTURA", obj.NRO_FACTURA);
                    cmd.Connection.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void update(FACTURA_X_ORDEN_PEDIDO_DETALLE obj)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("UPDATE  FACTURA_X_ORDEN_PEDIDO_DETALLE SET");
                sql.AppendLine("NRO_ORDEN_PEDIDO=@NRO_ORDEN_PEDIDO");
                sql.AppendLine(", NRO_ITEM=@NRO_ITEM");
                sql.AppendLine(", NRO_FACTURA=@NRO_FACTURA");
                sql.AppendLine("WHERE");
                using (SqlConnection con = GetConnection())
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql.ToString();
                    cmd.Parameters.AddWithValue("@NRO_ORDEN_PEDIDO", obj.NRO_ORDEN_PEDIDO);
                    cmd.Parameters.AddWithValue("@NRO_ITEM", obj.NRO_ITEM);
                    cmd.Parameters.AddWithValue("@NRO_FACTURA", obj.NRO_FACTURA);
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void delete(FACTURA_X_ORDEN_PEDIDO_DETALLE obj)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("DELETE  FACTURA_X_ORDEN_PEDIDO_DETALLE ");
                sql.AppendLine("WHERE");
                using (SqlConnection con = GetConnection())
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql.ToString();
                    cmd.Connection.Open();
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

