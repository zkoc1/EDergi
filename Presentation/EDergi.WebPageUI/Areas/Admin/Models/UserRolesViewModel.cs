namespace EDergi.WebPageUI.Areas.Admin.Models
{
	public class UserRolesViewModel
	{
		public Guid UserId { get; set; }
		public string Email { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public List<string> Roles { get; set; }
	}
}
