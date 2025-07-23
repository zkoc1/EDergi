using Microsoft.EntityFrameworkCore;
using DergiAPI.Domain.Entitites;

namespace DergiAPI.Persistence.Contexts;

public class EDergiAPIDbContext : DbContext
{
	public EDergiAPIDbContext(DbContextOptions<EDergiAPIDbContext> options) : base(options) { }

	// 🔧 DbSet'ler ekleniyor
	public DbSet<Article> Articles { get; set; }
	public DbSet<Author> Authors { get; set; }
	public DbSet<MDocument> MDocuments { get; set; }
	public DbSet<ReadIndex> ReadIndices { get; set; }
	public DbSet<Issue> Issues { get; set; }
	public DbSet<Magazine> Magazines { get; set; }
	public DbSet<Publisher> Publishers { get; set; }
	public DbSet<ViewStats> ViewStats { get; set; }
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

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
	}

	
}
