using EDergi.Application.Abstractions;
using EDergi.Application.Abstractions.Services;
using EDergi.Application.Repostories;
using EDergi.Domain.Entitites;
using EDergi.Persistence;
using EDergi.Persistence.Concretes;
using EDergi.Persistence.Contexts;
using EDergi.Persistence.Repositories;
using EDergi.Persistence.Repostories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// DbContext
builder.Services.AddDbContext<EDergiDbContext>(options =>
	options.UseSqlServer(configuration.GetConnectionString("SqlServer")));

// Identity (Role modelin artık özel Role sınıfın)
builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
	options.Password.RequireDigit = false;
	options.Password.RequiredLength = 6;
})
.AddEntityFrameworkStores<EDergiDbContext>()
.AddDefaultTokenProviders();
// JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options =>
	{
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuer = true,
			ValidateAudience = true,
			ValidateLifetime = true,
			ValidateIssuerSigningKey = true,
			ValidIssuer = configuration["Jwt:Issuer"],
			ValidAudience = configuration["Jwt:Audience"],
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
		};
	});

// Servisler
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddScoped<IReadRepository<Author>, ReadRepository<Author>>();
builder.Services.AddScoped<IWriteRepository<Author>, WriteRepository<Author>>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddPersistenceServices(configuration);
builder.Services.Configure<EmailSettings>(
	builder.Configuration.GetSection("Email"));


// Controllers
builder.Services.AddControllers()
	.AddJsonOptions(options =>
	{
		options.JsonSerializerOptions.PropertyNamingPolicy = null;
	});

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "EDergi", Version = "v1" });
	c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		Name = "Authorization",
		Type = SecuritySchemeType.Http,
		Scheme = "Bearer",
		BearerFormat = "JWT",
		In = ParameterLocation.Header,
		Description = "JWT Token'ınızı 'Bearer <token>' formatında girin."
	});
	c.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id = "Bearer"
				}
			},
			Array.Empty<string>()
		}
	});
});

// CORS
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAll", policy =>
	{
		policy.AllowAnyOrigin()
			  .AllowAnyMethod()
			  .AllowAnyHeader();
	});
});

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI(c =>
	{
		c.SwaggerEndpoint("/swagger/v1/swagger.json", "EDergi v1");
	});
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();
app.MapControllers();

app.Run();
