using DergiAPI.Domain.Entitites.Commmon;

namespace DergiAPI.Domain.Entitites
{
	public class ArticleIssue : BaseEntity
	{
		public Guid ArticleId { get; set; }
		public Article Article { get; set; }

		public Guid IssueId { get; set; }
		public Issue Issue { get; set; }
	}
}

