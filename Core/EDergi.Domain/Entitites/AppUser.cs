using EDergi.Domain.Entitites;
using Microsoft.AspNetCore.Identity;



public class AppUser : IdentityUser<Guid>
{
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public DateTime BirthDate { get; set; }
	public bool IsSysAdmin { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime? UpdatedAt { get; set; }
	public bool IsChangedPassword { get; set; }
	public bool Status { get; set; }
	public ICollection<AppUserRole> UserRoles { get; set; } = new HashSet<AppUserRole>();
}
