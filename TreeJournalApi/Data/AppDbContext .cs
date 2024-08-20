using Microsoft.EntityFrameworkCore;
using TreeJournalApi.Models;

namespace TreeJournalApi.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Tree> Trees { get; set; }
        public DbSet<TreeNode> TreeNodes { get; set; }
        public DbSet<ExceptionJournal> ExceptionJournals { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TreeNode>()
                .HasOne(node => node.Parent)
                .WithMany(parent => parent.Children)
                .HasForeignKey(node => node.ParentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TreeNode>()
            .HasOne(n => n.Tree)
            .WithMany(t => t.Nodes)
            .HasForeignKey(n => n.TreeId);

            base.OnModelCreating(modelBuilder);
        }

    }
}
