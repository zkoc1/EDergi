using EDergi.Application.Abstractions;
using EDergi.Application.Abstractions.Services;
using EDergi.Application.Repostories;
using EDergi.Domain.Entitites;
using EDergi.Infrastructure.Services;
using EDergi.Persistence.Concretes;
using EDergi.Persistence.Contexts;
using EDergi.Persistence.Repositories;
using EDergi.Persistence.Repostories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// DbContext ayarı
builder.Services.AddDbContext<EDergiDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

// Identity ayarları (AppUser ve AppRole kullanılıyor!)
builder.Services.AddIdentity<AppUser, AppRole>(options =>
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


builder.Services.AddScoped<RoleManager<AppRole>>();
builder.Services.AddScoped<UserManager<AppUser>>();

// Cookie ayarları
builder.Services.ConfigureApplicationCookie(options =>
{
	options.LoginPath = "/Admin/Account/Login";
	options.LogoutPath = "/Admin/Account/Logout";
	options.AccessDeniedPath = "/Admin/Account/AccessDenied";
	options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
	options.SlidingExpiration = true;
});

// Authorization politikaları
builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("SysAdminOnly", policy => policy.RequireRole("SysAdmin"));
	options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
	options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));
});

// DI servisleri
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IReadRepository<Author>, ReadRepository<Author>>();
builder.Services.AddScoped<IWriteRepository<Author>, WriteRepository<Author>>();
builder.Services.AddScoped<IMagazineService, MagazineService>();
builder.Services.AddScoped<IReadRepository<Magazine>, ReadRepository<Magazine>>();
builder.Services.AddScoped<IWriteRepository<Magazine>, WriteRepository<Magazine>>();
// UserManagement ve RoleManagement için servisler
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();







// MVC, Session, HTTP Client
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddHttpClient();

var app = builder.Build();

// Hata sayfası
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

// Middleware
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
	name: "adminDashboard",
	pattern: "Admin/Dashboard",
	defaults: new { area = "Admin", controller = "Dashboard", action = "Index" });

//app.MapControllerRoute(
//	name: "adminLogin",
//	pattern: "Admin",
//	defaults: new { area = "Admin", controller = "Account", action = "Login" });

app.MapControllerRoute(
	name: "adminSetNewPassword",
	pattern: "Admin/Account/SetNewPassword",
	defaults: new { area = "Admin", controller = "Account", action = "SetNewPassword" });

app.MapControllerRoute(
	name: "adminForgotPassword",
	pattern: "Admin/Account/ForgotPassword",
	defaults: new { area = "Admin", controller = "Account", action = "ForgotPassword" });

app.MapControllerRoute(
	name: "adminRegister",
	pattern: "Admin/Account/Register",
	defaults: new { area = "Admin", controller = "Account", action = "Register" });

app.MapControllerRoute(
	name: "adminChangePassword",
	pattern: "Admin/Account/ChangePassword",
	defaults: new { area = "Admin", controller = "Account", action = "ChangePassword" });

app.MapControllerRoute(
	name: "adminDefault",
	pattern: "Admin",
	defaults: new { area = "Admin", controller = "Account", action = "Login" });

app.MapControllerRoute(
	name: "adminLogout",
	pattern: "Admin/Account/Logout",
	defaults: new { area = "Admin", controller = "Account", action = "Logout" });

//app.MapControllerRoute(
//	name: "adminLogin",
//	pattern: "Admin/Account/Login",
//	defaults: new { area = "Admin", controller = "Account", action = "Login" });
// UserManagement ve RoleManagement için route'lar


app.MapControllerRoute(
		name: "areas",
		pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
	);

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
