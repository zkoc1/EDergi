using DergiAPI.Application.Repostories;
using DergiAPI.Persistence.Concretes;
using DergiAPI.Persistence.Contexts;
using DergiAPI.Persistence.Repositories;
using DergiAPI.Persistence.Repostories;
using EDergiAPI.Application.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DergiAPI.Persistence
{
	public static class ServiceRegistration
	{
		public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
		{
			// 🔧 DbContext servisi ekleniyor
			services.AddDbContext<EDergiAPIDbContext>(options =>
				options.UseSqlServer(configuration.GetConnectionString("SqlServer")));

			// 🔧 Generic repository servisleri
			services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
			services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));

			// 🔧 Özel servisler
			services.AddScoped<IArticleService, ArticleService>();
			services.AddScoped<IAuthorService, AuthorService>();
			services.AddScoped<IMDocumentService, MDocumentService>();
			services.AddScoped<IReadIndexService, ReadIndexService>();
			services.AddScoped<IIssueService, IssueService>();
			services.AddScoped<IMagazineService, MagazineService>();
			services.AddScoped<IPublisherService, PublisherService>();
			services.AddScoped<IViewStatsService, ViewStatsService>();
			services.AddScoped<IArticleIssueService, ArticleIssueService>();
			services.AddScoped<IVolumeService, VolumeService>();
			services.AddScoped<IMNumberOfService, MNumberOfService>();

			// 🔧 Kullanıcı ve Admin servisleri
			services.AddScoped<IAdminService, AdminService>();

			return services;
		}
	}
}
