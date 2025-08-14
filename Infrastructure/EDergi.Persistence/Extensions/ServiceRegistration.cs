using EDergi.Application.Abstractions;
using EDergi.Application.Abstractions.Services;
using EDergi.Infrastructure.Services;
using EDergi.Application.Interfaces.Services;
using EDergi.Application.Repostories;
using EDergi.Persistence.Concretes;
using EDergi.Persistence.Contexts;
using EDergi.Persistence.Repositories;
using EDergi.Persistence.Repostories;
using EDergi.Persistence.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EDergi.Persistence
{
	public static class ServiceRegistration
	{
		public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
		{
			// 🔧 DbContext servisi ekleniyor
			services.AddDbContext<EDergiDbContext>(options =>
				options.UseSqlServer(configuration.GetConnectionString("SqlServer")));

			// 🔧 Generic repository servisleri
			services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
			services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));

			// 🔧 Özel servisler
			services.AddScoped<IArticleService, ArticleService>();
			services.AddScoped<IAuthorService, AuthorService>();
			services.AddScoped<IDocumentService, DocumentService>();
			services.AddScoped<IReadIndexService, ReadIndexService>();
			services.AddScoped<IIssueService, IssueService>();
			services.AddScoped<IMagazineService, MagazineService>();
			services.AddScoped<IPublisherService, PublisherService>();
			services.AddScoped<IViewStatsService, ViewStatsService>();
			services.AddScoped<IVolumeService, VolumeService>();
		    services.AddScoped<ITokenService, TokenService>();
			services.AddScoped<IEndpointService, EndpointService>();
			services.AddScoped<IMenuService, MenuService>();
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IRoleService, RoleService>();
			services.AddScoped<IEmailService, EmailService>();
			services.AddScoped<IFileUploadService, FileUploadService>();






			// 🔧 Kullanıcı servisi
			services.AddScoped<IAuthService, AuthService>();

			return services;
		}
	}
}
