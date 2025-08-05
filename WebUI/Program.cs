using DergiAPI.Persistence.Contexts;
using EDergiAPI.Domain.Entitites;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. DbContext
builder.Services.AddDbContext<EDergiAPIDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

// 2. Identity
builder.Services.AddIdentity<User, Role>(options =>
{
	options.Password.RequireDigit = true;
	options.Password.RequiredLength = 6;
	options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<EDergiAPIDbContext>()
.AddDefaultTokenProviders();

// 3. Cookie
builder.Services.ConfigureApplicationCookie(options =>
{
	options.LoginPath = "/Login/Account/Login";
	options.AccessDeniedPath = "/Login/Account/AccessDenied";
	options.Cookie.Name = "EDergiCookie";
	options.Cookie.HttpOnly = true;
	options.ExpireTimeSpan = TimeSpan.FromDays(1);
	options.SlidingExpiration = true;
});

// 4. MVC & Razor
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// 5. HttpContext
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// 6. Middleware
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// Area Routing

// Admin paneli için login sayfasý route'u 
app.MapControllerRoute(
	name: "adminloginDefault",
	pattern: "adminLoginDefault",  // admin yazýnca direkt login gelecek
	defaults: new { area = "Admin", controller = "Account", action = "Login" });

app.MapControllerRoute(
	name: "areas",
	pattern: "{area:exists}/{controller=Auth}/{action=Login}/{id?}");


app.MapControllerRoute(
	name: "home",
	pattern: "Home/{action=Index}/{id?}",
	defaults: new { controller = "Home" });

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Account}/{action=Login}/{id?}",
	defaults: new { area = "Academic" });

app.Run();
