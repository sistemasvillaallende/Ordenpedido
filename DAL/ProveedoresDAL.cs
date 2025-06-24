using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class ProveedoresDAL
    {
        public static Entities.Proveedores getProveedorByPk(int cod)
        {
            Entities.Proveedores oProveedor = null;
            SqlCommand cmd;
            SqlDataReader dr;
            SqlConnection cn = null;
            StringBuilder strSQL = new StringBuilder();

            strSQL.AppendLine("SELECT *FROM Proveedores");
            strSQL.AppendLine("WHERE cod_proveedor = @cod");
            //strSQL.AppendLine("AND (baja =0 or baja is null)");


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
                    oProveedor = new Entities.Proveedores();
                    if (!dr.IsDBNull(dr.GetOrdinal("cod_proveedor"))) oProveedor.codProveedor = dr.GetInt32((dr.GetOrdinal("cod_proveedor")));
                    if (!dr.IsDBNull(dr.GetOrdinal("nom_proveedor"))) oProveedor.nomProveedor = dr.GetString((dr.GetOrdinal("nom_proveedor")));
                    if (!dr.IsDBNull(dr.GetOrdinal("nro_bad"))) oProveedor.nroBad = dr.GetInt32((dr.GetOrdinal("nro_bad")));
                    if (!dr.IsDBNull(dr.GetOrdinal("cod_tipo_proveedor"))) oProveedor.codTipoProveedor = dr.GetInt32((dr.GetOrdinal("cod_tipo_proveedor")));
                    if (!dr.IsDBNull(dr.GetOrdinal("cod_cond_ante_iva"))) oProveedor.codCondAnteIva = dr.GetInt32((dr.GetOrdinal("cod_cond_ante_iva")));
                    if (!dr.IsDBNull(dr.GetOrdinal("cod_calle"))) oProveedor.codCalle = dr.GetInt32((dr.GetOrdinal("cod_calle")));
                    if (!dr.IsDBNull(dr.GetOrdinal("nom_calle"))) oProveedor.nomCAlle = dr.GetString((dr.GetOrdinal("nom_calle")));
                    if (!dr.IsDBNull(dr.GetOrdinal("nro_dom"))) oProveedor.nroDom = dr.GetInt32((dr.GetOrdinal("nro_dom")));
                    if (!dr.IsDBNull(dr.GetOrdinal("cod_barrio"))) oProveedor.codBarrio = dr.GetInt32((dr.GetOrdinal("cod_barrio")));
                    if (!dr.IsDBNull(dr.GetOrdinal("cod_postal"))) oProveedor.codPostal = dr.GetString((dr.GetOrdinal("cod_postal")));
                    if (!dr.IsDBNull(dr.GetOrdinal("provincia"))) oProveedor.provincia = dr.GetString((dr.GetOrdinal("provincia")));
                    if (!dr.IsDBNull(dr.GetOrdinal("pais"))) oProveedor.pais = dr.GetString((dr.GetOrdinal("pais")));
                    if (!dr.IsDBNull(dr.GetOrdinal("nro_cuit"))) oProveedor.nroCuit = dr.GetString((dr.GetOrdinal("nro_cuit")));
                    if (!dr.IsDBNull(dr.GetOrdinal("nro_ing_bruto"))) oProveedor.nroIngBruto = dr.GetString((dr.GetOrdinal("nro_ing_bruto")));
                    if (!dr.IsDBNull(dr.GetOrdinal("nro_caja_jub"))) oProveedor.nroCajaJub = dr.GetString((dr.GetOrdinal("nro_caja_jub")));
                    if (!dr.IsDBNull(dr.GetOrdinal("telefono"))) oProveedor.telefono = dr.GetString((dr.GetOrdinal("telefono")));
                    if (!dr.IsDBNull(dr.GetOrdinal("piso_dpto"))) oProveedor.pisoDpto = dr.GetString((dr.GetOrdinal("piso_dpto")));
                    if (!dr.IsDBNull(dr.GetOrdinal("fecha_alta"))) oProveedor.fechaAlta = dr.GetDateTime((dr.GetOrdinal("fecha_alta")));
                    if (!dr.IsDBNull(dr.GetOrdinal("e_mail"))) oProveedor.eMail = dr.GetString((dr.GetOrdinal("e_mail")));
                    if (!dr.IsDBNull(dr.GetOrdinal("cod_subtipo"))) oProveedor.codSubtipo = dr.GetInt32((dr.GetOrdinal("cod_subtipo")));
                }


            }
            catch (Exception e)
            {
                Console.WriteLine("Error in query!" + e.ToString());
                throw e;
            }
            finally
            { cn.Close(); }
            return oProveedor;
        }

        public static List<Entities.Proveedores> findProveedorByNombre(string nom)
        {
            Entities.Proveedores oProveedor = null;
            List<Entities.Proveedores> lstProveedores = new List<Entities.Proveedores>();

            SqlCommand cmd;
            SqlDataReader dr;
            SqlConnection cn = null;
            StringBuilder strSQL = new StringBuilder();

            strSQL.AppendLine("SELECT *FROM Proveedores");
            strSQL.AppendLine("WHERE (baja=0 or baja is null) AND nom_proveedor LIKE @nom");

            cmd = new SqlCommand();

            cmd.Parameters.Add(new SqlParameter("@nom", "%"+nom+"%"));


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
                    oProveedor = new Entities.Proveedores();
                    if (!dr.IsDBNull(dr.GetOrdinal("cod_proveedor"))) oProveedor.codProveedor = dr.GetInt32((dr.GetOrdinal("cod_proveedor")));
                    if (!dr.IsDBNull(dr.GetOrdinal("nom_proveedor"))) oProveedor.nomProveedor = dr.GetString((dr.GetOrdinal("nom_proveedor")));
                    if (!dr.IsDBNull(dr.GetOrdinal("nro_bad"))) oProveedor.nroBad = dr.GetInt32((dr.GetOrdinal("nro_bad")));
                    if (!dr.IsDBNull(dr.GetOrdinal("cod_tipo_proveedor"))) oProveedor.codTipoProveedor = dr.GetInt32((dr.GetOrdinal("cod_tipo_proveedor")));
                    if (!dr.IsDBNull(dr.GetOrdinal("cod_cond_ante_iva"))) oProveedor.codCondAnteIva = dr.GetInt32((dr.GetOrdinal("cod_cond_ante_iva")));
                    if (!dr.IsDBNull(dr.GetOrdinal("cod_calle"))) oProveedor.codCalle = dr.GetInt32((dr.GetOrdinal("cod_calle")));
                    if (!dr.IsDBNull(dr.GetOrdinal("nom_calle"))) oProveedor.nomCAlle = dr.GetString((dr.GetOrdinal("nom_calle")));
                    if (!dr.IsDBNull(dr.GetOrdinal("nro_dom"))) oProveedor.nroDom = dr.GetInt32((dr.GetOrdinal("nro_dom")));
                    if (!dr.IsDBNull(dr.GetOrdinal("cod_barrio"))) oProveedor.codBarrio = dr.GetInt32((dr.GetOrdinal("cod_barrio")));
                    if (!dr.IsDBNull(dr.GetOrdinal("cod_postal"))) oProveedor.codPostal = dr.GetString((dr.GetOrdinal("cod_postal")));
                    if (!dr.IsDBNull(dr.GetOrdinal("provincia"))) oProveedor.provincia = dr.GetString((dr.GetOrdinal("provincia")));
                    if (!dr.IsDBNull(dr.GetOrdinal("pais"))) oProveedor.pais = dr.GetString((dr.GetOrdinal("pais")));
                    if (!dr.IsDBNull(dr.GetOrdinal("nro_cuit"))) oProveedor.nroCuit = dr.GetString((dr.GetOrdinal("nro_cuit")));
                    if (!dr.IsDBNull(dr.GetOrdinal("nro_ing_bruto"))) oProveedor.nroIngBruto = dr.GetString((dr.GetOrdinal("nro_ing_bruto")));
                    if (!dr.IsDBNull(dr.GetOrdinal("nro_caja_jub"))) oProveedor.nroCajaJub = dr.GetString((dr.GetOrdinal("nro_caja_jub")));
                    if (!dr.IsDBNull(dr.GetOrdinal("telefono"))) oProveedor.telefono = dr.GetString((dr.GetOrdinal("telefono")));
                    if (!dr.IsDBNull(dr.GetOrdinal("piso_dpto"))) oProveedor.pisoDpto = dr.GetString((dr.GetOrdinal("piso_dpto")));
                    if (!dr.IsDBNull(dr.GetOrdinal("fecha_alta"))) oProveedor.fechaAlta = dr.GetDateTime((dr.GetOrdinal("fecha_alta")));
                    if (!dr.IsDBNull(dr.GetOrdinal("e_mail"))) oProveedor.eMail = dr.GetString((dr.GetOrdinal("e_mail")));
                    if (!dr.IsDBNull(dr.GetOrdinal("cod_subtipo"))) oProveedor.codSubtipo = dr.GetInt32((dr.GetOrdinal("cod_subtipo")));
                    lstProveedores.Add(oProveedor);
                }


            }
            catch (Exception e)
            {
                Console.WriteLine("Error in query!" + e.ToString());
                throw e;
            }
            finally
            { cn.Close(); }
            return lstProveedores;
        }

        public static List<Entities.Proveedores> read()
        {
            Entities.Proveedores oProveedor = null;
            List<Entities.Proveedores> lstProveedores = new List<Entities.Proveedores>();

            SqlCommand cmd;
            SqlDataReader dr;
            SqlConnection cn = null;
            StringBuilder strSQL = new StringBuilder();

            strSQL.AppendLine("SELECT *FROM Proveedores");
            strSQL.AppendLine("WHERE (baja=0 or baja is null)");

            cmd = new SqlCommand();

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
                    oProveedor = new Entities.Proveedores();
                    if (!dr.IsDBNull(dr.GetOrdinal("cod_proveedor"))) oProveedor.codProveedor = dr.GetInt32((dr.GetOrdinal("cod_proveedor")));
                    if (!dr.IsDBNull(dr.GetOrdinal("nom_proveedor"))) oProveedor.nomProveedor = dr.GetString((dr.GetOrdinal("nom_proveedor")));
                    if (!dr.IsDBNull(dr.GetOrdinal("nro_bad"))) oProveedor.nroBad = dr.GetInt32((dr.GetOrdinal("nro_bad")));
                    if (!dr.IsDBNull(dr.GetOrdinal("cod_tipo_proveedor"))) oProveedor.codTipoProveedor = dr.GetInt32((dr.GetOrdinal("cod_tipo_proveedor")));
                    if (!dr.IsDBNull(dr.GetOrdinal("cod_cond_ante_iva"))) oProveedor.codCondAnteIva = dr.GetInt32((dr.GetOrdinal("cod_cond_ante_iva")));
                    if (!dr.IsDBNull(dr.GetOrdinal("cod_calle"))) oProveedor.codCalle = dr.GetInt32((dr.GetOrdinal("cod_calle")));
                    if (!dr.IsDBNull(dr.GetOrdinal("nom_calle"))) oProveedor.nomCAlle = dr.GetString((dr.GetOrdinal("nom_calle")));
                    if (!dr.IsDBNull(dr.GetOrdinal("nro_dom"))) oProveedor.nroDom = dr.GetInt32((dr.GetOrdinal("nro_dom")));
                    if (!dr.IsDBNull(dr.GetOrdinal("cod_barrio"))) oProveedor.codBarrio = dr.GetInt32((dr.GetOrdinal("cod_barrio")));
                    if (!dr.IsDBNull(dr.GetOrdinal("cod_postal"))) oProveedor.codPostal = dr.GetString((dr.GetOrdinal("cod_postal")));
                    if (!dr.IsDBNull(dr.GetOrdinal("provincia"))) oProveedor.provincia = dr.GetString((dr.GetOrdinal("provincia")));
                    if (!dr.IsDBNull(dr.GetOrdinal("pais"))) oProveedor.pais = dr.GetString((dr.GetOrdinal("pais")));
                    if (!dr.IsDBNull(dr.GetOrdinal("nro_cuit"))) oProveedor.nroCuit = dr.GetString((dr.GetOrdinal("nro_cuit")));
                    if (!dr.IsDBNull(dr.GetOrdinal("nro_ing_bruto"))) oProveedor.nroIngBruto = dr.GetString((dr.GetOrdinal("nro_ing_bruto")));
                    if (!dr.IsDBNull(dr.GetOrdinal("nro_caja_jub"))) oProveedor.nroCajaJub = dr.GetString((dr.GetOrdinal("nro_caja_jub")));
                    if (!dr.IsDBNull(dr.GetOrdinal("telefono"))) oProveedor.telefono = dr.GetString((dr.GetOrdinal("telefono")));
                    if (!dr.IsDBNull(dr.GetOrdinal("piso_dpto"))) oProveedor.pisoDpto = dr.GetString((dr.GetOrdinal("piso_dpto")));
                    if (!dr.IsDBNull(dr.GetOrdinal("fecha_alta"))) oProveedor.fechaAlta = dr.GetDateTime((dr.GetOrdinal("fecha_alta")));
                    if (!dr.IsDBNull(dr.GetOrdinal("e_mail"))) oProveedor.eMail = dr.GetString((dr.GetOrdinal("e_mail")));
                    if (!dr.IsDBNull(dr.GetOrdinal("cod_subtipo"))) oProveedor.codSubtipo = dr.GetInt32((dr.GetOrdinal("cod_subtipo")));
                    lstProveedores.Add(oProveedor);
                }


            }
            catch (Exception e)
            {
                Console.WriteLine("Error in query!" + e.ToString());
                throw e;
            }
            finally
            { cn.Close(); }
            return lstProveedores;
        }
    }
}
