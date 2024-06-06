using Microsoft.AspNetCore.Mvc;
using SampleProject.IServices;
using SampleProject.Models;

namespace SampleProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuestionsController : Controller
    {
        private readonly IUserDataService _userDataService;

        public QuestionsController(IUserDataService userDataService)
        {
            _userDataService = userDataService;
        }

        [HttpGet("get_question")]
        public IActionResult GetQuestion([FromQuery] string userId)
        {
            //string usrId = User.Identity.Name;

            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest(new {message = "UserId is Required" });
            }

            _userDataService.InitializaUser(userId);

            var question = _userDataService.GetNextQuestion(userId);

            if(question.QuestionText == "All Questions Answered")
            {
                return Ok(new {message = question.QuestionText});
            }
            return Ok(question);
        }

        [HttpPost("submit_response")]
        public IActionResult SubmitResponse([FromBody] QuestionResponse response)
        {
            //string userId = User.Identity.Name;

            if(string.IsNullOrEmpty(response.UserId) || string.IsNullOrEmpty(response.Response))
            {
                return BadRequest(new { message = "UserId and Response are Required" });
            }

            if(!_userDataService.UserExists(response.UserId))
            {
                return BadRequest(new { message = "Invalid Id, The Id Provided Doest Not Exist. Please Provide a Valid Id" });
            }

            try
            {
                _userDataService.SaveResponse(response.UserId, response.Response);
            }
            catch (FormatException ex)
            {
                return BadRequest(new {message = ex.Message});
            }

            _userDataService.IncrementQuestionIndex(response.UserId);

            return Ok(new {message = "Response Recorded", nextQuestion = _userDataService.GetNextQuestion(response.UserId).QuestionText});
        }

        [HttpGet("get_user_data")]
        public IActionResult GetUserData([FromQuery] string userId)
        {
            if(string.IsNullOrEmpty(userId))
            {
                return BadRequest(new { message = "UserId is Required" });
            }

            var userData = _userDataService.GetUserData(userId);

            if(userData == null)
            {
                return NotFound(new {message = "User Not Found"});
            }

            return Ok(userData);
        }
    }
}
