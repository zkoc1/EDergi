using DergiAPI.Application.Repostories;
using DergiAPI.Persistence.Concretes;
using DergiAPI.Persistence.Repostories;
using EDergiAPI.Application.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace DergiAPI.Persistence
{
	public static class ServiceRegistration
	{
		public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
		{
			services.AddScoped<IArticleService, ArticleService>();
			services.AddScoped<IAuthorService, AuthorService>();
			services.AddScoped<IMDocumentService, MDocumentService>();
			services.AddScoped<IReadIndexService, ReadIndexService>();
			services.AddScoped<IIssueService, IssueService>();
			services.AddScoped<IMagazineService, MagazineService>();
			services.AddScoped<IPublisherService, PublisherService>();
			services.AddScoped<IViewStatsService, ViewStatsService>();

			services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
			services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));

			return services;
		}
	}
}
