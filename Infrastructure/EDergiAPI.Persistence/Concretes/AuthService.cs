using DergiAPI.Application.DTOs;
using EDergiAPI.Application.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

public class AuthService : DergiAPI.Application.Abstractions.IAuthService
{
	private readonly UserManager<User> _userManager;
	private readonly SignInManager<User> _signInManager;
	private readonly IConfiguration _configuration;

	public AuthService(UserManager<User> userManager,
					   SignInManager<User> signInManager,
					   IConfiguration configuration)
	{
		_userManager = userManager;
		_signInManager = signInManager;
		_configuration = configuration;
	}

	public async Task<string> RegisterAsync(RegisterDto model)
	{
		var user = new User
		{
			UserName = model.Email,
			Email = model.Email,
			FirstName = model.FirstName,
			LastName = model.LastName,
			
		};

		var result = await _userManager.CreateAsync(user, model.Password);
		if (!result.Succeeded)
			return string.Join(", ", result.Errors.Select(e => e.Description));

		return "Kayıt başarılı!";
	}

	public async Task<string> LoginAsync(LoginDto model)
	{
		var user = await _userManager.FindByEmailAsync(model.Email);
		if (user == null) return "Kullanıcı bulunamadı.";

		var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
		if (!result.Succeeded) return "Şifre yanlış.";

		return GenerateToken(user); // aşağıda yazacağız
	}

	private string GenerateToken(User user)
	{
		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
		var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

		var claims = new[]
		{
			new Claim(JwtRegisteredClaimNames.Sub, user.Email),
			new Claim(ClaimTypes.Name, user.UserName),
			new Claim(ClaimTypes.NameIdentifier, user.Id)
		};

		var token = new JwtSecurityToken(
			issuer: _configuration["Jwt:Issuer"],
			audience: _configuration["Jwt:Audience"],
			claims: claims,
			expires: DateTime.UtcNow.AddMinutes(60),
			signingCredentials: creds
		);

		return new JwtSecurityTokenHandler().WriteToken(token);
	}
}

