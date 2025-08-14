using Microsoft.AspNetCore.Http;

namespace EDergi.Application.DTOs
{
	public class ReadIndexCreateDto
	{
		public Guid MagazineId { get; set; }
		public string Name { get; set; }

		// Çoklu resim yükleme
		public List<IFormFile> ImageFiles { get; set; }
	}
}
