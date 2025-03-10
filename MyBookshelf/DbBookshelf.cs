using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MyBookshelf
{
    public class DbBookshelf : IdentityDbContext
    {
        public DbBookshelf(DbContextOptions<DbBookshelf> options) : base(options)
        {

        }

        //public DbSet<Book> Books { get; set; } // tworzenie tabeli o nazwie Books

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
