using EDergi.Application.Abstractions;
using EDergi.Application.DTOs;
using EDergi.Application.Interfaces.Services;
using EDergi.Application.Repostories;
using EDergi.Domain.Entitites;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace EDergi.Persistence.Concretes
{
	public class MagazineService : IMagazineService
	{
		private readonly IReadRepository<Magazine> _readRepository;
		private readonly IWriteRepository<Magazine> _writeRepository;
		private readonly IFileUploadService _fileUploadService;
		public MagazineService(IReadRepository<Magazine> readRepository, IWriteRepository<Magazine> writeRepository)
		{
			_readRepository = readRepository;
			_writeRepository = writeRepository;
			
		}

		public async Task<List<Magazine>> GetAllAsync()
		{
			var magazines = await _readRepository.GetAll()
				 .Include(m => m.ViewStats)
				 .Select(m => new Magazine
				 {
					 Id = m.Id,
					 Title = m.Title,
					 ImageUrl = m.ImageUrl,
					 ISSN = m.ISSN,
					 StartDate=m.StartDate,
					 ViewStats = new ViewStats
					 {
						 ViewCount = m.ViewStats.ViewCount,
						 Id = m.ViewStats.Id,
						 MagazineId = m.Id,
						 FavoriteCount = m.ViewStats.FavoriteCount,
						 DownloadCount = m.ViewStats.DownloadCount
					 }
				 })
				.ToListAsync();

			return magazines;
		}



		public async Task<Magazine> GetByIdAsync(Guid id)
		{
			return await _readRepository.GetSingleAsync(m => m.Id == id);
		}

		public async Task<bool> CreateAsync(MagazineCreateDto dto)
		{
			var magazineId = Guid.NewGuid();
			

			var magazine = new Magazine
			{
				Id = magazineId,
				Title = dto.Title,
				Description = dto.Description,
				StartDate = dto.StartDate,
				ISSN = dto.ISSN,
				Period = dto.Period,
				Purpose = dto.Purpose,
				Scope = dto.Scope,
				WritingRules = dto.WritingRules,
				JournalRules = dto.JournalRules,
				ImageUrl = dto.ImageUrl,
                ImageName = dto.ImageName,
				PublisherId = dto.PublisherId,

				ViewStats = new ViewStats
				{
					Id = Guid.NewGuid(),
					MagazineId = magazineId,
					ViewCount = dto.ViewStats?.ViewCount ?? 0,
					FavoriteCount = dto.ViewStats?.FavoriteCount ?? 0,
					DownloadCount = dto.ViewStats?.DownloadCount ?? 0
				},

				Documents = dto.Documents?.Select(doc => new MDocument
				{
					Id = Guid.NewGuid(),
					FileName = doc.FileName,
					FilePath = doc.FilePath,
					MagazineId =magazineId,
					CreatedDate = doc.CreatedDate ?? DateTime.UtcNow
				}).ToList(),

				Indexes = dto.Indexes?.Select(index => new ReadIndex
				{
					Id = Guid.NewGuid(),
					Name = index.Name,
					ImageName = index.ImageName,
					ImageUrl = index.ImageUrl,
					MagazineId = magazineId
				}).ToList(),

				Volumes = dto.Volumes?.Select(volume => new Volume
				{
					Id = Guid.NewGuid(),
					Title = volume.Title,
					Year = volume.Year,
					MagazineId = magazineId,
					Issues = volume.Issues?.Select(issue => new Issue
					{
						Id = Guid.NewGuid(),
						IssueNumber = issue.IssueNumber,
						PublishDate = issue.PublishDate,
						VolumeId = issue.VolumeId,
						Articles = issue.Articles?.Select(articleDto => new Article
						{   
							Id = Guid.NewGuid(),
							Title = articleDto.Title,
							Description = articleDto.Description,
							Keywords = articleDto.Keywords,
							PdfUrl = articleDto.PdfUrl,
							SupportingInstitution = articleDto.SupportingInstitution,
							ProjectNumber = articleDto.ProjectNumber,
							Reference = articleDto.Reference,
							ArticleLink = articleDto.ArticleLink,
							IssueId = articleDto.IssueId,
							CreatedDate = DateTime.UtcNow,
							IsApproved = articleDto.IsApproved,

							// 🔧 Doğru şekilde ArticleAuthor ilişkisi kuruluyor
							ArticleAuthors = articleDto.AuthorIds?.Select(authorId => new ArticleAuthor
							{
							
								AuthorId = authorId
							}).ToList()

						}).ToList()

					}).ToList()
				}).ToList()
			};

			await _writeRepository.AddAsync(magazine);
			await _writeRepository.SaveAsync();
			return true;
		}



		public async Task UpdateAsync(Magazine magazine)
		{
			await _writeRepository.UpdateAsync(magazine);
		}

		public async Task DeleteAsync(Guid id)
		{
			var magazine = await _readRepository.GetSingleAsync(a => a.Id == id);
			if (magazine != null)
			{
				await _writeRepository.RemoveAsync(magazine);
			}
		}
	}
}
