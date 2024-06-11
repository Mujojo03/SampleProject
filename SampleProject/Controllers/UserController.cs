using Microsoft.AspNetCore.Mvc;
using SampleProject.IServices;
using SampleProject.Models;

namespace SampleProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserServices _userServices; //global variable

        public UserController(IUserServices userServices) //constructor
        {
            _userServices = userServices;
        }

        [HttpGet] //should return 'Úser successfully registerd'
        public IActionResult GetAllUsers([FromQuery] string email, [FromQuery] string password)
        {
            var users = _userServices.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("{id}")] //to get user by id
        public IActionResult GetUserById(int id)
        {
            var users = _userServices.GetUserById(id);
            if (users == null)
            {
                return NotFound("User Not Found.");
            }
            return Ok(users);
        }

        [HttpGet("get_user")]
        public IActionResult GetUser([FromQuery] string email = null, [FromQuery] string password = null)
        {
            if(string.IsNullOrEmpty(email) && string.IsNullOrEmpty(password))
            {
                return BadRequest("Email or Password Must be Provided");
            }
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
                var userByEmail = _userServices.GetUserByEmail(email);
                var userByPassword = _userServices.GetUserByPassword(password);

                if (userByEmail != null && userByPassword != null)
                {
                    return NotFound("User Not Found With The Provided Email or Password");
                }

                if (userByEmail != null && userByPassword != null && userByEmail.Id != userByPassword.Id)
                {
                    return NotFound("Email and Password do Not Match");
                }
                return Ok(userByEmail ?? userByPassword);
            }
            else if (!string.IsNullOrEmpty(email))
            {
                var user = _userServices.GetUserByEmail(email);
                if (user == null)
                {
                    return NotFound("User Not Found With The Provided");
                }
                return Ok(user);
            }
            else
            {
                var user = _userServices.GetUserByPassword(password);
                if (user == null)
                {
                    return NotFound("User Not Found With The Provided Password");
                }
                return Ok(user);
            }
            
        }

        //[HttpGet("get_user_by_email")] //to get user by email
        //public IActionResult GetUserByEmail([FromQuery]string email)
        //{
        //    var users = _userServices.GetUserByEmail(email);
        //    if (users == null)
        //    {
        //        return NotFound("User Not Found.");
        //    }
        //    return Ok(users);
        //}

        //[HttpGet("get_user_by_password")] //to get user by password
        //public IActionResult GetUserByPassword(string password)
        //{
        //    var users = _userServices.GetUserByPassword(password);
        //    if (users == null)
        //    {
        //        return NotFound("User Not Found.");
        //    }
        //    return Ok(users);
        //}


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
        public IActionResult DeleteUser([FromBody] LoginCredentials credentials, [FromQuery] string email, [FromQuery] string password)
        {
            var user = _userServices.GetUserByEmail(credentials.Email);
            if (user == null || user.Password != credentials.Password)
            {
                return BadRequest("Invalid Email or Password. Please Provide a Valid Email or Password");
            }
            //_userServices.DeleteUser(credentials.Email, credentials.Password);
            _userServices.DeleteUser(user);
            return Ok("Your Account Has Been Successfully Deleted!");
        }
    }
}
