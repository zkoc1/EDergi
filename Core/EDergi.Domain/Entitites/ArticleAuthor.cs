using EDergi.Domain.Entitites.Commmon;

namespace EDergi.Domain.Entitites
{
	public class ArticleAuthor
	{
		public Guid ArticleId { get; set; }
		public Article Article { get; set; }

		public Guid AuthorId { get; set; }
		public Author Author { get; set; }
	}
}
