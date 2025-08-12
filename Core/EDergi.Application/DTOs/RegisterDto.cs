namespace EDergi.Application.DTOs
{
	public class RegisterDto
	{
		public string Email { get; set; }
		public string Password { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public bool IsChangedPassword { get; set; } = false;
		public DateTime? BirthDate { get; set; }
		public string? RoleName { get; set; }

	}
}
