using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using EventManagerAPI.Models;
using System;

namespace EventManagerAPI.Data
{
    public class GuestsRepository
    {

        private readonly string _connectionString;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["EventManagerAPI"].ConnectionString);
        Event eventObj = new Event();
        public GuestsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool AddGuest(Guest guest)
        {
            bool res = false;
            if (guest != null)
            {
                SqlCommand cmd = new SqlCommand("[dbo].[AddGuest]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                // Add the parameters for the stored procedure
                cmd.Parameters.AddWithValue("@GuestCode", guest.GuestCode);
                cmd.Parameters.AddWithValue("@GuestName", guest.GuestName);
                cmd.Parameters.AddWithValue("@RelationshipType", guest.RelationshipType ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Phone", guest.Phone ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Email", guest.Email ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@InvitationSent", guest.InvitationSent ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@ArrivalConfirmed", guest.ArrivalConfirmed ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@NumPeople", guest.NumPeople ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@EventCode", guest.EventCode ?? (object)DBNull.Value);


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

        public List<Guest> GetGuestsEventCode(string EventCode)
        {

            SqlDataAdapter da = new SqlDataAdapter("[dbo].[GetGuestsByEventCode]", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@EventCode", EventCode);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<Guest> lstGuests= new List<Guest>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Guest Gu = new Guest();
                   Gu.GuestCode = dt.Rows[i]["GuestCode"].ToString();
                    Gu.GuestName = dt.Rows[i]["GuestName"].ToString();
                   Gu.RelationshipType = dt.Rows[i]["RelationshipType"].ToString();
                    Gu.EventCode = dt.Rows[i]["Phone"].ToString();
                    Gu.Email = dt.Rows[i]["Email"].ToString();
                    Gu.InvitationSent = Convert.ToBoolean(dt.Rows[i]["InvitationSent"]);
                    Gu.ArrivalConfirmed = Convert.ToBoolean(dt.Rows[i]["ArrivalConfirmed"]);
                    Gu.NumPeople = Convert.ToInt32(dt.Rows[i]["NumPeople"]);

                    lstGuests.Add(Gu);

            
    }

            }
            if (lstGuests.Count > 0)
            {
                return lstGuests;
            }
            else
            {
                return null;
            }
        }

    }
}