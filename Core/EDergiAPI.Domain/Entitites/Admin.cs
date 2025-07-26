using DergiAPI.Domain.Entitites.Commmon;

namespace DergiAPI.Domain.Entitites;

public class Admin : BaseEntity
{
	public string UserName { get; set; }
	public string Email { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string Role { get; set; }
}
