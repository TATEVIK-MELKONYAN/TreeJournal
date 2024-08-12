using TreeJournalApi.Models;

public class ExceptionJournalService : IExceptionJournalService
{
    private readonly IRepository<ExceptionJournal> _repository;

    public ExceptionJournalService(IRepository<ExceptionJournal> repository)
    {
        _repository = repository;
    }

    public async Task<ExceptionJournal> GetByIdAsync(long id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<ExceptionJournal>> GetRangeAsync(ExceptionJournalFilter filter)
    {
        var query = _repository.GetAllAsync().Result.AsQueryable();

        if (filter.From.HasValue)
        {
            query = query.Where(e => e.CreatedAt >= filter.From.Value);
        }

        if (filter.To.HasValue)
        {
            query = query.Where(e => e.CreatedAt <= filter.To.Value);
        }

        if (!string.IsNullOrEmpty(filter.Search))
        {
            query = query.Where(e => e.StackTrace.Contains(filter.Search));
        }

        return await Task.FromResult(query.ToList());
    }

    public async Task LogExceptionAsync(ExceptionJournal journalEntry)
    {
        await _repository.AddAsync(journalEntry);
    }
}
