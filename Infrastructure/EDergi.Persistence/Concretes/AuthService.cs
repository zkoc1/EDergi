using BCrypt.Net;
using EDergi.Application.Abstractions;
using EDergi.Application.DTOs;
using EDergi.Domain.Entitites;
using EDergi.Infrastructure.Services;
using EDergi.Persistence.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class AuthService : IAuthService
{
	private readonly EDergiDbContext _context;
	private readonly ITokenService _tokenService;
	private readonly IEmailService _emailService;
	private readonly UserManager<AppUser> _userManager;

	public AuthService(
		EDergiDbContext context,
		ITokenService tokenService,
		IEmailService emailService,
		UserManager<AppUser> userManager)
	{
		_context = context;
		_tokenService = tokenService;
		_emailService = emailService;
		_userManager = userManager;
	}

	public async Task<string> RegisterAsync(RegisterDto dto)
	{
		if (await _context.Users.AnyAsync(x => x.Email == dto.Email))
			return "Bu email zaten kayıtlı.";

		var user = new AppUser
		{
			Email = dto.Email,
			UserName = dto.Email,
			FirstName = dto.FirstName,
			LastName = dto.LastName,
			BirthDate = dto.BirthDate ?? DateTime.UtcNow,
			CreatedAt = DateTime.UtcNow,
			Status = true
		};

		user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

		await _context.Users.AddAsync(user);
		await _emailService.SendTemporaryPasswordAsync(user.Email, dto.Password, user.FirstName);

		if (!string.IsNullOrEmpty(dto.RoleName))
		{
			var role = await _context.Roles.FirstOrDefaultAsync(r => r.Name == dto.RoleName);
			if (role != null)
			{
				await _context.UserRoles.AddAsync(new AppUserRole
				{
					UserId = user.Id,
					RoleId = role.Id
				});
			}
		}

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

	public async Task<bool> ForgotPasswordAsync(ForgotPasswordDto request)
	{
		var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email);
		if (user == null) return false;

		// 6 haneli şifre üret
		var tempPassword = new Random().Next(100000, 999999).ToString();

		user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(tempPassword);
		user.IsChangedPassword = false;
		await _context.SaveChangesAsync();

		await _emailService.SendTemporaryPasswordAsync(user.Email, tempPassword, user.FirstName);

		return true;
	}

	public async Task<bool> ResetPasswordAsync(ResetPasswordDto request)
	{
		var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email);
		if (user == null) return false;

		user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
		user.IsChangedPassword = true;
		user.UpdatedAt = DateTime.UtcNow;

		_context.Users.Update(user);
		await _context.SaveChangesAsync();

		return true;
	}

	public Task<bool> LogoutAsync()
	{
		// Token blacklisting gibi işlemler varsa yapılmalı
		return Task.FromResult(true);
	}
	public async Task<bool> SendTemporaryPasswordAsync(string email, string tempPassword, string userName)
	{
		await _emailService.SendTemporaryPasswordAsync(email, tempPassword, userName);
		return true;
	}


	public async Task<bool> SetPasswordAsync(SetPasswordDto model)
	{
		var user = await _userManager.FindByEmailAsync(model.Email);
		if (user == null) return false;

		// Eski şifresi yoksa (örneğin sosyal giriş yaptıysa)
		var token = await _userManager.GeneratePasswordResetTokenAsync(user);
		var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);

		return result.Succeeded;
	}
}
