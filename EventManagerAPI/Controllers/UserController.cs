using EventManagerAPI.Data;
using EventManagerAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http; 

namespace EventManagerAPI.Controllers
{
    [RoutePrefix("api/users")]
    public class UserController : ApiController
    {
        private readonly UserRepository _userRepository;

        public UserController()
        {
            _userRepository = new UserRepository(ConfigurationManager.ConnectionStrings["EventManagerAPI"].ConnectionString);
        }
        [HttpPost]
        [System.Web.Http.Route("GetUserByGmail")]
        public IHttpActionResult GetUserByGmail([FromBody] GetUserByGmailRequest request)
        {
            var user = _userRepository.GetUserByGmail(request.Gmail);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }




        [HttpPost]
        [System.Web.Http.Route("AddUser")]
        public IHttpActionResult AddUser([FromBody] User newUser)
        {
            var success = _userRepository.AddUser(newUser);

            if (success)
            {
                return Ok("User Added successfully.");
            }

            return BadRequest("Failed to added the user.");
        }
    }
}