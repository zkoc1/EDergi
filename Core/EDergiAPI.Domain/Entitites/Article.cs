// Article.cs
using DergiAPI.Domain.Entitites;
using DergiAPI.Domain.Entitites.Commmon;

public class Article : BaseEntity
{
	public string Title { get; set; }
	public int Year { get; set; }
	public string Description { get; set; }
	public string Keywords { get; set; }
	public string SupportingInstitution { get; set; }
	public string ProjectNumber { get; set; }
	public string Reference { get; set; }
	public string ArticleLink { get; set; }

	public ICollection<Author> Authors { get; set; }
	public ICollection<Volume> Volumes { get; set; }

	// Çoktan çoğa ilişki
	public ICollection<ArticleIssue> ArticleIssues { get; set; }
}
