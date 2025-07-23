using DergiAPI.Application.Abstractions;
using DergiAPI.Application.Abstractions.Services;
using DergiAPI.Application.Repostories;
using DergiAPI.Persistence.Concretes;
using DergiAPI.Persistence.Concretes.Services;
using DergiAPI.Persistence.Repostories;
using EDergiAPI.Application.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace DergiAPI.Persistence
{
	public static class ServiceRegistration
	{
		public static void AddPersistenceServices(this IServiceCollection services, Microsoft.Extensions.Configuration.ConfigurationManager configuration)
		{
			// Servis katmanı bağımlılıkları
			services.AddScoped<IArticleService, ArticleService>();
			services.AddScoped<IArchiveService, ArchiveService>();
			services.AddScoped<IArticleIssueService, ArticleIssueService>();
			services.AddScoped<IAuthorService, AuthorService>();
			services.AddScoped<IDocumentService, DocumentService>();
			services.AddScoped<IReadIndexService, ReadIndexService>();
			services.AddScoped<IIssueService, IssueService>();
			services.AddScoped<IMagazineService, MagazineService>();	
			services.AddScoped<IPublisherService,PublisherService>();
			services.AddScoped<IPurposeScopeService, PurposeScopeService>();
			services.AddScoped<IRulesService, RulesService>();
			services.AddScoped<IViewStatsService, ViewStatsService>();
			services.AddScoped<IWritingRulesService, WritingRulesService>();



			// Generic repository bağımlılıkları
			services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
			services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));
		}
	}
}
