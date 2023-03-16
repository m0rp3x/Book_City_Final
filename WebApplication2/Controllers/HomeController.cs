using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Xml;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        ApplicationContext db;


        public HomeController(ApplicationContext context)
        {
            db = context;
            
            var Reviews = db.Reviews.ToList();

            ViewBag.jopochka = Reviews;


            if (!db.Authors.Any())
            {
                Author author1 = new Author { Name = "Александр Пушкин" };
                Author author2 = new Author { Name = "Лев Толстой" };
                Author author3 = new Author { Name = "Федор Достоевский" };
                Author author6 = new Author { Name = "Джордж Оруэлл" };
                Reviews reviews1 = new Reviews { Name_rew = "4040", rew = "123", BookID = 1 };
                Book book1 = new Book
                {
                    Name = "Евгений Онегин", Year_public = 1833, Author = author1, Tag = "Роман", litres_id = 7613069,
                    image_link = "https://cv6.litres.ru/pub/c/cover_415/7613069.webp",
                    Description =
                        "Первое полное издание знаменитого романа в стихах великого русского поэта Александра Сергеевича Пушкина. Книга содержит иллюстрации из лучших русских и зарубежных изданий романа XIX – XX веков, проиллюстрирована картинами русских художников XIX века, портретами А. С. Пушкина работы художников XIX – XX веков, рисунками А. С. Пушкина. Приводятся фотографии исполнителей ролей в опере П. А. Чайковского «Евгений Онегин».",
                    Description_facts =
                        "Остроумная ироническая драма  любви и дуэли между Онегиным и Ленским за сердце прекрасной Ольги Лариной."
                };
                Book book2 = new Book
                {
                    Name = "Война и мир", Year_public = 1869, Author = author2, Tag = "Роман", litres_id = 66691848,
                    image_link = "https://cv4.litres.ru/pub/c/cover_415/66691848.webp",
                    Description =
                        "Роман «Война и мир», одно из величайших произведений русской и мировой литературы, создавался Л.Н. Толстым на протяжении шести лет, восемь раз переписывался, а отдельные эпизоды – более двадцати раз. Исследователи насчитывают пятнадцать вариантов одного только начала романа. В данной книге использована вторая редакция «Войны и мира» (1873 год), наиболее полная и удобная для чтения, поскольку Толстой перевёл на русский весь французский текст романа.Книга снабжена большим количеством иллюстраций, показывающих прототипов главных героев, исторических персонажей, а также хронику нашествия Наполеона на Россию. Развернутые комментарии к ним дал российский литературовед, доктор филологических наук Борис Соколов. Из этих комментариев можно узнать много интересных и неожиданных подробностей об исторической канве «Войны и мира».",
                    Description_facts =
                        "Величественная эпопея, повествующая о судьбах русской знати в эпоху войн Наполеона с Россией. В романе смело переплетаются исторические факты и личные судьбы героев, создавая поразительную картины жизни того времени. В произведении содержится не только глубокое изучение русской жизни и общественных процессов того времени, но и философские размышления автора о смысле человеческой жизни и судьбе народа."
                };
                Book book3 = new Book
                {
                    Name = "Руслан и Людмила", Year_public = 1820, Author = author1, Tag = "Роман",
                    litres_id = 64499757, image_link = "https://cv5.litres.ru/pub/c/cover_415/64499757.webp",
                    Description =
                        "«Руслан и Людмила» – чудесная, нестареющая в веках поэма А. С. Пушкина, написанная в духе и по мотивам старинных русских сказок и былин. Современники Пушкина с восторгом приняли эту волшебную сказку в стихах. И с тех пор каждое новое поколение переживает за красавицу Людмилу, с замиранием сердца следит за приключениями князя Руслана и желает ему победы над злобным карликом Черномором.",
                    Description_facts =
                        "Киевский князь Владимир выдаёт свою дочь Людмилу за храброго богатыря Руслана, но в первую брачную ночь невесту похищает старый колдун Черномор — карлик с невероятно длинной бородой. Князь отменяет свадьбу и объявляет, что мужем Людмилы станет тот, кто её спасёт."
                };
                Book book4 = new Book
                {
                    Name = "Анна Каренина", Year_public = 1877, Author = author2, Tag = "Роман", litres_id = 172100,
                    image_link = "https://cv5.litres.ru/pub/c/cover_415/28057851.webp",
                    Description =
                        "Роман «Анна Каренина» был написан Л. Н. Толстым в 1877 году, а год спустя впервые выпущен отдельным книжным изданием. С тех пор минуло 140 лет. «Анна Каренина» выдержала более 30 экранизаций, бесчисленное количество театральных постановок и переизданий. Но, несмотря на это, постоянно появляются все новые и новые интерпретации этого бессмертного произведения. Ведь темы, затронутые в романе, не теряют и, уверены, не потеряют своей актуальности никогда. Мы ,в свою очередь, рады представить вам аудиоверсию романа «Анна Каренина» в классическом, профессиональном прочтении народного артиста РФ, ведущего актера Малого театра – Александра Владимировича Клюквина.",
                    Description_facts =
                        "Все счастливые семьи похожи друг на друга, каждая несчастливая семья несчастлива по-своему"
                };
                Book book5 = new Book
                {
                    Name = "Преступление и наказание", Year_public = 1866, Author = author3, Tag = "Роман",
                    litres_id = 54440594, image_link = "https://cv9.litres.ru/pub/c/cover_415/49626890.webp",
                    Description =
                        "«Преступление и наказание» – одно из самых значительных произведений в истории мировой литературы. Роман Федора Михайловича Достоевского ставит перед читателем важнейшие нравственно-мировоззренческие вопросы – о вере, совести, грехе и об искуплении через страдание. Задуманный как «психологический отчет одного преступления», роман Достоевского предстал перед читателем грандиозным художественно-философским исследованием человеческой природы, христианской трагедией о смерти и воскресении души.В новом коллекционном издании раскрыта религиозная, социальная и философская символика бессмертного произведения. Рассказано о биографии Федора Михайловича, об истории создания всемирного шедевра. Даны достоверные шокирующие факты из сыскных дел дореволюционной России Интереснейшие события созданного Достоевским философского полифонического романа здесь переплетены с реальной жизнью людей и перенесены на улицы красивейшего города планеты. В книге собраны редкие виды Санкт-Петербурга, сделанные в самом начале появления фотографии в России, во второй половине XIX века. Не менее интересны и документальные свидетельства того времени.",
                    Description_facts =
                        "За преступлением неизменно последует наказание, причем человек порой казнит себя строже, чем закон. "
                };


                Book book9 = new Book
                {
                    Name = "1984", Year_public = 1890, Author = author6, Tag = "Документалистика", litres_id = 129098,
                    image_link = "https://cv5.litres.ru/pub/c/cover_415/63576652.webp",
                    Description =
                        "Одна из самых знаменитых антиутопий XX века – роман «1984» английского писателя Джорджа Оруэлла (1903–1950) был написан в 1948 году и продолжает тему «преданной революции», раскрытую в «Скотном дворе». По Оруэллу, нет и не может быть ничего ужаснее тотальной несвободы. Тоталитаризм уничтожает в человеке все духовные потребности, мысли, чувства и сам разум, оставляя лишь постоянный страх и единственный выбор – между молчанием и смертью, и если Старший Брат смотрит на тебя и заявляет, что «дважды два – пять», значит, так и есть.",
                    Description_facts =
                        "Власть – не средство; она – цель. Диктатуру учреждают не для того, чтобы охранять революцию; революцию совершают для того, чтобы установить диктатуру. Цель репрессий – репрессии. Цель пытки – пытка. Цель власти – власть."
                };



                db.Authors.AddRange(author1, author2, author3, author6);
                db.Books.AddRange(book1, book2, book3, book4, book5, book9, book5);

                db.SaveChanges();
            }
        }

        public async Task<IActionResult> Index(SortState sortOrder = SortState.NameAsc)
        {
            IQueryable<Book> books = db.Books.Include(x => x.Author);
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
        [HttpPost]
        

        public IActionResult Create()
        {
            var Orders = db.Orders.ToList();
            ViewBag.data = Orders;
            var Books = db.Books.ToList();
            ViewBag.jopa = Books;

            return View();
        }

        [HttpPost]
        public Task<IActionResult> Create(Book book)
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
            if (id != null)

            {
                Book booke = await db.Books.FirstOrDefaultAsync(p => p.ID == id);
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
            var Orders = db.Orders.ToList();
            ViewBag.data = Orders;
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
            var Orders = db.Orders.ToList();
            ViewBag.data = Orders.FirstOrDefault(p => p.OrderID == id);
            if (id != null)

                if (id != null)
                {
                    Order booksBook = await db.Orders.FirstOrDefaultAsync(p => p.OrderID == id);
                    if (booksBook != null)
                    {
                        db.Orders.Remove(booksBook);
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
                Book user = db.Books.Include(b => b.Author)
                    .Include(b => b.Reviews)
                    .FirstOrDefault(m => m.ID == id);

                if (user != null)
                {
                    // Вычисляем средний рейтинг книги
                    double ratingSum = 0;
                    foreach (var review in user.Reviews)
                    {
                        ratingSum += review.Book.Rating;
                    }
                    double ratingAverage = user.Reviews.Count > 0 ? ratingSum / user.Reviews.Count : 0;

                    ViewData["RatingAverage"] = ratingAverage;

                    return View(user);
                }
            }

            return NotFound();
        }



        public ActionResult rss()
        {
            var reader = XmlReader.Create("https://rss.nytimes.com/services/xml/rss/nyt/Books.xml");
            var feed = SyndicationFeed.Load(reader);
            reader.Close();

            var model = (from item in feed.Items
                select new FeedItem
                {
                    Title = item.Title.Text,
                    Link = item.Id,
                    description = item.Summary.Text,
                    date = item.PublishDate.ToString()
                }).Take(5);


            ViewBag.data_govno = model.ToList();


            return View();
        }

        public class FeedItem
        {
            public string Title { get; set; }
            public string Link { get; set; }
            public string description { get; set; }
            public string date { get; set; }
        }

        public async Task<IActionResult> Reviews(int? id)
        {
            if (id != null)
            {
                await db.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Books ON");

                Reviews reviews = new Reviews();
                reviews.Book = await db.Books.FirstOrDefaultAsync(p => p.ID == id);
                if (reviews != null)
                {
                    return View(reviews);
                }
            }

            return NotFound();
        }

        [HttpPost]
          public  async Task<IActionResult> Reviews(Reviews reviews)
            {
                db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Books ON");
                
                db.Set<Book>().AsNoTracking();
                db.Entry(reviews).State = EntityState.Modified;

                await db.AddAsync(reviews);
                await db.SaveChangesAsync();
                

                return await Task.FromResult<IActionResult>(RedirectToAction("Index"));
            }


        }
        
    }
