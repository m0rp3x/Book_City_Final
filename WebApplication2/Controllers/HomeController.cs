using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2;
using WebApplication2.Models;
 
namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        ApplicationContext db;
        public HomeController(ApplicationContext context)
        {
            db = context;
        }   
         public async Task<IActionResult> Index()
                {
                    return View(await db.Books.ToListAsync());
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
    }
}