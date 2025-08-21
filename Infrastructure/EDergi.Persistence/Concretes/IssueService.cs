using EDergi.Application.Abstractions;
using EDergi.Application.Repostories;
using EDergi.Domain.Entitites;
using EDergi.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EDergi.Persistence.Concretes
{
	public class IssueService : IIssueService
	{
		private readonly IReadRepository<Issue> _readRepository;
		private readonly IWriteRepository<Issue> _writeRepository;

		public IssueService(IReadRepository<Issue> readRepository, IWriteRepository<Issue> writeRepository)
		{
			_readRepository = readRepository;
			_writeRepository = writeRepository;
		}

		public Task<List<Issue>> GetAllAsync()
		{
			var list = _readRepository.GetAll().ToList();
			return Task.FromResult(list);
		}

		public async Task<Issue> GetByIdAsync(Guid id)
		{
			return await _readRepository.GetSingleAsync(i => i.Id == id);
		}

		public async Task CreateAsync(IssueCreateDto dto)
		{
			var issue = new Issue
			{
				Id = Guid.NewGuid(),
				IssueNumber = dto.IssueNumber,
				PublishDate = dto.PublishDate,
				CreatedDate = DateTime.UtcNow,
				VolumeId = dto.VolumeId,
				Articles = dto.Articles?.Select(articleDto => new Article
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
					IssueId = Guid.NewGuid(), // Geçici IssueId, sonra güncellenecek
					IsApproved = articleDto.IsApproved,

					// ArticleAuthor ilişkisi kuruluyor
					ArticleAuthors = articleDto.AuthorIds?.Select(authorId => new ArticleAuthor
					{
						AuthorId = authorId
					}).ToList()
				}).ToList()
			};

			// Issue'yu veritabanına ekle
			await _writeRepository.AddAsync(issue);
			await _writeRepository.SaveAsync(); // Değişiklikleri kaydet

			// Article'ların IssueId'sini güncelle
			foreach (var article in issue.Articles)
			{
				article.IssueId = issue.Id;
			}

			// Issue'yu güncelle
			await _writeRepository.UpdateAsync(issue);
			await _writeRepository.SaveAsync();
		}

		public async Task UpdateAsync(Issue issue)
		{
			await _writeRepository.UpdateAsync(issue);
			await _writeRepository.SaveAsync();
		}

		public async Task<List<Issue>> GetByVolumeIdAsync(Guid volumeId)
		{
			return await _readRepository.GetWhereAsync(i => i.VolumeId == volumeId);
		}

		public async Task DeleteAsync(Guid id)
		{
			var issue = await _readRepository.GetSingleAsync(i => i.Id == id);
			if (issue != null)
			{
				await _writeRepository.RemoveAsync(issue);
				await _writeRepository.SaveAsync();
			}
		}
		public async Task<List<Issue>> GetIssuesByVolumeIdAsync(Guid volumeId)
		{
			return await _readRepository.GetWhereAsync(i => i.VolumeId == volumeId);
		}
		


	}
}
