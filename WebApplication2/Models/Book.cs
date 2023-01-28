using Microsoft.EntityFrameworkCore;
namespace WebApplication2.Models;

public class Book
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public int Year_public { get; set; }
    
    public int AuthorId { get; set; }
    
    public string Tag { get; set; }
    public  Author Author { get; set; } 
    
    
}

public class Author
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public List<Book> Books { get; set; }

    public Author()
    {
        Books = new List<Book>();
    }
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