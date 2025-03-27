using Microsoft.EntityFrameworkCore;
using MyBookshelf.Models;

namespace MyBookshelf
{
    public class AppDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Book>()
                        .Ignore(u => u.FormatAvailable);
        }
    }
}
