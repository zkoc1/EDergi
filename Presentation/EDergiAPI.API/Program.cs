using DergiAPI.Persistence;
using DergiAPI.Persistence.Contexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

// 🛠️ DbContext yapılandırılıyor
builder.Services.AddDbContext<EDergiAPIDbContext>(options =>
	options.UseSqlServer(configuration.GetConnectionString("SqlServer")));

// 🛠️ Identity yapılandırması (ApplicationUser sınıfı varsa onu kullanmalısın)
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
	options.Password.RequireDigit = true;
	options.Password.RequireUppercase = false;
	options.Password.RequiredLength = 6;
})
.AddEntityFrameworkStores<EDergiAPIDbContext>()
.AddDefaultTokenProviders();

// 🔐 JWT Authentication ayarı
builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
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

// 📦 Custom servisler
builder.Services.AddPersistenceServices(configuration);

// 🔧 Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo
	{
		Title = "EDergiAPI",
		Version = "v1"
	});
	// 🔐 Swagger JWT token desteği (isteğe bağlı)
	c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		In = ParameterLocation.Header,
		Description = "JWT token giriniz",
		Name = "Authorization",
		Type = SecuritySchemeType.Http,
		Scheme = "Bearer"
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
			new string[] {}
		}
	});
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI(c =>
	{
		c.SwaggerEndpoint("/swagger/v1/swagger.json", "EDergiAPI v1");
	});
}

app.UseHttpsRedirection();
app.UseAuthentication(); // 🔐 Token middleware
app.UseAuthorization();

app.MapControllers();

app.Run();
