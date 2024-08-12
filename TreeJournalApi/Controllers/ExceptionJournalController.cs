using Microsoft.AspNetCore.Mvc;
using TreeJournalApi.Models;

[ApiController]
[Route("api.user.journal")]
public class ExceptionJournalController : ControllerBase
{
    private readonly IExceptionJournalService _exceptionJournalService;

    public ExceptionJournalController(IExceptionJournalService exceptionJournalService)
    {
        _exceptionJournalService = exceptionJournalService;
    }

    [HttpPost("getRange")]
    public async Task<IActionResult> GetRange([FromBody] ExceptionJournalFilter filter)
    {
        var journals = await _exceptionJournalService.GetRangeAsync(filter);
        return Ok(journals);
    }

    [HttpGet("getSingle/{id}")]
    public async Task<IActionResult> GetSingle(long id)
    {
        var journal = await _exceptionJournalService.GetByIdAsync(id);
        if (journal == null)
        {
            return NotFound();
        }

        return Ok(journal);
    }
}
