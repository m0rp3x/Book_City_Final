using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using WebApplication2.Support;
using WebApplication2.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string? connection = builder.Configuration.GetConnectionString("WebApplication1Context");
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie(options =>
{
options.LoginPath = new PathString("/Account/Login");;
});

builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();

builder.Services.AddAuthorization(options =>
{
options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("admin"));
});



var app = builder.Build();

app.Use(async (context, next) =>
{
    await next();

    if (context.Response.StatusCode == 403)
    {
        context.Request.Path = "/Home/AccessDenied";
        await next();
    }
});

app.MapHub<ChatHub>("/chat");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
app.UseExceptionHandler("/Home/Error");


app.UseHsts();

}

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.UseEndpoints(endpoints =>
{
endpoints.MapControllerRoute(
name: "default",
pattern: "{controller=Home}/{action=Index}/{id?}");


endpoints.Map("/Create", [Authorize(Roles = "admin")]() => "Admin Panel");

});

app.Run();