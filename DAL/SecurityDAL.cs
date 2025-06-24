using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace DAL
{
    public class SecurityDAL
    {
        public bool ValidUser(string user, string password, string nombreUsuario)
        {
            bool resultado = false;
            string md5Passwd = "";
            string md5Passwd_ = "";
            bool? mExiste = false;


            SqlCommand cmd;
            SqlDataReader dr;
            SqlConnection cn = null;
            StringBuilder strSQL = new StringBuilder();

            MD5Encryption objMD5 = new MD5Encryption();

            user = user.Replace("'", "").Replace(",", "").Replace("=", "");

            strSQL.AppendLine("SELECT * From USUARIOS_V2 WHERE nombre = @user");
            //strSQL += "SELECT * From USUARIOS_V2 WHERE nombre='" + user + "'";
            cmd = new SqlCommand();

            cmd.Parameters.Add(new SqlParameter("@user", user));
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
                    mExiste = true;
                    nombreUsuario = dr.GetString(dr.GetOrdinal("nombre"));
                    md5Passwd = dr.GetString(dr.GetOrdinal("passwd"));
                    md5Passwd_ = objMD5.EncryptMD5(password.Trim().ToUpper() + user.Trim().ToUpper());
                    if (md5Passwd == md5Passwd_)
                        resultado = true;
                }
                return resultado;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message + " Error en la Autenticación!!!.");
            }

            finally
            {
                cn.Close();
                objMD5 = null;
            }
        }

        public bool ValidaPermiso(string User, string Proceso)
        {
            string strSQL = "";
            strSQL = "SELECT * FROM PROCESOS_V2 a, PROCESOS_x_USUARIO_V2 b, ";
            strSQL += "USUARIOS_V2 c WHERE ";
            strSQL += "c.nombre='" + User + "' AND ";
            strSQL += "c.cod_usuario=b.cod_usuario AND ";
            strSQL += "b.cod_proceso=a.cod_proceso AND ";
            strSQL += "a.proceso='" + Proceso + "'";



            SqlCommand cmd;
            SqlDataReader reader;
            SqlConnection cn = null;
            MD5Encryption objMD5 = new MD5Encryption();

            cn = DALBase.GetConnection();
            cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = strSQL.ToString();
            cmd.Connection.Open();
            
            try
            {
                //cn.Open();
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    reader.Close();
                    return true;
                }
                else
                {
                    strSQL = "SELECT * FROM USUARIOS_V2 WHERE ";
                    strSQL += "administrador=1 AND ";
                    strSQL += "nombre='" + User + "'";
                    reader.Close();
                    //cmd = db.GetSqlStringCommand(strSQL);
                    //reader = db.ExecuteReader(cmd);
                    cmd.CommandText = strSQL.ToString();
                    //cmd.Connection.Open();
                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        reader.Close();
                        return true;
                    }
                    else
                    {
                        reader.Close();
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " Error en la Autorización de Permiso!!!.");
            }
            finally
            {
                cn.Close();
            }
        }


        public bool ValidaPermiso(string User, string Proceso, out int id_oficina)
        {
            string strSQL = "";
            strSQL = "SELECT * FROM PROCESOS_V2 a, PROCESOS_x_USUARIO_V2 b, ";
            strSQL += "USUARIOS_V2 c WHERE ";
            strSQL += "c.nombre='" + User + "' AND ";
            strSQL += "c.cod_usuario=b.cod_usuario AND ";
            strSQL += "b.cod_proceso=a.cod_proceso AND ";
            strSQL += "a.proceso='" + Proceso + "'";

            //Database db = DatabaseFactory.CreateDatabase("SIIMVA");
            //DbConnection cn = db.CreateConnection();
            //MD5Encryption objMD5 = new MD5Encryption();
            //DbCommand cmd = db.GetSqlStringCommand(strSQL);
            //IDataReader reader = null;
            //cmd.Connection = cn;

            SqlCommand cmd;
            SqlDataReader reader;
            SqlConnection cn = null;
            MD5Encryption objMD5 = new MD5Encryption();

            cn = DALBase.GetConnection();
            cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = strSQL.ToString();
            cmd.Connection.Open();

            try
            {
                //cn.Open();
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    id_oficina = Convert.IsDBNull(reader["cod_oficina"]) ? 0 : Convert.ToInt16(reader["cod_oficina"]);
                    reader.Close();
                    return true;
                }
                else
                {
                    strSQL = "SELECT * FROM USUARIOS_V2 WHERE ";
                    strSQL += "administrador=1 AND ";
                    strSQL += "nombre='" + User + "'";
                    reader.Close();
                    //cmd = db.GetSqlStringCommand(strSQL);
                    //reader = db.ExecuteReader(cmd);
                    cmd.CommandText = strSQL.ToString();
                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        id_oficina = Convert.IsDBNull(reader["cod_oficina"]) ? 0 : Convert.ToInt16(reader["cod_oficina"]);
                        reader.Close();
                        return true;
                    }
                    else
                    {
                        reader.Close();
                        id_oficina = 0;
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " Error en la Autorización de Permiso!!!.");
            }
            finally { cn.Close(); }
        }



        public bool AutorizaOpcionesMenu(int id_opcion, string login)
        {
            bool autoriza = false;
            string strSQL = " ";
            strSQL += "SELECT  *    ";
            strSQL += "FROM SE_OPCIONES_X_USUARIO a ";
            strSQL += "join SE_USUARIO b on ";
            strSQL += "a.id_usuario=b.id_usuario ";
            strSQL += "WHERE b.login='" + login + "' ";
            strSQL += "AND a.id_opcion=" + id_opcion.ToString();

            //Database db = DatabaseFactory.CreateDatabase("SIIMVA");
            //DbCommand cmd = db.GetSqlStringCommand(strSQL);
            //DbConnection cnn = db.CreateConnection();
            //DbDataReader reader = null;
            //cmd.Connection = cnn;
            //cnn.Open();
            //reader = cmd.ExecuteReader();


            SqlCommand cmd;
            SqlDataReader reader;
            SqlConnection cn = null;
            MD5Encryption objMD5 = new MD5Encryption();

            cn = DALBase.GetConnection();
            cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = strSQL.ToString();
            cmd.Connection.Open();

            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                autoriza = true;
            }
            cn.Close();
            return autoriza;
        }


    }
}
