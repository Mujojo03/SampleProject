using Microsoft.AspNetCore.Mvc;
using SampleProject.IServices;
using SampleProject.Models;

namespace SampleProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserServices _userServices;

        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _userServices.GetAllUsers();
            return View(users);
        }

        [HttpGet("{id}")] //to get user by id
        public IActionResult GetUserById(int id)
        {
            var users = _userServices.GetUserById(id);
            if (users == null)
            {
                return NotFound();
            }
            return Ok(users);
        }

        [HttpGet("get_user_by_email")] //to get user by email
        public IActionResult GetUserByEmail(string email)
        {
            var users = _userServices.GetUserByEmail(email);
            if (users == null)
            {
                return NotFound();
            }
            return Ok(users);
        }

        [HttpGet("get_user_by_password")] //to get user by password
        public IActionResult GetUserByPassword(string password)
        {
            var users = _userServices.GetUserByPassword(password);
            if (users == null)
            {
                return NotFound();
            }
            return Ok(users);
        }


        //to register a new user
        [HttpPost]
        public IActionResult AddUser(/*int id,*/ [FromBody] UserRegistration userRegistration)
        {
            var user = new User
            {
                Username = userRegistration.Username,
                Email = userRegistration.Email,
                Password = userRegistration.Password
            };
            _userServices.AddUser(user);
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, "Account Has been Successfully registerd");
        }

        //to update a user
        [HttpPut]
        public IActionResult UpdateUser(int id, [FromBody] User updatedUser, [FromQuery] string email, [FromQuery] string password)
        {
            var existingUser = _userServices.GetUserById(id);
            if (existingUser != null)
            {
                return BadRequest("Invalid Email or Password. Pelase Provide a Valid Email and Password");
            }
            updatedUser.Id = id;
            _userServices.UpdateUser(updatedUser);
            return Ok("Your Details Have Been Successfully Updated");
        }

        [HttpDelete]
        public IActionResult DeleteUser([FromBody] LoginCredentials credentials)
        {
            var user = _userServices.GetUserByEmail(credentials.Email);
            if (user == null || user.Password != credentials.Password)
            {
                return BadRequest("Invalid Email or Password. Please Provide a Valid Email or Password");
            }
            _userServices.DeleteUser(credentials.Email, credentials.Password);
            return Ok("Your Account Has Been Successfully Deleted!");
        }
    }
}
