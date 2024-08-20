using TreeJournalApi.Models;
using TreeJournalApi.Repositories.Interfaces;
using TreeJournalApi.Services.Interfaces;

namespace TreeJournalApi.Services
{
    public class ExceptionJournalService : IExceptionJournalService
    {
        private readonly IRepository<ExceptionJournal> _exceptionJournalRepository;

        public ExceptionJournalService(IRepository<ExceptionJournal> exceptionJournalRepository)
        {
            _exceptionJournalRepository = exceptionJournalRepository;
        }

        public async Task<MRange<MJournalInfo>> GetRangeAsync(VJournalFilter filter)
        {
            var query = await _exceptionJournalRepository.GetAllAsync();

            if (!string.IsNullOrEmpty(filter.From))
            {
                var fromDate = DateTime.Parse(filter.From);
                query = query.Where(e => e.CreatedAt >= fromDate);
            }

            if (!string.IsNullOrEmpty(filter.To))
            {
                var toDate = DateTime.Parse(filter.To);
                query = query.Where(e => e.CreatedAt <= toDate);
            }

            if (!string.IsNullOrEmpty(filter.Search))
            {
                query = query.Where(e => e.StackTrace.Contains(filter.Search));
            }

            var count = query.Count();
            var items = query.Skip(filter.Skip).Take(filter.Count).ToList();

            return new MRange<MJournalInfo>
            {
                Skip = filter.Skip,
                Count = count,
                Items = items.Select(e => new MJournalInfo
                {
                    Id = e.Id,
                    EventId = e.EventId,
                    CreatedAt = e.CreatedAt.ToString("o")
                }).ToList()
            };
        }

        public async Task<MJournal> GetSingleAsync(long id)
        {
            var journal = await _exceptionJournalRepository.GetByIdAsync((int)id);
            return journal == null
                ? throw new ArgumentException("Journal entry not found")
                : new MJournal
            {
                Id = journal.Id,
                EventId = journal.EventId,
                CreatedAt = journal.CreatedAt.ToString("o"),
                Text = journal.StackTrace
            };
        }

    }
}