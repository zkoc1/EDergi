// Issue.cs
using DergiAPI.Domain.Entitites;
using DergiAPI.Domain.Entitites.Commmon;

public class Issue : BaseEntity
{
	public int IssueNumber { get; set; }
	public Guid VolumeId { get; set; }

	// Çoktan çoğa ilişki
	public ICollection<ArticleIssue> ArticleIssues { get; set; }
}
