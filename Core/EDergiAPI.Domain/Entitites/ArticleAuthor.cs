using DergiAPI.Domain.Entitites.Commmon;

namespace DergiAPI.Domain.Entitites
{
	public class ArticleAuthor
	{
		public Guid ArticleId { get; set; }
		public Article Article { get; set; }

		public Guid AuthorId { get; set; }
		public Author Author { get; set; }
	}
}
