using DergiAPI.Application.Abstractions;
using DergiAPI.Application.Interfaces.Services;
using DergiAPI.Application.Repostories;
using DergiAPI.Domain.Entitites;
using EDergiAPI.Application.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace DergiAPI.Persistence.Concretes
{
	public class MagazineService : IMagazineService
	{
		private readonly IReadRepository<Magazine> _readRepository;
		private readonly IWriteRepository<Magazine> _writeRepository;

		public MagazineService(IReadRepository<Magazine> readRepository, IWriteRepository<Magazine> writeRepository)
		{
			_readRepository = readRepository;
			_writeRepository = writeRepository;
		}

		public async Task<List<Magazine>> GetAllAsync()
		{
			return await _readRepository.GetAll().ToListAsync();
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
				PublisherId = dto.PublisherId,

				ViewStats = new ViewStats
				{
					Id = Guid.NewGuid(),
					ViewCount = dto.ViewStats?.ViewCount ?? 0,
					FavoriteCount = dto.ViewStats?.FavoriteCount ?? 0,
					DownloadCount = dto.ViewStats?.DownloadCount ?? 0
				},

				Documents = dto.Documents?.Select(doc => new MDocument
				{
					Id = Guid.NewGuid(),
					FileName = doc.FileName,
					FilePath = doc.FilePath,
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
					JMagazineId = magazineId,
					Issues = volume.Issues?.Select(issue => new Issue
					{
						Id = Guid.NewGuid(),
						IssueNumber = issue.IssueNumber,
						PublishDate = issue.PublishDate,
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

		public async Task<bool> DeleteAsync(Guid id)
		{
			var magazine = await _readRepository.GetSingleAsync(m => m.Id == id);
			if (magazine != null)
			{
				await _writeRepository.RemoveAsync(magazine);
				return true;
			}
			return false;
		}
	}
}
