using EventManagementSystemApi.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EventManagementSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManager _userManager;
        public UserController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        // GET: api/<UserController>
        [HttpGet("GetAllUser")]
        [Authorize]
        public List<Object> GetAllUser()
        {
            return _userManager.GetUsers();

        }


        // POST api/<UserController>
        [HttpPost("Register")]
        public string Register([FromQuery] string name, string email, string password)
        {
            return _userManager.RegisterUser(name, email, password);
        }

        // PUT api/<UserController>/5
        [HttpPost("LogIn")]
        public string Put([FromQuery] string email, string password)
        {
            return _userManager.AuthenticateUser(email, password);
        }


    }
}
