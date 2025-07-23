
using DergiAPI.Application.Repostories;
using DergiAPI.Persistence.Concretes;
using DergiAPI.Persistence.Contexts;
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
				options.UseSqlServer(configuration.GetConnectionString("SqlConnection")));

			// 🔧 Repository servisleri ekleniyor
			services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
			services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));

			// 🔧 Servisler ekleniyor
			services.AddScoped<IArticleService, ArticleService>();
			services.AddScoped<IAuthorService, AuthorService>();
			services.AddScoped<IMDocumentService, MDocumentService>();
			services.AddScoped<IReadIndexService, ReadIndexService>();
			services.AddScoped<IIssueService, IssueService>();
			services.AddScoped<IMagazineService, MagazineService>();
			services.AddScoped<IPublisherService, PublisherService>();
			services.AddScoped<IViewStatsService, ViewStatsService>();
			services.AddScoped<IArticleIssueService, ArticleIssueService>();

			return services;
		}
	}
}
