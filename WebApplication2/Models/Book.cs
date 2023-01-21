using Microsoft.EntityFrameworkCore;
namespace WebApplication2.Models;

public class Book
{
    public int Id { get; set; }
    public string Name { get; set; } 
    public string Author { get; set; } 
}