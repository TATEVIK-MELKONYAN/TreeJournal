using TreeJournalApi.Data;
using TreeJournalApi.Models;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var eventId = Guid.NewGuid().ToString();
        var stackTrace = exception.StackTrace;

        var journalEntry = new ExceptionJournal
        {
            EventId = long.Parse(eventId),
            CreatedAt = DateTime.UtcNow,
            StackTrace = stackTrace,
            QueryParameters = context.Request.QueryString.Value,
            BodyParameters = await new StreamReader(context.Request.Body).ReadToEndAsync()
        };

        var dbContext = context.RequestServices.GetRequiredService<AppDbContext>();
        dbContext.ExceptionJournals.Add(journalEntry);
        await dbContext.SaveChangesAsync();

        context.Response.ContentType = "application/json";
        if (exception is SecureException)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsync(new
            {
                type = "Secure",
                id = eventId,
                data = new { message = exception.Message }
            }.ToString());
        }
        else
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsync(new
            {
                type = "Exception",
                id = eventId,
                data = new { message = $"Internal server error ID = {eventId}" }
            }.ToString());
        }
    }
}
