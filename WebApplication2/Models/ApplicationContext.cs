using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Storage;

namespace WebApplication2.Models
{

    public sealed class ApplicationContext : DbContext
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DbSet<Book> Books { get; set; } = null!;

        public DbSet<Account> Accounts { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        
        public DbSet<Author> Authors { get; set; } = null!;
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DbSet<Reviews> Reviews { get; set; } = null!;
 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Reviews)
                .WithOne(r => r.Book)
                .HasForeignKey<Reviews>(r => r.BookID);
        }



        //OnModelCreating

     


        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();    
            
           
            

        }
        
    }
}