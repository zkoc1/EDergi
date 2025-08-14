using EDergi.Application.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace EDergi.Persistence.Concretes
{
	public class FileUploadService : IFileUploadService
	{
		private readonly IConfiguration _config;

		public FileUploadService(IConfiguration config)
		{
			_config = config;
		}

		public async Task<(string FileName, string FileUrl)> UploadFileAsync(IFormFile file)
		{
			if (file == null || file.Length == 0)
				throw new ArgumentException("Dosya boş olamaz.");

			var uploadPath = _config["FileUpload:UploadPath"] ?? Path.Combine("wwwroot", "images");
			var fileName = GetUniqueFileName(file.FileName);
			var filePath = Path.Combine(uploadPath, fileName);

			if (!Directory.Exists(uploadPath))
				Directory.CreateDirectory(uploadPath);

			using (var stream = new FileStream(filePath, FileMode.Create))
			{
				await file.CopyToAsync(stream);
			}

			var fileUrl = $"/images/{fileName}";
			return (fileName, fileUrl);
		}

		private string GetUniqueFileName(string fileName)
		{
			var name = Path.GetFileNameWithoutExtension(fileName);
			var ext = Path.GetExtension(fileName);
			var uniqueName = $"{name}_{Guid.NewGuid()}{ext}";
			return uniqueName;
		}
	}
}
