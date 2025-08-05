// Article.cs
using EDergi.Domain.Entitites;
using EDergi.Domain.Entitites.Commmon;

public class Article : BaseEntity
{
	public Guid Id { get; set; }
	public string Title { get; set; }
	public string Description { get; set; }
	public string Keywords { get; set; }
	public string PdfUrl { get; set; }
	public string SupportingInstitution { get; set; }
	public string ProjectNumber { get; set; }
	public string Reference { get; set; }
	public string ArticleLink { get; set; }
	public bool IsApproved { get; set; } = false;
	public Guid IssueId { get; set; }
	public virtual Issue Issue { get; set; }
    public ICollection <ArticleAuthor> ArticleAuthors { get; set; }
}
