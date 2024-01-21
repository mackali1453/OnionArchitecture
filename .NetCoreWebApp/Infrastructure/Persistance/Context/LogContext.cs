using Github.NetCoreWebApp.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Github.NetCoreWebApp.Infrastructure.Persistance.Context
{
    public class LogContext: DbContext
    {
        public DbSet<LogEntry> LogEntries { get; set; }

        public LogContext(DbContextOptions<LogContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
