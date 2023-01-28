using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2;
using WebApplication2.Models;
using System.Linq;
 
namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        ApplicationContext db;
        public HomeController(ApplicationContext context)
        {
            db = context;

            if (db.Authors.Count() == 0)
            {
                Author author1 = new Author { Name = "Александр Пушкин" };
                Author author2 = new Author { Name = "Лев Толстой" };
                Book book1 = new Book { Name = "Евгений Онегин", Year_public = 1833, Author = author1, Tag = "Роман" };
                Book book2 = new Book { Name = "Война и мир", Year_public = 1869, Author = author2, Tag = "Роман" };
                
                db.Authors.AddRange(author1,author2);
                db.Books.AddRange(book1,book2);
                db.SaveChanges();
            }
        }   
        public async Task<IActionResult> Index(SortState sortOrder = SortState.NameAsc)
                {
                    IQueryable<Book> books = db.Books.Include(x=>x.Author);
                    ViewData["NameSort"] = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
                    ViewData["YearSort"] = sortOrder == SortState.YearAsc ? SortState.YearDesc : SortState.YearAsc;
                    ViewData["AuthorSort"] = sortOrder == SortState.AuthorAsc ? SortState.AuthorDesc : SortState.AuthorAsc;
                    ViewData["TagSort"] = sortOrder == SortState.TagAsc ? SortState.TagDesc : SortState.TagAsc;
                    
books = sortOrder switch
                    {
                        SortState.NameDesc => books.OrderByDescending(s => s.Name),
                        SortState.YearAsc => books.OrderBy(s => s.Year_public),
                        SortState.YearDesc => books.OrderByDescending(s => s.Year_public),
                        SortState.AuthorAsc => books.OrderBy(s => s.Author.Name),
                        SortState.AuthorDesc => books.OrderByDescending(s => s.Author.Name),
                        SortState.TagAsc => books.OrderBy(s => s.Tag),
                        SortState.TagDesc => books.OrderByDescending(s => s.Tag),
                        _ => books.OrderBy(s => s.Name),
                    };
                    return View(await books.AsNoTracking().ToListAsync());
                }
                public IActionResult Create()
                {
                    return View();
                }
                [HttpPost]
                public async Task<IActionResult> Create(Book book)
                {

                    db.Books.Add(book);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                    
                }
                
                [HttpGet]
                [ActionName("Delete")]
                public async Task<IActionResult> ConfirmDelete(int? id)
                {
                    if (id != null)
                    {
                        Book userBook = await db.Books.FirstOrDefaultAsync(p => p.Id == id);
                        if (userBook != null)
                            return View(userBook);
                    }
                    return NotFound();
                }
                
                
                [HttpPost]
                public async Task<IActionResult> Delete(int? id)
                {
                    if (id != null)
                    {
                        Book booksBook = await db.Books.FirstOrDefaultAsync(p => p.Id == id);
                        if (booksBook != null)
                        {
                            db.Books.Remove(booksBook);
                            await db.SaveChangesAsync();
                            return RedirectToAction("Index");
                        }
                    }
                    return NotFound();
                }
                
                public async Task<IActionResult> Edit(int? id)
                {
                    if(id!=null)
                    {
                        Book booke = await db.Books.FirstOrDefaultAsync(p=>p.Id==id);
                        if (booke != null)
                            return View(booke);
                    }
                    return NotFound();
                }
                [HttpPost]
                public async Task<IActionResult> Edit(Book booke)
                {
                    db.Books.Update(booke);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }

                public IActionResult Privacy()
                {
                    return View();
                }
    }
}