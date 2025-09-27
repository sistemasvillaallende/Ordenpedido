using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Entities;

namespace DAL
{
    public class SecretariaDAL : DALBase
    {
        public static void Insert(Secretaria secretaria)
        {
            using (var cn = GetConnection())
            using (var cmd = new SqlCommand("INSERT INTO SECRETARIA (descripcion, codigo, activa, fecha_creacion) VALUES (@descripcion, @codigo, @activa, @fecha_creacion)", cn))
            {
                cmd.Parameters.AddWithValue("@descripcion", secretaria.descripcion);
                cmd.Parameters.AddWithValue("@codigo", secretaria.codigo ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@activa", secretaria.activa);
                cmd.Parameters.AddWithValue("@fecha_creacion", secretaria.fecha_creacion);
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void Update(Secretaria secretaria)
        {
            using (var cn = GetConnection())
            using (var cmd = new SqlCommand("UPDATE SECRETARIA SET descripcion=@descripcion, codigo=@codigo, activa=@activa WHERE id_secretaria=@id_secretaria", cn))
            {
                cmd.Parameters.AddWithValue("@id_secretaria", secretaria.id_secretaria);
                cmd.Parameters.AddWithValue("@descripcion", secretaria.descripcion);
                cmd.Parameters.AddWithValue("@codigo", secretaria.codigo ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@activa", secretaria.activa);
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void Delete(int id_secretaria)
        {
            using (var cn = GetConnection())
            using (var cmd = new SqlCommand("DELETE FROM SECRETARIA WHERE id_secretaria=@id_secretaria", cn))
            {
                cmd.Parameters.AddWithValue("@id_secretaria", id_secretaria);
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static Secretaria GetByPk(int id_secretaria)
        {
            using (var cn = GetConnection())
            using (var cmd = new SqlCommand("SELECT * FROM SECRETARIA WHERE id_secretaria=@id_secretaria", cn))
            {
                cmd.Parameters.AddWithValue("@id_secretaria", id_secretaria);
                cn.Open();
                using (var dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        return Map(dr);
                    }
                }
            }
            return null;
        }

        public static List<Secretaria> GetAll()
        {
            var list = new List<Secretaria>();
            using (var cn = GetConnection())
            using (var cmd = new SqlCommand("SELECT * FROM SECRETARIA", cn))
            {
                cn.Open();
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        list.Add(Map(dr));
                    }
                }
            }
            return list;
        }

        private static Secretaria Map(IDataReader dr)
        {
            return new Secretaria
            {
                id_secretaria = Convert.ToInt32(dr["id_secretaria"]),
                descripcion = dr["descripcion"].ToString(),
                codigo = dr["codigo"] == DBNull.Value ? null : dr["codigo"].ToString(),
                activa = dr["activa"] != DBNull.Value && Convert.ToBoolean(dr["activa"]),
                fecha_creacion = dr["fecha_creacion"] != DBNull.Value ? Convert.ToDateTime(dr["fecha_creacion"]) : DateTime.MinValue
            };
        }
    }
}