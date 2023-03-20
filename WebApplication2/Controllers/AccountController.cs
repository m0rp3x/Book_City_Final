using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplication2.Models; 
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace WebApplication2.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationContext db;
        public AccountController(ApplicationContext context)
        {
            db = context;
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<RedirectToActionResult> Login(string Email, string Password)

        {
            if (ModelState.IsValid)
            {
                {
                    Account? user = await db.Accounts.FirstOrDefaultAsync(u => u.Email == Email && u.Password == Password);
                    if (user != null)
                    {
                        await Authenticate(Email); // аутентификация
 
                        return RedirectToAction("Index", "Home");
                    }
                }

                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            //return tag sort account
            
            
            
            return RedirectToAction("Index", "Home");
            
            //return redirect sort tag account
            
                 
            
        




                
        }
        [HttpGet]
        public IActionResult Register()
        {
            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(string Email, string Password)
        {
            {
                Account? user = await db.Accounts.FirstOrDefaultAsync(u => u.Email == Email);
                if (user == null)
                {
                   
                    Account UE =  new Account { Email = Email, Password = Password, Role = await db.Roles.FirstOrDefaultAsync(r => r.Name == "user")};
                    db.Accounts.Add(UE);
                    await db.SaveChangesAsync();
 
                    await Authenticate(Email); // аутентификация
 
                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View();
        }
 
        private async Task Authenticate(string userName)
        {
            
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)

                
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
 
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}