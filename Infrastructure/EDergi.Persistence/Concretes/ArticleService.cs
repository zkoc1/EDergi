using EDergi.Application.Abstractions;
using EDergi.Application.Abstractions.Services;
using EDergi.Application.DTOs;
using EDergi.Application.Interfaces.Services;
using EDergi.Application.Repostories;
using EDergi.Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EDergi.Persistence.Concretes
{
	public class ArticleService : IArticleService
	{
		private readonly IReadRepository<Article> _readRepository;
		private readonly IWriteRepository<Article> _writeRepository;
		private readonly IVolumeService _volumeService;
		private readonly IIssueService _issueService;
		public ArticleService(IReadRepository<Article> readRepository, IWriteRepository<Article> writeRepository, IVolumeService volumeService, IIssueService issueService)
		{
			_readRepository = readRepository;
			_writeRepository = writeRepository;
			_volumeService = volumeService;
			_issueService = issueService;
		}

		public async Task<List<Article>> GetAllAsync()
		{
			return await _readRepository.GetAll().ToListAsync();
		}


		public async Task<Article> GetByIdAsync(Guid id)
		{
			return await _readRepository.GetSingleAsync(a => a.Id == id);
		}
		public async Task CreateAsync(ArticleCreateDto dto)
		{
			var article = new Article
			{
				Id = Guid.NewGuid(),
				Title = dto.Title,
				Description = dto.Description,
				Keywords = dto.Keywords,
				PdfUrl = dto.PdfUrl,
				SupportingInstitution = dto.SupportingInstitution,
				ProjectNumber = dto.ProjectNumber,
				Reference = dto.Reference,
				ArticleLink = dto.ArticleLink,
				IssueId = dto.IssueId,
				IsApproved = dto.IsApproved,
				CreatedDate = DateTime.UtcNow,

				// N-N ilişki: ArticleAuthor tablosu
				ArticleAuthors = dto.AuthorIds?.Select(authorId => new ArticleAuthor
				{
					AuthorId = authorId
				}).ToList()
			};

			await _writeRepository.AddAsync(article);
		}
		public async Task<List<ArticleListDto>> GetPendingAsync()
		{
			return await _readRepository.GetWhere(x => !x.IsApproved)
				.Include(x => x.ArticleAuthors)
				.Select(a => new ArticleListDto
				{
					Id = a.Id,
					Title = a.Title,
					Description = a.Description,
					Keywords = a.Keywords,
					PdfUrl = a.PdfUrl,
					SupportingInstitution = a.SupportingInstitution,
					ProjectNumber = a.ProjectNumber,
					Reference = a.Reference,
					ArticleLink = a.ArticleLink,
					IssueId = a.IssueId,
					IsApproved = a.IsApproved,
					AuthorIds = a.ArticleAuthors.Select(aa => aa.AuthorId).ToList()
				}).ToListAsync();
		}



		public async Task RejectAsync(Guid id)
		{
			var article = await _readRepository.GetByIdAsync(id);
			if (article != null)
			{
				await _writeRepository.RemoveAsync(article);
			}
		}
		//public async Task<List<ArticleListDto>> GetByIssueIdAsync(Guid issueId)
		//{
		//	return await _readRepository.GetWhere(x => x.IssueId == issueId)
		//		.Include(x => x.ArticleAuthors) // ArticleAuthors tablosunu dahil et
		//		.Select(a => new ArticleListDto
		//		{
		//			Id = a.Id,
		//			Title = a.Title,
		//			Description = a.Description,
		//			Keywords = a.Keywords,
		//			PdfUrl = a.PdfUrl,
		//			SupportingInstitution = a.SupportingInstitution,
		//			ProjectNumber = a.ProjectNumber,
		//			Reference = a.Reference,
		//			ArticleLink = a.ArticleLink,
		//			IssueId = a.IssueId,
		//			IsApproved = a.IsApproved,
		//			AuthorIds = a.ArticleAuthors.Select(aa => aa.AuthorId).ToList()
		//		}).ToListAsync();
		//}
		public async Task<List<Article>> GetByIssueIdAsync(Guid issueId)
		{
			return await _readRepository.GetWhereAsync(i => i.IssueId == issueId);
		}

		public async Task UpdateAsync(Article article)
		{
			await _writeRepository.UpdateAsync(article);
		}

		public async Task DeleteAsync(Guid id)
		{
			var article = await _readRepository.GetSingleAsync(a => a.Id == id);
			if (article != null)
			{
				await _writeRepository.RemoveAsync(article);
			}
		}
		public async Task CreateAsync(ArticleCreateDto dto, Guid magazineId)
		{
			// Önce IssueId'yi bul
			

			var article = new Article
			{
				Id = Guid.NewGuid(),
				Title = dto.Title,
				Description = dto.Description,
				Keywords = dto.Keywords,
				PdfUrl = dto.PdfUrl,
				SupportingInstitution = dto.SupportingInstitution,
				ProjectNumber = dto.ProjectNumber,
				Reference = dto.Reference,
				ArticleLink = dto.ArticleLink,
				IssueId = magazineId, // Artık doğru IssueId kullanılıyor
				IsApproved = false,
				CreatedDate = DateTime.UtcNow,
				ArticleAuthors = dto.AuthorIds?.Select(authorId => new ArticleAuthor
				{
					AuthorId = authorId
				}).ToList()
			};

			await _writeRepository.AddAsync(article);
		}

		public async Task<Guid> GetIssueIdByMagazineIdAsync(Guid magazineId)
		{
			if (magazineId == Guid.Empty)
				throw new ArgumentException("Geçersiz dergi ID'si.");

			// 1. Magazine'e ait Volume'ları çek
			var volumes = await _volumeService.GetVolumesByMagazineIdAsync(magazineId);
			if (volumes == null || !volumes.Any())
				throw new Exception("Bu dergiye ait cilt bulunamadı.");

			// 2. En son Volume'u seç (yıla göre sırala)
			var latestVolume = volumes.OrderByDescending(v => v.Year).FirstOrDefault();

			// 3. Volume'a ait Issue'ları çek
			var issues = await _issueService.GetIssuesByVolumeIdAsync(latestVolume.Id);
			if (issues == null || !issues.Any())
				throw new Exception("Bu cilde ait sayı bulunamadı.");

			// 4. En son Issue'u seç (tarihe göre sırala)
			var latestIssue = issues.OrderByDescending(i => i.PublishDate).FirstOrDefault();

			return latestIssue.Id;
		}



	}
}