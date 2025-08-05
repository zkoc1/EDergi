using EDergi.Application.Abstractions;
using EDergi.Application.DTOs;
using EDergi.Persistence.Contexts;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;

public class AuthService : IAuthService
{
	private readonly EDergiDbContext _context;
	private readonly ITokenService _tokenService;

	public AuthService(EDergiDbContext context, ITokenService tokenService)
	{
		_context = context;
		_tokenService = tokenService;
	}

	public async Task<string> RegisterAsync(RegisterDto dto)
	{
		if (_context.Users.Any(x => x.Email == dto.Email))
			return "Bu email zaten kayıtlı.";

		var user = new User
		{
			Email = dto.Email,
			FirstName = dto.FirstName,
			LastName = dto.LastName,
			UserName = dto.Email,
	
		};

		user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

		await _context.Users.AddAsync(user);
		await _context.SaveChangesAsync();

		return "Kayıt başarılı!";
	}

	public async Task<LoginResultDto> LoginAsync(LoginDto dto)
	{
		var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == dto.Email);
		if (user == null)
			return new LoginResultDto { Error = "Kullanıcı bulunamadı." };

		if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
			return new LoginResultDto { Error = "Şifre yanlış." };

	
		var token = _tokenService.CreateToken(user.Email);

		return new LoginResultDto { Token = token };
	}

	
}
