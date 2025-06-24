using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class OficinasDAL
    {
        public static Entities.Oficinas getOficinasByPk(int cod)
        {
            Entities.Oficinas oOficina = null;
            SqlCommand cmd;
            SqlDataReader dr;
            SqlConnection cn = null;
            StringBuilder strSQL = new StringBuilder();

            strSQL.AppendLine("SELECT *FROM OFICINAS");
            strSQL.AppendLine("WHERE codigo_oficina = @cod");

            cmd = new SqlCommand();

            cmd.Parameters.Add(new SqlParameter("@cod", cod));


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
                    oOficina = new Entities.Oficinas();
                    if (!dr.IsDBNull(dr.GetOrdinal("codigo_oficina"))) oOficina.idOficina = dr.GetInt32((dr.GetOrdinal("codigo_oficina")));
                    if (!dr.IsDBNull(dr.GetOrdinal("nombre_oficina"))) oOficina.nombre = dr.GetString((dr.GetOrdinal("nombre_oficina")));
                    //if (!dr.IsDBNull(dr.GetOrdinal("secretaria"))) oOficina.secretaria = dr.GetByte(dr.GetOrdinal("secretaria"));
                    //if (!dr.IsDBNull(dr.GetOrdinal("activo"))) oOficina.activo = dr.GetByte(dr.GetOrdinal("activo"));
                }


            }
            catch (Exception e)
            {
                Console.WriteLine("Error in query!" + e.ToString());
                throw e;
            }
            finally
            { cn.Close(); }
            return oOficina;
        }

        public static List<Entities.Oficinas> findOficinasByNombre(string nombre)
        {
            Entities.Oficinas oOficina = null;
            List<Entities.Oficinas> lstOficinas = new List<Entities.Oficinas>();
            SqlCommand cmd;
            SqlDataReader dr;
            SqlConnection cn = null;
            StringBuilder strSQL = new StringBuilder();

            strSQL.AppendLine("SELECT *FROM OFICINAS");
            strSQL.AppendLine("WHERE nombre_oficina LIKE @nom");

            cmd = new SqlCommand();

            cmd.Parameters.Add(new SqlParameter("@nom", "%" + nombre + "%"));


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
                    oOficina = new Entities.Oficinas();
                    if (!dr.IsDBNull(dr.GetOrdinal("codigo_oficina"))) oOficina.idOficina = dr.GetInt32((dr.GetOrdinal("codigo_oficina")));
                    if (!dr.IsDBNull(dr.GetOrdinal("nombre_oficina"))) oOficina.nombre = dr.GetString((dr.GetOrdinal("nombre_oficina")));
                    //if (!dr.IsDBNull(dr.GetOrdinal("secretaria"))) oOficina.secretaria = Convert.ToInt32(dr.GetByte(dr.GetOrdinal("secretaria")));
                    //if (!dr.IsDBNull(dr.GetOrdinal("activo"))) oOficina.activo = Convert.ToInt32(dr.GetByte(dr.GetOrdinal("activo")));
                    lstOficinas.Add(oOficina);
                }


            }
            catch (Exception e)
            {
                Console.WriteLine("Error in query!" + e.ToString());
                throw e;
            }
            finally
            { cn.Close(); }
            return lstOficinas;
        }


        public static List<Entities.Oficinas> OficinasByUsuario(string user)
        {
            Entities.Oficinas oOficina = null;
            List<Entities.Oficinas> lstOficinas = new List<Entities.Oficinas>();
            SqlCommand cmd;
            SqlDataReader dr;
            SqlConnection cn = null;
            string strSQL = @"SELECT 
                              c.codigo_oficina, 
                              c.nombre_oficina, 
                              orden = 1 
                            FROM USUARIOS_V2 a 
                            join OFICINAS c on 
                            a.COD_OFICINA = c.codigo_oficina 
                            WHERE a.baja=0 AND
                            a.nombre = @user 
                            UNION
                            SELECT 
                              c.codigo_oficina, 
                              c.nombre_oficina, 
                              orden = 2 
                            FROM USUARIOS_V2 a 
                            join USUARIOS_X_OFICINA b on 
                            b.COD_USUARIO = a.COD_USUARIO 
                            join OFICINAS c on 
                            b.COD_OFICINA = c.codigo_oficina 
                            WHERE a.baja=0 AND
                            a.nombre = @user 
                            ORDER BY orden, c.nombre_oficina";
            cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@user", user);
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
                    oOficina = new Entities.Oficinas();
                    if (!dr.IsDBNull(dr.GetOrdinal("codigo_oficina"))) oOficina.idOficina = dr.GetInt32((dr.GetOrdinal("codigo_oficina")));
                    if (!dr.IsDBNull(dr.GetOrdinal("nombre_oficina"))) oOficina.nombre = dr.GetString((dr.GetOrdinal("nombre_oficina")));
                    lstOficinas.Add(oOficina);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error in query!" + e.ToString());
                throw e;
            }
            finally
            { cn.Close(); }
            return lstOficinas;
        }


        public static DataSet ListOficinas(int id)
        {
            DataSet ds;
            SqlDataAdapter adapter;
            string strSQL = @"SELECT 
                                codigo_oficina, 
                                nombre_oficina 
                              FROM OFICINAS
                              WHERE codigo_oficina=@id";

            using (SqlConnection conn = DALBase.GetConnection())
            {
                try
                {
                    ds = new DataSet();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    if (id > 0)
                    {
                        cmd.CommandText = "";
                        cmd.Parameters.AddWithValue("@id", id);
                    }
                    else
                        cmd.CommandText = "SELECT * FROM OFICINAS";
                    cmd.Connection.Open();
                    //
                    adapter = new SqlDataAdapter(cmd);
                    adapter.SelectCommand = cmd;
                    adapter.Fill(ds);

                    return ds;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

    }
}
