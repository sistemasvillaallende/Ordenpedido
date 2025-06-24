using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;

namespace DAL
{
    public class DALBase
    {
        public DALBase()
        {

        }

        public static SqlConnection GetConnection()
        {
            string connectionString;
            SqlConnection objCon;

            connectionString = ConfigurationManager.ConnectionStrings["Siimva"].ConnectionString;
            objCon = new SqlConnection(connectionString);

            return objCon;
        }
    }
}
