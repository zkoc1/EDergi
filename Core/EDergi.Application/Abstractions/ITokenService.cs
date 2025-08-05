using System;

public interface ITokenService
{
	/// <summary>
	/// Verilen e-posta ve rol bilgilerine göre bir JWT oluşturur.
	/// </summary>
	/// <param name="email">Kullanıcının e-posta adresi.</param>
	
	/// <returns>Oluşturulan JWT.</returns>
	string CreateToken(string email);
}
