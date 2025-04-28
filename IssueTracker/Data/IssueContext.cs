using IssueTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace IssueTracker.Data
{
    public class IssueContext : DbContext
    {
        public IssueContext(DbContextOptions<IssueContext> options) : base(options) { }
        public DbSet<IssueModel> Issues { get; set; }
        public DbSet<ProjectModel> Projects { get; set; }

        public string DbPath { get; }


/*        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite($"Data Source={DbPath}");*/
    }
}
