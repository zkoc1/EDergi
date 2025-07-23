using EDergiAPI.Application.Abstractions;
using EDergiAPI.Persistence.Concretes;
using EDergiAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EDergiAPI.Persistence
{
	public static class ServiceRegistration
	{
		public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
		{
			// Servislerin kaydı
			services.AddScoped<IProductService, ProductService>();

			// DbContext konfigürasyonu
			services.AddDbContext<EDergiAPIDbContext>(options =>
			{
				// Burada "SqlConnection" ismi, appsettings.json içinde tanımlanan connection string adıdır
				options.UseSqlServer(configuration.GetConnectionString("SqlConnection"));
				options.EnableDetailedErrors();
				options.EnableSensitiveDataLogging();
			});
		}
	}
}
