using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Entities;

namespace DAL
{
    public class DesplegablesDAL
    {
        public static List<Secretaria> GetSecretarias(int idSecretaria)
        {
            var lista = new List<Secretaria>();
            string strSQL = "SELECT id_secretaria, descripcion FROM SECRETARIA WHERE activa=1";
            if (idSecretaria > 0)
                strSQL += " AND id_secretaria=" + idSecretaria;
            strSQL += " ORDER BY id_secretaria";

            using (SqlConnection conn = DALBase.GetConnection())
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSQL;
                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var sec = new Secretaria();
                        if (!dr.IsDBNull(dr.GetOrdinal("id_secretaria")))
                            sec.id_secretaria = dr.GetInt32(dr.GetOrdinal("id_secretaria"));
                        if (!dr.IsDBNull(dr.GetOrdinal("descripcion")))
                            sec.descripcion = dr.GetString(dr.GetOrdinal("descripcion"));
                        lista.Add(sec);
                    }
                }
            }
            return lista;
        }

        public static List<Direccion> GetDirecciones(int idSecretaria)
        {
            var lista = new List<Direccion>();
            string strSQL = "SELECT DISTINCT dxs.id_direccion, d.Descripcion AS direccion, dxs.nro_cta " +
                            "FROM DIRECCION_X_SECRETARIA dxs " +
                            "JOIN DIRECCION d ON d.id_direccion = dxs.id_direccion " +
                            "WHERE dxs.activo=1";
            if (idSecretaria > 0)
                strSQL += " AND dxs.id_secretaria=" + idSecretaria;
            strSQL += " ORDER BY dxs.id_direccion";

            using (SqlConnection conn = DALBase.GetConnection())
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSQL;
                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var dir = new Direccion();
                        if (!dr.IsDBNull(dr.GetOrdinal("id_direccion")))
                            dir.id_direccion = dr.GetInt32(dr.GetOrdinal("id_direccion"));
                        if (!dr.IsDBNull(dr.GetOrdinal("direccion")))
                            dir.descripcion = dr.GetString(dr.GetOrdinal("direccion"));
                        if (!dr.IsDBNull(dr.GetOrdinal("nro_cta")))
                            dir.nro_cta = dr.GetString(dr.GetOrdinal("nro_cta"));
                        lista.Add(dir);
                    }
                }
            }
            return lista;
        }
    }
}
