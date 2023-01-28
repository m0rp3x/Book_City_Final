using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Models
{

    public sealed class ApplicationContext : DbContext
    {
        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<Author> Authors { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();

        }
    }
}