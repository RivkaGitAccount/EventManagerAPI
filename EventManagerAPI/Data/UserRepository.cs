using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using EventManagerAPI.Models;
using System.Configuration;

namespace EventManagerAPI.Data
{
    public class UserRepository
    {
        private readonly string _connectionString;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["EventManagerAPI"].ConnectionString);
        User userObj = new User();

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }


        
        public User GetUserByGmail(string Gmail)
        {

            SqlDataAdapter da = new SqlDataAdapter("[dbo].[GetUserByGmail]", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Gmail", Gmail);
            DataTable dt = new DataTable();
            da.Fill(dt);
            User user   = new User();
            if (dt.Rows.Count > 0)
            {
                user.Gmail  = dt.Rows[0]["Gmail"].ToString();
                user.Pass  = dt.Rows[0]["Pass"].ToString();
                user.Fname = dt.Rows[0]["Fname"].ToString();
                 

            }
            if (user != null)
            {
                return user;
            }
            else
            {
                return null;
            }
        }

        public bool AddUser(User user)
        {
            bool res = false;
            if ( user != null) 
            {
                SqlCommand cmd = new SqlCommand("[dbo].[AddUser]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                // Add the parameters for the stored procedure
                cmd.Parameters.AddWithValue("@Gmail", user.Gmail );
                cmd.Parameters.AddWithValue("@Fname", user.Fname);
                cmd.Parameters.AddWithValue("@Pass", user.Pass); 

                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();
                if (i > 0)
                {
                    res = true;
                }
                else
                {
                    res = false;
                }
            }

            return res;

        }
    }
}
 