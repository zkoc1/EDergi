using EDergi.Application.Abstractions;
using EDergi.Application.Interfaces.Services;
using EDergi.Application.Repostories;
using EDergi.Domain.Entitites;
using EDergi.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EDergi.Persistence.Services
{
	public class VolumeService : IVolumeService
	{
		private readonly IReadRepository<Volume> _readRepository;
		private readonly IWriteRepository<Volume> _writeRepository;

		public VolumeService(IReadRepository<Volume> readRepository, IWriteRepository<Volume> writeRepository)
		{
			_readRepository = readRepository;
			_writeRepository = writeRepository;
		}

		public Task<List<Volume>> GetAllAsync()
		{
			var list = _readRepository.GetAll().ToList();
			return Task.FromResult(list);
		}

		public async Task<Volume> GetByIdAsync(Guid id)
		{
			return await _readRepository.GetSingleAsync(v => v.Id == id);
		}

		public Task<List<Volume>> GetByMagazineIdAsync(Guid magazineId)
		{
			var list = _readRepository.GetWhere(v => v.JMagazineId == magazineId).ToList();
			return Task.FromResult(list);
		}

		public async Task<Volume> CreateAsync(VolumeCreateDto dto)
		{
			var volume = new Volume
			{
				Id = Guid.NewGuid(),
				Title = dto.Title,
				Year = dto.Year,
				CreatedDate = DateTime.UtcNow,
				Issues = dto.Issues?.Select(issueDto => new Issue
				{
					Id = Guid.NewGuid(),
					IssueNumber = issueDto.IssueNumber,
					PublishDate = issueDto.PublishDate,
					CreatedDate = DateTime.UtcNow,
					Articles = issueDto.Articles?.Select(articleDto => new Article
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

						ArticleAuthors = articleDto.AuthorIds?.Select(authorId => new ArticleAuthor
						{
							AuthorId = authorId
						}).ToList()

					}).ToList()
				}).ToList()
			};

			await _writeRepository.AddAsync(volume);
			return volume;
		}


		public async Task<Volume> UpdateAsync(Volume volume)
		{
			await _writeRepository.UpdateAsync(volume);
			return volume;
		}

		public async Task<bool> DeleteAsync(Guid id)
		{
			var entity = await _readRepository.GetSingleAsync(v => v.Id == id);
			if (entity == null) return false;

			await _writeRepository.RemoveAsync(entity);
			return true;
		}
	}
}
