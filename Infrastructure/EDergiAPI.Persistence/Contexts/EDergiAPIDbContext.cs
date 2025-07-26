using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DergiAPI.Domain.Entitites;

namespace DergiAPI.Persistence.Contexts;

public class EDergiAPIDbContext : IdentityDbContext<User> // 👈 önemli düzeltme
{

	public EDergiAPIDbContext(DbContextOptions<EDergiAPIDbContext> options) : base(options) { }

	// 🔧 DbSet'ler ekleniyor
	public DbSet<Article> Articles { get; set; }
	public DbSet<Author> Authors { get; set; }
	public DbSet<Issue> Issues { get; set; }
	public DbSet<MDocument> MDocuments { get; set; }
	public DbSet<Magazine> Magazines { get; set; }
	public DbSet<Publisher> Publishers { get; set; }
	public DbSet<ReadIndex> ReadIndices { get; set; }
	public DbSet<ViewStats> ViewStats { get; set; }
	public DbSet<Volume> Volumes { get; set; }
	public DbSet<ArticleIssue> ArticleIssues { get; set; }
	public DbSet<MNumberOf> MNumbers { get; set; }
	public DbSet<User> Users { get; set; } // Yeni eklenen kullanıcı tablosu
	public DbSet<Admin> Admins { get; set; } // Yeni eklenen admin tablosu

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		// 🔧 ArticleIssue ilişkileri
		modelBuilder.Entity<ArticleIssue>()
			.HasOne(ai => ai.Article)
			.WithMany(a => a.ArticleIssues)
			.HasForeignKey(ai => ai.ArticleId)
			.OnDelete(DeleteBehavior.Restrict);

		modelBuilder.Entity<ArticleIssue>()
			.HasOne(ai => ai.Issue)
			.WithMany(i => i.ArticleIssues)
			.HasForeignKey(ai => ai.IssueId)
			.OnDelete(DeleteBehavior.Restrict);

		// 🔧 User ve Admin tabloları için varsayılan ayarlar
		modelBuilder.Entity<User>(entity =>
		{
			entity.HasKey(u => u.Id);
			entity.Property(u => u.UserName).IsRequired().HasMaxLength(50);
			entity.Property(u => u.Email).IsRequired().HasMaxLength(100);
			entity.Property(u => u.FirstName).HasMaxLength(50);
			entity.Property(u => u.LastName).HasMaxLength(50);
			entity.Property(u => u.ProfilePictureUrl).HasMaxLength(255);
			entity.Property(u => u.IsAdmin).HasDefaultValue(false);
		});

		modelBuilder.Entity<Admin>(entity =>
		{
			entity.HasKey(a => a.Id);
			entity.Property(a => a.UserName).IsRequired().HasMaxLength(50);
			entity.Property(a => a.Email).IsRequired().HasMaxLength(100);
			entity.Property(a => a.FirstName).HasMaxLength(50);
			entity.Property(a => a.LastName).HasMaxLength(50);
			entity.Property(a => a.Role).HasMaxLength(50); // Role özelliği eklendi
		});
	}
}
