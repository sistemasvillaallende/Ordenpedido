using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL
{      public class ConsultaEstadoOP : DALBase
    {
        public int nro_nota_pedido { get; set; }
        public int nro_paso { get; set; }
        public string estado { get; set; }
        public string fecha { get; set; }
        public string observaciones { get; set; }
        public string responsable_op { get; set; }
        public string usuario { get; set; }

        public static List<ConsultaEstadoOP> GetByOP(int nroNotaPedido)
        {
            var movimientos = new List<ConsultaEstadoOP>();
            using (SqlConnection cn = DALBase.GetConnection())
            {
                string sql = @"
                    SELECT 
                        a.nro_nota_pedido, 
                        a.nro_paso, 
                        CASE 
                          WHEN cod_estado_op=1 THEN 'OP. Recibida'
                          WHEN cod_estado_op=2 THEN 'OP. Devuelta'
                          WHEN cod_estado_op=0 THEN 'Sin Estado'
                        END AS estado,
                        CONVERT(VARCHAR(10),a.fecha_mov_op,103) AS fecha,
                        a.observaciones, a.responsable_op, a.usuario
                    FROM ORDENES_PEDIDO_MOVIMIENTOS a
                    WHERE a.nro_nota_pedido=@nro";
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@nro", nroNotaPedido);
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var mov = new ConsultaEstadoOP();
                            if (!dr.IsDBNull(dr.GetOrdinal("nro_nota_pedido")))
                                mov.nro_nota_pedido = dr.GetInt32(dr.GetOrdinal("nro_nota_pedido"));
                            if (!dr.IsDBNull(dr.GetOrdinal("nro_paso")))
                                mov.nro_paso = dr.GetInt32(dr.GetOrdinal("nro_paso"));
                            if (!dr.IsDBNull(dr.GetOrdinal("estado")))
                                mov.estado = dr.GetString(dr.GetOrdinal("estado"));
                            if (!dr.IsDBNull(dr.GetOrdinal("fecha")))
                                mov.fecha = dr.GetString(dr.GetOrdinal("fecha"));
                            if (!dr.IsDBNull(dr.GetOrdinal("observaciones")))
                                mov.observaciones = dr.GetString(dr.GetOrdinal("observaciones"));
                            if (!dr.IsDBNull(dr.GetOrdinal("responsable_op")))
                                mov.responsable_op = dr.GetString(dr.GetOrdinal("responsable_op"));
                            if (!dr.IsDBNull(dr.GetOrdinal("usuario")))
                                mov.usuario = dr.GetString(dr.GetOrdinal("usuario"));
                            movimientos.Add(mov);
                        }
                    }
                }
            }
            return movimientos;
        }
    }
}
