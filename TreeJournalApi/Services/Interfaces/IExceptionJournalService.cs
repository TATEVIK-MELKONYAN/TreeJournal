using TreeJournalApi.Models;

public interface IExceptionJournalService
{
    Task<ExceptionJournal> GetByIdAsync(long id);
    Task<IEnumerable<ExceptionJournal>> GetRangeAsync(ExceptionJournalFilter filter);
    Task LogExceptionAsync(ExceptionJournal journalEntry);
}
