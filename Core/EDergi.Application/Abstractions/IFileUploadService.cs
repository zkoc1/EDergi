using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace EDergi.Application.Abstractions
{
	public interface IFileUploadService
	{
		Task<(string FileName, string FileUrl)> UploadFileAsync(IFormFile file);
	}
}
