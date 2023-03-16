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
    
    public string Description { get; set; }
    
    public string Description_facts { get; set; }
    
    public int AuthorID { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

    
    public string? Tag { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

    public  Author Author { get; set; } = null!;
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]


    public Order  Order { get;set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public List<Reviews> Reviews { get; set; }

    public double Rating => Reviews?.Count > 0 ? Math.Round(Reviews.Average(r => r.Stars),1) : 0;
        
        
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

[PrimaryKey("RewID")]
public class Reviews
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

    public int RewID { get; set; }
    public string Name_rew { get; set; }
    public string rew { get; set; }
    [ForeignKey("ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int BookID { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

    public virtual Book Book { get; set; }
    public int Stars { get; set; }
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

