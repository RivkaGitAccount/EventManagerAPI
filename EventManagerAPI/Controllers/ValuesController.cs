using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;
using System.Configuration;
using EventManagerAPI.Models;
using System.Data;
using Newtonsoft.Json;

namespace EventManagerAPI.Controllers
{
    public class ValuesController : ApiController
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["EventManagerAPI"].ConnectionString);
        Event eventObj= new Event();
        // GET api/values
        public List<Event> Get()
        {
            SqlDataAdapter da = new SqlDataAdapter("[dbo].[GetEventslIST]", con);
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

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
