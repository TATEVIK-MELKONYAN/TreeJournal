﻿using Microsoft.EntityFrameworkCore;
using TreeJournalApi.Data;
using TreeJournalApi.Models;
using TreeJournalApi.Repositories.Interfaces;

namespace TreeJournalApi.Repositories
{
    public class ExceptionJournalRepository : IRepository<ExceptionJournal>
    {
        private readonly AppDbContext _context;

        public ExceptionJournalRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ExceptionJournal> GetByIdAsync(int id)
        {
            var journal = await _context.ExceptionJournals.FindAsync(id);
            return journal ?? throw new InvalidOperationException($"No ExceptionJournal found with id {id}");
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

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _context.ExceptionJournals.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}