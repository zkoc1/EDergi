using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDergi.Application.DTOs
{
	public class EndpointDto
	{
		public Guid Id { get; set; }
		public string ActionType { get; set; }
		public string HttpType { get; set; }
		public string Definition { get; set; }
		public string Code { get; set; }
		public Guid MenuId { get; set; }
		public List<string> RoleNames { get; set; }
	}
}
