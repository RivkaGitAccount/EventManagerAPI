using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using EventManagerAPI.Models;
using System;

namespace EventManagerAPI.Data
{
    public class EventRepository
    {
        private readonly string _connectionString;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["EventManagerAPI"].ConnectionString);
        Event eventObj = new Event();
        public EventRepository(string connectionString)
        {
            _connectionString = connectionString;
        }



        public List<Event> GetEventsByGmail(string GMAIL)
        {

            SqlDataAdapter da = new SqlDataAdapter("[dbo].[GetEventsByGmail]", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@GMAIL", GMAIL);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<Event> lstevent = new List<Event>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Event ev = new Event();
                    ev.EventName = dt.Rows[i]["EventName"].ToString();
                    ev.EventLocation = dt.Rows[i]["EventLocation"].ToString();
                    ev.GMAIL = dt.Rows[i]["GMAIL"].ToString();
                    ev.EventCode = dt.Rows[i]["EventCode"].ToString();
                    ev.EventDate = Convert.ToDateTime(dt.Rows[i]["EventDate"]);
                    lstevent.Add(ev);


                }

            }
            if (lstevent.Count > 0)
            {
                return lstevent;
            }
            else
            {
                return null;
            }
        }

        public List<Event> GetEvents()
        {

            SqlDataAdapter da = new SqlDataAdapter("[dbo].[GetEventsList]", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<Event> lstevent = new List<Event>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Event ev = new Event();
                    ev.EventName = dt.Rows[i]["EventName"].ToString();
                    ev.EventLocation = dt.Rows[i]["EventLocation"].ToString();
                    ev.GMAIL = dt.Rows[i]["GMAIL"].ToString();
                    ev.EventCode = dt.Rows[i]["EventCode"].ToString();
                    ev.EventDate = Convert.ToDateTime(dt.Rows[i]["EventDate"]);
                    lstevent.Add(ev);


                }

            }
            if (lstevent.Count > 0)
            {
                return lstevent;
            }
            else
            {
                return null;
            }
        }

        public Event GetEventByEventCode(string EventCode)
        {

            SqlDataAdapter da = new SqlDataAdapter("[dbo].[GetEventByEventCode]", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@EventCode", EventCode);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Event ev = new Event();
            if (dt.Rows.Count > 0)
            {
                ev.EventName = dt.Rows[0]["EventName"].ToString();
                ev.EventLocation = dt.Rows[0]["EventLocation"].ToString();
                ev.GMAIL = dt.Rows[0]["GMAIL"].ToString();
                ev.EventCode = dt.Rows[0]["EventCode"].ToString();
                ev.EventDate = Convert.ToDateTime(dt.Rows[0]["EventDate"]);



            }
            if (ev != null)
            {
                return ev;
            }
            else
            {
                return null;
            }
        }



        public bool AddEvent(Event ev)
        {
            bool res=false;
            if (ev != null)
            {
                SqlCommand cmd = new SqlCommand("[dbo].[AddEvent]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                // Add the parameters for the stored procedure
                cmd.Parameters.AddWithValue("@EventCode", ev.EventCode);
                cmd.Parameters.AddWithValue("@EventName", ev.EventName);
                cmd.Parameters.AddWithValue("@EventDate", ev.EventDate);
                cmd.Parameters.AddWithValue("@EventLocation", ev.EventLocation);
                cmd.Parameters.AddWithValue("@GMAIL", ev.GMAIL);

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