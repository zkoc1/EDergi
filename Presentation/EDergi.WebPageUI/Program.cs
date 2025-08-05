using EDergi.Application.Abstractions;
using EDergi.Persistence.Concretes;
using EDergi.Persistence.Contexts;
using EDergi.Application.Abstractions.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EDergi.Domain.Entitites;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<EDergiDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
	options.Password.RequireDigit = true;
	options.Password.RequiredLength = 6;
	options.Password.RequireNonAlphanumeric = false;
	options.Password.RequireUppercase = false;
	options.Password.RequireLowercase = false;
	options.User.RequireUniqueEmail = true;
	options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
	options.Lockout.MaxFailedAccessAttempts = 5;
})
.AddEntityFrameworkStores<EDergiDbContext>()
.AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
	options.LoginPath = "/Admin/Account/Login";
	options.LogoutPath = "/Admin/Account/Logout";
	options.AccessDeniedPath = "/Admin/Account/AccessDenied";
	options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
	options.SlidingExpiration = true;
});

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddHttpClient();

var app = builder.Build();

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
app.UseSession();

app.MapControllerRoute(
	name: "admin_default",
	pattern: "admin",
	defaults: new { area = "Admin", controller = "Dashboard", action = "Index" });

app.MapControllerRoute(
	name: "areas",
	pattern: "{area:exists}/{controller=Account}/{action=Login}/{id?}");

// ❗ Kullanıcı siteye direkt girerse login'e gitsin:
app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Account}/{action=Login}/{id?}",
	defaults: new { area = "Admin" });


app.Run();
