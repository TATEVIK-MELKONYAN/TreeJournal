﻿using TreeJournalApi.Data;
using TreeJournalApi.Models;

namespace TreeJournalApi.Middleware
{
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
            var stackTrace = exception.StackTrace ?? string.Empty;

            var journalEntry = new ExceptionJournal
            {
                EventId = long.Parse(eventId),
                CreatedAt = DateTime.UtcNow,
                StackTrace = stackTrace,
                QueryParameters = context.Request.QueryString.Value ?? string.Empty,
                BodyParameters = await new StreamReader(context.Request.Body).ReadToEndAsync()
            };

            var dbContext = context.RequestServices.GetRequiredService<AppDbContext>();
            dbContext.ExceptionJournals.Add(journalEntry);
            await dbContext.SaveChangesAsync();

            context.Response.ContentType = "application/json";
            var response = new
            {
                type = exception is SecureException ? "Secure" : "Exception",
                id = eventId,
                data = new
                {
                    message = exception is SecureException ? exception.Message
                                    ?? string.Empty : $"Internal server error ID = {eventId}"
                }
            };

            var jsonResponse = System.Text.Json.JsonSerializer.Serialize(response);
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsync(jsonResponse);
        }
    }
}