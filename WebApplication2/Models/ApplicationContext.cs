using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Models
{

    public sealed class ApplicationContext : DbContext
    {
        public DbSet<Book> Books { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            
            Database.EnsureCreated(); 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Name = "Метро 2033", Author = "Глуховский" }
            );
        }
    }
}