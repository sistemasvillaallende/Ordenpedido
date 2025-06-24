using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;
using System.Data.SqlClient;
using System.Data;
namespace DAL
{
    public class UsuariosDAL
    {
        public static List<Usuarios> getUserByOffice(int codOffice)
        {
            Usuarios oUsu = new Usuarios();
            List<Usuarios> lstUser = new List<Usuarios>();
            SqlCommand cmd;
            SqlDataReader dr;
            SqlConnection cn = null;
            StringBuilder strSQL = new StringBuilder();

            strSQL.AppendLine("SELECT cod_usuario, nombre, nombre_completo FROM usuarios_v2");
            strSQL.AppendLine("WHERE cod_oficina = @cod");

            cmd = new SqlCommand();

            cmd.Parameters.Add(new SqlParameter("@cod", codOffice));

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
                    oUsu = new Usuarios();
                    if (!dr.IsDBNull(dr.GetOrdinal("cod_usuario"))) 
                        oUsu.codUsuario = dr.GetInt32((dr.GetOrdinal("cod_usuario")));
                    if (!dr.IsDBNull(dr.GetOrdinal("nombre"))) oUsu.nombre =
                        dr.GetString((dr.GetOrdinal("nombre")));
                    if (!dr.IsDBNull(dr.GetOrdinal("nombre_completo")))
                        oUsu.nombre_completo = dr.GetString(dr.GetOrdinal("nombre_completo"));
                    lstUser.Add(oUsu);
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
            return lstUser;
        }
    }
}
