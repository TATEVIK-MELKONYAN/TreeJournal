using Microsoft.EntityFrameworkCore;
using TreeJournalApi.Data;
using TreeJournalApi.Models;

public class ExceptionJournalRepository : IRepository<ExceptionJournal>
{
    private readonly AppDbContext _context;

    public ExceptionJournalRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ExceptionJournal> GetByIdAsync(long id)
    {
        return await _context.ExceptionJournals.FindAsync(id);
    }

    public async Task<IEnumerable<ExceptionJournal>> GetAllAsync()
    {
        return await _context.ExceptionJournals.ToListAsync();
    }

    public async Task AddAsync(ExceptionJournal entity)
    {
        _context.ExceptionJournals.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(ExceptionJournal entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            _context.ExceptionJournals.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
