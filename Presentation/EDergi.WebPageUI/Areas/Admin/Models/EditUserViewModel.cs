using Microsoft.AspNetCore.Identity;

namespace EDergi.WebPageUI.Areas.Admin.Models
{
	public class EditUserViewModel
	{
		public Guid Id { get; set; }
		public string Email { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public List<string> SelectedRoles { get; set; }
		public List<string> Roles { get; set; }
		public List<string> AllRoles { get; set; }
	}
}
