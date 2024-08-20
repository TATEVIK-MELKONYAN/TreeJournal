using Microsoft.EntityFrameworkCore;
using TreeJournalApi.Data;
using TreeJournalApi.Models;
using TreeJournalApi.Repositories.Interfaces;

namespace TreeJournalApi.Repositories
{
    public class TreeNodeRepository : IRepository<TreeNode>
    {
        private readonly AppDbContext _context;

        public TreeNodeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TreeNode> GetByIdAsync(int id)
        {
            var node = await _context.TreeNodes
                             .Include(n => n.Children)
                             .SingleOrDefaultAsync(n => n.Id == id);

            return node ?? throw new InvalidOperationException($"No TreeNode found with id {id}");
        }

        public async Task<IEnumerable<TreeNode>> GetAllAsync()
        {
            return await _context.TreeNodes
                .Include(n => n.Children)
                .ToListAsync();
        }

        public async Task AddAsync(TreeNode entity)
        {
            _context.TreeNodes.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TreeNode entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _context.TreeNodes.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}