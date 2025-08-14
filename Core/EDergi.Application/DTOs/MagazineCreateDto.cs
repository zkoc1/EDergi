using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDergi.Application.DTOs
{
	public class MagazineCreateDto
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public DateTime StartDate { get; set; }
		public string ISSN { get; set; }
		public string Period { get; set; }
		public string Purpose { get; set; }
		public string Scope { get; set; }
		public string WritingRules { get; set; }
		public string JournalRules { get; set; }
		public string ImageUrl { get; set; }
		public string ImageName { get; set; }

		public Guid PublisherId { get; set; } // zaten Publisher'ı dışarıdan bağlıyorsun

		public ViewStatsDto ViewStats { get; set; } // birebir ilişkide nested şekilde ViewStats alırız
		public List<MDocumentDto> Documents { get; set; }
		public List<ReadIndexDto> Indexes { get; set; }
		public List<VolumeCreateDto> Volumes { get; set; }
	
	}


}
