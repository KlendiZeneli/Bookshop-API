using BookNookAPI.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BookNookAPI.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Book> Books { get; set; }
    }
}
