using EventManagerAPI.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Data.SqlClient; 
using EventManagerAPI.Models;

namespace EventManagerAPI.Controllers
{

    [RoutePrefix("api/guests")]
    public class GuestsController : ApiController
    {



        private readonly GuestsRepository _GuestsRepository;

        public GuestsController()
        {
            _GuestsRepository = new GuestsRepository(ConfigurationManager.ConnectionStrings["EventManagerAPI"].ConnectionString);
        }
        [HttpPost]
        [System.Web.Http.Route("GetGuestsEventCode/{EventCode}")]
        public IHttpActionResult GetGuestsEventCode(string EventCode)
        {
            var Guests = _GuestsRepository.GetGuestsEventCode(EventCode);
            if (Guests == null)
            {
                return NotFound();
            }

            return Ok(Guests);
        }
        [HttpPost]
       [ System.Web.Http.Route("AddGuest")]
        public IHttpActionResult AddGuest([FromBody] Guest guest)
        {
            if (guest == null)
            {
                return BadRequest("Invalid guest data.");
            }

            var success = _GuestsRepository.AddGuest(guest);

            if (success)
            {
                return Ok("Guest added successfully.");
            }
            else
            {
                return BadRequest("Failed to add the guest.");
            }
        }
    }
}