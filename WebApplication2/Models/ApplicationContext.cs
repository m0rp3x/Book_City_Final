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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]


        public DbSet<Account> Accounts { get; set; } = null!;
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public DbSet<Order> Orders { get; set; } = null!;
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public DbSet<Author> Authors { get; set; } = null!;
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DbSet<Reviews> Reviews { get; set; } = null!;
        public DbSet<Role?> Roles { get; set; } = null!;


 

        

        //OnModelCreating

     


        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();    
            
           
            

        }
        
    }
}