using EventManagerAPI.Data;
 using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Data.SqlClient;
using System.Configuration;
using EventManagerAPI.Models;

namespace EventManagerAPI.Controllers
{

    [RoutePrefix("api/events")]
    public class EventController : ApiController
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["EventManagerAPI"].ConnectionString);
        Event eventObj = new Event();

        private readonly EventRepository _EventRepository;

        public EventController()
        {
            _EventRepository = new EventRepository(ConfigurationManager.ConnectionStrings["EventManagerAPI"].ConnectionString);
        }
        [HttpGet]
        [Route("GetEvents")]
        public IHttpActionResult GetEvents()
        {
            return Ok(_EventRepository.GetEvents());

        }

        [HttpPost]
        [System.Web.Http.Route("GetEventsByGmail")]
        public IHttpActionResult GetEventsByGmail([FromBody] GetEventsByGmailRequest request)
        {
            var events = _EventRepository.GetEventsByGmail(request.GMAIL);
            if (events == null)
            {
                return NotFound();
            }

            return Ok(events);
        }

        [HttpGet]
        [System.Web.Http.Route("GetEventByEventCode/{EventCode}")]

        public IHttpActionResult GetEventByEventCode(string EventCode)
        {
            var events = _EventRepository.GetEventByEventCode(EventCode);
            if (events == null)
            {
                return NotFound();
            }

            return Ok(events);
        }

        [HttpPost]
        [System.Web.Http.Route("AddEvent")]
        public IHttpActionResult AddEvent([FromBody] Event newEvent)
        {
            var success = _EventRepository.AddEvent(newEvent);

            if (success)
            {
                return Ok("Event created successfully.");
            }

            return BadRequest("Failed to create the event.");
        }
    }
}