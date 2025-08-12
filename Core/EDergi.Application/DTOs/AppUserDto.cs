using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDergi.Application.DTOs
{
	public class AppUserDto
	{
		public Guid Id { get; set; }
		public string Email { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public DateTime BirthDate { get; set; }
		public bool IsChangedPassword { get; set; } = false;
		public bool IsSysAdmin { get; set; }
		public List<string> Roles { get; set; }
	}
}
