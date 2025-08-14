using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDergi.Application.DTOs
{
	public class VolumeCreateDto
	{
		public Guid Id { get; set; } = Guid.NewGuid();
		public string Title { get; set; }
		public int Year { get; set; }
		public Guid MagazineId { get; set; }
		public List<IssueCreateDto> Issues { get; set; }
	
	}

}
