using EDergi.Application.Abstractions;
using EDergi.Domain.Entitites;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace EDergi.Infrastructure.Services
{
	public class EmailService : IEmailService
	{
		private readonly IConfiguration _config;

		public EmailService(IConfiguration config)
		{
			_config = config;
		}

		public async Task<EmailResult> SendEmailAsync(string to, string subject, string body, bool isHtml = true, List<string> cc = null, List<string> bcc = null, List<EmailAttachment> attachments = null)
		{
			try
			{
				var smtpClient = new SmtpClient(_config["Email:Smtp"])
				{
					Port = int.Parse(_config["Email:Port"]),
					Credentials = new NetworkCredential(_config["Email:Username"], _config["Email:Password"]),
					EnableSsl = true
				};

				var mailMessage = new MailMessage
				{
					From = new MailAddress(_config["Email:From"]),
					Subject = subject,
					Body = body,
					IsBodyHtml = isHtml
				};

				mailMessage.To.Add(to);

				// CC alıcıları ekle
				if (cc != null && cc.Any())
				{
					foreach (var ccRecipient in cc)
					{
						mailMessage.CC.Add(ccRecipient);
					}
				}

				// BCC alıcıları ekle
				if (bcc != null && bcc.Any())
				{
					foreach (var bccRecipient in bcc)
					{
						mailMessage.Bcc.Add(bccRecipient);
					}
				}

				// Ek dosyaları ekle
				if (attachments != null && attachments.Any())
				{
					foreach (var attachment in attachments)
					{
						var ms = new MemoryStream(attachment.Content);
						mailMessage.Attachments.Add(new Attachment(ms, attachment.FileName, attachment.ContentType));
					}
				}

				await smtpClient.SendMailAsync(mailMessage);
				return EmailResult.Ok("Mail başarıyla gönderildi.");
			}
			catch (Exception ex)
			{
				return EmailResult.Fail($"Mail gönderilemedi: {ex.Message}");
			}
		}

		public Task<EmailResult> SendTemporaryPasswordAsync(string to, string tempPassword, string userName = "Kullanıcı")
		{
			string subject = "Geçici Şifreniz";
			string body = $@"
                <p>Merhaba {userName},</p>
                <p>Geçici şifreniz: <strong>{tempPassword}</strong></p>
                <p>Lütfen ilk girişte şifrenizi değiştirin.</p>";

			return SendEmailAsync(to, subject, body);
		}
	}
}
