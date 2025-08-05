// Issue.cs
using EDergi.Domain.Entitites;
using EDergi.Domain.Entitites.Commmon;

public class Issue : BaseEntity
{
	public int IssueNumber { get; set; }
	public DateTime PublishDate { get; set; }

	public Guid VolumeId { get; set; }
	public virtual Volume Volume { get; set; }

	public ICollection<Article> Articles { get; set; }

}
