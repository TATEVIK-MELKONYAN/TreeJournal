using Microsoft.AspNetCore.Mvc;

namespace TreeJournalApi.Controllers
{
    [Route("api.user.partner")]
    [ApiController]
    public class ParentController : ControllerBase
    {

        public ParentController()
        {
        }

        [HttpPost("rememberMe")]
        public IActionResult RememberMe([FromBody] RememberMeRequest request)
        {
            try
            {
                if (request == null || string.IsNullOrEmpty(request.UserName))
                {
                    return BadRequest(new { message = "Invalid request" });
                }

                return Ok(new { message = $"Received request from {request.UserName ?? string.Empty}" });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    type = "Exception",
                    id = DateTime.Now.Ticks,
                    data = new { message = $"Internal server error ID = {DateTime.Now.Ticks}" }
                });
            }
        }
    }

    public class RememberMeRequest
    {
        public string? UserName { get; set; } 
    }
}
