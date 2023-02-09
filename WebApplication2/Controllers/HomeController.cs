using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2;
using WebApplication2.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore.Storage;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        ApplicationContext db;
        
        
        
        public HomeController(ApplicationContext context)
        {
            db = context;
            
            
            
            
          
           

            if (!db.Authors.Any())
            {
                Author author1 = new Author { Name = "Александр Пушкин" };
                Author author2 = new Author { Name = "Лев Толстой" };
                Author author3 = new Author { Name = "Федор Достоевский" };
                Author author6 = new Author { Name = "Джордж Оруэлл" };
                Book book1 = new Book { Name = "Евгений Онегин", Year_public = 1833, Author = author1, Tag = "Роман", litres_id =7613069 , image_link = "https://cv6.litres.ru/pub/c/cover_415/7613069.webp"};
                Book book2 = new Book { Name = "Война и мир", Year_public = 1869, Author = author2, Tag = "Роман", litres_id  = 66691848, image_link = "https://cv4.litres.ru/pub/c/cover_415/66691848.webp"};
                Book book3 = new Book
                    { Name = "Руслан и Людмила", Year_public = 1820, Author = author1, Tag = "Роман", litres_id = 64499757, image_link = "https://cv5.litres.ru/pub/c/cover_415/64499757.webp"};
                Book book4 = new Book { Name = "Анна Каренина", Year_public = 1877, Author = author2, Tag = "Роман", litres_id = 172100, image_link = "https://cv5.litres.ru/pub/c/cover_415/28057851.webp"};
                Book book5 = new Book
                    { Name = "Преступление и наказание", Year_public = 1866, Author = author3, Tag = "Роман", litres_id =54440594, image_link = "https://cv9.litres.ru/pub/c/cover_415/49626890.webp"};
             

                Book book9 = new Book { Name = "1984", Year_public = 1890, Author = author6, Tag = "Документалистика", litres_id = 129098, image_link = "https://cv5.litres.ru/pub/c/cover_415/63576652.webp"};

                




                db.Authors.AddRange(author1,author2,author3);
                db.Books.AddRange(book1,book2,book3,book4,book5);
                
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
                public  Task<IActionResult> Create(Book book)
                {
                     db.AddRange(book);
                     db.SaveChanges();
                    return Task.FromResult<IActionResult>(RedirectToAction("Index"));
                    
                }
                
                
                [HttpGet]
                [ActionName("Delete")]
                public async Task<IActionResult> ConfirmDelete(int? id)
                {
                    if (id != null)
                    {
                        Book userBook = await db.Books.FirstOrDefaultAsync(p => p.ID == id);
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
                        Book booksBook = await db.Books.FirstOrDefaultAsync(p => p.ID == id);
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

                        Book booke = await db.Books.FirstOrDefaultAsync(p=>p.ID==id);
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

                public IActionResult Support()
                {
                    return View();
                } 
                public async Task<IActionResult> Order(int? id)
                {
                    if (id != null)
                    {
                        await db.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Books ON");

                            Order order = new Order();
                            order.Book = await db.Books.FirstOrDefaultAsync(p => p.ID == id);
                            if (order != null)
                                return View(order);
                    }
                    
                    

                return View();

                }
                
                

                
                [HttpPost]
                public async Task<IActionResult> Order(Order order)
                {
                    //Save AsNoTracking

                    
                    db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Books ON");
                    db.Set<Book>().AsNoTracking();
                    db.Set<Order>().AsNoTracking();
                    db.Entry(order).State = EntityState.Modified;
                    await db.AddAsync(order);
                    await db.SaveChangesAsync();
                    
                    
                    return await Task.FromResult<IActionResult>(RedirectToAction("Index"));
                    
                    
                }
                public async Task<IActionResult> Details(int? id)
                {
                    if (id != null)
                    {
                        Order user = await db.Orders.FirstOrDefaultAsync(p => p.OrderID == id);
                        if (user != null)
                            return View(user);
                    }
                    return NotFound();
                    return NotFound();
                }
                public async Task<IActionResult> OrderList()
                {
                    IQueryable<Order> orders = db.Orders.Include(x => x.Book);
                    return View(await orders.AsNoTracking().ToListAsync());
                }
                
                [HttpGet]
                [ActionName("DeleteOrder")]
                public async Task<IActionResult> ConfirmDeleteOrder(int? id)
                {
                    if (id != null)
                    {
                        Order userBook = await db.Orders.FirstOrDefaultAsync(p => p.OrderID == id);
                        if (userBook != null)
                            return View(userBook);
                    }
                    return NotFound();
                }
                
                
                [HttpPost]
                public async Task<IActionResult> DeleteOrder(int? id)
                {
                    if (id != null)
                    {
                        Order booksBook = await db.Orders.FirstOrDefaultAsync(p => p.OrderID == id);
                        if (booksBook != null)
                        {
                            db.Orders.Remove(booksBook );
                            await db.SaveChangesAsync();
                           
                            return RedirectToAction("Index");
                           
                        }
                    }
                    return NotFound();
                }
                public async Task<IActionResult> BookDetail(int? id)
                {
                    if (id != null)
                    {
                        Book user = await db.Books.FirstOrDefaultAsync(p => p.ID == id);
                        if (user != null)
                            return View(user);
                    }
                    return NotFound();
                    return NotFound();
                }
    }
}