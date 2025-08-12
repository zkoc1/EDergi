using EDergi.Application.DTOs;
using System.Threading.Tasks;

namespace EDergi.Application.Abstractions
{
	public interface IAuthService
	{
		Task<string> RegisterAsync(RegisterDto model);
		Task<LoginResultDto> LoginAsync(LoginDto model);
		Task<bool> ForgotPasswordAsync(ForgotPasswordDto request);
		Task<bool> ResetPasswordAsync(ResetPasswordDto request);
		Task<bool> LogoutAsync();
		Task<bool> SetPasswordAsync(SetPasswordDto model);
		Task<bool> SendTemporaryPasswordAsync(string email, string tempPassword, string userName);


	}
}
