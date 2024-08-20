using TreeJournalApi.Models;

namespace TreeJournalApi.Services.Interfaces
{
    public interface IExceptionJournalService
    {
        Task<MRange<MJournalInfo>> GetRangeAsync(VJournalFilter filter);
        Task<MJournal> GetSingleAsync(long id);
    }
}