using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SampleProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        [HttpGet("getDoctors")]
        public async Task<ActionResult<List<string>>> GetDoctors()
        {
            return new List<string> { "Jane", "Alex" };
        }

    }
}
