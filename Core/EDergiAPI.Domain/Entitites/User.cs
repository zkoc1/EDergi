using Microsoft.AspNetCore.Identity;

public class User : IdentityUser
{
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public bool IsAdmin { get; set; }
	public string ProfilePictureUrl { get; set; }

	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

}
