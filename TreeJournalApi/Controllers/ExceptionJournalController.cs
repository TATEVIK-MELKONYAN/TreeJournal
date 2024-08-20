using Microsoft.AspNetCore.Mvc;
using TreeJournalApi.Models;
using TreeJournalApi.Services.Interfaces;

namespace TreeJournalApi.Controllers
{
    [ApiController]
    [Route("api.user.journal")]
    public class JournalController : ControllerBase
    {
        private readonly IExceptionJournalService _journalService;

        public JournalController(IExceptionJournalService journalService)
        {
            _journalService = journalService;
        }

        [HttpPost("getRange")]
        public async Task<IActionResult> GetRange([FromBody] VJournalFilter filter)
        {
            var result = await _journalService.GetRangeAsync(filter);
            return Ok(result);
        }

        [HttpPost("getSingle")]
        public async Task<IActionResult> GetSingle([FromBody] long id)
        {
            var journal = await _journalService.GetSingleAsync(id);
            return Ok(journal);
        }
    }
}