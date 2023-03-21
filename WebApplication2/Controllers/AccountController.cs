using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationContext db;

        public AccountController(ApplicationContext context)
        {
            db = context;
        }


        public async Task<IActionResult> AcessDenied()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<RedirectToActionResult> Login(string Email, string Password)
        {
            if (ModelState.IsValid)
            {
                var user = await db.Accounts
                    .Include(a => a.Role)
                    .FirstOrDefaultAsync(u => u.Email == Email && u.Password == Password);

                if (user != null)
                {
                    await Authenticate(user.Email, user.Role?.Name); // аутентификация

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
                }
            }

            return RedirectToAction("Index", "Home");
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
            Account? user = await db.Accounts.FirstOrDefaultAsync(u => u.Email == Email);
            if (user == null)
            {
                var defaultRole = await db.Roles.FirstOrDefaultAsync(r => r.Name == "user");
                if (defaultRole == null)
                {
                    throw new Exception("Default role not found");
                }

                var UE = new Account { Email = Email, Password = Password, Role = defaultRole };
                db.Accounts.Add(UE);
                await db.SaveChangesAsync();

                await Authenticate(Email, UE.Role?.Name); // аутентификация

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            return View();
        }


        private async Task Authenticate(string userName, string roleName)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, roleName)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
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

