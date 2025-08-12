using EDergi.Domain.Entitites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EDergi.Application.Abstractions
{
	public interface IEmailService
	{
		Task<EmailResult> SendEmailAsync(string to, string subject, string body, bool isHtml = true, List<string> cc = null, List<string> bcc = null, List<EmailAttachment> attachments = null);
		Task<EmailResult> SendTemporaryPasswordAsync(string to, string tempPassword, string userName = "Kullanıcı");
	}
}
