using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDergiAPI.Application.DTOs
{
	public class VolumeCreateDto
	{
		public string Title { get; set; }
		public int Year { get; set; }
		public List<IssueCreateDto> Issues { get; set; }
	
	}

}
