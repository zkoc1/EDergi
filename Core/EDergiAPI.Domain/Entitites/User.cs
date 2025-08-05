using Microsoft.AspNetCore.Identity;



public class User : IdentityUser
{
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	//public UserRole Role { get; set; } = UserRole.User;
}
