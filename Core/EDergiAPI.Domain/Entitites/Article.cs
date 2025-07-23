
using DergiAPI.Domain.Entitites.Commmon;
namespace DergiAPI.Domain.Entitites
{
	public class Article : BaseEntity
	{
	
		public string Title { get; set; }
		public int Year { get; set; }
        public string Description { get; set; } // makale açıklaması
        public string Keywords { get; set; } // anahtar kelimeler
        public string SupportingInstitution { get; set; } // destekleyen kurum
        public string ProjectNumber { get; set; } // proje numarası
        public string Reference { get; set; }
        public string ArticleLink { get; set; }
		public ICollection<Author> Authors { get; set; }
        public ICollection<Volume> Volumes { get; set; } // makale ciltleri
        public ICollection<Issue> Issues { get; set; } // makale ciltlere ait olan sayılar
    }
}
