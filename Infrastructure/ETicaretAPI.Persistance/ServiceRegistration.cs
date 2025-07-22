using ETicaretAPI.Application.Abstractions;
using ETicaretAPI.Persistance.Concretes;
using ETicaretAPI.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ETicaretAPI.Persistance
{
	public static class ServiceRegistration
	{
		public static void AddPersistanceServices(this IServiceCollection services, IConfiguration configuration)
		{
			// Servislerin kaydı
			services.AddScoped<IProductService, ProductService>();

			// DbContext konfigürasyonu
			services.AddDbContext<ETicaretAPIDbContext>(options =>
			{
				// Burada "SqlConnection" ismi, appsettings.json içinde tanımlanan connection string adıdır
				options.UseSqlServer(configuration.GetConnectionString("SqlConnection"));
				options.EnableDetailedErrors();
				options.EnableSensitiveDataLogging();
			});
		}
	}
}
