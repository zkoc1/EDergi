using DergiAPI.Domain.Entitites;
using EDergiAPI.Domain.Entitites;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DergiAPI.Persistence.Contexts
{
	public class EDergiAPIDbContext : IdentityDbContext<User, Role, string>
	{
		public EDergiAPIDbContext(DbContextOptions<EDergiAPIDbContext> options) : base(options) { }

		// 📦 DbSet Tanımları
		public DbSet<Article> Articles { get; set; }
		public DbSet<Author> Authors { get; set; }
		public DbSet<Issue> Issues { get; set; }
		public DbSet<MDocument> MDocuments { get; set; }
		public DbSet<Magazine> Magazines { get; set; }
		public DbSet<Publisher> Publishers { get; set; }
		public DbSet<ReadIndex> ReadIndices { get; set; }
		public DbSet<ViewStats> ViewStats { get; set; }
		public DbSet<Volume> Volumes { get; set; }
		public DbSet<ArticleAuthor> ArticleAuthors { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			// 🧩 User Entity Ayarları
			modelBuilder.Entity<User>(entity =>
			{
				entity.Property(u => u.FirstName).HasMaxLength(50).IsRequired();
				entity.Property(u => u.LastName).HasMaxLength(50).IsRequired();
				entity.Property(u => u.CreatedAt).IsRequired();
			});

			// 🔗 ArticleAuthor Many-to-Many
			modelBuilder.Entity<ArticleAuthor>()
				.HasKey(aa => new { aa.ArticleId, aa.AuthorId });

			modelBuilder.Entity<ArticleAuthor>()
				.HasOne(aa => aa.Article)
				.WithMany(a => a.ArticleAuthors)
				.HasForeignKey(aa => aa.ArticleId);

			modelBuilder.Entity<ArticleAuthor>()
				.HasOne(aa => aa.Author)
				.WithMany(a => a.ArticleAuthors)
				.HasForeignKey(aa => aa.AuthorId);

			// 🎯 Magazine - Publisher İlişkisi
			modelBuilder.Entity<Magazine>()
				.HasOne(m => m.Publisher)
				.WithMany(p => p.Magazines)
				.HasForeignKey(m => m.PublisherId);

			// 🎯 Volume - Magazine İlişkisi
			modelBuilder.Entity<Volume>()
				.HasOne(v => v.Magazine)
				.WithMany(m => m.Volumes)
				.HasForeignKey(v => v.JMagazineId);

			// 🎯 Issue - Volume İlişkisi
			modelBuilder.Entity<Issue>()
				.HasOne(i => i.Volume)
				.WithMany(v => v.Issues)
				.HasForeignKey(i => i.VolumeId);

			// 🎯 Article - Issue İlişkisi
			modelBuilder.Entity<Article>()
				.HasOne(a => a.Issue)
				.WithMany(i => i.Articles)
				.HasForeignKey(a => a.IssueId);

			// 🎯 Magazine - ViewStats One-to-One
			modelBuilder.Entity<Magazine>()
				.HasOne(m => m.ViewStats)
				.WithOne(v => v.Magazine)
				.HasForeignKey<ViewStats>(v => v.MagazineId);
		}
	}
}
