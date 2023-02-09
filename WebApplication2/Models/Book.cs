using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace WebApplication2.Models;
[PrimaryKey("ID")]
public class Book
{
   
    
    [ForeignKey("OrderID")]
    [Key]
    [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
    public int? ID { get; set; }
    
    public int litres_id { get; set; }
    
    public string image_link { get; set; }

    public string Name { get; set; }
   

    public int Year_public { get; set; }
    

    
    public int AuthorID { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

    
    public string? Tag { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

    public  Author Author { get; set; } = null!;
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]


    public Order  Order { get;set; }


}


[PrimaryKey("AuthorID")]
public class Author
{

    public int AuthorID { get; set; }

    public string Name { get; set; }
}

[PrimaryKey("OrderID")]
public class Order {
    public int OrderID { get; set; }
    [ForeignKey("ID")]
    public int BookID { get; set; }
    public Book? Book { get; set; }
    public string CitySent { get; set; }
    public string CityReceived { get; set; }
    public string AddressSent { get; set; }
    public string AddressReceived { get; set; }
    public string weight { get; set; }
    
    
}


public class Account
{

    public int ID { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    


}

public enum SortState
{
    NameAsc,
    NameDesc,
    YearAsc,
    YearDesc,
    AuthorAsc,
    AuthorDesc,
    TagAsc,
    TagDesc
}

