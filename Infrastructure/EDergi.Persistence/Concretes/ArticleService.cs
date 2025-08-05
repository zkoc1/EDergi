using EDergi.Application.Abstractions.Services;
using EDergi.Application.Repostories;
using EDergi.Domain.Entitites;
using EDergi.Application.DTOs;
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

		public ArticleService(IReadRepository<Article> readRepository, IWriteRepository<Article> writeRepository)
		{
			_readRepository = readRepository;
			_writeRepository = writeRepository;
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


	}
}